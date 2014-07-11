﻿
namespace Sitecore.MobileSDK.UserRequest
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
      if (string.IsNullOrWhiteSpace(sitecoreDatabase))
      {
        throw new ArgumentException("AbstractGetItemRequestBuilder.Database : The input cannot be null or empty");
      }
      else if (null != this.itemSourceAccumulator.Database)
      {
        throw new InvalidOperationException("AbstractGetItemRequestBuilder.Database : The database cannot be assigned twice");
      }


      this.itemSourceAccumulator = new ItemSourcePOD(
        sitecoreDatabase, 
        this.itemSourceAccumulator.Language, 
        this.itemSourceAccumulator.Version);

      return this;
    }

    public IGetItemRequestParametersBuilder<T> Language(string itemLanguage)
    {
      if (string.IsNullOrWhiteSpace(itemLanguage))
      {
        throw new ArgumentException("AbstractGetItemRequestBuilder.Language : The input cannot be null or empty");
      }
      else if (null != this.itemSourceAccumulator.Language)
      {
        throw new InvalidOperationException("AbstractGetItemRequestBuilder.Language : The language cannot be assigned twice");
      }


      this.itemSourceAccumulator = new ItemSourcePOD(
        this.itemSourceAccumulator.Database, 
        itemLanguage, 
        this.itemSourceAccumulator.Version);

      return this;
    }

    public IGetItemRequestParametersBuilder<T> Payload(PayloadType payload)
    {
      if (null != this.queryParameters.Payload)
      {
        throw new InvalidOperationException("AbstractGetItemRequestBuilder.Payload : The payload cannot be assigned twice");
      }

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
        fieldName => !string.IsNullOrWhiteSpace(fieldName);
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

