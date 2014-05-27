using System;

namespace Sitecore.MobileSDK.UrlBuilder
{
    public class ItemSourceUrlBuilder
    {
        private ItemSourceUrlBuilder ()
        {
        }

        public  ItemSourceUrlBuilder (ItemSource itemSource)
        {
            this.itemSource = itemSource;
            this.Validate ();
        }

        private void Validate()
        {
            if (null == this.itemSource)
            {
                throw new ArgumentNullException ("[ItemSourceUrlBuilder.itemSource] Do not pass null");
            }
        }

        private ItemSource itemSource;
    }
}

