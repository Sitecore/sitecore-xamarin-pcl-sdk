using Sitecore.MobileSDK.SessionSettings;

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

        public string GetUrlForRequest(IGetItemByPathRequest request)
        {
            this.ValidateRequest (request);
            string escapedPath = Uri.EscapeDataString(request.ItemPath);

            SessionConfigUrlBuilder sessionBuilder = new SessionConfigUrlBuilder(this.restGrammar, this.webApiGrammar);
            string result = sessionBuilder.BuildUrlString(request.SessionSettings);

            ItemSourceUrlBuilder sourceBuilder = new ItemSourceUrlBuilder (this.restGrammar, this.webApiGrammar, request.ItemSource);
            string itemSourceArgs = sourceBuilder.BuildUrlQueryString ();

            result += 
                escapedPath + 
                this.restGrammar.HostAndArgsSeparator + 
                itemSourceArgs;

            return result.ToLowerInvariant();
        }

        private void ValidateRequest(IGetItemByPathRequest request)
        {
            if (null == request)
            {
                throw new ArgumentNullException("ItemByPathUrlBuilder.GetUrlForRequest() : request cannot be null");
            }

            this.ValidatePath (request.ItemPath);
        }

        private void ValidatePath(string itemPath)
        {
            if (null == itemPath)
            {
                throw new ArgumentNullException ("ItemByPathUrlBuilder.GetUrlForRequest() : item path cannot be null or empty");
            }
            else if (string.Empty.Equals (itemPath))
            {
                throw new ArgumentException ("ItemByPathUrlBuilder.GetUrlForRequest() : item path cannot be null or empty");
            }
            else if (!itemPath.StartsWith("/"))
            {
                throw new ArgumentException("ItemByPathUrlBuilder.GetUrlForRequest() : item path should begin with '/'");
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
