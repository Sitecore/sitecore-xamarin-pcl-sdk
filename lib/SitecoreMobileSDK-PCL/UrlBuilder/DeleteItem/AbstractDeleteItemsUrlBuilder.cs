﻿namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using System;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;

  public abstract class AbstractDeleteItemsUrlBuilder<TRequest> : IDeleteItemsUrlBuilder<TRequest>
    where TRequest : IBaseDeleteItemRequest
  {
    private SessionConfigUrlBuilder sessionConfigUrlBuilder;
    readonly ScopeParametersUrlBuilder scopeBuilder;
    protected IRestServiceGrammar restGrammar;
    protected IWebApiUrlParameters webApiGrammar;

    public AbstractDeleteItemsUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
    {
      this.restGrammar = restGrammar;
      this.webApiGrammar = webApiGrammar;
      this.sessionConfigUrlBuilder = new SessionConfigUrlBuilder(restGrammar, webApiGrammar);
      this.scopeBuilder = new ScopeParametersUrlBuilder(restGrammar, webApiGrammar);
    }

    public abstract void ValidateSpecificPart(TRequest request);

    public abstract string GetUrlForRequest(TRequest request);

    public virtual string GetBaseUrlForRequest(TRequest request)
    {
      return this.sessionConfigUrlBuilder.BuildUrlString(request.SessionConfig);
    }

    protected string GetParametersString(TRequest request)
    {
      var parametersString = "";

      var scopeString = this.scopeBuilder.ScopeToRestArgumentStatement(request.ScopeParameters);
      var database = request.Database;

      if (!string.IsNullOrEmpty(scopeString))
      {
        parametersString += scopeString;
      }

      if (!string.IsNullOrEmpty(database))
      {
        if (!string.IsNullOrEmpty(parametersString))
        {
          parametersString += this.restGrammar.FieldSeparator;
        }
        parametersString += this.webApiGrammar.DatabaseParameterName + this.restGrammar.KeyValuePairSeparator 
          + database;
      }
      return parametersString;
    }

    protected void Validate(TRequest request)
    {
      if (null == request)
      {
        throw new ArgumentNullException("request",
          "AbstractDeleteItemsUrlBuilder.GetBaseUrlForRequest() : do not pass null request");
      }

      if (null == request.SessionConfig)
      {
        throw new ArgumentNullException("SessionConfig",
          "AbstractDeleteItemsUrlBuilder.GetBaseUrlForRequest() : do not pass null SessionConfig");
      }

      if (null == request.SessionConfig.InstanceUrl)
      {
        throw new ArgumentNullException("InstanceUrl",
          "AbstractDeleteItemsUrlBuilder.GetBaseUrlForRequest() : SessionSettings.InstanceUrl cannot be null");
      }

      if (null == request.SessionConfig.ItemWebApiVersion)
      {
        throw new ArgumentNullException("ItemWebApiVersion",
          "AbstractDeleteItemsUrlBuilder.GetBaseUrlForRequest() : SessionSettings.InstanceUrl.ItemWebApiVersion cannot be null");
      }

      this.ValidateSpecificPart(request);
    }
  }
}