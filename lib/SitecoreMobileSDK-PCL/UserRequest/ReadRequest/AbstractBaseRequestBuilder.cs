﻿namespace Sitecore.MobileSDK.UserRequest.ReadRequest
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
  using Sitecore.MobileSDK.Validators;

  public abstract class AbstractBaseRequestBuilder<T> : IBaseReadRequestParametersBuilder<T>
    where T : class
  {
    public IBaseRequestParametersBuilder<T> Database(string sitecoreDatabase)
    {
      if (string.IsNullOrEmpty(sitecoreDatabase))
      {
        return this;
      }

      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(sitecoreDatabase, this.GetType().Name + ".Database");

      BaseValidator.CheckForTwiceSetAndThrow(this.itemSourceAccumulator.Database, this.GetType().Name + ".Database");

      this.itemSourceAccumulator = new ItemSourcePOD(
        sitecoreDatabase,
        this.itemSourceAccumulator.Language,
        this.itemSourceAccumulator.VersionNumber);

      return this;
    }

    public IBaseRequestParametersBuilder<T> Language(string itemLanguage)
    {
      if (string.IsNullOrEmpty(itemLanguage))
      {
        return this;
      }

      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(itemLanguage, this.GetType().Name + ".Language");

      BaseValidator.CheckForTwiceSetAndThrow(this.itemSourceAccumulator.Language, this.GetType().Name + ".Language");

      this.itemSourceAccumulator = new ItemSourcePOD(
        this.itemSourceAccumulator.Database,
        itemLanguage,
        this.itemSourceAccumulator.VersionNumber);

      return this;
    }

    public IBaseRequestParametersBuilder<T> AddFieldsToRead(IEnumerable<string> fields)
    {
      BaseValidator.CheckNullAndThrow(fields, this.GetType().Name + ".Fields");

      if (!fields.Any())
      {
        return this;
      }

      Func<string, bool> fieldNameValidator =
        fieldName => !string.IsNullOrWhiteSpace(fieldName);
      var validFields = fields.Where(fieldNameValidator).ToList();

      IEnumerable<string> currentFields = this.queryParameters.Fields;
      if (null == currentFields)
      {
        currentFields = new string[0] { };
      };



      int myFieldsCount = currentFields.Count();
      int newFieldsCount = validFields.Count;

      int appendedFieldsCount = myFieldsCount + newFieldsCount;
      string[] newFields = new string[appendedFieldsCount];


      currentFields.ToArray().CopyTo(newFields, 0);
      validFields.CopyTo(newFields, myFieldsCount);

      bool isFieldListHasDuplicates = DuplicateEntryValidator.IsDuplicatedFieldsInTheList(newFields);
      if (isFieldListHasDuplicates)
      {
        throw new InvalidOperationException(this.GetType().Name + ".Fields" + " : duplicate fields are not allowed");
      }

      this.queryParameters = new QueryParameters(newFields);

      return this;
    }

    public IBaseRequestParametersBuilder<T> AddFieldsToRead(params string[] fieldParams)
    {
      BaseValidator.CheckNullAndThrow(fieldParams, this.GetType().Name + ".Fields");
      BaseValidator.CheckNullAndThrow(fieldParams[0], this.GetType().Name + ".Fields");

      if (1 == fieldParams.Length)
      {
        if (null == fieldParams[0])
        {
          return this;
        }
      }

      return this.AddFieldsToRead((IEnumerable<string>)fieldParams);
    }

    public IBaseRequestParametersBuilder<T> IcludeStanderdTemplateFields(bool include)
    {
      this.icludeStanderdTemplateFields = include;

      return this;
    }

    public abstract T Build();

    protected bool icludeStanderdTemplateFields = false;
    protected ItemSourcePOD itemSourceAccumulator = new ItemSourcePOD(null, null, null);
    protected QueryParameters queryParameters = new QueryParameters(null);
  }
}

