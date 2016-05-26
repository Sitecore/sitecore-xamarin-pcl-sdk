namespace Sitecore.MobileSDK.SessionSettings
{
  using System;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.Validators;

  public class SessionConfigUrlBuilder
  {
    public SessionConfigUrlBuilder(IRestServiceGrammar restGrammar, ISSCUrlParameters sscGrammar)
    {
      this.restGrammar = restGrammar;
      this.sscGrammar = sscGrammar;

      this.Validate();
    }

    public virtual string BuildUrlString(ISessionConfig request)
    {
      this.ValidateRequest(request);

      string autocompletedInstanceUrl = SessionConfigValidator.AutocompleteInstanceUrl(request.InstanceUrl);

      string result =
        autocompletedInstanceUrl + this.sscGrammar.ItemSSCEndpoint;

      return result.ToLowerInvariant();
    }


    private void ValidateRequest(ISessionConfig request)
    {
      if (null == request)
      {
        throw new ArgumentNullException("SSCUrlBuilder.GetUrlForRequest() : do not pass null");
      }
    }

    private void Validate()
    {
      if (null == this.restGrammar)
      {
        throw new ArgumentNullException("[SessionConfigUrlBuilder] restGrammar cannot be null");
      }
      else if (null == this.sscGrammar)
      {
        throw new ArgumentNullException("[SessionConfigUrlBuilder] sscGrammar cannot be null");
      }
    }

    private IRestServiceGrammar restGrammar;
    private ISSCUrlParameters sscGrammar;
  }
}
