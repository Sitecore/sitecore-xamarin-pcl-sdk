﻿namespace Sitecore.MobileSDK.UrlBuilder.Search
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.Validators;

  public class RunStoredQuerryUrlBuilder : GetPagedItemsUrlBuilder<IReadItemsByIdRequest>
  {
    public RunStoredQuerryUrlBuilder(IRestServiceGrammar restGrammar, ISSCUrlParameters sscGrammar)
      : base(restGrammar, sscGrammar)
    {
    }

    protected override string GetHostUrlForRequest(IReadItemsByIdRequest request)
    {
      string hostUrl = base.GetHostUrlForRequest(request);
      string itemId = UrlBuilderUtils.EscapeDataString(request.ItemId.ToLowerInvariant());

      string result = hostUrl + this.restGrammar.PathComponentSeparator + itemId + sscGrammar.RunStoredQueryAction;
      return result;
    }

    protected override string GetItemIdenticationForRequest(IReadItemsByIdRequest request)
    {
      return null;
    }

    protected override void ValidateSpecificRequest(IReadItemsByIdRequest request)
    {
      base.ValidateSpecificRequest(request);
      ItemIdValidator.ValidateItemId(request.ItemId, this.GetType().Name + ".ItemId");
    }
  }
}
