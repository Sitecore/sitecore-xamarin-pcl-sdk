namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.ChangeItem;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.Validators;

  public abstract class AbstractCreateItemUrlBuilder<TRequest> : ICreateItemUrlBuilder<TRequest>
    where TRequest : ICreateItemByPathRequest
  {
    private readonly SessionConfigUrlBuilder sessionConfigUrlBuilder;
    protected IRestServiceGrammar RestGrammar;
    protected ISSCUrlParameters SSCGrammar;


    public AbstractCreateItemUrlBuilder(IRestServiceGrammar restGrammar, ISSCUrlParameters sscGrammar)
    {
      this.RestGrammar = restGrammar;
      this.SSCGrammar = sscGrammar;
      this.sessionConfigUrlBuilder = new SessionConfigUrlBuilder(restGrammar, sscGrammar);
    }

    public abstract string GetUrlForRequest(TRequest request);

    protected void Validate(TRequest request)
    {
      BaseValidator.CheckNullAndThrow(request, this.GetType().Name + ".request");

      this.ValidateSpecificPart(request);
    }

    public virtual string GetBaseUrlForRequest(TRequest request)
    {
      string hostUrl = this.sessionConfigUrlBuilder.BuildUrlString(request.SessionSettings);
      hostUrl = hostUrl + this.SSCGrammar.ItemSSCItemsEndpoint;

      return hostUrl;
    }

    public abstract void ValidateSpecificPart(TRequest request);

//    //TODO: IGK probable we do not need this class at all for now
//    protected override string GetSpecificPartForRequest(TRequest request)
//    {
//      return "";

////      string escapedTemplate = UrlBuilderUtils.EscapeDataString(request.ItemTemplateId).ToLowerInvariant();
////      string escapedName = UrlBuilderUtils.EscapeDataString(request.ItemName);
////
////      string result =
////        this.sscGrammar.TemplateParameterName
////        + this.restGrammar.KeyValuePairSeparator
////        + escapedTemplate;
////
////
////      if (!string.IsNullOrEmpty(escapedName))
////      {
////        result = result
////          + this.restGrammar.FieldSeparator
////          + this.sscGrammar.ItemNameParameterName
////          + this.restGrammar.KeyValuePairSeparator
////          + escapedName;
////      }
////
////      return result;
//    }
  }
}

