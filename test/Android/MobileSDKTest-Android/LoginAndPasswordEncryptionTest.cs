namespace MobileSDKTestAndroid
{
    using NUnit.Framework;
    using Org.BouncyCastle.Math;
    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.PublicKey;

    [TestFixture]
    public class LoginAndPasswordEncryptionTest
    {
        private PublicKeyX509Certificate publicCertificate;
        private PublicKeyX509Certificate privateCertificate;

        [SetUp]
        public void Setup()
        {
            string modulus = "0jvvnZgV2r8hlQ6rPIFcoQxJntKBnu3dsmPVzv+diFpkEHrQxX1XRz3KK2f4EBqXASEXFQrluJda7c0d82p76HFjcORGqF5/iTvnlEXotzgy+dAa4BGYa//LNp4DFOipfdvGQlN7lZJyRZqaXGVryueyBHK6MiT6KPcoDmZNZN8=";
            string exponent = "AQAB";

            this.publicCertificate = new PublicKeyX509Certificate(modulus, exponent);

            modulus = "v4oNDJlxRJB7svUE3U3dTuJeqr/qtwyccCAQZkbKICOVs8ECuh/CTKks3YyGPbcUdriMqmwjbIP5KSxI22/2QfvaDRSUoXQLzq+pr4MNKyL9ZPI3g2fcByCQPa45LMUfBgbqA6uggET/Tj6SI87l0mgPL6wZwoAFHOrVfGHLSRcuKHZTrfXSoiB7l24RgeArXTGuDbQ3b5eLPXowv5kCsdDvzg//nlED4kQ/HphrSd5XCYBXXTbZrTtwxRWpi/Y0MucX1xn34TLb6VFhZ2AKqlsYKG2Wxjlt/4bxMKAPsz31q4Cv1xulkP6jX9sivkNUKFDncyMmxuXx4/rIlL7zxw==";
            exponent = "AQAB";
            this.privateCertificate = new PublicKeyX509Certificate(modulus, exponent);
        }

        [TearDown]
        public void TearDown()
        {
            this.publicCertificate = null;
            this.privateCertificate = null;
        }

        [Test]
        public void TestCertificatesBigIntegers()
        {
            EncryptionUtil publicEcnryptor = new EncryptionUtil(this.publicCertificate);

            BigInteger publicModulusInteger = publicEcnryptor.BigIntegerForModulus();
            BigInteger publicExponentInteger = publicEcnryptor.BigIntegerForExponent();

            EncryptionUtil privateEcnryptor = new EncryptionUtil(this.privateCertificate);

            BigInteger privateModulusInteger = privateEcnryptor.BigIntegerForModulus();
            BigInteger privateExponentInteger = privateEcnryptor.BigIntegerForExponent();

            Assert.AreEqual(publicModulusInteger.Equals(privateModulusInteger), true);
            Assert.AreEqual(publicExponentInteger.Equals(privateExponentInteger), true);
        }
    }
}
