namespace Sitecore.MobileSDK.UserRequest.UpdateRequest
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.UserRequest.ChangeRequest;
  using Sitecore.MobileSDK.Validators;

  public abstract class AbstractUpdateItemRequestBuilder<T> : AbstractChangeItemRequestBuilder<T>, 
    IUpdateItemRequestParametersBuilder<T> 
    where T : class
  {

    public IUpdateItemRequestParametersBuilder<T> Version(int? itemVersion)
    {
      BaseValidator.AssertPositiveNumber(itemVersion, this.GetType().Name + ".Version");

      BaseValidator.CheckForTwiceSetAndThrow(this.itemSourceAccumulator.VersionNumber, this.GetType().Name + ".Version");

      this.itemSourceAccumulator = new ItemSourcePOD(
        this.itemSourceAccumulator.Database,
        this.itemSourceAccumulator.Language,
        itemVersion);

      return this;
    }

    public IUpdateItemRequestParametersBuilder<T> AddFieldsRawValuesByNameToSet(IDictionary<string, string> fieldsRawValuesByName)
    {
      return (IUpdateItemRequestParametersBuilder<T>)base.AddFieldsRawValuesByNameToSet(fieldsRawValuesByName);
    }

    public IUpdateItemRequestParametersBuilder<T> AddFieldsRawValuesByNameToSet(string fieldKey, string fieldValue)
    {
      return (IUpdateItemRequestParametersBuilder<T>)base.AddFieldsRawValuesByNameToSet(fieldKey, fieldValue);
    }

    public IUpdateItemRequestParametersBuilder<T> Database(string sitecoreDatabase)
    {
      return (IUpdateItemRequestParametersBuilder<T>)base.Database(sitecoreDatabase);
    }

    public IUpdateItemRequestParametersBuilder<T> Language(string itemLanguage)
    {
      return (IUpdateItemRequestParametersBuilder<T>)base.Language(itemLanguage);
    }

    public IUpdateItemRequestParametersBuilder<T> Payload(PayloadType payload)
    {
      return (IUpdateItemRequestParametersBuilder<T>)base.Payload(payload);
    }

    public IUpdateItemRequestParametersBuilder<T> AddFieldsToRead(IEnumerable<string> fields)
    {
      return (IUpdateItemRequestParametersBuilder<T>)base.AddFieldsToRead(fields);
    }

    public IUpdateItemRequestParametersBuilder<T> AddFieldsToRead(params string[] fieldParams)
    {
      return (IUpdateItemRequestParametersBuilder<T>)base.AddFieldsToRead(fieldParams);
    }
  }
}

