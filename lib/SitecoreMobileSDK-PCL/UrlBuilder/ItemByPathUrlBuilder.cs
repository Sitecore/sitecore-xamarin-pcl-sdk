namespace Sitecore.MobileSDK.UrlBuilder
{
    using System;
    using Sitecore.MobileSDK.Items;

    public class ItemByPathUrlBuilder
    {
        public ItemByPathUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
        {
            this.restGrammar = restGrammar;
            this.webApiGrammar = webApiGrammar;

            this.Validate();
        }

        public void ValidatePath(string itemPath)
        {
            if (string.IsNullOrEmpty(itemPath))
            {
                throw new ArgumentNullException("ItemByPathUrlBuilder.GetUrlForRequest() : item path cannot be null or empty");
            }

            if (!itemPath.StartsWith("/"))
            {
                throw new ArgumentException("ItemByPathUrlBuilder.GetUrlForRequest() : item path should begin with '/'");
            }
        }

        public string GetUrlForRequest(IGetItemByPathRequest request)
        {
            return null;
            //            ReadItemByPathParameters config = (ReadItemByPathParameters)request;
            //
            //            this.ValidatePath(config.ItemPath);
            //
            //            string escapedPath = Uri.EscapeUriString(config.ItemPath);
            //
            //            result += escapedPath;
            //            return result.ToLowerInvariant();
        }

        private void Validate()
        {
            // TODO : implement me
        }

        private IRestServiceGrammar restGrammar;
        private IWebApiUrlParameters webApiGrammar;
    }
}
