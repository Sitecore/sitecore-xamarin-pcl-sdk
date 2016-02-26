namespace Sitecore.MobileSDK.Items.Delete
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public class DeleteItemByIdParameters : IDeleteItemsByIdRequest
  {
    public DeleteItemByIdParameters(ISessionConfig sessionConfig, string database, string itemId)
    {
      this.SessionConfig = sessionConfig;
      this.Database = database;
      this.ItemId = itemId;
    }

    public ISessionConfig SessionConfig { get; private set; }
    public string Database { get; private set; }
    public string ItemId { get; private set; }

    public IDeleteItemsByIdRequest DeepCopyDeleteItemRequest()
    {
      ISessionConfig sessionConfig = null;
      string database = null;
      string itemId = null;

      if (null != this.SessionConfig)
      {
        sessionConfig = this.SessionConfig.SessionConfigShallowCopy();
      }

      if (null != this.Database)
      {
        database = this.Database;
      }

      if (null != this.ItemId)
      {
        itemId = this.ItemId;
      }

      return new DeleteItemByIdParameters(sessionConfig, database, itemId);
    }
  }
}
