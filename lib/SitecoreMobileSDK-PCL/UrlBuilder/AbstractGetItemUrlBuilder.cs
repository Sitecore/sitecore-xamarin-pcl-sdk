using Sitecore.MobileSDK.UrlBuilder;
using Sitecore.MobileSDK.SessionSettings;
using Sitecore.MobileSDK.Items;
using System.Collections.Generic;


namespace Sitecore.MobileSDK
{
  using System;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;


  public abstract class AbstractGetItemUrlBuilder<TRequest>
    where TRequest : IBaseGetItemRequest
  {
    public AbstractGetItemUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
    {
      this.restGrammar = restGrammar;
      this.webApiGrammar = webApiGrammar;

      this.Validate();
    }

    #region Entry Point
    public virtual string GetUrlForRequest(TRequest request)
    {
      this.ValidateRequest( request );

      string hostUrl = this.GetHostUrlForRequest(request);
      string baseUrl = this.GetCommonPartForRequest( request ).ToLowerInvariant();
      string specificParameters = this.GetSpecificPartForRequest( request );

      string allParameters = baseUrl;

      if (!string.IsNullOrEmpty(specificParameters))
      {
        allParameters = 
          allParameters +
          this.restGrammar.FieldSeparator + specificParameters;
      }
        
      bool shouldTruncateBaseUrl = ( !string.IsNullOrEmpty(allParameters) && allParameters.StartsWith(this.restGrammar.FieldSeparator) );
      while (shouldTruncateBaseUrl)
      {
          allParameters = allParameters.Substring(1);
          shouldTruncateBaseUrl = ( !string.IsNullOrEmpty(allParameters) && allParameters.StartsWith(this.restGrammar.FieldSeparator) );
      }


      string result = hostUrl;

      if (!string.IsNullOrEmpty(allParameters))
      {
        result = 
          result +
          this.restGrammar.HostAndArgsSeparator + allParameters;
      }

      return result;
    }

    protected virtual void ValidateRequest(TRequest request)
    {
      this.ValidateCommonRequest( request );
      this.ValidateSpecificRequest( request );
    }
    #endregion Entry Point

    #region Common Logic
    protected virtual string GetHostUrlForRequest(TRequest request)
    {
      SessionConfigUrlBuilder sessionBuilder = new SessionConfigUrlBuilder(this.restGrammar, this.webApiGrammar);
      string hostUrl = sessionBuilder.BuildUrlString(request.SessionSettings);

      return hostUrl;
    }

    private string GetCommonPartForRequest(TRequest request)
    {
      QueryParametersUrlBuilder queryParametersUrlBuilder = new QueryParametersUrlBuilder(this.restGrammar, this.webApiGrammar);
      string queryParamsUrl = queryParametersUrlBuilder.BuildUrlString(request.QueryParameters);

      ItemSourceUrlBuilder sourceBuilder = new ItemSourceUrlBuilder (this.restGrammar, this.webApiGrammar, request.ItemSource);
      string itemSourceArgs = sourceBuilder.BuildUrlQueryString ();

      string result = string.Empty;

      string[] restSubQueries = { itemSourceArgs, queryParamsUrl };
      foreach (string currentSubQuery in restSubQueries)
      {
        if (!string.IsNullOrEmpty(currentSubQuery))
        {
          result = 
            result +
            this.restGrammar.FieldSeparator + currentSubQuery;
        }
      }

      return result.ToLowerInvariant();
    }

    private void ValidateCommonRequest(TRequest request)
    {
      if ( null == request )
      {
        throw new ArgumentNullException( "AbstractGetItemUrlBuilder.GetUrlForRequest() : request cannot be null" );
      }
      else if ( null == request.SessionSettings )
      {
        throw new ArgumentNullException( "AbstractGetItemUrlBuilder.GetUrlForRequest() : request.SessionSettings cannot be null" );
      }
      else if ( null == request.SessionSettings.InstanceUrl )
      {
        throw new ArgumentNullException( "AbstractGetItemUrlBuilder.GetUrlForRequest() : request.SessionSettings.InstanceUrl cannot be null" );
      }
      else if ( null == request.SessionSettings.ItemWebApiVersion )
      {
        throw new ArgumentNullException( "AbstractGetItemUrlBuilder.GetUrlForRequest() : request.SessionSettings.InstanceUrl.ItemWebApiVersion cannot be null" );
      }

      if (null != request.QueryParameters)
      {
        this.ValidateFields(request.QueryParameters.Fields);
      }
    }

    private void ValidateFields(ICollection<string> fields)
    {
      if (null == fields)
      {
        return;
      }

      var uniqueFields = new HashSet<string>();
      foreach (string singleField in fields)
      {
        string lowercaseSingleField = singleField.ToLowerInvariant();

        if (uniqueFields.Contains(lowercaseSingleField))
        {
          throw new ArgumentException("AbstractGetItemUrlBuilder.GetUrlForRequest() : request.QueryParameters.Fields must contain NO duplicates");
        }

        uniqueFields.Add(lowercaseSingleField);
      }
    }

    private void Validate()
    {
      if (null == this.restGrammar)
      {
        throw new ArgumentNullException ("[GetItemUrlBuilder] restGrammar cannot be null"); 
      }
      else if (null == this.webApiGrammar)
      {
        throw new ArgumentNullException ("[GetItemUrlBuilder] webApiGrammar cannot be null");
      }
    }
    #endregion Common Logic

    #region Abstract Methods
    protected abstract string GetSpecificPartForRequest(TRequest request);

    protected abstract void ValidateSpecificRequest(TRequest request);
    #endregion Abstract Methods

    #region instance variables
    protected IRestServiceGrammar restGrammar;
    protected IWebApiUrlParameters webApiGrammar;
    #endregion instance variables
  }
}

