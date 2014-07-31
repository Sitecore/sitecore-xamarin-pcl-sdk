namespace ItemGetterCLI
{
    using System;
    using System.Threading.Tasks;

    using Sitecore.MobileSDK.API;
    using Sitecore.MobileSDK.API.Items;
    using Sitecore.MobileSDK.API.Request.Parameters;


    class Program
    {
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
    }
}
