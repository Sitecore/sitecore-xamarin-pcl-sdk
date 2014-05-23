using System.Xml;
using System.IO;


namespace Sitecore.MobileSDK.PublicKey
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class PublicKeyXmlParser
    {
        /// <exception cref="ArgumentNullException">PublicKeyXmlParser : xmlText cannot be null</exception>
        public PublicKeyX509Certificate Parse(Stream xmlStream)
        {
            if (null == xmlStream)
            {
                throw new ArgumentNullException("PublicKeyXmlParser : xmlText cannot be null");
            }


            using (XmlReader reader = XmlReader.Create(xmlStream))
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
        }
    }
}
