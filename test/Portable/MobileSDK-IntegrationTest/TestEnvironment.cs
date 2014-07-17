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

      result.Items.AllowedItem.Id = "{387B69B2-B2EA-4618-8C3E-2785DC0469A7}";
      result.Items.AllowedItem.Path = "/sitecore/content/Home/Allowed_Parent/Allowed_Item";
      result.Items.AllowedItem.DisplayName = "Allowed_Item";

      result.Items.AllowedParent.Id = "{2075CBFF-C330-434D-9E1B-937782E0DE49}";
      result.Items.AllowedParent.Path = "/sitecore/content/Home/Allowed_Parent";
      result.Items.AllowedParent.DisplayName = "Allowed_Parent";

      result.Items.CreateItemsHere.Id = "{C50613DC-D792-467C-832F-F93BB121D775}";
      result.Items.CreateItemsHere.Path = "/sitecore/content/Home/Android/Folder for create items";
      result.Items.CreateItemsHere.DisplayName = "Folder for create items";

      return result;
    }

    private TestEnvironment() { }
    public string InstanceUrl { get; private set; }
    public string ShellSite { get; private set; }

    public UsersList Users = new UsersList();
    public ItemsList Items = new ItemsList();

    public class UsersList
    {
      public WebApiCredentialsPOD Admin = new WebApiCredentialsPOD("sitecore\\admin", "b");
      public WebApiCredentialsPOD Anonymous = new WebApiCredentialsPOD(null, null);
      public WebApiCredentialsPOD Creatorex = new WebApiCredentialsPOD("extranet\\creatorex", "creatorex");
      public WebApiCredentialsPOD SitecoreCreator = new WebApiCredentialsPOD("sitecore\\creator", "creator");
      public WebApiCredentialsPOD NoReadAccess = new WebApiCredentialsPOD("extranet\\noreadaccess", "noreadaccess");
      public WebApiCredentialsPOD FakeAnonymous = new WebApiCredentialsPOD("extranet\\FakeAnonymous", "b");
      public WebApiCredentialsPOD NotExistent = new WebApiCredentialsPOD("sitecore\\notexistent", "notexistent");
      public WebApiCredentialsPOD NoCreateAccess = new WebApiCredentialsPOD("sitecore\\nocreate", "nocreate");
    }

    public class ItemsList
    {
      public Item Home = new Item();
      public Item ItemWithVersions = new Item();
      public Item TestFieldsItem = new Item();
      public Item AllowedItem = new Item();
      public Item AllowedParent = new Item();
      public Item CreateItemsHere = new Item();
    }

    public class Item
    {
      public string Id { get; set; }
      public string Path { get; set; }
      public string DisplayName { get; set; }
      public string Template { get; set; }
    }

    public void AssertItemsAreEqual(Item expected, ISitecoreItem actual)
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
  }
}

