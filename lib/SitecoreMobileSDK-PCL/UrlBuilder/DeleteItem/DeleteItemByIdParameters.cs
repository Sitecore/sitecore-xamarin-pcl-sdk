namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public class DeleteItemByIdParameters : IDeleteItemsByIdRequest
  {
    public ISessionConfig SessionConfig { get; private set; }
    public IScopeParameters ScopeParameters { get; private set; }
    public string Database { get; private set; }
    public string ItemId { get; private set; }

    public DeleteItemByIdParameters(ISessionConfig sessionConfig, IScopeParameters scopeParameters, string database, string itemId)
    {
      this.SessionConfig = sessionConfig;
      this.ScopeParameters = scopeParameters;
      this.Database = database;
      this.ItemId = itemId;
    }
  }
}
