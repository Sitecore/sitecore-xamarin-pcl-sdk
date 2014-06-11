
namespace Sitecore.MobileSDK
{
    using System.Collections.Generic;

    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.UrlBuilder.QueryParameters;


    public abstract class AbstractGetItemRequestBuilder<T> : IGetItemRequestParametersBuilder<T>
        where T : class
    {
        public IGetItemRequestParametersBuilder<T> Database (string sitecoreDatabase)
        {
            this.itemSourceAccumulator = new ItemSourcePOD (
                sitecoreDatabase, 
                this.itemSourceAccumulator.Language, 
                this.itemSourceAccumulator.Version);

            return this;
        }

        public IGetItemRequestParametersBuilder<T> Language (string itemLanguage)
        {
            this.itemSourceAccumulator = new ItemSourcePOD (
                this.itemSourceAccumulator.Database, 
                itemLanguage, 
                this.itemSourceAccumulator.Version);

            return this;
        }

        public IGetItemRequestParametersBuilder<T> Version (string itemVersion)
        {
            this.itemSourceAccumulator = new ItemSourcePOD (
                this.itemSourceAccumulator.Database, 
                this.itemSourceAccumulator.Language,
                itemVersion);

            return this;
        }

        public IGetItemRequestParametersBuilder<T> Payload(PayloadType payload)
        {
            this.queryParameters = new QueryParameters(payload, this.queryParameters.Fields );
            return this;
        }

        public IGetItemRequestParametersBuilder<T> LoadFields( ICollection<string> fields )
        {
            this.queryParameters = new QueryParameters( this.queryParameters.Payload, fields );
            return this;
        }


        public abstract T Build();

        protected ItemSourcePOD itemSourceAccumulator = new ItemSourcePOD( null, null, null );
        protected QueryParameters queryParameters = new QueryParameters( PayloadType.Default, null );
        protected ICollection<string> fields = null;
    }
}

