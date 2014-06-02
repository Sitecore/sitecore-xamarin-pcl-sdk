namespace Sitecore.MobileSDK.SessionSettings
{
    using System;
	using Sitecore.MobileSDK.Utils;
    using Sitecore.MobileSDK.UrlBuilder;
    using Sitecore.MobileSDK.UrlBuilder.Rest;
    using Sitecore.MobileSDK.UrlBuilder.WebApi;

    public class SessionConfigUrlBuilder
    {
        public SessionConfigUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
        {
            this.restGrammar = restGrammar;
            this.webApiGrammar = webApiGrammar;

            this.Validate();
        }

        public virtual string BuildUrlString(ISessionConfig request)
        {
            this.ValidateRequest(request);

            string autocompletedInstanceUrl = request.InstanceUrl;
            if (!this.IsValidUrlScheme(autocompletedInstanceUrl))
            {
                autocompletedInstanceUrl = autocompletedInstanceUrl.Insert(0, "http://");
            }

            string escapedVersion = Uri.EscapeDataString(request.ItemWebApiVersion);


            string result =
                autocompletedInstanceUrl;

            result +=
                    this.webApiGrammar.ItemWebApiEndpoint +
                    escapedVersion;

            if (!string.IsNullOrEmpty (request.Site))
            {
                string escapedSite = UrlBuilderUtils.EscapeDataString(request.Site);
				result += this.restGrammar.PathComponentSeparator +
                    escapedSite;
            }

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

        private void ValidateRequest(ISessionConfig request)
        {
            if (null == request)
            {
                throw new ArgumentNullException("WebApiUrlBuilder.GetUrlForRequest() : do not pass null");
            }
        }

        private void Validate()
        {
            if (null == this.restGrammar)
            {
                throw new ArgumentNullException ("[SessionConfigUrlBuilder] restGrammar cannot be null");
            }
            else if (null == this.webApiGrammar)
            {
                throw new ArgumentNullException ("[SessionConfigUrlBuilder] webApiGrammar cannot be null");
            }
        }

        private IRestServiceGrammar restGrammar;
        private IWebApiUrlParameters webApiGrammar;
    }
}
