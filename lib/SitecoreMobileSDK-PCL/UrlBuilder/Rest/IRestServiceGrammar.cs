namespace Sitecore.MobileSDK.UrlBuilder.Rest
{
  public interface IRestServiceGrammar
  {
    string PathComponentSeparator { get; }
    string KeyValuePairSeparator { get; }
    string FieldSeparator { get; }
    string HostAndArgsSeparator { get; }
    string ItemFieldSeparator { get; }
  }
}

