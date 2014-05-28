using System;

namespace Sitecore.MobileSDK.UrlBuilder
{
    public class RestServiceGrammar : IRestServiceGrammar
    {
        public static RestServiceGrammar ItemWebApiV2Grammar()
        {
            RestServiceGrammar result = new RestServiceGrammar ();
            result.KeyValuePairSeparator = "=";
            result.FieldSeparator        = "&";
            result.HostAndArgsSeparator  = "?";

            return result;
        }
            
        private RestServiceGrammar ()
        {
        }

        public string KeyValuePairSeparator { get; private set; }
        public string FieldSeparator        { get; private set; }
        public string HostAndArgsSeparator  { get; private set; }
    }
}

