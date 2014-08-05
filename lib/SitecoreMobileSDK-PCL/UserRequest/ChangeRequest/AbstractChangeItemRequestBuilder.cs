﻿

namespace Sitecore.MobileSDK
{
  using System;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.UserRequest;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;
  using Sitecore.MobileSDK.UrlBuilder.UpdateItem;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public abstract class AbstractUpdateItemRequestBuilder<T> : AbstractGetVersionedItemRequestBuilder<T>, IUpdateItemRequestParametersBuilder<T> 
    where T : class
  {

    //TODO: @igk copypaste from AbstractCreateItemRequestBuilder merge!!!
    public IUpdateItemRequestParametersBuilder<T> AddFieldsRawValuesByName (IDictionary<string, string> fieldsRawValuesByName)
    {
      if (fieldsRawValuesByName.Count == 0)
      {
        return this;
      }

      Dictionary<string, string> newFields = new Dictionary<string, string>();

      if (null != this.FieldsRawValuesByName)
      {
        foreach (var fieldElem in this.FieldsRawValuesByName)
        {
          newFields.Add (fieldElem.Key, fieldElem.Value);
        }
      }

      foreach (var fieldElem in fieldsRawValuesByName)
      {
        newFields.Add (fieldElem.Key.ToLowerInvariant(), fieldElem.Value);
      }

      this.FieldsRawValuesByName = newFields;

      return this;
    }

    public IUpdateItemRequestParametersBuilder<T> AddFieldsRawValuesByName (string fieldKey, string fieldValue)
    {
      if (string.IsNullOrEmpty(fieldKey) || string.IsNullOrEmpty(fieldValue))
      {
        return this;
      }

      Dictionary<string, string> newFields = new Dictionary<string, string>();

      if (null != this.FieldsRawValuesByName)
      {
        foreach (var fieldElem in this.FieldsRawValuesByName)
        {
          newFields.Add (fieldElem.Key, fieldElem.Value);
        }
      }
      newFields.Add (fieldKey.ToLowerInvariant(), fieldValue);

      this.FieldsRawValuesByName = newFields;

      return this;
    }

    new public IUpdateItemRequestParametersBuilder<T> Version(string version)
    {
      return (IUpdateItemRequestParametersBuilder<T>)base.Version(version);
    }

    new public IUpdateItemRequestParametersBuilder<T> Database(string sitecoreDatabase)
    {
      return (IUpdateItemRequestParametersBuilder<T>)base.Database(sitecoreDatabase);
    }

    new public IUpdateItemRequestParametersBuilder<T> Language(string itemLanguage)
    {
      return (IUpdateItemRequestParametersBuilder<T>)base.Language(itemLanguage);
    }

    new public IUpdateItemRequestParametersBuilder<T> Payload(PayloadType payload)
    {
      return (IUpdateItemRequestParametersBuilder<T>)base.Payload(payload);
    }

    new public IUpdateItemRequestParametersBuilder<T> AddFields(IEnumerable<string> fields)
    {
      return (IUpdateItemRequestParametersBuilder<T>)base.AddFields(fields);
    }

    new public IUpdateItemRequestParametersBuilder<T> AddFields(params string[] fieldParams)
    {
      return (IUpdateItemRequestParametersBuilder<T>)base.AddFields(fieldParams);
    }

    new public IUpdateItemRequestParametersBuilder<T> AddScope(IEnumerable<ScopeType> scope)
    {
      return (IUpdateItemRequestParametersBuilder<T>)base.AddScope(scope);
    }

    new public IUpdateItemRequestParametersBuilder<T> AddScope(params ScopeType[] scope)
    {
      return (IUpdateItemRequestParametersBuilder<T>)base.AddScope(scope);
    }

    protected IDictionary<string, string> FieldsRawValuesByName;
  }
}

