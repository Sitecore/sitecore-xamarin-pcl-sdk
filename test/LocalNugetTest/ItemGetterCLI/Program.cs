namespace ItemGetterCLI
{
    using System;
    using System.Threading.Tasks;

    using Sitecore.MobileSDK.API;
    using Sitecore.MobileSDK.API.Items;
    using Sitecore.MobileSDK.API.Request.Parameters;
    using Sitecore.MobileSDK.API.MediaItem;
    using System.IO;


    class Program
    {
        #region Authenticated

        class InsecureDemoCredentials : IWebApiCredentials
        {
            public string Username
            {
                get
                {
                    return "admin";
                }
            }

            public string Password
            {
                get
                {
                    return "b";
                }
            }

            public IWebApiCredentials CredentialsShallowCopy()
            {
                return this;
            }

            public void Dispose()
            {
                //IDLE
            }
        }

        private static void Main(string[] args)
        {
            using (var demoCredentials = new InsecureDemoCredentials())
            {
                var session =
                    SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("http://mobiledev1ua1.dk.sitecore.net:7220")
                                                .Credentials(demoCredentials)
                                                .Site("/sitecore/shell")
                                                .DefaultDatabase("web")
                                                .DefaultLanguage("en")
                                                .MediaLibraryRoot("/sitecore/media library")
                                                .MediaPrefix("~/media/")
                                                .DefaultMediaResourceExtension("ashx")
                                                .BuildReadonlySession();

                var request =
                    ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/home")
                                            .AddScope(ScopeType.Self)
                                            .Payload(PayloadType.Content)
                                            .Build();
                var readHomeItemTask = session.ReadItemAsync(request);

                // @adk : cannot use "await" in main
                Task.WaitAll(readHomeItemTask);

                ScItemsResponse items = readHomeItemTask.Result;
                string fieldText = items.Items[0].FieldWithName("Text").RawValue;

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Clear();

                Console.WriteLine("Home Item Text");
                Console.WriteLine();
                Console.WriteLine(fieldText);

                Console.ReadKey();
            }
        }

        #endregion Authenticated

        #region Anonymous read
        private static void __Main(string[] args)
        {
            string instanceUrl = "http://mobiledev1ua1.dk.sitecore.net:7220";
            var session =
                SitecoreWebApiSessionBuilder.AnonymousSessionWithHost(instanceUrl)
                                            .Site("/sitecore/shell")
                                            .DefaultDatabase("web")
                                            .DefaultLanguage("en")
                                            .MediaLibraryRoot("/sitecore/media library")
                                            .MediaPrefix("~/media/")
                                            .DefaultMediaResourceExtension("ashx")
                                            .BuildReadonlySession();

            var request =
                ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/home")
                                        .AddScope(ScopeType.Self)
                                        .Payload(PayloadType.Content)
                                        .Build();
            var readHomeItemTask = session.ReadItemAsync(request);

            // @adk : cannot use "await" in main
            Task.WaitAll(readHomeItemTask);

            ScItemsResponse items = readHomeItemTask.Result;
            string fieldText = items.Items[0].FieldWithName("Text").RawValue;

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            Console.WriteLine("Home Item Text");
            Console.WriteLine();
            Console.WriteLine(fieldText);

            Console.ReadKey();
        }
        #endregion

        private static async void __Main_Media(string[] args)
        {
            string instanceUrl = "http://mobiledev1ua1.dk.sitecore.net:7220";
            var session =
                SitecoreWebApiSessionBuilder.AnonymousSessionWithHost(instanceUrl)
                                            .Site("/sitecore/shell")
                                            .DefaultDatabase("web")
                                            .DefaultLanguage("en")
                                            .MediaLibraryRoot("/sitecore/media library")
                                            .MediaPrefix("~/media/")
                                            .DefaultMediaResourceExtension("ashx")
                                            .BuildReadonlySession();

            var options = new MediaOptionsBuilder().Set
                                                   .MaxWidth(1920)
                                                   .MaxHeight(1080)
                                                   .Width(1024)
                                                   .Height(768)
                                                   .BackgroundColor("red")
                                                   .DisableMediaCache(false)
                                                   .AllowStrech(false)
                                                   .Scale(1.0f)
                                                   .DisplayAsThumbnail(false)
                                                   .Build();

            var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("/sitecore/media library/Images/butterfly2_large")
                                                     .Database("master")
                                                     .Language("fr")
                                                     .Version("1")
                                                     .DownloadOptions(options)
                                                     .Build();

             Stream response = await session.DownloadResourceAsync(request);
        }
    }
}
