namespace MobileSDKTestAndroid
{
    using System;
    using System.Text;
    using NUnit.Framework;
    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Crypto.Parameters;
    using Org.BouncyCastle.Math;
    using Org.BouncyCastle.Security;
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
            string modulus = "v4oNDJlxRJB7svUE3U3dTuJeqr/qtwyccCAQZkbKICOVs8ECuh/CTKks3YyGPbcUdriMqmwjbIP5KSxI22/2QfvaDRSUoXQLzq+pr4MNKyL9ZPI3g2fcByCQPa45LMUfBgbqA6uggET/Tj6SI87l0mgPL6wZwoAFHOrVfGHLSRcuKHZTrfXSoiB7l24RgeArXTGuDbQ3b5eLPXowv5kCsdDvzg//nlED4kQ/HphrSd5XCYBXXTbZrTtwxRWpi/Y0MucX1xn34TLb6VFhZ2AKqlsYKG2Wxjlt/4bxMKAPsz31q4Cv1xulkP6jX9sivkNUKFDncyMmxuXx4/rIlL7zxw==";
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

            Assert.AreEqual(true, publicModulusInteger.Equals(privateModulusInteger));
            Assert.AreEqual(true, publicExponentInteger.Equals(privateExponentInteger));
        }

        [Test]
        public void TestValidEncryptDecrypt()
        {
            var login = "extranet\\creatorex";

            EncryptionUtil encryptor = new EncryptionUtil(this.publicCertificate);
            string encryptedLogin = encryptor.Encrypt(login);

            Assert.AreEqual(login, this.Decrypt(encryptedLogin));
        }

        private string Decrypt(string str)
        {
            var cipher = CipherUtilities.GetCipher("RSA/None/PKCS1Padding");

            ICipherParameters rsaOptions = this.GetPrivateKeyParams();
            cipher.Init(false, rsaOptions);

            byte[] decryptedData = Convert.FromBase64String(str);

            int size = cipher.GetOutputSize(decryptedData.Length);
            byte[] result = new byte[size];
            int olen = cipher.ProcessBytes(decryptedData, 0, decryptedData.Length, result, 0);
            olen += cipher.DoFinal(result, olen);

            if (olen < size)
            {
                byte[] tmp = new byte[olen];
                Array.Copy(result, 0, tmp, 0, olen);
                result = tmp;
            }

            return Encoding.UTF8.GetString(result);
        }

        private RsaPrivateCrtKeyParameters GetPrivateKeyParams()
        {
            BigInteger modulus = EncryptionUtil.Base64StringToBigInteger("v4oNDJlxRJB7svUE3U3dTuJeqr/qtwyccCAQZkbKICOVs8ECuh/CTKks3YyGPbcUdriMqmwjbIP5KSxI22/2QfvaDRSUoXQLzq+pr4MNKyL9ZPI3g2fcByCQPa45LMUfBgbqA6uggET/Tj6SI87l0mgPL6wZwoAFHOrVfGHLSRcuKHZTrfXSoiB7l24RgeArXTGuDbQ3b5eLPXowv5kCsdDvzg//nlED4kQ/HphrSd5XCYBXXTbZrTtwxRWpi/Y0MucX1xn34TLb6VFhZ2AKqlsYKG2Wxjlt/4bxMKAPsz31q4Cv1xulkP6jX9sivkNUKFDncyMmxuXx4/rIlL7zxw==");
            BigInteger publicExponent = EncryptionUtil.Base64StringToBigInteger("AQAB");
            BigInteger privateExponent = EncryptionUtil.Base64StringToBigInteger("AQAB");
            BigInteger p = EncryptionUtil.Base64StringToBigInteger("6L8PQWR/m2u1/eCKqDjudd1NeijO9PwMWWatdus4zEXwj0cjBNxHmTHsZ8/DtVwdAMxewxTfswBP4fAkEyQKpmW8ylTy+8jvDfGDICgAsyyvPeQwzQfQLaXgl+AljCeEb4rRcmytEDcpc/BIsmUTjDrcYEHhoyU8L8zNUkEr6gc=");
            BigInteger q = EncryptionUtil.Base64StringToBigInteger("0q0KXJhlaHZGWGzjkX4eECJaguuctXy0zued7OlrNelU4PnU2BHMsL19lp0zdMv2W7yp0LHECN209SyK1rI9l3536jS3v4EmjS+BB4g9YMpbzq+fmQzJDFqtZ2xWXABU20h6JM0p42TEwwlQE2POX6PixWbnIAMXfggT1AmhOEE=");
            BigInteger dP = EncryptionUtil.Base64StringToBigInteger("v54tLSVUedzf5Lis73qPuLIOS3i1irTIZgJG5hUamfMq7osepa3FtLaJb555/iJsLnATxIC2+2RSYC2ZRbjym7Q/DkWKFwmC5vPjhOHqUZmEXWw1UVgRaMOceO2tfAo82qhrb81RXnxLwIwfDeBxi44aSZVz8yGAv8nZcNL/GJk=");
            BigInteger dQ = EncryptionUtil.Base64StringToBigInteger("i8PMuf1IWXMN3B/xIVa/7wg7b9uLjUN8WD+En+WDALMZYl+b+vRkDWTI6qDDwFqHx/hz2EX2vcMICBdSzHhXMCfwuenbdSrjPosjWLHjtlDJc2dDxC+dOZr2q0ROTp7RrOB6V+vcPEVf29xTyWlPQlfhXACWmMA0V6JYNTVmRYE=");
            BigInteger qInv = EncryptionUtil.Base64StringToBigInteger("iaavrj9Noieus3jm8IwXJa5rE3hpo2UBdrDOOo7rOyFAQz5gBgfuICry4BGZxYr4XIEYmNNNlbVXXQ6D+fULDTpKS/qm7eW/MOW9zr8UMnMuVg9i/38/DiNFox8cm8B/DK/AKIZYB/F9WYgltleBUnSfP898Aw+/ouuMOp0A65Q=");

            return new RsaPrivateCrtKeyParameters(modulus, publicExponent, privateExponent, p, q, dP, dQ, qInv);
        }
    }
}
