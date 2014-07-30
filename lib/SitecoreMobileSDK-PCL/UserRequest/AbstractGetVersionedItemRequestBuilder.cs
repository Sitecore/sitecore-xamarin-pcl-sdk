namespace Sitecore.MobileSDK.UserRequest
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Validators;

  public abstract class AbstractGetVersionedItemRequestBuilder<T> : 
    AbstractBaseRequestBuilder<T>, 
    IGetVersionedItemRequestParametersBuilder<T>
  where T : class
  {
    public IGetVersionedItemRequestParametersBuilder<T> Version(string itemVersion)
    {
      if (string.IsNullOrWhiteSpace(itemVersion))
      {
        BaseValidator.ThrowNullOrEmptyParameterException(this.GetType().Name + ".itemVersion");
      }
      else if (null != this.itemSourceAccumulator.Version)
      {
        BaseValidator.ThrowParameterSetTwiceException(this.GetType().Name + ".Version");
      }

      this.itemSourceAccumulator = new ItemSourcePOD(
        this.itemSourceAccumulator.Database,
        this.itemSourceAccumulator.Language,
        itemVersion);

      return this;
    }

    //REVIEW: "new" modifier
    #region Compatibility Casts
    new public IGetVersionedItemRequestParametersBuilder<T> Database(string sitecoreDatabase)
    {
      return (IGetVersionedItemRequestParametersBuilder<T>)base.Database(sitecoreDatabase);
    }

    new public IGetVersionedItemRequestParametersBuilder<T> Language(string itemLanguage)
    {
      return (IGetVersionedItemRequestParametersBuilder<T>)base.Language(itemLanguage);
    }

    new public IGetVersionedItemRequestParametersBuilder<T> Payload(PayloadType payload)
    {
      return (IGetVersionedItemRequestParametersBuilder<T>)base.Payload(payload);
    }

    new public IGetVersionedItemRequestParametersBuilder<T> AddFields(IEnumerable<string> fields)
    {
      return (IGetVersionedItemRequestParametersBuilder<T>)base.AddFields(fields);
    }

    new public IGetVersionedItemRequestParametersBuilder<T> AddFields(params string[] fieldParams)
    {
      return (IGetVersionedItemRequestParametersBuilder<T>)base.AddFields(fieldParams);
    }

    new public IGetVersionedItemRequestParametersBuilder<T> AddScope(params ScopeType[] scope)
    {
      return (IGetVersionedItemRequestParametersBuilder<T>)base.AddScope(scope);
    }

    new public IGetVersionedItemRequestParametersBuilder<T> AddScope(IEnumerable<ScopeType> scope)
    {
      return (IGetVersionedItemRequestParametersBuilder<T>)base.AddScope(scope);
    }
    #endregion Compatibility Casts
  }
}

