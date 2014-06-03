using System;

namespace Sitecore.MobileSDK.Items
{
    public class ItemSourcePOD : IItemSource
    {
        public static IItemSource DefaultSource()
        {
            return new ItemSourcePOD ("web", "en", null);
        }

        public ItemSourcePOD(string database, string language, string version = null)
        {
            this.Database = database;
            this.Language = language;
            this.Version  = version ;
        }

        public override bool Equals (object obj)
        {
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            ItemSourcePOD other = (ItemSourcePOD)obj;
            if (null == other)
            {
                return false;
            }

            bool isDbEqual =  object.Equals ( this.Database, other.Database );
            bool isLangEqual = object.Equals (this.Language, other.Language);
            bool isVersionEqual = object.Equals (this.Version, other.Version);

            return isDbEqual && isLangEqual && isVersionEqual;
        } 

        public override int GetHashCode ()
        {
            return base.GetHashCode() + this.Database.GetHashCode () + this.Language.GetHashCode () + this.Version.GetHashCode ();
        }

        public string Database  { get; private set; }
        public string Language  { get; private set; }
        public string Version   { get; private set; }
    }
}

