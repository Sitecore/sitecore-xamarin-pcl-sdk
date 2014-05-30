

namespace Sitecore.MobileSDK.CrudTasks
{
    using System.Net.Http;

    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.TaskFlow;
    using Sitecore.MobileSDK.UrlBuilder;


    public class GetItemsByIdTasks : AbstractGetItemTask<GetItemsByIdParameters>
    {
        public GetItemsByIdTasks(ItemByIdUrlBuilder urlBuilder, HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.urlBuilder = urlBuilder;
        }

        protected override string UrlToGetItemWithRequest(GetItemsByIdParameters request)
        {
            return this.urlBuilder.GetUrlForRequest(request);
        }
            
        private readonly ItemByIdUrlBuilder urlBuilder;
    }
}

