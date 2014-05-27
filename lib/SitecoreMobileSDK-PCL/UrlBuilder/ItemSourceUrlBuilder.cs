using System;

namespace Sitecore.MobileSDK.UrlBuilder
{
    public class ItemSourceUrlBuilder
    {
        private ItemSourceUrlBuilder ()
        {
        }

        public  ItemSourceUrlBuilder (IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar, ItemSource itemSource)
        {
            this.itemSource = itemSource;
            this.restGrammar = restGrammar;
            this.webApiGrammar = webApiGrammar;

            this.Validate ();
        }

        public string BuildUrlQueryString()
        {
            string result = 
                this.webApiGrammar.DatabaseParameterName + this.restGrammar.KeyValuePairSeparator + this.itemSource.Database +

                this.restGrammar.FieldSeparator + 
                this.webApiGrammar.LanguageParameterName + this.restGrammar.KeyValuePairSeparator + this.itemSource.Language;

            if (null != this.itemSource.Version)
            {
                result += 
                    this.restGrammar.FieldSeparator + 
                    this.webApiGrammar.VersionParameterName + this.restGrammar.KeyValuePairSeparator + this.itemSource.Version;
            }

            return result;
        }

        private void Validate()
        {
            if (null == this.itemSource)
            {
                throw new ArgumentNullException ("[ItemSourceUrlBuilder.itemSource] Do not pass null");
            }
            else if (null == this.restGrammar)
            {
                throw new ArgumentNullException ("[ItemSourceUrlBuilder.grammar] Do not pass null");
            }
            else if (null == this.webApiGrammar)
            {
                throw new ArgumentNullException ("[ItemSourceUrlBuilder.grammar] Do not pass null");
            }
        }

        private ItemSource           itemSource   ;
        private IRestServiceGrammar  restGrammar  ;
        private IWebApiUrlParameters webApiGrammar;
    }
}

