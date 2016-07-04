namespace Sitecore.MobileSDK.UrlBuilder.SortParameters
{
  using System.Linq;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public class SortParameters : ISortParameters
  {
    public SortParameters(IEnumerable<string> fields)
    {
      this.Fields = fields;
    }

    public virtual ISortParameters DeepCopy()
    {
      string[] fields = null;
      if (null != this.Fields)
      {
        fields = this.Fields.ToArray();
      }

      return new SortParameters(fields);
    }

    public IEnumerable<string> Fields { get; private set; }
  }
}
