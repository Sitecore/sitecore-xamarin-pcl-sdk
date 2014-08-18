namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request.Parameters;

  /// <summary>
  /// Interface represents flow for creation of requets that create items.
  /// </summary>
  /// <typeparam name="T">Type of request</typeparam>
  public interface ICreateItemRequestParametersBuilder<T> : IChangeItemRequestParametersBuilder<T>
  where T : class
  {
    /// <summary>
    /// Adds fields that will be updated in item.
    /// </summary>
    /// <param name="fieldsRawValuesByName">Name of field and raw value pairs</param>
    /// <returns>
    /// this
    /// </returns>
    new ICreateItemRequestParametersBuilder<T> AddFieldsRawValuesByNameToSet(IDictionary<string, string> fieldsRawValuesByName);

    /// <summary>
    /// Adds fields that will be updated in item.
    /// </summary>
    /// <param name="fieldName">Field name.</param>
    /// <param name="fieldValue">Field raw value.</param>
    /// <returns>
    /// this
    /// </returns>
    new ICreateItemRequestParametersBuilder<T> AddFieldsRawValuesByNameToSet(string fieldName, string fieldValue);

    /// <summary>
    /// Specifies sitecore database.
    /// </summary>
    /// <param name="sitecoreDatabase">The sitecore database.</param>
    /// <returns>
    /// this
    /// </returns>
    new ICreateItemRequestParametersBuilder<T> Database(string sitecoreDatabase);

    /// <summary>
    /// Specifies item language.
    /// </summary>
    /// <param name="itemLanguage">The item language.</param>
    /// <returns>
    /// this
    /// </returns>
    new ICreateItemRequestParametersBuilder<T> Language(string itemLanguage);

    /// <summary>
    /// Specifies payload.
    /// </summary>
    /// <param name="payload">The payload.</param>
    /// <returns>
    /// this
    /// </returns>
    /// <seealso cref="PayloadType" />
    new ICreateItemRequestParametersBuilder<T> Payload(PayloadType payload);

    /// <summary>
    /// Adds the fields that will be read from the server.
    /// </summary>
    /// <param name="fields">The fields.</param>
    /// <returns>
    /// this
    /// </returns>
    new ICreateItemRequestParametersBuilder<T> AddFieldsToRead(IEnumerable<string> fields);

    /// <summary>
    /// Adds the fields that will be read from the server.
    /// </summary>
    /// <param name="fieldParams">The field parameters.</param>
    /// <returns>
    /// this
    /// </returns>
    /// <seealso cref="AddFieldsToRead(System.Collections.Generic.IEnumerable{string})" />
    new ICreateItemRequestParametersBuilder<T> AddFieldsToRead(params string[] fieldParams);
  }
}

