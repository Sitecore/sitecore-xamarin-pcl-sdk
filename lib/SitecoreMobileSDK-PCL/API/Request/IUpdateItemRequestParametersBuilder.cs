namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request.Parameters;

  /// <summary>
  /// Interface represents basic flow for creation of requets that update items.
  /// </summary>
  /// <typeparam name="T">Type of request</typeparam>
  public interface IUpdateItemRequestParametersBuilder<T> : IChangeItemRequestParametersBuilder<T>
    where T : class
  {
    /// <summary>
    /// Specifies item version. It is a positive integer number.
    /// A null value stands for the "latest" version.
    /// 
    /// For example: 1
    /// </summary>
    /// <param name="itemVersion">The item version.</param>
    /// <returns>
    /// this
    /// </returns>
    IUpdateItemRequestParametersBuilder<T> Version(int? itemVersion);

    /// <summary>
    /// Adds fields that will be updated in item.
    /// key   - must contain field name.
    /// value - must contain new field raw value.
    /// </summary>
    /// <param name="fieldsRawValuesByName">Name of field and raw value pairs</param>
    /// <returns>this</returns>
    new IUpdateItemRequestParametersBuilder<T> AddFieldsRawValuesByNameToSet(IDictionary<string, string> fieldsRawValuesByName);

    /// <summary>
    /// Adds fields that will be updated in item.
    /// </summary>
    /// <param name="fieldName">Field name.</param>
    /// <param name="fieldValue">Field raw value.</param>
    /// <returns>
    /// this
    /// </returns>
    new IUpdateItemRequestParametersBuilder<T> AddFieldsRawValuesByNameToSet(string fieldName, string fieldValue);

    /// Specifies item database.
    /// For example: "web"
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <param name="sitecoreDatabase">The Sitecore database.</param>
    /// <returns>
    /// this
    /// </returns>
    new IUpdateItemRequestParametersBuilder<T> Database(string sitecoreDatabase);

    /// <summary>
    /// Specifies item language.
    /// For example: "en"
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <param name="itemLanguage">The item language.</param>
    /// <returns>
    /// this
    /// </returns>
    new IUpdateItemRequestParametersBuilder<T> Language(string itemLanguage);

    /// <summary>
    /// Specifies payload.
    /// </summary>
    /// <param name="payload"><see cref="PayloadType" /> The payload.</param>
    /// <returns>
    /// this
    /// </returns>
    new IUpdateItemRequestParametersBuilder<T> Payload(PayloadType payload);

    /// <summary>
    /// Adds the fields that will be read from the server.
    /// </summary>
    /// <param name="fields">The fields names.</param>
    /// <returns>
    /// this
    /// </returns>
    new IUpdateItemRequestParametersBuilder<T> AddFieldsToRead(IEnumerable<string> fields);

    /// <summary>
    /// Adds the fields that will be read from the server.
    /// </summary>
    /// <param name="fieldParams">The field names.</param>
    /// <returns>
    /// this
    /// </returns>
    /// <seealso cref="AddFieldsToRead(System.Collections.Generic.IEnumerable{string})" />
    new IUpdateItemRequestParametersBuilder<T> AddFieldsToRead(params string[] fieldParams);
  }
}


