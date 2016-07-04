using Sitecore.MobileSDK.API.Request.Parameters;

namespace Sitecore.MobileSDK.UserRequest.SearchRequest
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
  using Sitecore.MobileSDK.UrlBuilder.SortParameters;
  using Sitecore.MobileSDK.Validators;

  public class SitecoreSearchRequestBuilder : AbstractGetVersionedItemRequestBuilder<SitecoreSearchParameters>
  {
    public SitecoreSearchRequestBuilder(string term)
    {
      ItemIdValidator.ValidateSearchRequest(term, this.GetType().Name + ".term");

      this.term = term;
    }

    public override SitecoreSearchParameters Build()
    {
      IPagingParameters pagingSettings = this.AccumulatedPagingParameters;

      SitecoreSearchParameters result = new SitecoreSearchParameters(
        null, 
        this.itemSourceAccumulator, 
        this.queryParameters, 
        pagingSettings,
        this.sortParameters,
        this.term);

      return result;
    }

    public SitecoreSearchRequestBuilder AddAscendingSortingFields(params string[] fieldParams)
    {
      BaseValidator.CheckNullAndThrow(fieldParams, this.GetType().Name + ".Fields");
      BaseValidator.CheckNullAndThrow(fieldParams[0], this.GetType().Name + ".Fields");

      if (1 == fieldParams.Length) {
        if (null == fieldParams[0]) {
          return this;
        }
      }

      return this.AddFieldsToSort((IEnumerable<string>)fieldParams, "a");
    }

    public SitecoreSearchRequestBuilder AddDescendingSortingFields(params string[] fieldParams)
    {
      BaseValidator.CheckNullAndThrow(fieldParams, this.GetType().Name + ".Fields");
      BaseValidator.CheckNullAndThrow(fieldParams[0], this.GetType().Name + ".Fields");

      if (1 == fieldParams.Length) {
        if (null == fieldParams[0]) {
          return this;
        }
      }

      return this.AddFieldsToSort((IEnumerable<string>)fieldParams, "d");
    }

    public SitecoreSearchRequestBuilder AddAscendingFieldsToSort(IEnumerable<string> fields)
    {
      return this.AddFieldsToSort(fields, "a");
    }

    public SitecoreSearchRequestBuilder AddDescendingFieldsToSort(IEnumerable<string> fields)
    {
      return this.AddFieldsToSort(fields, "d");
    }

    private SitecoreSearchRequestBuilder AddFieldsToSort(IEnumerable<string> fields, string sortOrder)
    {
      BaseValidator.CheckNullAndThrow(fields, this.GetType().Name + ".FieldsToSort");

      if (!fields.Any()) {
        return this;
      }

      Func<string, bool> fieldNameValidator =
        fieldName => !string.IsNullOrWhiteSpace(fieldName);
      var validFieldsNoOrder = fields.Where(fieldNameValidator).ToList();

      //adding sort order
      var validFields = new List<string>();
      foreach (string field in validFieldsNoOrder) {
        validFields.Add(sortOrder + field);
      }

      IEnumerable<string> currentFields = this.sortParameters.Fields;
      if (null == currentFields) {
        currentFields = new string[0] { };
      };

      int myFieldsCount = currentFields.Count();
      int newFieldsCount = validFields.Count;

      int appendedFieldsCount = myFieldsCount + newFieldsCount;
      string[] newFields = new string[appendedFieldsCount];

      currentFields.ToArray().CopyTo(newFields, 0);
      validFields.CopyTo(newFields, myFieldsCount);

      bool isFieldListHasDuplicates = DuplicateEntryValidator.IsDuplicatedFieldsInTheList(newFields);
      if (isFieldListHasDuplicates) {
        throw new InvalidOperationException(this.GetType().Name + ".Fields" + " : duplicate fields are not allowed");
      }

      this.sortParameters = new SortParameters(newFields);

      return this;
    }
    protected SortParameters sortParameters = new SortParameters(null);
    private readonly string term;
  }
}

