using Sitecore.MobileSDK.PublicKey;

namespace Sitecore.MobileSDK.CrudTasks
{
    using System.Net.Http;
    using Sitecore.MobileSDK.API.Request;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.TaskFlow;
    using Sitecore.MobileSDK.UrlBuilder;
    using Sitecore.MobileSDK.UrlBuilder.ItemByPath;


    public class GetItemsByPathTasks : AbstractGetItemTask<IReadItemsByPathRequest>
    {
        public GetItemsByPathTasks(ItemByPathUrlBuilder urlBuilder, HttpClient httpClient, ICredentialsHeadersCryptor credentialsHeadersCryptor) 
            : base(httpClient, credentialsHeadersCryptor)
        {
            this.urlBuilder = urlBuilder;
        }
        protected override string UrlToGetItemWithRequest(IReadItemsByPathRequest request)
        {
            return this.urlBuilder.GetUrlForRequest(request);
        }
            
        private readonly ItemByPathUrlBuilder urlBuilder;
    }
}
