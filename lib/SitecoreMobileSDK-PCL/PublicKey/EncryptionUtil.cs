namespace Sitecore.MobileSDK.PublicKey
{
    using System;
    using System.Text;
    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Crypto.Parameters;
    using Org.BouncyCastle.Math;
    using Org.BouncyCastle.Security;

    public class EncryptionUtil
    {
        private readonly PublicKeyX509Certificate certificate;

        /// <exception cref="ArgumentNullException">cert cannot be null</exception>
        public EncryptionUtil(PublicKeyX509Certificate cert)
        {
            if (null == cert)
            {
                throw new ArgumentNullException("cert cannot be null");
            }

            this.certificate = cert;
        }


        private EncryptionUtil()
        {
        }

        public static BigInteger Base64StringToBigInteger(string str)
        {
            byte[] binModulus = Convert.FromBase64String(str);
            BigInteger modulus = new BigInteger(1, binModulus);

            return modulus;
        }

        public BigInteger BigIntegerForModulus()
        {
            return Base64StringToBigInteger(this.certificate.ModulusInBase64);
        }

        public BigInteger BigIntegerForExponent()
        {
            return Base64StringToBigInteger(this.certificate.ExponentInBase64);
        }

        public string Encrypt(string text)
        {
            if (null == text)
            {
                return null;
            }

            var cipher = CipherUtilities.GetCipher("RSA/None/PKCS1Padding");

            BigInteger modulus = this.BigIntegerForModulus();
            BigInteger exponent = this.BigIntegerForExponent();

            RsaKeyParameters publicKey = new RsaKeyParameters(false, modulus, exponent);

            ICipherParameters rsaOptions = publicKey;
            cipher.Init(true, rsaOptions);

            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] rawEncryptedData = cipher.DoFinal(utf8.GetBytes(text));
            string encryptedData = Convert.ToBase64String(rawEncryptedData);

            return encryptedData;
        }
    }
}
