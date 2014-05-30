namespace Sitecore.MobileSDK.SessionSettings
{
    using System;
    using Sitecore.MobileSDK.UrlBuilder;

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
                autocompletedInstanceUrl +
                    this.webApiGrammar.ItemWebApiEndpoint +
                    escapedVersion;

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
            // TODO : implement me
        }

        private IRestServiceGrammar restGrammar;
        private IWebApiUrlParameters webApiGrammar;
    }
}
