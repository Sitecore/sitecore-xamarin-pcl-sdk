
namespace Sitecore.MobileSDK.UserRequest
{
  using System;
  using System.Collections.Generic;

  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;



  public abstract class AbstractGetVersionedItemRequestBuilder<T> : AbstractGetItemRequestBuilder<T>, IGetVersionedItemRequestParametersBuilder<T>
    where T : class
  {
    public IGetVersionedItemRequestParametersBuilder<T> Version(string itemVersion)
    {
      if (string.IsNullOrWhiteSpace(itemVersion))
      {
        throw new ArgumentException("AbstractGetItemRequestBuilder.Version : The input cannot be null or empty");
      }
      else if (null != this.itemSourceAccumulator.Version)
      {
        throw new InvalidOperationException("AbstractGetItemRequestBuilder.Version : The item's version cannot be assigned twice");
      }

      this.itemSourceAccumulator =  new ItemSourcePOD(
        this.itemSourceAccumulator.Database, 
        this.itemSourceAccumulator.Language,
        itemVersion);

      return this;
    }

    #region Compatibility Casts
    public IGetVersionedItemRequestParametersBuilder<T> Database(string sitecoreDatabase)
    {
      return (IGetVersionedItemRequestParametersBuilder<T>)base.Database(sitecoreDatabase);
    }

    public IGetVersionedItemRequestParametersBuilder<T> Language(string itemLanguage)
    {
      return (IGetVersionedItemRequestParametersBuilder<T>)base.Language(itemLanguage);
    }

    public IGetVersionedItemRequestParametersBuilder<T> Payload(PayloadType payload)
    {
      return (IGetVersionedItemRequestParametersBuilder<T>)base.Payload(payload);
    }
      
    public IGetVersionedItemRequestParametersBuilder<T> AddFields(ICollection<string> fields)
    {
      return (IGetVersionedItemRequestParametersBuilder<T>)base.AddFields(fields);
    }

    public IGetVersionedItemRequestParametersBuilder<T> AddFields(params string[] fieldParams)
    {
      return (IGetVersionedItemRequestParametersBuilder<T>)base.AddFields(fieldParams);
    }

    public IGetVersionedItemRequestParametersBuilder<T> AddScope(params ScopeType[] scope)
    {
      return (IGetVersionedItemRequestParametersBuilder<T>)base.AddScope(scope);
    }

    public IGetVersionedItemRequestParametersBuilder<T> AddScope(ICollection<ScopeType> scope)
    {
      return (IGetVersionedItemRequestParametersBuilder<T>)base.AddScope(scope);
    }
    #endregion Compatibility Casts
  }
}

