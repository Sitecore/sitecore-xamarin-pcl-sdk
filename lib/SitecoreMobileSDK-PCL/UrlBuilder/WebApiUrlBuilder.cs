
namespace Sitecore.MobileSDK.UrlBuilder
{
    using System;

    public class WebApiUrlBuilder
    {
        public WebApiUrlBuilder()
        {
        }

        public string GetUrlForRequest(IRequestConfig request)
        {
            this.ValidateRequest(request);

            string escapedId = Uri.EscapeDataString(request.ItemId);

            if (!request.InstanceUrl.StartsWith("https://") && !request.InstanceUrl.StartsWith("http://"))
            {
                request.InstanceUrl = request.InstanceUrl.Insert(0, "http://");
            }

            string result = request.InstanceUrl + "/-/item/" + request.WebApiVersion + "?sc_itemid=" + escapedId;
            return result.ToLower();
        }

        private void ValidateRequest(IRequestConfig request)
        {
            if (null == request)
            {
                throw new ArgumentNullException("WebApiUrlBuilder.GetUrlForRequest() : do not pass null");
            }
        }
    }
}
