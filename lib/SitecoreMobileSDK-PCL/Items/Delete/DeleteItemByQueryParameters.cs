namespace Sitecore.MobileSDK.Items.Delete
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public class DeleteItemByQueryParameters : IDeleteItemsByQueryRequest
  {
    public DeleteItemByQueryParameters(ISessionConfig sessionConfig,
      string database, string sitecoreQuery)
    {
      this.SessionConfig = sessionConfig;
      this.Database = database;
      this.SitecoreQuery = sitecoreQuery;
    }

    public ISessionConfig SessionConfig { get; private set; }
    public string Database { get; private set; }
    public string SitecoreQuery { get; private set; }

    public IDeleteItemsByQueryRequest DeepCopyDeleteItemRequest()
    {
      ISessionConfig sessionConfig = null;
      string database = null;
      string query = null;

      if (null != this.SessionConfig)
      {
        sessionConfig = this.SessionConfig.SessionConfigShallowCopy();
      }

      if (null != this.Database)
      {
        database = this.Database;
      }

      if (null != this.SitecoreQuery)
      {
        query = this.SitecoreQuery;
      }

      return new DeleteItemByQueryParameters(sessionConfig, database, query);
    }
  }
}
