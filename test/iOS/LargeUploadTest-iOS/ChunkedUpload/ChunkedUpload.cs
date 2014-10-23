using System;
using System.IO;
using System.Net;
using System.Web;
using System.Text;
using System.Diagnostics;

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

    public void Upload()  
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
        string chunkResult = ChunkRequest(bytes, formDataBoundary);  
        Debug.WriteLine("---===" + i.ToString() + " chunkResult: " + chunkResult);
      }  
    }  

    private string ChunkRequest(byte[] buffer, string boundary)  
    {  
      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.chunkedRequest.RequestUrl);  
      request.Method = "POST";  

      //TODO: @igk AUTH
      request.Headers.Add("X-Scitemwebapi-Username", "sitecore\\admin");
      request.Headers.Add("X-Scitemwebapi-Password", "b");

   
      string contentType = "multipart/form-data; boundary=" + boundary;

      request.ContentType = contentType;  

//      string requestParameters = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"datafile\"; filename=\"file.mov\"\r\n\r\n{1}",
//        boundary,
//        HttpUtility.UrlEncode( Convert.ToBase64String(buffer))
//      );
      string requestParameters = @"fileName=" + this.chunkedRequest.FileName +  
        "&data=" + HttpUtility.UrlEncode( Convert.ToBase64String(buffer) ); 

      byte[] byteData = Encoding.UTF8.GetBytes(requestParameters);  

      request.ContentLength = byteData.Length;  

      Stream writer = request.GetRequestStream();  
      writer.Write(byteData, 0, byteData.Length);  
      writer.Close();  


      // here we will receive the response from HttpHandler  
      StreamReader stIn = new StreamReader(request.GetResponse().GetResponseStream());  
      string strResponse = stIn.ReadToEnd();  
      stIn.Close();  
      return strResponse;
    } 
  }
}

