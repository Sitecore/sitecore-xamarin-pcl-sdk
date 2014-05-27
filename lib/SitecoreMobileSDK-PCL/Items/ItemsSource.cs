using System;

namespace Sitecore.MobileSDK.Items
{
    public class ItemSource
    {
        public static ItemSource DefaultSource()
        {
            return new ItemSource ("web", "en", null);
        }

        public ItemSource ( string database, string language, string version = null )
        {
            this.Database = database;
            this.Language = language;
            this.Version  = version ;

            this.Validate ();
        }

        private void Validate()
        {
            if (null == this.Database)
            {
                throw new ArgumentNullException ("[ItemSource.Database] Do not pass null to constructor");
            }
            else if (null == this.Language)
            {
                throw new ArgumentNullException ("[ItemSource.Language] Do not pass null to constructor");
            }
        }

        public string Database  { get; private set; }
        public string Language  { get; private set; }
        public string Version   { get; private set; }

        public int VersionNumber 
        { 
            get 
            { 
                if (null == this.Version)
                {
                    throw new ArgumentNullException ("[ItemSource.VersionNumber] : Cannot convert nil version to int");
                }

                return Convert.ToInt32 (this.Version); 
            } 
        }
    } 
}

