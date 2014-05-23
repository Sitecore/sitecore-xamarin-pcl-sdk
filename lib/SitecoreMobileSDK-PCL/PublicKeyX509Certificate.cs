namespace Sitecore.MobileSDK
{
    public class PublicKeyX509Certificate
    {
        // pclcrypto, bouncycastke
        //		http://blog.nerdbank.net/2014/03/all-about-rsa-key-formats.html

        // bouncycastle
        // http://awkwardcoder.blogspot.com/2011/08/using-bouncy-castle-on-windows-phone-7.html
        //		https://www.cryptool.org/trac/CrypTool2/browser/trunk/CrypPlugins/bouncycastle/test/src/test/RSATest.cs?rev=4


        public PublicKeyX509Certificate(string modulus, string exponent)
        {

            this.Modulus = modulus;
            this.Exponent = exponent;
        }

        public string Modulus { get; private set; }

        public string Exponent { get; private set; }
    }
}

