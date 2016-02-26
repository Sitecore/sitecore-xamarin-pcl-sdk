namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request.Paging;
  using Sitecore.MobileSDK.API.Request.Parameters;


  /// <summary>
  /// Interface represents basic flow for creation of requets that have ability to specify scope for request.
  /// </summary>
  /// <typeparam name="T">Type of request</typeparam>
  public interface IScopedRequestParametersBuilder<T> where T : class
  {
    /// <summary>
    /// Specifies a number of the page to download.
    /// It should be a positive number or zero.
    /// It should be in a range of "TotalItemsInResponse" / "ItemsPerPage"
    /// 
    /// The parameter is optional. However, it requires "ItemsPerPage" parameters once specified.
    /// On a repeated invocation an InvalidOperationException is thrown.
    /// 
    /// </summary>
    /// <param name="pageNumber">Index of a page to download.
    /// An ArgumentException is thrown if negative number is submitted.
    /// </param>
    /// <returns>
    /// An object capable of setting ItemsPerPage parameter. Usually it is same as "this" object. 
    /// It is done to ensure that either both parameters are used ore none of them is specified.
    /// </returns>
    IPageNumberAccumulator<T> PageNumber(int pageNumber);

    /// <summary>
    /// Specifies item database.
    /// For example: "web"
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <param name="sitecoreDatabase">The Sitecore database.</param>
    /// <returns>
    /// this
    /// </returns>
    IScopedRequestParametersBuilder<T> Database(string sitecoreDatabase);

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
    IScopedRequestParametersBuilder<T> Language(string itemLanguage);

    /// <summary>
    /// Adds the fields that will be read from the server.
    /// 
    /// The values is case insensitive.
    /// </summary>
    /// <param name="fields">The fields names.</param>
    /// <returns>
    /// this
    /// </returns>
    IScopedRequestParametersBuilder<T> AddFieldsToRead(IEnumerable<string> fields);

    /// <summary>
    /// Adds the fields that will be read from the server.
    /// 
    /// The values is case insensitive.
    /// </summary>
    /// <param name="fieldParams">The field names.</param>
    /// <returns>
    /// this
    /// </returns>
    /// <seealso cref="AddFieldsToRead(System.Collections.Generic.IEnumerable{string})" />
    IScopedRequestParametersBuilder<T> AddFieldsToRead(params string[] fieldParams);

    /// <summary>
    /// Builds request with specified parameters.
    /// </summary>
    /// <returns>request</returns>
    T Build();
  }
}
