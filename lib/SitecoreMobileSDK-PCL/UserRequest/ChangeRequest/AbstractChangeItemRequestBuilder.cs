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
    public IChangeItemRequestParametersBuilder<T> AddFieldsRawValuesByNameToSet(IDictionary<string, string> fieldsRawValuesByName)
    {
      BaseValidator.CheckNullAndThrow(fieldsRawValuesByName, this.GetType().Name + ".FieldsRawValuesByName");

      if (fieldsRawValuesByName.Count == 0)
      {
        return this;
      }

      foreach (var fieldElem in fieldsRawValuesByName)
      {
        this.AddFieldsRawValuesByNameToSet(fieldElem.Key, fieldElem.Value);
      }

      return this;
    }

    public IChangeItemRequestParametersBuilder<T> AddFieldsRawValuesByNameToSet(string fieldName, string fieldValue)
    {
      BaseValidator.CheckForNullAndEmptyOrThrow(fieldName, this.GetType().Name + ".fieldName");
      BaseValidator.CheckForNullAndEmptyOrThrow(fieldValue, this.GetType().Name + ".fieldValue");

      if (null == this.FieldsRawValuesByName)
      {
        Dictionary<string, string> newFields = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        this.FieldsRawValuesByName = newFields;
      }

      string lowerCaseField = fieldName.ToLowerInvariant();

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

    new public IChangeItemRequestParametersBuilder<T> AddFieldsToRead(IEnumerable<string> fields)
    {
      return (IChangeItemRequestParametersBuilder<T>)base.AddFieldsToRead(fields);
    }

    new public IChangeItemRequestParametersBuilder<T> AddFieldsToRead(params string[] fieldParams)
    {
      return (IChangeItemRequestParametersBuilder<T>)base.AddFieldsToRead(fieldParams);
    }

    protected IDictionary<string, string> FieldsRawValuesByName;
  }
}

