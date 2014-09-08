namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request.Parameters;

  /// <summary>
  /// Interface represents basic flow for creation of requets.
  /// </summary>
  /// <typeparam name="T">Type of request</typeparam>
  public interface IBaseRequestParametersBuilder<T>
  where T : class
  {
    /// <summary>
    /// Specifies item database.
    /// For example: "web"
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <returns>
    /// this
    /// </returns>
    IBaseRequestParametersBuilder<T> Database(string sitecoreDatabase);

    /// <summary>
    /// Specifies item language.
    /// For example: "en"
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <returns>
    /// this
    /// </returns>
    IBaseRequestParametersBuilder<T> Language(string itemLanguage);

    /// <summary>
    /// Specifies payload.
    /// </summary>
    /// <param name="payload">The payload.</param>
    /// <returns>
    /// this
    /// </returns>
    /// <seealso cref="PayloadType" />
    IBaseRequestParametersBuilder<T> Payload(PayloadType payload);

    /// <summary>
    /// Adds the fields that will be read from the server.
    /// </summary>
    /// <param name="fields">The fields.</param>
    /// <returns>
    /// this
    /// </returns>
    IBaseRequestParametersBuilder<T> AddFieldsToRead(IEnumerable<string> fields);

    /// <summary>
    /// Adds the fields that will be read from the server.
    /// </summary>
    /// <param name="fieldParams">The field parameters.</param>
    /// <returns>
    /// this
    /// </returns>
    /// <seealso cref="AddFieldsToRead(System.Collections.Generic.IEnumerable{string})" />
    IBaseRequestParametersBuilder<T> AddFieldsToRead(params string[] fieldParams);

    /// <summary>
    /// Builds request with specified parameters.
    /// </summary>
    /// <returns>request</returns>
    T Build();
  }
}

