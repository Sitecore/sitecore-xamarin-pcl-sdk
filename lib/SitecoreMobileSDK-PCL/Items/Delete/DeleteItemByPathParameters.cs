namespace Sitecore.MobileSDK.Items.Delete
{
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public class DeleteItemByPathParameters : IDeleteItemsByPathRequest
  {
    public DeleteItemByPathParameters(ISessionConfig sessionConfig, IScopeParameters scopeParameters,
      string database, string itemPath)
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

    public IDeleteItemsByPathRequest DeepCopyDeleteItemRequest()
    {
      ISessionConfig sessionConfig = null;
      IScopeParameters scopeParameters = null;
      string database = null;
      string itemPath = null;

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

      if (null != this.ItemPath)
      {
        itemPath = this.ItemPath;
      }

      return new DeleteItemByPathParameters(sessionConfig, scopeParameters, database, itemPath);
    }
  }
}
