
namespace Sitecore.MobileSDK.UrlBuilder
{
    using System;

    public class WebApiUrlBuilder
    {
        protected WebApiUrlBuilder()
        {
        }

        public WebApiUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
        {
            this.restGrammar = restGrammar;
            this.webApiGrammar = webApiGrammar;

            this.Validate();
        }

        public virtual string GetUrlForRequest(IRequestConfig request)
        {
            this.ValidateRequest(request);

            if (!this.IsValidUrlScheme(request.InstanceUrl))
            {
                request.InstanceUrl = request.InstanceUrl.Insert(0, "http://");
            }

            string escapedVersion = Uri.EscapeDataString(request.WebApiVersion);

            string result =
                request.InstanceUrl +
                    this.webApiGrammar.ItemWebApiEndpoint +
                    escapedVersion +
                    this.restGrammar.HostAndArgsSeparator;

            return result.ToLowerInvariant();
        }

        private bool IsValidUrlScheme(string url)
        {
            string lowercaseUrl = url.ToLowerInvariant();

            bool isHttps = lowercaseUrl.StartsWith("https://");
            bool isHttp = lowercaseUrl.StartsWith("http://");
            bool result = (isHttps || isHttp);

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

        protected IRestServiceGrammar restGrammar;
        protected IWebApiUrlParameters webApiGrammar;
    }
}
