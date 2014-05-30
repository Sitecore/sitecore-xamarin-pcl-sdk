

namespace Sitecore.MobileSDK.Items
{
    using System;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.UrlBuilder.Rest;
    using Sitecore.MobileSDK.UrlBuilder.WebApi;


    public class ItemSourceUrlBuilder
    {
        private ItemSourceUrlBuilder()
        {
        }

        public ItemSourceUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar, IItemSource itemSource)
        {
            this.itemSource = itemSource;
            this.restGrammar = restGrammar;
            this.webApiGrammar = webApiGrammar;

            this.Validate();
        }

        public string BuildUrlQueryString()
        {
            string escapedDatabase = Uri.EscapeDataString(this.itemSource.Database);
            string escapedLanguage = Uri.EscapeDataString(this.itemSource.Language);

            string result =
                this.webApiGrammar.DatabaseParameterName + this.restGrammar.KeyValuePairSeparator + escapedDatabase +

                this.restGrammar.FieldSeparator +
                this.webApiGrammar.LanguageParameterName + this.restGrammar.KeyValuePairSeparator + escapedLanguage;

            if (null != this.itemSource.Version)
            {
                string escapedVersion = Uri.EscapeDataString(this.itemSource.Version);

                result +=
                    this.restGrammar.FieldSeparator +
                    this.webApiGrammar.VersionParameterName + this.restGrammar.KeyValuePairSeparator + escapedVersion;
            }

            return result.ToLowerInvariant();
        }

        private void Validate()
        {
            if (null == this.itemSource)
            {
                throw new ArgumentNullException("[ItemSourceUrlBuilder.itemSource] Do not pass null");
            }
            else if (null == this.restGrammar)
            {
                throw new ArgumentNullException("[ItemSourceUrlBuilder.grammar] Do not pass null");
            }
            else if (null == this.webApiGrammar)
            {
                throw new ArgumentNullException("[ItemSourceUrlBuilder.grammar] Do not pass null");
            }
        }

        private IItemSource itemSource;
        private IRestServiceGrammar restGrammar;
        private IWebApiUrlParameters webApiGrammar;
    }
}

