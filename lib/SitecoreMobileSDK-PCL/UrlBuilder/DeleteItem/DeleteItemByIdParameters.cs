namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public class DeleteItemByIdParameters : IDeleteItemsByIdRequest
  {
    public DeleteItemByIdParameters(ISessionConfig sessionConfig, IScopeParameters scopeParameters, string database, string itemId)
    {
      this.SessionConfig = sessionConfig;
      this.ScopeParameters = scopeParameters;
      this.Database = database;
      this.ItemId = itemId;
    }

    public ISessionConfig SessionConfig { get; private set; }
    public IScopeParameters ScopeParameters { get; private set; }
    public string Database { get; private set; }
    
    public string ItemId { get; private set; }

    public IDeleteItemsByIdRequest DeepCopyDeleteItemRequest()
    {
      ISessionConfig sessionConfig = null;
      IScopeParameters scopeParameters = null;
      string database = null;
      string itemId = null;

      if (null != this.SessionConfig)
      {
        sessionConfig = this.SessionConfig.SessionConfigShallowCopy();
      }

      if (null != this.ScopeParameters)
      {
        scopeParameters = this.ScopeParameters.ShallowCopyScopeParametersInterface();
      }

      if (null != this.Database)
      {
        database = this.Database;
      }

      if (null != this.ItemId)
      {
        itemId = this.ItemId;
      }

      return new DeleteItemByIdParameters(sessionConfig, scopeParameters, database, itemId);
    }
  }
}
