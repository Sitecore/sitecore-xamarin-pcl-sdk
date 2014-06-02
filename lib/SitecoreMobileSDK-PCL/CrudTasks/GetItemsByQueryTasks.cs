﻿using System;
using Sitecore.MobileSDK.UrlBuilder.ItemByPath;
using System.Net.Http;
using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;

namespace Sitecore.MobileSDK.CrudTasks
{
    public class GetItemsByQueryTasks : AbstractGetItemTask<ReadItemByQueryParameters>
    {
        private GetItemsByQueryTasks ()
        {
        }

        public GetItemsByQueryTasks(ItemByQueryUrlBuilder urlBuilder, HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.urlBuilder = urlBuilder;

            this.Validate ();
        }

        protected override string UrlToGetItemWithRequest (ReadItemByQueryParameters request)
        {
            this.ValidateRequest (request);

            string result = this.urlBuilder.GetUrlForRequest (request);
            return result;
        }

        private void ValidateRequest(ReadItemByQueryParameters request)
        {
            if (null == request)
            {
                throw new ArgumentNullException ("[GetItemsByQueryTasks] request cannot be null");
            }
        }

        private void Validate()
        {
            if (null == this.httpClient)
            {
                throw new ArgumentNullException ("GetItemsByQueryTasks.httpClient cannot be null");
            }
            else if (null == this.urlBuilder)
            {
                throw new ArgumentNullException ("GetItemsByQueryTasks.urlBuilder cannot be null");
            }
        }

        private readonly ItemByQueryUrlBuilder urlBuilder;
    }
}

