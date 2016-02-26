namespace Sitecore.MobileSDK.Items.Delete
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public class DeleteItemByPathParameters : IDeleteItemsByPathRequest
  {
    public DeleteItemByPathParameters(ISessionConfig sessionConfig, string database, string itemPath)
    {
      this.SessionConfig = sessionConfig;
      this.Database = database;
      this.ItemPath = itemPath;
    }

    public ISessionConfig SessionConfig { get; private set; }
    public string Database { get; private set; }
    public string ItemPath { get; private set; }

    public IDeleteItemsByPathRequest DeepCopyDeleteItemRequest()
    {
      ISessionConfig sessionConfig = null;
      string database = null;
      string itemPath = null;

      if (null != this.SessionConfig)
      {
        sessionConfig = this.SessionConfig.SessionConfigShallowCopy();
      }

      if (null != this.Database)
      {
        database = this.Database;
      }

      if (null != this.ItemPath)
      {
        itemPath = this.ItemPath;
      }

      return new DeleteItemByPathParameters(sessionConfig, database, itemPath);
    }
  }
}
