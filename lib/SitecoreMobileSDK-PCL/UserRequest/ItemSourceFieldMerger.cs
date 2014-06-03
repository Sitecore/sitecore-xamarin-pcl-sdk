
namespace Sitecore.MobileSDK
{
    using System;
    using Sitecore.MobileSDK.Items;

    public class ItemSourceFieldMerger
    {
        public ItemSourceFieldMerger (IItemSource defaultSource)
        {
            this.defaultSource = defaultSource;

            this.Validate ();
        }

        public IItemSource FillItemSourceGaps(IItemSource userSource)
        {
            if (null == userSource)
            {
                return this.defaultSource;
            }

            string database = (null != userSource.Database) ? userSource.Database : this.defaultSource.Database;
            string language = (null != userSource.Language) ? userSource.Language : this.defaultSource.Language;
            string version  = (null != userSource.Version ) ? userSource.Version  : this.defaultSource.Version ;


            return new ItemSource (database, language, version);
        }

        private void Validate()
        {
            if (null == this.defaultSource)
            {
                throw new ArgumentNullException ("ItemSourceFieldMerger.defaultSource cannot be null");
            }
            else if (null == this.defaultSource.Database)
            {
                throw new ArgumentNullException ("ItemSourceFieldMerger.defaultSource.Database cannot be null");
            }
            else if (null == this.defaultSource.Language)
            {
                throw new ArgumentNullException ("ItemSourceFieldMerger.defaultSource.Language cannot be null");
            }
        }

        private readonly IItemSource defaultSource;
    }
}

