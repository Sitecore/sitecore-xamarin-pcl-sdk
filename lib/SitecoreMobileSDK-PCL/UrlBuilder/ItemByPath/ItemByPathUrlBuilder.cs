﻿

namespace Sitecore.MobileSDK.UrlBuilder.ItemByPath
{
  using System;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.SessionSettings;


  public class ItemByPathUrlBuilder : AbstractGetItemUrlBuilder<IReadItemsByPathRequest>
  {
    public ItemByPathUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
      : base( restGrammar, webApiGrammar )
    {
    }

    protected override string GetHostUrlForRequest(IReadItemsByPathRequest request)
    {
      string hostUrl = base.GetHostUrlForRequest(request);
      string escapedPath = UrlBuilderUtils.EscapeDataString(request.ItemPath.ToLowerInvariant());

      string result = hostUrl + escapedPath;
      return result;
    }

    protected override string GetSpecificPartForRequest(IReadItemsByPathRequest request)
    {
      return null;
    }

    protected override void ValidateSpecificRequest(IReadItemsByPathRequest request)
    {
      ItemPathValidator.ValidateItemPath (request.ItemPath);
    }
  }
}
