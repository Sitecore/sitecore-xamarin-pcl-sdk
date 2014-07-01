
namespace Sitecore.MobileSDK
{
  using System.Collections.Generic;

  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;


  public abstract class AbstractGetItemRequestBuilder<T> : IGetItemRequestParametersBuilder<T>
    where T : class
  {
    public IGetItemRequestParametersBuilder<T> Database (string sitecoreDatabase)
    {
      this.itemSourceAccumulator = new ItemSourcePOD (
        sitecoreDatabase, 
        this.itemSourceAccumulator.Language, 
        this.itemSourceAccumulator.Version);

      return this;
    }

    public IGetItemRequestParametersBuilder<T> Language (string itemLanguage)
    {
      this.itemSourceAccumulator = new ItemSourcePOD (
        this.itemSourceAccumulator.Database, 
        itemLanguage, 
        this.itemSourceAccumulator.Version);

      return this;
    }

    public IGetItemRequestParametersBuilder<T> Version (string itemVersion)
    {
      this.itemSourceAccumulator = new ItemSourcePOD (
        this.itemSourceAccumulator.Database, 
        this.itemSourceAccumulator.Language,
        itemVersion);

      return this;
    }

    public IGetItemRequestParametersBuilder<T> Payload(PayloadType payload)
    {
      this.queryParameters = new QueryParameters(payload, this.queryParameters.Fields );
      return this;
    }

    public IGetItemRequestParametersBuilder<T> AddFields( ICollection<string> fields )
    {
      if (null == fields)
      {
        return this;
      }
      else if (0 == fields.Count)
      {
        return this;
      }


      ICollection<string> currentFields = this.queryParameters.Fields;
      if (null == currentFields)
      {
        currentFields = new string[0]{};
      };


      int myFieldsCount = currentFields.Count;
      int newFieldsCount = fields.Count;

      int appendedFieldsCount = myFieldsCount + newFieldsCount;
      string[] newFields = new string[appendedFieldsCount];


      currentFields.CopyTo( newFields, 0 );
      fields.CopyTo( newFields, myFieldsCount );

      this.queryParameters = new QueryParameters( this.queryParameters.Payload, newFields );
      return this;
    }

    public IGetItemRequestParametersBuilder<T> AddFields( string singleField )
    {
      if (null == singleField)
      {
        return this;
      }

      string[] arrayOfNewField = {singleField};
      return this.AddFields( arrayOfNewField );
    }

    public abstract T Build();

    protected ItemSourcePOD itemSourceAccumulator = new ItemSourcePOD( null, null, null );
    protected QueryParameters queryParameters = new QueryParameters( null, null );
  }
}

