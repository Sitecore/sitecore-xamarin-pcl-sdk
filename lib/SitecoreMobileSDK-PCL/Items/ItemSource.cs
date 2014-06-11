using System;

namespace Sitecore.MobileSDK.Items
{
    public class ItemSource : ItemSourcePOD
    {
        public const string DefaultDatabase = "web";
        public const string DefaultLanguage = "en";

        public static ItemSource DefaultSource()
        {
			return new ItemSource (ItemSource.DefaultDatabase, ItemSource.DefaultLanguage, null);
        }

		public ItemSource(string database, string language, string version = null) 
            : base(database, language, version)
        {
            this.Validate ();
        }

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


        /// <exception cref="ArgumentNullException">[ItemSource.Database] Do not pass null to constructor</exception>
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
    } 
}

