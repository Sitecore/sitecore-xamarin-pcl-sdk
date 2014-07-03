namespace MobileSDKIntegrationTest
{
  using NUnit.Framework;
  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;

  public class TestEnvironment
  {
    public static TestEnvironment DefaultTestEnvironment()
    {
      var result = new TestEnvironment
      {
        InstanceUrl = "http://mobiledev1ua1.dk.sitecore.net:7220",
        ShellSite = "/sitecore/shell"
      };

      result.Users.Admin.Username = "sitecore\\admin";
      result.Users.Admin.Password = "b";

      result.Users.Anonymous.Username = null;
      result.Users.Anonymous.Password = null;

      result.Users.Creatorex.Username = "extranet\\creatorex";
      result.Users.Creatorex.Password = "creatorex";

      result.Users.NoReadAccess.Username = "extranet\\noreadaccess";
      result.Users.NoReadAccess.Password = "noreadaccess";


      result.Items.Home.Id = "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}";
      result.Items.Home.Path = "/sitecore/content/Home";
      result.Items.Home.DisplayName = "Home";
      result.Items.Home.Template = "Sample/Sample Item";

      result.Items.ItemWithVersions.Id = "{B86C2CBB-7808-4798-A461-1FB3EB0A43E5}";
      result.Items.ItemWithVersions.Path = "/sitecore/content/FieldsTest/TestFieldsVersionsAndDB";
      result.Items.ItemWithVersions.DisplayName = "TestFieldsVersionsAndDB";

      result.Items.TestFieldsItem.Id = "{00CB2AC4-70DB-482C-85B4-B1F3A4CFE643}";
      result.Items.TestFieldsItem.Path = "/sitecore/content/Home/Test Fields";
      result.Items.TestFieldsItem.DisplayName = "Test Fields";
      result.Items.TestFieldsItem.Template = "Test Templates/Sample fields";

      return result;
    }

    private TestEnvironment() { }
    public string InstanceUrl { get; private set; }
    public string ShellSite { get; private set; }

    public UsersList Users = new UsersList();
    public ItemsList Items = new ItemsList();

    public class UsersList
    {
      public User Admin = new User();
      public User Anonymous = new User();
      public User Creatorex = new User();
      public User NoReadAccess = new User();
    }

    public class ItemsList
    {
      public Item Home = new Item();
      public Item ItemWithVersions = new Item();
      public Item TestFieldsItem = new Item();
      public Item MediaLibrary = new Item();
    }

    public class Item
    {
      public string Id { get; set; }
      public string Path { get; set; }
      public string DisplayName { get; set; }
      public string Template { get; set; }
    }

    public class User
    {
      public string Username { get; set; }
      public string Password { get; set; }
    }

    public void AssertItemsAreEqual(TestEnvironment.Item expected, ISitecoreItem actual)
    {
      if (null != expected.DisplayName)
      {
        Assert.AreEqual(expected.DisplayName, actual.DisplayName);
      }
      if (null != expected.Id)
      {
        Assert.AreEqual(expected.Id, actual.Id);
      }
      if (null != expected.Path)
      {
        Assert.AreEqual(expected.Path, actual.Path);
      }
      if (null != expected.Template)
      {
        Assert.AreEqual(expected.Template, actual.Template);
      }
    }

    public void AssertItemSourcesAreEqual(IItemSource expected, IItemSource actual)
    {
        Assert.AreEqual(expected.Database, actual.Database);
        Assert.AreEqual(expected.Language, actual.Language);
        Assert.AreEqual(expected.Version, actual.Version);
    }

    public void AssertItemsCount(int itemCount, ScItemsResponse response)
    {
      Assert.AreEqual(itemCount, response.TotalCount);
      Assert.AreEqual(itemCount, response.ResultCount);
      Assert.AreEqual(itemCount, response.Items.Count);
    }

    public ScApiSession GetSession(string url, string username, string password, ItemSource itemSource = null, string site = null)
    {
      var config = new SessionConfig(url, username, password, site);
      var session = new ScApiSession(config, itemSource);
      return session;
    }
  }
}

