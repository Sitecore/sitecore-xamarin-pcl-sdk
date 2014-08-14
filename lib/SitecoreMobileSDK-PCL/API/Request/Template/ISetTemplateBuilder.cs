namespace Sitecore.MobileSDK.API.Request.Template
{
  /// <summary>
  /// Interface that specifies flow for creation of item.
  /// </summary>
  /// <typeparam name="T">Type of request</typeparam>
  public interface ISetTemplateBuilder<T> where T : class
  {
    /// <summary>
    /// Sepcifies item's template path.
    /// </summary>
    /// <param name="templatePath">The template path.</param>
    /// <returns><see cref="ISetNewItemNameBuilder{T}"/></returns>
    ISetNewItemNameBuilder<T> ItemTemplatePath(string templatePath);

    /// <summary>
    /// Specifies item's template GUID.
    /// </summary>
    /// <param name="templateId">The template GUID.</param>
    /// <returns><see cref="ISetNewItemNameBuilder{T}"/></returns>
    ISetNewItemNameBuilder<T> ItemTemplateId(string templateId);

    //    ICreateItemRequestParametersBuilder<T> BranchId(string branchId);

    /// <summary>
    /// Specifies branch GUID to create item from.
    /// </summary>
    /// <param name="branchId">The branch GUID.</param>
    /// <returns><see cref="ISetNewItemNameBuilder{T}"/></returns>
    ISetNewItemNameBuilder<T> BranchId(string branchId);
  }
}

