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

    public IUpdateItemRequestParametersBuilder<T> AddFieldsRawValuesByName(IDictionary<string, string> fieldsRawValuesByName)
    {
      return (IUpdateItemRequestParametersBuilder<T>)base.AddFieldsRawValuesByName(fieldsRawValuesByName);
    }

    public IUpdateItemRequestParametersBuilder<T> AddFieldsRawValuesByName(string fieldKey, string fieldValue)
    {
      return (IUpdateItemRequestParametersBuilder<T>)base.AddFieldsRawValuesByName(fieldKey, fieldValue);
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

    public IUpdateItemRequestParametersBuilder<T> AddFields(IEnumerable<string> fields)
    {
      return (IUpdateItemRequestParametersBuilder<T>)base.AddFields(fields);
    }

    public IUpdateItemRequestParametersBuilder<T> AddFields(params string[] fieldParams)
    {
      return (IUpdateItemRequestParametersBuilder<T>)base.AddFields(fieldParams);
    }
  }
}

