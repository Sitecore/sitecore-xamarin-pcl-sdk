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
            this.database = database;
            this.language = language;
            this.site     = site    ;
            this.version  = version ;
        }

        public string Database  { get { return this.database; } }
        public string Language  { get { return this.language; } }
        public string Site      { get { return this.site    ; } }
        public string Version   { get { return this.version ; } }


        private string database;
        private string language;
        private string site    ;
        private string version ;
    }     
}         

