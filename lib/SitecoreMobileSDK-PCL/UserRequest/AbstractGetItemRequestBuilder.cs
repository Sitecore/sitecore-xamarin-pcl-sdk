
namespace Sitecore.MobileSDK
{
  using System;
  using System.Linq;
  using System.Collections.Generic;

  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Validators;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;


  public abstract class AbstractGetItemRequestBuilder<T> : IGetItemRequestParametersBuilder<T>
    where T : class
  {
    public IGetItemRequestParametersBuilder<T> Database(string sitecoreDatabase)
    {
      this.itemSourceAccumulator = new ItemSourcePOD(
        sitecoreDatabase, 
        this.itemSourceAccumulator.Language, 
        this.itemSourceAccumulator.Version);

      return this;
    }

    public IGetItemRequestParametersBuilder<T> Language(string itemLanguage)
    {
      this.itemSourceAccumulator = new ItemSourcePOD(
        this.itemSourceAccumulator.Database, 
        itemLanguage, 
        this.itemSourceAccumulator.Version);

      return this;
    }

    public IGetItemRequestParametersBuilder<T> Version(string itemVersion)
    {
      this.itemSourceAccumulator = new ItemSourcePOD(
        this.itemSourceAccumulator.Database, 
        this.itemSourceAccumulator.Language,
        itemVersion);

      return this;
    }

    public IGetItemRequestParametersBuilder<T> Payload(PayloadType payload)
    {
      this.queryParameters = new QueryParameters(payload, this.queryParameters.ScopeParameters, this.queryParameters.Fields);
      return this;
    }

    public IGetItemRequestParametersBuilder<T> AddScope(ScopeType scope)
    {
      ScopeParameters scopeParameters;

      if (null == this.queryParameters.ScopeParameters)
      {
        scopeParameters = new ScopeParameters();
      }
      else
      {
        scopeParameters = this.queryParameters.ScopeParameters.ShallowCopy();
      }
      scopeParameters.AddScope(scope);
      this.queryParameters = new QueryParameters(this.queryParameters.Payload, scopeParameters, this.queryParameters.Fields);
      return this;
    }

    public IGetItemRequestParametersBuilder<T> AddFields(ICollection<string> fields)
    {
      if (null == fields)
      {
        return this;
      }
      else if (0 == fields.Count)
      {
        return this;
      }

      Func<string, bool> fieldNameValidator = 
        fieldName => !string.IsNullOrEmpty(fieldName);
      var validFields = fields.Where(fieldNameValidator).ToList();



      ICollection<string> currentFields = this.queryParameters.Fields;
      if (null == currentFields)
      {
        currentFields = new string[0]{};
      };


      int myFieldsCount = currentFields.Count;
      int newFieldsCount = validFields.Count;

      int appendedFieldsCount = myFieldsCount + newFieldsCount;
      string[] newFields = new string[appendedFieldsCount];


      currentFields.CopyTo(newFields, 0);
      validFields.CopyTo(newFields, myFieldsCount);

      bool isFieldListHasDuplicates = DuplicateEntryValidator.IsDuplicatedFieldsInTheList(newFields);
      if (isFieldListHasDuplicates)
      {
        throw new ArgumentException("RequestBuilder : duplicate fields are not allowed");
      }

      this.queryParameters = new QueryParameters( this.queryParameters.Payload, this.queryParameters.ScopeParameters, newFields );
      return this;
    }

    public IGetItemRequestParametersBuilder<T> AddFields(params string[] fieldParams)
    {
      if (null == fieldParams)
      {
        return this;
      }
      else if (1 == fieldParams.Length)
      {
        if (null == fieldParams[0])
        {
          return this;
        }
      }

      return this.AddFields( (ICollection<string>)fieldParams );
    }

    public abstract T Build();

    protected ItemSourcePOD itemSourceAccumulator = new ItemSourcePOD(null, null, null);
    protected QueryParameters queryParameters = new QueryParameters(null, null, null);
  }
}

