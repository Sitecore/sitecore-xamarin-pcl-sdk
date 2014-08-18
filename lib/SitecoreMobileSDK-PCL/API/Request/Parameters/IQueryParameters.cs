namespace Sitecore.MobileSDK.API.Request.Parameters
{
  using System.Collections.Generic;

  /// <summary>
  /// Interface represents 3 parameters needed for read item requests:
  /// <para><c>Scope</c></para><para><c>Payload</c></para><para><c>Fields</c></para>
  /// </summary>
  public interface IQueryParameters
  {
    /// <summary>
    /// Performs deep the copy of <see cref="IQueryParameters"/>.
    /// </summary>
    /// <returns><see cref="IQueryParameters"/></returns>
    IQueryParameters DeepCopy();

    /// <summary>
    /// Gets the scope parameters.
    /// </summary>
    /// <value>
    /// The scope parameters.
    /// </value>
    /// <returns><see cref="IScopeParameters"/></returns>
    IScopeParameters ScopeParameters { get; }

    /// <summary>
    /// Gets the payload.
    /// </summary>
    /// <value>
    /// The payload.
    /// </value>
    /// <returns><see cref="PayloadType"/></returns>
    PayloadType? Payload { get; }

    /// <summary>
    /// Gets the fields.
    /// </summary>
    /// <value>
    /// The fields.
    /// </value>
    /// <returns><see cref="IEnumerable{T}"/> with fields</returns>
    IEnumerable<string> Fields { get; }
  }
}
