using Sitecore.MobileSDK.UrlBuilder.QueryParameters;


namespace Sitecore.MobileSDK
{
  using System;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.UserRequest;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;

  public abstract class AbstractCreateItemRequestBuilder<T> : AbstractBaseRequestBuilder<T>, ICreateItemRequestParametersBuilder<T> 
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
        newFields.Add (fieldElem.Key.ToLowerInvariant(), fieldElem.Value);
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
      newFields.Add (fieldKey.ToLowerInvariant(), fieldValue);

      this.itemParametersAccumulator = 
        new CreateItemParameters(this.itemParametersAccumulator.ItemName, this.itemParametersAccumulator.ItemTemplate, newFields);

      return this;
    }

    new public ICreateItemRequestParametersBuilder<T> Database(string sitecoreDatabase)
    {
      return (ICreateItemRequestParametersBuilder<T>)base.Database(sitecoreDatabase);
    }

    new public ICreateItemRequestParametersBuilder<T> Language(string itemLanguage)
    {
      return (ICreateItemRequestParametersBuilder<T>)base.Language(itemLanguage);
    }

    new public ICreateItemRequestParametersBuilder<T> Payload(PayloadType payload)
    {
      return (ICreateItemRequestParametersBuilder<T>)base.Payload(payload);
    }

    new public ICreateItemRequestParametersBuilder<T> AddFields(ICollection<string> fields)
    {
      return (ICreateItemRequestParametersBuilder<T>)base.AddFields(fields);
    }

    new public ICreateItemRequestParametersBuilder<T> AddFields(params string[] fieldParams)
    {
      return (ICreateItemRequestParametersBuilder<T>)base.AddFields(fieldParams);
    }

    new public ICreateItemRequestParametersBuilder<T> AddScope(ICollection<ScopeType> scope)
    {
      return (ICreateItemRequestParametersBuilder<T>)base.AddScope(scope);
    }

    new public ICreateItemRequestParametersBuilder<T> AddScope(params ScopeType[] scope)
    {
      return (ICreateItemRequestParametersBuilder<T>)base.AddScope(scope);
    }

    protected CreateItemParameters itemParametersAccumulator = new CreateItemParameters(null, null, null);
  }
}

