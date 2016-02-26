namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Validators;

  public abstract class AbstractDeleteItemUrlBuilder<TRequest> : IDeleteItemsUrlBuilder<TRequest>
    where TRequest : IBaseDeleteItemRequest
  {
    private readonly SessionConfigUrlBuilder sessionConfigUrlBuilder;
    protected IRestServiceGrammar RestGrammar;
    protected IWebApiUrlParameters WebApiGrammar;

    protected AbstractDeleteItemUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
    {
      this.RestGrammar = restGrammar;
      this.WebApiGrammar = webApiGrammar;
      this.sessionConfigUrlBuilder = new SessionConfigUrlBuilder(restGrammar, webApiGrammar);
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

      var database = request.Database;

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
      BaseValidator.CheckNullAndThrow(request, this.GetType().Name + ".request");

      BaseValidator.CheckNullAndThrow(request.SessionConfig, this.GetType().Name + ".SessionConfig");

      BaseValidator.CheckNullAndThrow(request.SessionConfig.InstanceUrl, this.GetType().Name + ".InstanceUrl");

      BaseValidator.CheckNullAndThrow(request.SessionConfig.ItemWebApiVersion,
        this.GetType().Name + ".ItemWebApiVersion");

      this.ValidateSpecificPart(request);
    }
  }
}
