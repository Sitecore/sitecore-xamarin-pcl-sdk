namespace MobileSDKTestAndroid
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using NUnit.Framework;
    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.PublicKey;

    [TestFixture]
    public class PublicKeyParsingTest
    {
        private PublicKeyXmlParser parser;

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
            TestDelegate action = () => this.parser.Parse(null);
            Assert.Throws<ArgumentNullException>(action, "cannot parse null xml string");
        }

        [Test]
        public void TestXmlParserThrowsOnInvalidXml()
        {
            TestDelegate action = () =>
            {
                string invalidXml = "<RSAKeyValue><Modus>0jvvnZgV2r8hlQ6rPIFcoQxJntKBnu3dsmPVzv+diFpkEHrQxX1XRz3KK2f4EBqXASEXFQrluJda7c0d82p76HFjcORGqF5/iTvnlEXotzgy+dAa4BGYa//LNp4DFOipfdvGQlN7lZJyRZqaXGVryueyBHK6MiT6KPcoDmZNZN8=</Modulus><Exponent>AQAB<xponensdadt></RSAKeyValue>";
                Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(invalidXml));

                this.parser.Parse(xmlStream);
            };
            Assert.Throws<XmlException>(action, "cannot parse invalid xml");
        }

        [Test]
        public void TestXmlParserFetchesValidDataCorrectly()
        {
            string validXml = "<RSAKeyValue><Modulus>0jvvnZgV2r8hlQ6rPIFcoQxJntKBnu3dsmPVzv+diFpkEHrQxX1XRz3KK2f4EBqXASEXFQrluJda7c0d82p76HFjcORGqF5/iTvnlEXotzgy+dAa4BGYa//LNp4DFOipfdvGQlN7lZJyRZqaXGVryueyBHK6MiT6KPcoDmZNZN8=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(validXml));

            PublicKeyX509Certificate result = this.parser.Parse(xmlStream);

            Assert.AreEqual(result.ModulusInBase64, "0jvvnZgV2r8hlQ6rPIFcoQxJntKBnu3dsmPVzv+diFpkEHrQxX1XRz3KK2f4EBqXASEXFQrluJda7c0d82p76HFjcORGqF5/iTvnlEXotzgy+dAa4BGYa//LNp4DFOipfdvGQlN7lZJyRZqaXGVryueyBHK6MiT6KPcoDmZNZN8=");
            Assert.AreEqual(result.ExponentInBase64, "AQAB");
        }


        [Test]
        public void TestXmlParserDoesNotDependOnNodeOrder()
        {
            string validXml = "<RSAKeyValue><Exponent>AQAB</Exponent><Modulus>0jvvnZgV2r8hlQ6rPIFcoQxJntKBnu3dsmPVzv+diFpkEHrQxX1XRz3KK2f4EBqXASEXFQrluJda7c0d82p76HFjcORGqF5/iTvnlEXotzgy+dAa4BGYa//LNp4DFOipfdvGQlN7lZJyRZqaXGVryueyBHK6MiT6KPcoDmZNZN8=</Modulus></RSAKeyValue>";
            Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(validXml));

            PublicKeyX509Certificate result = this.parser.Parse(xmlStream);

            Assert.AreEqual(result.ModulusInBase64, "0jvvnZgV2r8hlQ6rPIFcoQxJntKBnu3dsmPVzv+diFpkEHrQxX1XRz3KK2f4EBqXASEXFQrluJda7c0d82p76HFjcORGqF5/iTvnlEXotzgy+dAa4BGYa//LNp4DFOipfdvGQlN7lZJyRZqaXGVryueyBHK6MiT6KPcoDmZNZN8=");
            Assert.AreEqual(result.ExponentInBase64, "AQAB");
        }
    }
}
