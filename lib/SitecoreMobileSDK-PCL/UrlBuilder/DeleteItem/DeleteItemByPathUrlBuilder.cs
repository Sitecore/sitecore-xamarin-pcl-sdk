namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.Validators;

  public class DeleteItemByPathUrlBuilder : AbstractDeleteItemUrlBuilder<IDeleteItemsByPathRequest>
  {
    public DeleteItemByPathUrlBuilder(IRestServiceGrammar restGrammar, ISSCUrlParameters sscGrammar)
      : base(restGrammar, sscGrammar)
    {
    }


    //TODO: @igk saved in demo purposes to test item manipulations via path
    //FIXME: @igk remove before release
    public override string GetUrlForRequest(IDeleteItemsByPathRequest request)
    {
      this.Validate(request);

      string result = base.GetBaseUrlForRequest(request);

      string escapedPath = UrlBuilderUtils.EscapeDataString(request.ItemPath.ToLowerInvariant());
      string strItemPath = this.SSCGrammar.ItemPathParameterName + this.RestGrammar.KeyValuePairSeparator + escapedPath;
      string lowerCaseItemPath = strItemPath.ToLowerInvariant();

      result += this.RestGrammar.HostAndArgsSeparator + lowerCaseItemPath;

      string parameters = this.GetParametersString(request);

      if (!string.IsNullOrEmpty(parameters))
      {
        result += this.RestGrammar.FieldSeparator + parameters;
      }

      return result.ToLowerInvariant();
    }

    public override void ValidateSpecificPart(IDeleteItemsByPathRequest request)
    {
      ItemPathValidator.ValidateItemPath(request.ItemPath, this.GetType().Name + ".ItemPath");
    }
  }
}
