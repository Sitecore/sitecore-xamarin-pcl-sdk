namespace Sitecore.MobileSDK.UrlBuilder.QueryParameters
{
    public class QueryParameters : IQueryParameters
    {
        public QueryParameters(PayloadType payload)
        {
            this.Payload = payload;
        }

        public PayloadType Payload { get; set; }
    }
}
