namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;

  /// <summary>
  /// Interface represents basic flow for requests that change item.
  /// </summary>
  /// <typeparam name="T">Type of request</typeparam>
  public interface IChangeItemRequestParametersBuilder<T> : IBaseRequestParametersBuilder<T> 
    where T : class
  {
    /// <summary>
    /// Adds fields that will be updated in item.
    /// </summary>
    /// <param name="fieldsRawValuesByName">Name of field and raw value pairs</param>
    /// <returns>this</returns>
    IChangeItemRequestParametersBuilder<T> AddFieldsRawValuesByNameToSet(IDictionary<string, string> fieldsRawValuesByName);

    /// <summary>
    /// Adds fields that will be updated in item.
    /// </summary>
    /// <param name="fieldName">Field name.</param>
    /// <param name="fieldValue">Field raw value.</param>
    /// <returns>this</returns>
    IChangeItemRequestParametersBuilder<T> AddFieldsRawValuesByNameToSet(string fieldName, string fieldValue);
  }
}
