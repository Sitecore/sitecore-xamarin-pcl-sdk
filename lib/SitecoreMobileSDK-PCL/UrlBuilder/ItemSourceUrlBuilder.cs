namespace Sitecore.MobileSDK.UrlBuilder
{
  using System;
  using System.Globalization;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.Utils;

  public class ItemSourceUrlBuilder
  {
    private ItemSourceUrlBuilder()
    {
    }

    public ItemSourceUrlBuilder(IRestServiceGrammar restGrammar, ISSCUrlParameters sscGrammar, IItemSource itemSource)
    {
      this.itemSource = itemSource;
      this.restGrammar = restGrammar;
      this.sscGrammar = sscGrammar;

      this.Validate();
    }

    public string BuildUrlQueryString()
    {
      string escapedDatabase = null;
      string escapedLanguage = null;

      if (null != this.itemSource)
      {
        escapedDatabase = UrlBuilderUtils.EscapeDataString(this.itemSource.Database);
        escapedLanguage = UrlBuilderUtils.EscapeDataString(this.itemSource.Language);
      }

      string result = string.Empty;

      if (!string.IsNullOrEmpty(escapedDatabase))
      {
        result =
          result +
          this.sscGrammar.DatabaseParameterName + this.restGrammar.KeyValuePairSeparator + escapedDatabase;
      }

      if (!string.IsNullOrEmpty(escapedLanguage))
      {
        result =
          result +
          this.restGrammar.FieldSeparator +
          this.sscGrammar.LanguageParameterName + this.restGrammar.KeyValuePairSeparator + escapedLanguage;
      }


      if (null != this.itemSource.VersionNumber)
      {
        int iVersion = this.itemSource.VersionNumber.Value;
        string strVersion = iVersion.ToString(CultureInfo.InvariantCulture);


        string escapedVersion = UrlBuilderUtils.EscapeDataString(strVersion);

        result +=
          this.restGrammar.FieldSeparator +
          this.sscGrammar.VersionParameterName + this.restGrammar.KeyValuePairSeparator + escapedVersion;
      }

      return result.ToLowerInvariant();
    }

    private void Validate()
    {
      if (null == this.itemSource)
      {
        throw new ArgumentNullException("[ItemSourceUrlBuilder.itemSource] Do not pass null");
      }
      else if (null == this.restGrammar)
      {
        throw new ArgumentNullException("[ItemSourceUrlBuilder.grammar] Do not pass null");
      }
      else if (null == this.sscGrammar)
      {
        throw new ArgumentNullException("[ItemSourceUrlBuilder.grammar] Do not pass null");
      }
    }

    private IItemSource itemSource;
    private IRestServiceGrammar restGrammar;
    private ISSCUrlParameters sscGrammar;
  }
}

