using Sitecore.MobileSDK.PublicKey;

namespace Sitecore.MobileSDK.CrudTasks
{
    using System.Net.Http;

    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.API.Request;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.TaskFlow;
    using Sitecore.MobileSDK.UrlBuilder;
    using Sitecore.MobileSDK.UrlBuilder.ItemById;


    public class GetItemsByIdTasks : AbstractGetItemTask<IReadItemsByIdRequest>
    {
        public GetItemsByIdTasks(ItemByIdUrlBuilder urlBuilder, HttpClient httpClient, ICredentialsHeadersCryptor credentialsHeadersCryptor) 
            : base(httpClient, credentialsHeadersCryptor)
        {
            this.urlBuilder = urlBuilder;
        }

        protected override string UrlToGetItemWithRequest(IReadItemsByIdRequest request)
        {
            return this.urlBuilder.GetUrlForRequest(request);
        }
            
        private readonly ItemByIdUrlBuilder urlBuilder;
    }
}

