using System;
using System.IO;
using System.Net;
using System.Web;
using System.Text;

namespace LargeUploadTestiOS
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

    private void ConvertToChunks()  
    {
      string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());

      int chunkSize = this.chunkedRequest.ChunkSize;
      double fullStreamLenght = this.resourceStream.Length;

      string fileName = this.chunkedRequest.FileName;  
      int totalChunks = (int)Math.Ceiling(fullStreamLenght / chunkSize);  

      for (int i = 0; i < totalChunks; i++)  
      {  
        int startIndex = i * chunkSize;  
        int endIndex = (int)(startIndex + chunkSize > fullStreamLenght ?   fullStreamLenght : startIndex + chunkSize);  
        int length = endIndex - startIndex;  

        byte[] bytes = new byte[length];  
        this.resourceStream.Read(bytes, 0, bytes.Length);  
        ChunkRequest(bytes, formDataBoundary);  
      }  
    }  

    private void ChunkRequest(byte[] buffer, string boundary)  
    {  
      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.chunkedRequest.RequestUrl);  
      request.Method = "POST";  

      string contentType = "multipart/form-data; boundary=" + boundary;

      request.ContentType = contentType;  

      // Chunk(buffer) is converted to Base64 string that will be convert to Bytes on  the handler.   
      string requestParameters = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"datafile\"\r\n\r\n{1}",
        boundary,
        HttpUtility.UrlEncode( Convert.ToBase64String(buffer));

      // finally whole request will be converted to bytes that will be transferred to HttpHandler  
      byte[] byteData = Encoding.UTF8.GetBytes(requestParameters);  

      request.ContentLength = byteData.Length;  

      Stream writer = request.GetRequestStream();  
      writer.Write(byteData, 0, byteData.Length);  
      writer.Close();  


      // here we will receive the response from HttpHandler  
      StreamReader stIn = new StreamReader(request.GetResponse().GetResponseStream());  
      string strResponse = stIn.ReadToEnd();  
      stIn.Close();  
    } 
  }
}

