
namespace Sitecore.MobileSDK
{
  using System;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;

  public abstract class AbstractCreateItemRequestBuilder<T> : Sitecore.MobileSDK.UserRequest.AbstractGetItemRequestBuilder<T>, ICreateItemRequestParametersBuilder<T> 
    where T : class
  {

    public ICreateItemRequestParametersBuilder<T> ItemName (string itemName)
    {
      this.itemParametersAccumulator = 
        new CreateItemParameters(itemName, this.itemParametersAccumulator.ItemTemplate, this.itemParametersAccumulator.FieldsRawValuesByName);

      return this;
    }

    public ICreateItemRequestParametersBuilder<T> ItemTemplate (string itemTemplate)
    {
      this.itemParametersAccumulator = 
        new CreateItemParameters(this.itemParametersAccumulator.ItemName, itemTemplate, this.itemParametersAccumulator.FieldsRawValuesByName);

      return this;
    }

    public ICreateItemRequestParametersBuilder<T> AddFieldsRawValuesByName (Dictionary<string, string> fieldsRawValuesByName)
    {
      this.itemParametersAccumulator = 
        new CreateItemParameters(this.itemParametersAccumulator.ItemName, this.itemParametersAccumulator.ItemTemplate, fieldsRawValuesByName);

      return this;
    }

    public ICreateItemRequestParametersBuilder<T> AddFieldsRawValuesByName (string fieldKey, string fieldValue)
    {
      //TODO: @igk make fieldsRawValuesByName appending
      return this;
    }

    protected CreateItemParameters itemParametersAccumulator = new CreateItemParameters(null, null, null);
  }
}

