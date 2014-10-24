using System;
using System.IO;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sitecore.ChunkedUpload
{
  public class ChunkedUpload
  {
    private Stream resourceStream;
    private IChunkedRequest chunkedRequest;
    byte[] currentPackageBody;
    HttpClient httpClient;

    public ChunkedUpload(Stream dataStream, IChunkedRequest chunkedRequest, HttpClient httpClient)
    {
      this.resourceStream = dataStream;
      this.chunkedRequest = chunkedRequest.ShallowCopy();
      this.httpClient = httpClient;
    }

    public async Task<string> Upload()  
    {
      string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());

      byte chunkSize = 255;
      double fullStreamLenght = this.resourceStream.Length;

      string fileName = this.chunkedRequest.FileName;  
      int totalChunks = (int)Math.Ceiling(fullStreamLenght / chunkSize); 

      string result;

      for (int i = 0; i < totalChunks; i++)  
      {  
        int startIndex = i * chunkSize;  
        int endIndex = (int)(startIndex + chunkSize > fullStreamLenght ?   fullStreamLenght : startIndex + chunkSize);  
        int length = endIndex - startIndex;  

        byte[] bytes = new byte[length];  
        this.resourceStream.Read(bytes, 0, bytes.Length);  
        result = await AppendPacketBodyWithChunk(bytes); 

        if (result != null)
        {
          Debug.WriteLine ("---===" + i.ToString () + " chunkResult: " + result);
        }
      }  

      return await this.SendLastChunk();

    }  

    private async Task<string> SendLastChunk()
    {
      byte[] endLine = {0x0d,0x0a};
      byte[] nullLine = {0x00};

      byte[] resultBody = new byte[5];
      int index = 0;

      nullLine.CopyTo(resultBody, index);
      index += nullLine.Length;

      endLine.CopyTo(resultBody, index);
      index += endLine.Length;

      endLine.CopyTo(resultBody, index);
      index += endLine.Length;

      this.AppendPacketBodyWithBytes(resultBody);

      return await this.SendRequestWithCurrentBody ();
    }

    private async Task<string> AppendPacketBodyWithChunk(byte[] buffer)  
    {  
      // one chunk
      byte[] endLine = {0x0d,0x0a};
      byte[] chunkSizeHex = BitConverter.GetBytes(buffer.Length);
      int resultSize = chunkSizeHex.Length + endLine.Length + buffer.Length + endLine.Length;
      byte[] resultBody = new byte[resultSize];

      int index = 0;
      chunkSizeHex.CopyTo(resultBody, index);
      index += chunkSizeHex.Length;

      endLine.CopyTo(resultBody, index);
      index += endLine.Length;

      buffer.CopyTo(resultBody, index);
      index += buffer.Length;

      endLine.CopyTo(resultBody, index);
      // end one chunk

      this.AppendPacketBodyWithBytes(resultBody);

      string result = null;

      if (this.currentPackageBody.Length >= this.chunkedRequest.ChunkSize)
      {
        result = await this.SendRequestWithCurrentBody ();
        this.currentPackageBody = null;
      }

      return result;
    } 
         
    private void AppendPacketBodyWithBytes(byte[] buffer)
    {
      if (null == this.currentPackageBody)
      {
        this.currentPackageBody = buffer;
        return;
      }

      //request body
      int bodyResultSize = buffer.Length + this.currentPackageBody.Length;
      byte[] bodyResultArray = new byte[bodyResultSize];
      this.currentPackageBody.CopyTo(bodyResultArray, 0);
      buffer.CopyTo(bodyResultArray, this.currentPackageBody.Length);
      this.currentPackageBody = bodyResultArray;
    }

    private async Task<string> SendRequestWithCurrentBody()
    {  
      Uri url = new Uri (this.chunkedRequest.RequestUrl);

      HttpRequestMessage result = new HttpRequestMessage (HttpMethod.Post, url);

      MultipartFormDataContent multiPartContent = new MultipartFormDataContent ();


      result.Headers.Add ("X-Scitemwebapi-Username", "sitecore\\admin");
      result.Headers.Add ("X-Scitemwebapi-Password", "b");


      ContentDispositionHeaderValue cdHeaderValue = new ContentDispositionHeaderValue ("data");
      cdHeaderValue.FileName = "\"" + this.chunkedRequest.FileName + "\"";
      cdHeaderValue.Name = "\"datafile\"";


//      result.Headers.Add ("Transfer-Encoding", "chunked");
//      result.Headers.TransferEncodingChunked = true;

//      TransferCodingHeaderValue tcHeaderValue = new TransferCodingHeaderValue ("chunked");


      Stream stream = new MemoryStream(this.currentPackageBody);
      StreamContent strContent = new StreamContent(stream);

      strContent.Headers.ContentDisposition = cdHeaderValue;
     

      if (null != this.chunkedRequest.ContentType)
      {
        strContent.Headers.ContentType = MediaTypeHeaderValue.Parse(this.chunkedRequest.ContentType);
      }
      multiPartContent.Add(strContent);
      result.Content = multiPartContent;

      //SENDING

      HttpResponseMessage httpResponse = await httpClient.SendAsync(result);
      return await httpResponse.Content.ReadAsStringAsync();

    }


  }
}

