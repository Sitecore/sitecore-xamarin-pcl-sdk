namespace Sitecore.MobileSDK.UserRequest.ChangeRequest
{
  using System;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.UserRequest;
  using Sitecore.MobileSDK.Validators;

  public abstract class AbstractChangeItemRequestBuilder<T> : AbstractGetVersionedItemRequestBuilder<T>, IUpdateItemRequestParametersBuilder<T>
    where T : class
  {

    public IUpdateItemRequestParametersBuilder<T> AddFieldsRawValuesByName(IDictionary<string, string> fieldsRawValuesByName)
    {
      BaseValidator.CheckNullAndThrow(fieldsRawValuesByName, this.GetType().Name + ".FieldsRawValuesByName");

      if (fieldsRawValuesByName.Count == 0)
      {
        return this;
      }

      foreach (var fieldElem in fieldsRawValuesByName)
      {
        this.AddFieldsRawValuesByName(fieldElem.Key, fieldElem.Value);
      }

      return this;
    }

    public IUpdateItemRequestParametersBuilder<T> AddFieldsRawValuesByName(string fieldKey, string fieldValue)
    {
      BaseValidator.CheckForNullAndEmptyOrThrow(fieldKey, this.GetType().Name + ".fieldKey");
      BaseValidator.CheckForNullAndEmptyOrThrow(fieldValue, this.GetType().Name + ".fieldValue");

      if (null == this.FieldsRawValuesByName)
      {
        Dictionary<string, string> newFields = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        this.FieldsRawValuesByName = newFields;
      }

      string lowerCaseField = fieldKey.ToLowerInvariant();

      bool keyIsDuplicated = DuplicateEntryValidator.IsDuplicatedFieldsInTheDictionary(this.FieldsRawValuesByName, lowerCaseField);
      if (keyIsDuplicated)
      {
        throw new ArgumentException(this.GetType().Name + ".FieldsRawValuesByName : duplicate fields are not allowed");
      }

      this.FieldsRawValuesByName.Add(lowerCaseField, fieldValue);

      return this;
    }

    new public IUpdateItemRequestParametersBuilder<T> Version(int? version)
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

