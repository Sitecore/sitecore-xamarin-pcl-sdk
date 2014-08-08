namespace Sitecore.MobileSDK
{
  public class PublicKeyX509Certificate
  {
    public PublicKeyX509Certificate(string modulus, string exponent)
    {

      this.ModulusInBase64 = modulus;
      this.ExponentInBase64 = exponent;
    }

    public string ModulusInBase64 { get; private set; }

    public string ExponentInBase64 { get; private set; }
  }
}

