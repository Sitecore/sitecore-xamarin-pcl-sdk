
namespace Sitecore.MobileSDK
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.UserRequest;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;
  using Sitecore.MobileSDK.Validators;

  public abstract class AbstractCreateItemRequestBuilder<T> : AbstractUpdateItemRequestBuilder<T>, ICreateItemRequestParametersBuilder<T>
    where T : class
  {
    protected CreateItemParameters itemParametersAccumulator = new CreateItemParameters(null, null, null);

    public ICreateItemRequestParametersBuilder<T> ItemName(string itemName)
    {
      if (string.IsNullOrEmpty(itemName) || string.IsNullOrWhiteSpace(itemName))
      {
        BaseValidator.ThrowNullOrEmptyParameterException(this.GetType().Name + ".ItemName");
      }

      if (!string.IsNullOrEmpty(this.itemParametersAccumulator.ItemName))
      {
        BaseValidator.ThrowParameterSetTwiceException(this.GetType().Name + ".ItemName");
      }

      this.itemParametersAccumulator =
        new CreateItemParameters(itemName, this.itemParametersAccumulator.ItemTemplate, this.itemParametersAccumulator.FieldsRawValuesByName);

      return this;
    }

    public ICreateItemRequestParametersBuilder<T> ItemTemplate(string itemTemplate)
    {
      if (string.IsNullOrEmpty(itemTemplate) || string.IsNullOrWhiteSpace(itemTemplate))
      {
        BaseValidator.ThrowNullOrEmptyParameterException(this.GetType().Name + ".ItemTemplate");
      }

      if (!string.IsNullOrEmpty(this.itemParametersAccumulator.ItemTemplate))
      {
        BaseValidator.ThrowParameterSetTwiceException(this.GetType().Name + ".ItemTemplate");
      }

      this.itemParametersAccumulator =
        new CreateItemParameters(this.itemParametersAccumulator.ItemName, itemTemplate, this.itemParametersAccumulator.FieldsRawValuesByName);

      return this;
    }

    new public ICreateItemRequestParametersBuilder<T> AddFieldsRawValuesByName(IDictionary<string, string> fieldsRawValuesByName)
    {
      base.AddFieldsRawValuesByName(fieldsRawValuesByName);
      this.itemParametersAccumulator =
        new CreateItemParameters(this.itemParametersAccumulator.ItemName, this.itemParametersAccumulator.ItemTemplate, this.FieldsRawValuesByName);
      return (ICreateItemRequestParametersBuilder<T>)this;

    }

    new public ICreateItemRequestParametersBuilder<T> AddFieldsRawValuesByName(string fieldKey, string fieldValue)
    {
      base.AddFieldsRawValuesByName(fieldKey, fieldValue);
      this.itemParametersAccumulator =
        new CreateItemParameters(this.itemParametersAccumulator.ItemName, this.itemParametersAccumulator.ItemTemplate, this.FieldsRawValuesByName);
      return (ICreateItemRequestParametersBuilder<T>)this;
    }

    private bool CheckForDuplicate(string key)
    {
      bool isDuplicate = false;

      if (null != this.itemParametersAccumulator.FieldsRawValuesByName)
      {
        foreach (var fieldElem in this.itemParametersAccumulator.FieldsRawValuesByName)
        {
          isDuplicate = fieldElem.Key.Equals(key);
        }
      }

      return isDuplicate;
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
  }
}

