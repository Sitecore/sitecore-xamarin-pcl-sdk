namespace Sitecore.MobileSDK.UrlBuilder
{
    using System;
    using Sitecore.MobileSDK.Items;

    public class ItemPathUrlBuilder : WebApiUrlBuilder
    {
        public ItemPathUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
            : base(restGrammar, webApiGrammar)
        {
        }

        public void ValidatePath(string itemPath)
        {
            if (string.IsNullOrEmpty(itemPath))
            {
                throw new ArgumentNullException("ItemPathUrlBuilder.GetUrlForRequest() : item path cannot be null or empty");
            }

            if (!itemPath.StartsWith("/"))
            {
                throw new ArgumentException("ItemPathUrlBuilder.GetUrlForRequest() : item path should begin with '/'");
            }
        }

        public override string GetUrlForRequest(IRequestConfig request)
        {
            string result = base.GetUrlForRequest(request);
            
            ReadItemByPathParameters config = (ReadItemByPathParameters)request;

            this.ValidatePath(config.ItemPath);

            string escapedPath = Uri.EscapeUriString(config.ItemPath);

            result += escapedPath;
            return result.ToLowerInvariant();
        }
    }
}
