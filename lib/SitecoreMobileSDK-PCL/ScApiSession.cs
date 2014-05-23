namespace Sitecore.MobileSDK
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Crypto.Parameters;
    using Org.BouncyCastle.Math;
    using Org.BouncyCastle.Security;
    using Sitecore.MobileSDK.PublicKey;

    public class ScApiSession
    {
        private readonly HttpClient httpClient;
        private readonly SessionConfig sessionConfig;
        private PublicKeyX509Certificate publicCertifiacte;

        public ScApiSession(SessionConfig config)
        {
            this.sessionConfig = config;
            this.httpClient = new HttpClient();
        }

        private ScApiSession()
        {
        }

        public async Task<PublicKeyX509Certificate> GetPublicKey()
        {
            string url = this.sessionConfig.InstanceUrl + "/-/item/v1/-/actions/getpublickey";

            Task<Stream> asyncPublicKeyStream = this.httpClient.GetStreamAsync(url);
            using (Stream publicKeyStream = await asyncPublicKeyStream)
            {
                Func<PublicKeyX509Certificate> syncParsePublicKey = () =>
                {
                    return new PublicKeyXmlParser().Parse(publicKeyStream);
                };
                this.publicCertifiacte = await Task.Factory.StartNew(syncParsePublicKey);
                return this.publicCertifiacte;
            }
        }


		private BigInteger Base64StringToBigInteger(string str)
        {
            byte[] binModulus = Convert.FromBase64String(str);
            BigInteger modulus = new BigInteger(1, binModulus);

            return modulus;
        }

        public string EncryptString(string data)
        {
            var cipher = CipherUtilities.GetCipher("RSA/None/PKCS1Padding");

            BigInteger modulus = this.Base64StringToBigInteger(this.publicCertifiacte.ModulusInBase64);
            BigInteger exponent = this.Base64StringToBigInteger(this.publicCertifiacte.ExponentInBase64);

            RsaKeyParameters publicKey = new RsaKeyParameters(false, modulus, exponent);

            ICipherParameters rsaOptions = publicKey;
            cipher.Init(true, rsaOptions);

            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] rawEncryptedData = cipher.DoFinal(utf8.GetBytes(data));
            string encryptedData = Convert.ToBase64String(rawEncryptedData);

            return encryptedData;
        }
    }
}