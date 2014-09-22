namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;
  using SecureStringPasswordProvider.API;


  [TestFixture]
  public class PortablePasswordStorageTest
  {
    [Test]
    public void TestPasswordStorageReturnsSameValues()
    {
      const string login = "admin";
      const string password = "bimba";

      using (var passwordStorage = new SecureStringPasswordProvider(login, password))
      {
        Assert.AreEqual(login, passwordStorage.Username);
        Assert.AreEqual(password, passwordStorage.Password);
      }
    }


    [Test]
    public void TestPasswordStorageCopyReturnsSameValues()
    {
      const string login = "duncan";
      const string password = "There can be only one";

      using (var passwordStorage = new SecureStringPasswordProvider(login, password))
      {
        Assert.AreEqual(login, passwordStorage.Username);
        Assert.AreEqual(password, passwordStorage.Password);

        using (var passwordStorageCopy = passwordStorage.CredentialsShallowCopy())
        {
          Assert.AreNotSame(passwordStorage, passwordStorageCopy);

          Assert.AreEqual(login, passwordStorageCopy.Username);
          Assert.AreEqual(password, passwordStorageCopy.Password);
        }
      }
    }

    [Test]
    public void TestPasswordStorageRejectsNullLogin()
    {
      const string login = null;
      const string password = "bimba";

      Assert.Throws<ArgumentException>( 
        () => new SecureStringPasswordProvider(login, password), 
        "Exception for empty login expected");
    }

    [Test]
    public void TestPasswordStorageRejectsEmptyLogin()
    {
      const string login = "";
      const string password = "bimba";

      Assert.Throws<ArgumentException>( 
        () => new SecureStringPasswordProvider(login, password), 
        "Exception for empty login expected");
    }

    [Test]
    public void TestPasswordStorageAllowsWhitespaceLogin()
    {
      const string login = "     ";
      const string password = "bimba";

      using (var passwordStorage = new SecureStringPasswordProvider(login, password))
      {
        Assert.AreEqual(login, passwordStorage.Username);
        Assert.AreEqual(password, passwordStorage.Password);
      }
    }


    [Test]
    public void TestPasswordStorageAllowsEmptyPassword()
    {
      const string login = "ashot";
      const string password = "";

      using (var passwordStorage = new SecureStringPasswordProvider(login, password))
      {
        Assert.AreEqual(login, passwordStorage.Username);
        Assert.AreEqual(password, passwordStorage.Password);
      }
    }


    [Test]
    public void TestPasswordStorageAllowsNullPassword()
    {
      const string login = "arnold";
      const string password = null;

      using (var passwordStorage = new SecureStringPasswordProvider(login, password))
      {
        Assert.AreEqual(login, passwordStorage.Username);
        Assert.AreEqual(password, passwordStorage.Password);
      }
    }

  

    [Test]
    public void TestPasswordStorageAllowsWhitespacePassword()
    {
      const string login = "whitespace";
      const string password = "   ";

      using (var passwordStorage = new SecureStringPasswordProvider(login, password))
      {
        Assert.AreEqual(login, passwordStorage.Username);
        Assert.AreEqual(password, passwordStorage.Password);
      }
    }
  
  }
}

