namespace Sitecore.MobileSDK.UrlBuilder.MediaItem
{
  using System.Collections.Generic;

  public class CreateMediaParameters
  {
    public CreateMediaParameters(
      byte[] imageData, 
      string imageName, 
      string itemTemlate,
      string folderPathInsideMediaLibrary
    )
    {
      this.ImageData = imageData;
      this.ImageName = imageName; 
      this.ItemTemlate = itemTemlate;
      this.FolderPathInsideMediaLibrary = folderPathInsideMediaLibrary;
    }

    public CreateMediaParameters ShallowCopy()
    {
      return new CreateMediaParameters(this.ImageData, this.ImageName, this.ItemTemlate, this.FolderPathInsideMediaLibrary);
    }

    //TODO: @igk imageData probably should be moved to request?
    public byte[] ImageData { get; private set; }
    public string ImageName { get; private set; }
    public string ItemTemlate { get; private set; }
    public string FolderPathInsideMediaLibrary { get; private set; }
  }
}

