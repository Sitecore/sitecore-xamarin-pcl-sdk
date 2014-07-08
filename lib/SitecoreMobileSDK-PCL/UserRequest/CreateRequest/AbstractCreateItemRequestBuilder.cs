
namespace Sitecore.MobileSDK
{
  using System;
  using System.Collections.Generic;

  public abstract class AbstractCreateItemRequestBuilder<T> : AbstractGetItemRequestBuilder<T>, ICreateItemRequestParametersBuilder<T> 
    where T : class
  {

    public AbstractCreateItemRequestBuilder<T> ItemName (string itemName)
    {
      this.itemName = itemName;
      return this;
    }

    public ICreateItemRequestParametersBuilder<T> ItemTemplate (string itemTemplate)
    {
      this.itemTemplate = itemTemplate;
      return this;
    }

    public AbstractCreateItemRequestBuilder<T> AddFieldsRawValuesByName (Dictionary<string, string> fieldsRawValuesByName)
    {
      return this;
    }

    public AbstractCreateItemRequestBuilder<T> AddFieldsRawValuesByName (string fieldKey, string fieldValue)
    {
      return this;
    }

    protected string itemName;
    protected string itemTemplate;
    protected Dictionary<string, string> fieldsRawValuesByName;
  }
}

