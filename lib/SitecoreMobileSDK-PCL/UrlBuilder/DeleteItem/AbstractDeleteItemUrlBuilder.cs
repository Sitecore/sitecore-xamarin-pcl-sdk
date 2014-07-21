namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using System;
  using Sitecore.MobileSDK.Items.Delete;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;

  public abstract class AbstractDeleteItemUrlBuilder<TRequest> : IDeleteItemsUrlBuilder<TRequest>
    where TRequest : IBaseDeleteItemRequest
  {
    private readonly SessionConfigUrlBuilder sessionConfigUrlBuilder;
    readonly ScopeParametersUrlBuilder scopeBuilder;
    protected IRestServiceGrammar RestGrammar;
    protected IWebApiUrlParameters WebApiGrammar;

    protected AbstractDeleteItemUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
    {
      this.RestGrammar = restGrammar;
      this.WebApiGrammar = webApiGrammar;
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
          parametersString += this.RestGrammar.FieldSeparator;
        }
        parametersString += this.WebApiGrammar.DatabaseParameterName + this.RestGrammar.KeyValuePairSeparator
          + database;
      }
      return parametersString;
    }

    protected void Validate(TRequest request)
    {
      if (null == request)
      {
        throw new ArgumentNullException("request",
          "AbstractDeleteItemUrlBuilder.GetBaseUrlForRequest() : do not pass null request");
      }

      if (null == request.SessionConfig)
      {
        throw new ArgumentNullException("SessionConfig",
          "AbstractDeleteItemUrlBuilder.GetBaseUrlForRequest() : do not pass null SessionConfig");
      }

      if (null == request.SessionConfig.InstanceUrl)
      {
        throw new ArgumentNullException("InstanceUrl",
          "AbstractDeleteItemUrlBuilder.GetBaseUrlForRequest() : SessionSettings.InstanceUrl cannot be null");
      }

      if (null == request.SessionConfig.ItemWebApiVersion)
      {
        throw new ArgumentNullException("ItemWebApiVersion",
          "AbstractDeleteItemUrlBuilder.GetBaseUrlForRequest() : " +
          "SessionSettings.InstanceUrl.ItemWebApiVersion cannot be null");
      }

      this.ValidateSpecificPart(request);
    }
  }
}
