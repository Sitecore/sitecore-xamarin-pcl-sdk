
namespace MobileSDKUnitTest.Mock
{
    public class TestEnvironment
    {
        public static TestEnvironment DefaultTestEnvironment()
        {
            var result = new TestEnvironment
            {
                AnonymousInstanceUrl = "http://mobiledev1ua1.dk.sitecore.net:750",
                AuthenticatedInstanceUrl = "http://mobiledev1ua1.dk.sitecore.net:7119"
            };

            result.Users.Admin.Username = "sitecore\\admin";
            result.Users.Admin.Password = "b";
            result.Users.Anonymous.Username = null;
            result.Users.Anonymous.Password = null;

            result.Items.Home.Id = "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}";
            result.Items.Home.Path = "/sitecore/content/Home";
            result.Items.Home.Name = "Home";
            result.Items.Home.Template = "Sample/Sample Item";

            return result;
        }

        private TestEnvironment () { }
        public string AnonymousInstanceUrl { get; private set; }
        public string AuthenticatedInstanceUrl { get; private set; }

        public UsersList Users = new UsersList();
        public ItemsList Items = new ItemsList();

        public class UsersList
        {
            public User Admin = new User();
            public User Anonymous = new User();
        }        
        
        public class ItemsList
        {
            public Item Home = new Item();
            public Item MediaLibrary = new Item();
        }

        public class Item
        {
            public string Id { get; set; }
            public string Path { get; set; }
            public string Name { get; set; }
            public string Template { get; set;}
        }

        public class User
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        
    }
}

