namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.Validators;

  public abstract class AbstractDeleteItemUrlBuilder<TRequest> : IDeleteItemsUrlBuilder<TRequest>
    where TRequest : IBaseDeleteItemRequest
  {
    private readonly SessionConfigUrlBuilder sessionConfigUrlBuilder;
    protected IRestServiceGrammar RestGrammar;
    protected ISSCUrlParameters SSCGrammar;

    protected AbstractDeleteItemUrlBuilder(IRestServiceGrammar restGrammar, ISSCUrlParameters sscGrammar)
    {
      this.RestGrammar = restGrammar;
      this.SSCGrammar = sscGrammar;
      this.sessionConfigUrlBuilder = new SessionConfigUrlBuilder(restGrammar, sscGrammar);
    }

    public abstract void ValidateSpecificPart(TRequest request);

    public abstract string GetUrlForRequest(TRequest request);

    public virtual string GetBaseUrlForRequest(TRequest request)
    {
      string hostUrl = this.sessionConfigUrlBuilder.BuildUrlString(request.SessionConfig);
      hostUrl = hostUrl + this.SSCGrammar.ItemSSCItemsEndpoint;

      return hostUrl;
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
        parametersString += this.SSCGrammar.DatabaseParameterName + this.RestGrammar.KeyValuePairSeparator
          + database;
      }
      return parametersString;
    }

    protected void Validate(TRequest request)
    {
      BaseValidator.CheckNullAndThrow(request, this.GetType().Name + ".request");

      BaseValidator.CheckNullAndThrow(request.SessionConfig, this.GetType().Name + ".SessionConfig");

      BaseValidator.CheckNullAndThrow(request.SessionConfig.InstanceUrl, this.GetType().Name + ".InstanceUrl");

      this.ValidateSpecificPart(request);
    }
  }
}
