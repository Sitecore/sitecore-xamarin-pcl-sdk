﻿namespace Sitecore.MobileSDK.API.Request.Template
{
  /// <summary>
  /// Interface that specifies flow for creation of request to create item.
  /// </summary>
  /// <typeparam name="T">Type of request</typeparam>
  public interface ISetTemplateBuilder<T> where T : class
  {
    /// <summary>
    /// Sepcifies item's template path, relative to the default template's folder("/sitecore/templates").
    /// Must stars with the '/' symbol.
    /// </summary>
    /// <param name="templatePath">The template path.</param>
    /// <returns><seealso cref="ISetNewItemNameBuilder{T}"/></returns>
    ISetNewItemNameBuilder<T> ItemTemplatePath(string templatePath);

    /// <summary>
    /// Specifies item's template GUID. Must containg appropriate template item GUID value enclosed in curly braces.
    /// For example: "{B174990B-37B1-4A60-9C7D-891B521E1B76}"
    /// </summary>
    /// <param name="templateId">The template GUID.</param>
    /// <returns><seealso cref="ISetNewItemNameBuilder{T}"/></returns>
    ISetNewItemNameBuilder<T> ItemTemplateId(string templateId);

  }
}

