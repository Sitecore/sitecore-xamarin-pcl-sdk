namespace Sitecore.MobileSDK.UserRequest
{
  using System;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
  using Sitecore.MobileSDK.UserRequest.ReadRequest;
  using Sitecore.MobileSDK.Validators;

  public abstract class AbstractScopedRequestParametersBuilder<T> : AbstractBaseRequestBuilder<T>,
    IScopedRequestParametersBuilder<T>
    where T : class
  {
    public IScopedRequestParametersBuilder<T> AddScope(IEnumerable<ScopeType> scope)
    {
      ScopeParameters scopeParameters = new ScopeParameters(this.queryParameters.ScopeParameters);

      foreach (ScopeType singleScope in scope)
      {
        if (!scopeParameters.AddScope(singleScope))
        {
          throw new InvalidOperationException(this.GetType().Name + ".Scope : Adding scope parameter duplicates is forbidden");
        }
      }

      this.queryParameters = new QueryParameters(this.queryParameters.Payload, scopeParameters, this.queryParameters.Fields);
      return this;
    }

    public IScopedRequestParametersBuilder<T> AddScope(params ScopeType[] scope)
    {
      BaseValidator.CheckNullAndThrow(scope, this.GetType().Name + ".Scope");

      IEnumerable<ScopeType> castedScope = scope;
      return this.AddScope(castedScope);
    }

    public IScopedRequestParametersBuilder<T> Database(string sitecoreDatabase)
    {
      return (IScopedRequestParametersBuilder<T>)base.Database(sitecoreDatabase);
    }

    public IScopedRequestParametersBuilder<T> Language(string itemLanguage)
    {
      return (IScopedRequestParametersBuilder<T>)base.Language(itemLanguage);
    }

    public IScopedRequestParametersBuilder<T> Payload(PayloadType payload)
    {
      return (IScopedRequestParametersBuilder<T>)base.Payload(payload);
    }

    public IScopedRequestParametersBuilder<T> AddFields(IEnumerable<string> fields)
    {
      return (IScopedRequestParametersBuilder<T>)base.AddFields(fields);
    }

    public IScopedRequestParametersBuilder<T> AddFields(params string[] fieldParams)
    {
      return (IScopedRequestParametersBuilder<T>)base.AddFields(fieldParams);
    }
  }
}
