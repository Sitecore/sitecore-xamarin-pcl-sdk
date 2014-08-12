namespace Sitecore.MobileSDK.UserRequest.ChangeRequest
{
  using System;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.UserRequest.ReadRequest;
  using Sitecore.MobileSDK.Validators;

  public abstract class AbstractChangeItemRequestBuilder<T> : AbstractBaseRequestBuilder<T>,
    IChangeItemRequestParametersBuilder<T>
    where T : class
  {
    public IChangeItemRequestParametersBuilder<T> AddFieldsRawValuesByName(IDictionary<string, string> fieldsRawValuesByName)
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

    public IChangeItemRequestParametersBuilder<T> AddFieldsRawValuesByName(string fieldKey, string fieldValue)
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
        throw new InvalidOperationException(this.GetType().Name + ".FieldsRawValuesByName : duplicate fields are not allowed");
      }

      this.FieldsRawValuesByName.Add(lowerCaseField, fieldValue);

      return this;
    }

    new public IChangeItemRequestParametersBuilder<T> Database(string sitecoreDatabase)
    {
      return (IChangeItemRequestParametersBuilder<T>)base.Database(sitecoreDatabase);
    }

    new public IChangeItemRequestParametersBuilder<T> Language(string itemLanguage)
    {
      return (IChangeItemRequestParametersBuilder<T>)base.Language(itemLanguage);
    }

    new public IChangeItemRequestParametersBuilder<T> Payload(PayloadType payload)
    {
      return (IChangeItemRequestParametersBuilder<T>)base.Payload(payload);
    }

    new public IChangeItemRequestParametersBuilder<T> AddFields(IEnumerable<string> fields)
    {
      return (IChangeItemRequestParametersBuilder<T>)base.AddFields(fields);
    }

    new public IChangeItemRequestParametersBuilder<T> AddFields(params string[] fieldParams)
    {
      return (IChangeItemRequestParametersBuilder<T>)base.AddFields(fieldParams);
    }

    protected IDictionary<string, string> FieldsRawValuesByName;
  }
}

