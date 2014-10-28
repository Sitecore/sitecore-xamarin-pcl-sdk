namespace Sitecore.MobileSDK.Items.Delete
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public class DeleteItemByQueryParameters : IDeleteItemsByQueryRequest
  {
    public DeleteItemByQueryParameters(ISessionConfig sessionConfig, IScopeParameters scopeParameters,
      string database, string sitecoreQuery)
    {
      this.SessionConfig = sessionConfig;
      this.ScopeParameters = scopeParameters;
      this.Database = database;
      this.SitecoreQuery = sitecoreQuery;
    }

    public ISessionConfig SessionConfig { get; private set; }
    public IScopeParameters ScopeParameters { get; private set; }
    public string Database { get; private set; }
    public string SitecoreQuery { get; private set; }

    public IDeleteItemsByQueryRequest DeepCopyDeleteItemRequest()
    {
      ISessionConfig sessionConfig = null;
      IScopeParameters scopeParameters = null;
      string database = null;
      string query = null;

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

      if (null != this.SitecoreQuery)
      {
        query = this.SitecoreQuery;
      }

      return new DeleteItemByQueryParameters(sessionConfig, scopeParameters, database, query);
    }
  }
}
