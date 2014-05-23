namespace Sitecore.MobileSDK.PublicKey
{
    using Org.BouncyCastle.Math;

    public class EncryptionUtil
    {
        private PublicKeyX509Certificate certificate;

        private EncryptionUtil()
        {
        }


        public EncryptionUtil(PublicKeyX509Certificate cert)
        {
            this.certificate = cert;
        }


        public BigInteger BigIntegerForModulus()
        {
            return null;
        }

        public BigInteger BigIntegerForExponent()
        {
            return null;
        }
    }
}
