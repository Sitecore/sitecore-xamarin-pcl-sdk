
namespace Sitecore.MobileSDK
{
    using System;
    using Sitecore.MobileSDK.UrlBuilder.Rest;
    using Sitecore.MobileSDK.UrlBuilder.WebApi;


    public class AbstractGetItemUrlBuilder
    {
        public AbstractGetItemUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
        {
            this.restGrammar = restGrammar;
            this.webApiGrammar = webApiGrammar;

            this.Validate();
        }

        private void Validate()
        {
            if (null == this.restGrammar)
            {
                throw new ArgumentNullException ("[GetItemUrlBuilder] restGrammar cannot be null"); 
            }
            else if (null == this.webApiGrammar)
            {
                throw new ArgumentNullException ("[GetItemUrlBuilder] webApiGrammar cannot be null");
            }
        }


        protected IRestServiceGrammar restGrammar;
        protected IWebApiUrlParameters webApiGrammar;
    }
}

