namespace Sitecore.MobileSDK.UrlBuilder.MediaItem
{
  using System.Collections.Generic;
  using System.IO;

  public class CreateMediaParameters
  {
    public CreateMediaParameters(
      Stream imageDataStream, 
      string itemName, 
      string itemTemlate,
      string mediaPath
    )
    {
      this.ImageDataStream = imageDataStream;
      this.ImageName = itemName; 
      this.ItemTemlate = itemTemlate;
      this.MediaPath = mediaPath;
    }

    public CreateMediaParameters ShallowCopy()
    {
      return new CreateMediaParameters(this.ImageDataStream, this.ImageName, this.ItemTemlate, this.MediaPath);
    }

    //TODO: @igk imageData probably should be moved to request?
    public Stream ImageDataStream { get; private set; }
    public string ImageName { get; private set; }
    public string ItemTemlate { get; private set; }
    public string MediaPath { get; private set; }
  }
}

