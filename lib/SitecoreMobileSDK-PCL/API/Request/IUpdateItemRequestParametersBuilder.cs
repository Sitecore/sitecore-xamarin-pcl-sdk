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
    /// Specifies item version.
    /// </summary>
    /// <param name="itemVersion">The item version.</param>
    /// <returns>
    /// this
    /// </returns>
    IUpdateItemRequestParametersBuilder<T> Version(int? itemVersion);

    /// <summary>
    /// Adds fields that will be updated in item.
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

    /// <summary>
    /// Specifies sitecore database.
    /// </summary>
    /// <param name="sitecoreDatabase">The sitecore database.</param>
    /// <returns>
    /// this
    /// </returns>
    new IUpdateItemRequestParametersBuilder<T> Database(string sitecoreDatabase);

    /// <summary>
    /// Specifies item language.
    /// </summary>
    /// <param name="itemLanguage">The item language.</param>
    /// <returns>
    /// this
    /// </returns>
    new IUpdateItemRequestParametersBuilder<T> Language(string itemLanguage);

    /// <summary>
    /// Specifies payload.
    /// </summary>
    /// <param name="payload">The payload.</param>
    /// <returns>
    /// this
    /// </returns>
    /// <seealso cref="PayloadType" />
    new IUpdateItemRequestParametersBuilder<T> Payload(PayloadType payload);

    /// <summary>
    /// Adds the fields that will be read from the server.
    /// </summary>
    /// <param name="fields">The fields.</param>
    /// <returns>
    /// this
    /// </returns>
    new IUpdateItemRequestParametersBuilder<T> AddFieldsToRead(IEnumerable<string> fields);

    /// <summary>
    /// Adds the fields that will be read from the server.
    /// </summary>
    /// <param name="fieldParams">The field parameters.</param>
    /// <returns>
    /// this
    /// </returns>
    /// <seealso cref="AddFieldsToRead(System.Collections.Generic.IEnumerable{string})" />
    new IUpdateItemRequestParametersBuilder<T> AddFieldsToRead(params string[] fieldParams);
  }
}


