namespace Sitecore.MobileSDK.Items.Fields
{
  using Sitecore.MobileSDK.API.Fields;

  public class ScField : IField
  {
    public string Name { get; private set; }
    public string RawValue { get; private set; }

    public ScField(string name, string rawValue)
    {
      this.Name = name;
      this.RawValue = rawValue;
    }
  }
}

