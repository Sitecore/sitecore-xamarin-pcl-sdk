

namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using System.IO;
  using System.Text;
  using System.Xml;
  using System.Threading;
  using System.Threading.Tasks;

  using NUnit.Framework;
  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.PublicKey;

  [TestFixture]
  public class PublicKeyParsingTest
  {
    private PublicKeyXmlParser parser;

    const string VALID_PUBLIC_KEY_XML = "<RSAKeyValue><Modulus>0jvvnZgV2r8hlQ6rPIFcoQxJntKBnu3dsmPVzv+diFpkEHrQxX1XRz3KK2f4EBqXASEXFQrluJda7c0d82p76HFjcORGqF5/iTvnlEXotzgy+dAa4BGYa//LNp4DFOipfdvGQlN7lZJyRZqaXGVryueyBHK6MiT6KPcoDmZNZN8=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

    [SetUp]
    public void Setup()
    {
      this.parser = new PublicKeyXmlParser();
    }

    [TearDown]
    public void TearDown()
    {
      this.parser = null;
    }

    [Test]
    public void TestXmlParserThrowsOnNullInput()
    {
      TestDelegate action = () => this.parser.Parse(null, CancellationToken.None);
      Assert.Throws<ArgumentNullException>(action, "cannot parse null xml string");
    }

    [Test]
    public void TestXmlParserThrowsOnInvalidXml()
    {
      TestDelegate action = () =>
      {
        string invalidXml = "<RSAKeyValue><Modus>0jvvnZgV2r8hlQ6rPIFcoQxJntKBnu3dsmPVzv+diFpkEHrQxX1XRz3KK2f4EBqXASEXFQrluJda7c0d82p76HFjcORGqF5/iTvnlEXotzgy+dAa4BGYa//LNp4DFOipfdvGQlN7lZJyRZqaXGVryueyBHK6MiT6KPcoDmZNZN8=</Modulus><Exponent>AQAB<xponensdadt></RSAKeyValue>";
        using (var xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(invalidXml)) )
        {
          this.parser.Parse(xmlStream, CancellationToken.None);
        }
      };
      Assert.Throws<XmlException>(action, "cannot parse invalid xml");
    }

    [Test]
    public void TestXmlParserFetchesValidDataCorrectly()
    {
      string validXml = VALID_PUBLIC_KEY_XML;
      using (Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(validXml)))
      {
        PublicKeyX509Certificate result = this.parser.Parse(xmlStream, CancellationToken.None);

        Assert.AreEqual(result.ModulusInBase64, "0jvvnZgV2r8hlQ6rPIFcoQxJntKBnu3dsmPVzv+diFpkEHrQxX1XRz3KK2f4EBqXASEXFQrluJda7c0d82p76HFjcORGqF5/iTvnlEXotzgy+dAa4BGYa//LNp4DFOipfdvGQlN7lZJyRZqaXGVryueyBHK6MiT6KPcoDmZNZN8=");
        Assert.AreEqual(result.ExponentInBase64, "AQAB");
      }
    }


    [Test]
    public void TestXmlParserDoesNotDependOnNodeOrder()
    {
      string validXml = "<RSAKeyValue><Exponent>AQAB</Exponent><Modulus>0jvvnZgV2r8hlQ6rPIFcoQxJntKBnu3dsmPVzv+diFpkEHrQxX1XRz3KK2f4EBqXASEXFQrluJda7c0d82p76HFjcORGqF5/iTvnlEXotzgy+dAa4BGYa//LNp4DFOipfdvGQlN7lZJyRZqaXGVryueyBHK6MiT6KPcoDmZNZN8=</Modulus></RSAKeyValue>";
      using (Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(validXml)))
      {
        PublicKeyX509Certificate result = this.parser.Parse(xmlStream, CancellationToken.None);

        Assert.AreEqual(result.ModulusInBase64, "0jvvnZgV2r8hlQ6rPIFcoQxJntKBnu3dsmPVzv+diFpkEHrQxX1XRz3KK2f4EBqXASEXFQrluJda7c0d82p76HFjcORGqF5/iTvnlEXotzgy+dAa4BGYa//LNp4DFOipfdvGQlN7lZJyRZqaXGVryueyBHK6MiT6KPcoDmZNZN8=");
        Assert.AreEqual(result.ExponentInBase64, "AQAB");
      }
    }

    [Test]
    public void TestCancellationCausesOperationCanceledException()
    {
      TestDelegate testAction = async () =>
      {
        var cancel = new CancellationTokenSource ();
        using (Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(VALID_PUBLIC_KEY_XML)))
        {
          Task<PublicKeyX509Certificate> action = Task.Factory.StartNew( ()=> 
          {
            int millisecondTimeout = 10;
            Thread.Sleep(millisecondTimeout);    

            return this.parser.Parse(xmlStream, cancel.Token);
          });
          cancel.Cancel();

          await action;
        }
      };

      Assert.Catch<OperationCanceledException>(testAction);
    }
  }
}
