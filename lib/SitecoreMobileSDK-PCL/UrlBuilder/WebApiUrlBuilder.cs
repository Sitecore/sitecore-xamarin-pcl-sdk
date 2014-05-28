
namespace Sitecore.MobileSDK.UrlBuilder
{
    using System;

    public class WebApiUrlBuilder
    {
        private WebApiUrlBuilder()
        {
        }

        public WebApiUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
        {
            this.restGrammar = restGrammar;
            this.webApiGrammar = webApiGrammar;

            this.Validate ();
        }

        public string GetUrlForRequest(IRequestConfig request)
        {
            this.ValidateRequest(request);

            string escapedId = Uri.EscapeDataString(request.ItemId);
            string escapedVersion = Uri.EscapeDataString (request.WebApiVersion);

            if ( !this.IsValidUrlScheme(request.InstanceUrl) )
            {
                request.InstanceUrl = request.InstanceUrl.Insert(0, "http://");
            }



            string result = request.InstanceUrl + "/-/item/" + escapedVersion + "?sc_itemid=" + escapedId;
            return result.ToLowerInvariant();
        }

        private bool IsValidUrlScheme( string url )
        {
            string lowercaseUrl = url.ToLowerInvariant ();

            bool isHttps = lowercaseUrl.StartsWith ("https://");
            bool isHttp = lowercaseUrl.StartsWith ("http://");
            bool result =  (isHttps || isHttp);

            return result;
        }

        private void ValidateRequest(IRequestConfig request)
        {
            if (null == request)
            {
                throw new ArgumentNullException("WebApiUrlBuilder.GetUrlForRequest() : do not pass null");
            }

            bool hasOpeningBrace = request.ItemId.StartsWith("{");
            bool hasClosingBrace = request.ItemId.EndsWith("}");
            bool isValidId = hasOpeningBrace && hasClosingBrace;
            if (!isValidId)
            {
                throw new ArgumentException("WebApiUrlBuilder.GetUrlForRequest() : item id must have curly braces '{}'");
            }

        }

        private void Validate()
        {
            // TODO : implement me
        }

        private IRestServiceGrammar restGrammar;
        private IWebApiUrlParameters webApiGrammar;
    }
}
