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

    public ChunkedUpload(Stream dataStream, IChunkedRequest chunkedRequest)
    {
      this.resourceStream = dataStream;
      this.chunkedRequest = chunkedRequest.ShallowCopy();
    }

    public async void Upload()  
    {
      string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());

      int chunkSize = this.chunkedRequest.ChunkSize;
      double fullStreamLenght = this.resourceStream.Length;

      string fileName = this.chunkedRequest.FileName;  
      int totalChunks = (int)Math.Ceiling(fullStreamLenght / chunkSize); 

      HttpClientHandler handler = new HttpClientHandler();
      HttpClient httpClient = new HttpClient(handler);

      for (int i = 0; i < totalChunks; i++)  
      {  
        int startIndex = i * chunkSize;  
        int endIndex = (int)(startIndex + chunkSize > fullStreamLenght ?   fullStreamLenght : startIndex + chunkSize);  
        int length = endIndex - startIndex;  

        byte[] bytes = new byte[length];  
        this.resourceStream.Read(bytes, 0, bytes.Length);  
        string chunkResult = await ChunkRequest(bytes, httpClient); 

        Debug.WriteLine("---===" + i.ToString() + " chunkResult: " + chunkResult);
      }  


    }  

    private async Task<string> ChunkRequest(byte[] buffer, HttpClient httpClient)  
    {  
      Uri url = new Uri (this.chunkedRequest.RequestUrl);

      HttpRequestMessage result = new HttpRequestMessage(HttpMethod.Post, url);

      MultipartFormDataContent multiPartContent = new MultipartFormDataContent();

      result.Headers.Add ("X-Scitemwebapi-Username", "sitecore\\admin");
      result.Headers.Add ("X-Scitemwebapi-Password", "b");
      result.Headers.Add ("Transfer-Encoding", "chunked");


      ContentDispositionHeaderValue cdHeaderValue = new ContentDispositionHeaderValue("data");
      cdHeaderValue.FileName = "\"" + this.chunkedRequest.FileName + "\"";
      cdHeaderValue.Name = "\"datafile\"";

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

      Stream stream = new MemoryStream(resultBody);
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

