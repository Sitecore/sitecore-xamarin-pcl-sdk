
namespace Sitecore.MobileSDK.API.Request.Parameters
{
  using System;
  using System.IO;

  public interface ICreateMediaParameters
  {
    Stream ImageDataStream { get; }
    string FileName { get; }
    string ItemName { get; }
    string ItemTemlate { get; }
    string MediaPath { get; }
    string ContentType { get; }
  }
}

