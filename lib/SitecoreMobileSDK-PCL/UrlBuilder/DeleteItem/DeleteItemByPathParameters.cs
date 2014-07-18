namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public class DeleteItemByPathParameters : IDeleteItemsByPathRequest
  {
    public DeleteItemByPathParameters(ISessionConfig sessionConfig, IScopeParameters scopeParameters, string database, string itemPath)
    {
      this.SessionConfig = sessionConfig;
      this.ScopeParameters = scopeParameters;
      this.Database = database;
      this.ItemPath = itemPath;
    }

    public ISessionConfig SessionConfig { get; private set; }
    public IScopeParameters ScopeParameters { get; private set; }
    public string Database { get; private set; }
    public string ItemPath { get; private set; }

  }
}
