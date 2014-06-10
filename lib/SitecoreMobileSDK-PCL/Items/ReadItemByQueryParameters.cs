
namespace Sitecore.MobileSDK
{
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;
    using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

    public class ReadItemByQueryParameters : IReadItemsByQueryRequest
    {
        public ReadItemByQueryParameters(
            ISessionConfig sessionSettings,
            IItemSource itemSource, 
            IQueryParameters queryParameters, 
            string sitecoreQuery)
        {
            this.SessionSettings = sessionSettings;
            this.ItemSource = itemSource;
            this.SitecoreQuery = sitecoreQuery;
            this.QueryParameters = queryParameters;
        }

        public string SitecoreQuery { get; private set; }

        public IItemSource ItemSource { get; private set; }

        public ISessionConfig SessionSettings { get; private set; }

        public IQueryParameters QueryParameters { get; private set; }
    }
}

