namespace Sitecore.MobileSDK.UrlBuilder.MediaItem
{
  using System.Collections.Generic;
  using System.IO;

  using Sitecore.MobileSDK.API.Request.Parameters;

  public class CreateMediaParameters : ICreateMediaParameters
  {
    public CreateMediaParameters(
      Stream imageDataStream, 
      string fileName, 
      string itemName,
      string itemTemlate,
      string mediaPath,
      string contentType
    )
    {
      this.ImageDataStream = imageDataStream;
      this.FileName = fileName; 
      this.ItemName = itemName; 
      this.ItemTemlate = itemTemlate;
      this.MediaPath = mediaPath;
      this.ContentType = contentType;
    }

    public CreateMediaParameters ShallowCopy()
    {
      return new CreateMediaParameters(this.ImageDataStream, this.FileName, this.ItemName, this.ItemTemlate, this.MediaPath, this.ContentType);
    }

    public Stream ImageDataStream { get; private set; }
    public string FileName { get; private set; }
    public string ItemName { get; private set; }
    public string ItemTemlate { get; private set; }
    public string MediaPath { get; private set; }
    public string ContentType { get; private set; }
  }
}

