using Sitecore.MobileSDK.UrlBuilder.ItemById;
using Sitecore.MobileSDK.UrlBuilder.ItemByPath;
using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;

namespace Sitecore.MobileSDK
{
    using System;

    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;


    public class UserRequestMerger
    {
        public UserRequestMerger (ISessionConfig defaultSessionConfig, IItemSource defaultSource)
        {
            this.itemSourceMerger = new ItemSourceFieldMerger (defaultSource);
            this.sessionConfigMerger = new SessionConfigMerger (defaultSessionConfig);
        }

        public IReadItemsByIdRequest FillReadItemByIdGaps(IReadItemsByIdRequest userRequest)
        {
            IItemSource mergedSource = this.itemSourceMerger.FillItemSourceGaps (userRequest.ItemSource);
            ISessionConfig mergedSessionConfig = this.sessionConfigMerger.FillSessionConfigGaps (userRequest.SessionSettings);

            return new ReadItemsByIdParameters (mergedSessionConfig, mergedSource, userRequest.ItemId);
        }

        public IReadItemsByPathRequest FillReadItemByPathGaps(IReadItemsByPathRequest userRequest)
        {
            IItemSource mergedSource = this.itemSourceMerger.FillItemSourceGaps (userRequest.ItemSource);
            ISessionConfig mergedSessionConfig = this.sessionConfigMerger.FillSessionConfigGaps (userRequest.SessionSettings);

            return new ReadItemByPathParameters (mergedSessionConfig, mergedSource, userRequest.ItemPath);
        }

        public IReadItemsByQueryRequest FillReadItemByQueryGaps(IReadItemsByQueryRequest userRequest)
        {
            IItemSource mergedSource = this.itemSourceMerger.FillItemSourceGaps (userRequest.ItemSource);
            ISessionConfig mergedSessionConfig = this.sessionConfigMerger.FillSessionConfigGaps (userRequest.SessionSettings);

            return new ReadItemByQueryParameters (mergedSessionConfig, mergedSource, userRequest.SitecoreQuery);
        }

        private ItemSourceFieldMerger itemSourceMerger;
        private SessionConfigMerger sessionConfigMerger;
    }
}

