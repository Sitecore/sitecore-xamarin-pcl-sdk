
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
      if (fieldsRawValuesByName.Count == 0)
      {
        return this;
      }

      Dictionary<string, string> newFields = new Dictionary<string, string>();

      if (null != this.itemParametersAccumulator.FieldsRawValuesByName)
      {
        foreach (var fieldElem in this.itemParametersAccumulator.FieldsRawValuesByName)
        {
          newFields.Add (fieldElem.Key, fieldElem.Value);
        }
      }

      foreach (var fieldElem in fieldsRawValuesByName)
      {
        newFields.Add (fieldElem.Key, fieldElem.Value);
      }

      this.itemParametersAccumulator = 
        new CreateItemParameters(this.itemParametersAccumulator.ItemName, this.itemParametersAccumulator.ItemTemplate, newFields);

      return this;
    }

    public ICreateItemRequestParametersBuilder<T> AddFieldsRawValuesByName (string fieldKey, string fieldValue)
    {
      if (string.IsNullOrEmpty(fieldKey) || string.IsNullOrEmpty(fieldValue))
      {
        return this;
      }

      Dictionary<string, string> newFields = new Dictionary<string, string>();

      if (null != this.itemParametersAccumulator.FieldsRawValuesByName)
      {
        foreach (var fieldElem in this.itemParametersAccumulator.FieldsRawValuesByName)
        {
          newFields.Add (fieldElem.Key, fieldElem.Value);
        }
      }
      newFields.Add (fieldKey, fieldValue);

      this.itemParametersAccumulator = 
        new CreateItemParameters(this.itemParametersAccumulator.ItemName, this.itemParametersAccumulator.ItemTemplate, newFields);

      return this;
    }

    protected CreateItemParameters itemParametersAccumulator = new CreateItemParameters(null, null, null);
  }
}

