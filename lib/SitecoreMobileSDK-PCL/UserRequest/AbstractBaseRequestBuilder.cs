
namespace Sitecore.MobileSDK.UserRequest
{
  using System;
  using System.Linq;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Validators;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public abstract class AbstractBaseRequestBuilder<T> : IBaseRequestParametersBuilder<T>
    where T : class
  {
    public IBaseRequestParametersBuilder<T> Database(string sitecoreDatabase)
    {
      if (string.IsNullOrWhiteSpace(sitecoreDatabase))
      {
        BaseValidator.ThrowNullOrEmptyParameterException(this.GetType().Name + ".Database");
      }
      else if (null != this.itemSourceAccumulator.Database)
      {
        BaseValidator.ThrowParameterSetTwiceException(this.GetType().Name + ".Database");
      }


      this.itemSourceAccumulator = new ItemSourcePOD(
        sitecoreDatabase, 
        this.itemSourceAccumulator.Language, 
        this.itemSourceAccumulator.Version);

      return this;
    }

    public IBaseRequestParametersBuilder<T> Language(string itemLanguage)
    {
      if (string.IsNullOrWhiteSpace(itemLanguage))
      {
        BaseValidator.ThrowNullOrEmptyParameterException(this.GetType().Name + ".Language");
      }
      else if (null != this.itemSourceAccumulator.Language)
      {
        BaseValidator.ThrowParameterSetTwiceException(this.GetType().Name + ".Language");
      }


      this.itemSourceAccumulator = new ItemSourcePOD(
        this.itemSourceAccumulator.Database, 
        itemLanguage, 
        this.itemSourceAccumulator.Version);

      return this;
    }

    public IBaseRequestParametersBuilder<T> Payload(PayloadType payload)
    {
      if (null != this.queryParameters.Payload)
      {
        BaseValidator.ThrowParameterSetTwiceException(this.GetType().Name + ".Payload");
      }

      this.queryParameters = new QueryParameters(payload, this.queryParameters.ScopeParameters, this.queryParameters.Fields);
      return this;
    }

    public IBaseRequestParametersBuilder<T> AddScope(ICollection<ScopeType> scope)
    {
      ScopeParameters scopeParameters = new ScopeParameters(this.queryParameters.ScopeParameters);

      foreach (ScopeType singleScope in scope)
      {
        scopeParameters.AddScope(singleScope);
      }

      this.queryParameters = new QueryParameters(this.queryParameters.Payload, scopeParameters, this.queryParameters.Fields);
      return this;
    }

    public IBaseRequestParametersBuilder<T> AddScope(params ScopeType[] scope)
    {
      ICollection<ScopeType> castedScope = (ICollection<ScopeType>)scope;
      return this.AddScope(castedScope);
    }

    public IBaseRequestParametersBuilder<T> AddFields(ICollection<string> fields)
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
        throw new ArgumentException(this.GetType().Name + " : duplicate fields are not allowed");
      }

      this.queryParameters = new QueryParameters( this.queryParameters.Payload, this.queryParameters.ScopeParameters, newFields );
      return this;
    }

    public IBaseRequestParametersBuilder<T> AddFields(params string[] fieldParams)
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

