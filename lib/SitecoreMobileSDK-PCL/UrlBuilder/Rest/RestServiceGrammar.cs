using System;

namespace Sitecore.MobileSDK.UrlBuilder.Rest
{
    public class RestServiceGrammar : IRestServiceGrammar
    {
        public static RestServiceGrammar ItemWebApiV2Grammar()
        {
            RestServiceGrammar result = new RestServiceGrammar ();
            result.KeyValuePairSeparator = "=";
            result.FieldSeparator        = "&";
            result.HostAndArgsSeparator  = "?";
            result.PathComponentSeparator = "/";

            return result;
        }
            
        private RestServiceGrammar ()
        {
        }

        public string KeyValuePairSeparator { get; private set; }
        public string FieldSeparator        { get; private set; }
        public string HostAndArgsSeparator  { get; private set; }
        public string PathComponentSeparator{ get; private set; }
    }
}

