

namespace Sitecore.MobileSDK.Items
{
    using System;
    using System.Collections.Generic;
    using Sitecore.MobileSDK.Items.Fields;



    public interface ISitecoreItem
    {
         IItemSource Source { get; }

         string DisplayName { get; }

         bool HasChildren { get; }

         string Id { get; }

         string LongId { get; }

         string Path { get; }

         string Template { get; }

         IList<IField> Fields { get; }
    }
}

