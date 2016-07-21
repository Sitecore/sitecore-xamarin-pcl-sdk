namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public interface IBaseReadRequestParametersBuilder<T> : IBaseRequestParametersBuilder<T>
  where T : class
  {
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
  }
}

