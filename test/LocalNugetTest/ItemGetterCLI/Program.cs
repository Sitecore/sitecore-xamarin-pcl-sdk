using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

namespace ItemGetterCLI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;


    class Program
    {
        static void Main(string[] args)
        {
            var endpoint = new SessionConfig("http://mobiledev1ua1.dk.sitecore.net:722", "admin", "b", "/sitecore/shell");
            var defaultSource = ItemSource.DefaultSource();
            var session = new ScApiSession(endpoint, defaultSource);

            var request =
                ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/home")
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
