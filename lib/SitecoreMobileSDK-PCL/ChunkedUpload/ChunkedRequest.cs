using System;

namespace Sitecore.ChunkedUpload
{
  public class ChunkedRequest : IChunkedRequest
  {
    public ChunkedRequest()
    {
    }

    public IChunkedRequest ShallowCopy()
    {
      return this;
    }

    public string FileName { get; set; }

    public string ItemName { get; set; }

    public string ContentType { get; set; }

    public int ChunkSize { get; set; }

    public string RequestUrl { get; set; }
  }
}

