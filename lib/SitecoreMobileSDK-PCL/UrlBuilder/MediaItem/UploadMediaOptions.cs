namespace Sitecore.MobileSDK.UrlBuilder.MediaItem
{
  using System.Collections.Generic;
  using System.IO;

  using Sitecore.MobileSDK.API.Request.Parameters;

  public class UploadMediaOptions : IUploadMediaOptions
  {
    public UploadMediaOptions(
      Stream imageDataStream, 
      string fileName, 
      string itemName,
      string itemTemlatePath,
      string mediaPath,
      string contentType
    )
    {
      this.ImageDataStream = imageDataStream;
      this.FileName = fileName; 
      this.ItemName = itemName; 
      this.ItemTemplatePath = itemTemlatePath;
      this.mediaPath = mediaPath;
      this.ContentType = contentType;
    }

    public UploadMediaOptions ShallowCopy()
    {
      return new UploadMediaOptions(this.ImageDataStream, this.FileName, this.ItemName, this.ItemTemplatePath, this.MediaPath, this.ContentType);
    }

    public Stream ImageDataStream { get; private set; }
    public string FileName { get; private set; }
    public string ItemName { get; private set; }
    public string ItemTemplatePath { get; private set; }
    public string ContentType { get; private set; }
    public string MediaPath 
    { 
      get 
      { 
        if (null == this.mediaPath)
        {
          return @"";
        }
        else
        {
          return this.mediaPath;
        }
      }
    }

    private string mediaPath;
  }
}

