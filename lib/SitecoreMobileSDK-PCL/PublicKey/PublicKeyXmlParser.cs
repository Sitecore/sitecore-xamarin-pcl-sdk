namespace Sitecore.MobileSDK.PublicKey
{
  using System;
  using System.Xml;
  using System.IO;
  using System.Threading;

  public class PublicKeyXmlParser
  {
    /// <exception cref="ArgumentNullException">PublicKeyXmlParser : xmlText cannot be null</exception>
    public PublicKeyX509Certificate Parse(Stream xmlStream, CancellationToken cancelToken)
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
          cancelToken.ThrowIfCancellationRequested();

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
