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
                Func<PublicKeyX509Certificate> syncParsePublicKey = ScApiSession.ParsePublicKeyFromStreamDelegate(publicKeyStream);
                this.publicCertifiacte = await Task.Factory.StartNew(syncParsePublicKey);
                return this.publicCertifiacte;
            }
        }

        public string EncryptString(string data)
        {
            var cipher = CipherUtilities.GetCipher("RSA/None/PKCS1Padding");

            byte[] binModulus = Convert.FromBase64String(this.publicCertifiacte.Modulus);
            byte[] binExponent = Convert.FromBase64String(this.publicCertifiacte.Exponent);

            BigInteger modulus = new BigInteger(1, binModulus);
            BigInteger exponent = new BigInteger(1, binExponent);

            RsaKeyParameters publicKey = new RsaKeyParameters(false, modulus, exponent);

            ICipherParameters rsaOptions = publicKey;
            cipher.Init(true, rsaOptions);

            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] rawEncryptedData = cipher.DoFinal(utf8.GetBytes(data));
            string encryptedData = Convert.ToBase64String(rawEncryptedData);

            return encryptedData;
        }

        private static Func<PublicKeyX509Certificate> ParsePublicKeyFromStreamDelegate(Stream publicKeyStream)
        {
            Func<PublicKeyX509Certificate> syncParsePublicKey = () =>
            {
                using (XmlReader reader = XmlReader.Create(publicKeyStream))
                {
                    string modulus = null;
                    string exponent = null;

                    while (true)
                    {
                        if (reader.Name.Equals("Modulus"))
                        {
                            modulus = reader.ReadElementContentAsString();
                            continue;
                        }
                        else if (reader.Name.Equals("Exponent"))
                        {
                            exponent = reader.ReadElementContentAsString();
                            continue;
                        }

                        if (!reader.Read())
                        {
                            break;
                        }
                    }
                    return new PublicKeyX509Certificate(modulus, exponent);
                }
            };

            return syncParsePublicKey;
        }
    }
}