using Sitecore.MobileSDK.PublicKey;

namespace Sitecore.MobileSDK.CrudTasks
{
    using System.Net.Http;

    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.TaskFlow;
    using Sitecore.MobileSDK.UrlBuilder;
    using Sitecore.MobileSDK.UrlBuilder.ItemById;


    public class GetItemsByIdTasks : AbstractGetItemTask<ReadItemsByIdParameters>
    {
        public GetItemsByIdTasks(ItemByIdUrlBuilder urlBuilder, HttpClient httpClient, ICredentialsHeadersCryptor credentialsHeadersCryptor) 
            : base(httpClient, credentialsHeadersCryptor)
        {
            this.urlBuilder = urlBuilder;
        }

        protected override string UrlToGetItemWithRequest(ReadItemsByIdParameters request)
        {
            return this.urlBuilder.GetUrlForRequest(request);
        }
            
        private readonly ItemByIdUrlBuilder urlBuilder;
    }
}

