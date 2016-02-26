namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request.Parameters;

  /// <summary>
  /// Interface represents basic flow for creation of requets that read versioned items.
  /// </summary>
  /// <typeparam name="T">Type of request</typeparam>
  public interface IGetVersionedItemRequestParametersBuilder<T> : IScopedRequestParametersBuilder<T>
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
    IGetVersionedItemRequestParametersBuilder<T> Version(int? itemVersion);

    /// <summary>
    /// Specifies Sitecore database.
    /// For example: "web" 
    /// 
    /// The value is case insensitive.     
    /// </summary>
    /// <param name="sitecoreDatabase">The Sitecore database.</param>
    /// <returns>
    /// this
    /// </returns>
    new IGetVersionedItemRequestParametersBuilder<T> Database(string sitecoreDatabase);

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
    new IGetVersionedItemRequestParametersBuilder<T> Language(string itemLanguage);

    /// <summary>
    /// Adds the fields that will be read from the server.
    /// </summary>
    /// <param name="fields">The fields.</param>
    /// <returns>
    /// this
    /// </returns>
    new IGetVersionedItemRequestParametersBuilder<T> AddFieldsToRead(IEnumerable<string> fields);

    /// <summary>
    /// Adds the fields that will be read from the server.
    /// </summary>
    /// <param name="fieldParams">The field parameters.</param>
    /// <returns>
    /// this
    /// </returns>
    /// <seealso cref="AddFieldsToRead(System.Collections.Generic.IEnumerable{string})" />
    new IGetVersionedItemRequestParametersBuilder<T> AddFieldsToRead(params string[] fieldParams);

  }
}

