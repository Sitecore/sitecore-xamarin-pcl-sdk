using System;

namespace Sitecore.MobileSDK
{
    public class ItemSource
    {
        public static ItemSource DefaultSource()
        {
            return new ItemSource ("web", "en", null, null);
        }

        public ItemSource ( string database, string language, string site = null, string version = null )
        {
            this.Database = database;
            this.Language = language;
            this.Site     = site    ;
            this.Version  = version ;
        }

        public string Database  { get; private set; }
        public string Language  { get; private set; }
        public string Site      { get; private set; }
        public string Version   { get; private set; }
    }     
}         

