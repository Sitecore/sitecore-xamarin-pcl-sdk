namespace Sitecore.MobileSDK.Items
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public class UpdateItemByIdParameters : IUpdateItemByIdRequest
  {
    public UpdateItemByIdParameters(ISessionConfig sessionSettings, IItemSource itemSource, IQueryParameters queryParameters, IDictionary<string, string> fieldsRawValuesByName, string itemId)
    {
      this.SessionSettings = sessionSettings;
      this.ItemSource = itemSource;
      this.ItemId = itemId;
      this.QueryParameters = queryParameters;
      this.FieldsRawValuesByName = fieldsRawValuesByName;
    }

    public virtual IUpdateItemByIdRequest DeepCopyUpdateItemByIdRequest()
    {
      ISessionConfig connection = null;
      IItemSource itemSrc = null;
      IQueryParameters payload = null;

      if (null != this.SessionSettings)
      {
        connection = this.SessionSettings.SessionConfigShallowCopy();
      }

      if (null != this.ItemSource)
      {
        itemSrc = this.ItemSource.ShallowCopy();
      }

      if (null != this.QueryParameters)
      {
        payload = this.QueryParameters.DeepCopy();
      }

      return new UpdateItemByIdParameters(connection, itemSrc, payload, this.FieldsRawValuesByName, this.ItemId);
    }

    public virtual IBaseItemRequest DeepCopyBaseGetItemRequest()
    {
      return this.DeepCopyUpdateItemByIdRequest();
    }


    public string ItemId { get; private set; }

    public IItemSource ItemSource { get; private set; }

    public ISessionConfig SessionSettings { get; private set; }

    public IQueryParameters QueryParameters { get; private set; }

    public IDictionary<string, string> FieldsRawValuesByName { get; private set; }

  }
}

