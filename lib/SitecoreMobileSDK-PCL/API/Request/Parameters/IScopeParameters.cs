namespace Sitecore.MobileSDK.API.Request.Parameters
{
  using System.Collections.Generic;

  /// <summary>
  /// Interface that represents collection of "Scope" parameters for requests.
  ///  </summary>
  public interface IScopeParameters
  {
    /// <summary>
    /// Performs shallow copy of scope parameters.
    /// </summary>
    /// <returns><seealso cref="IScopeParameters"/></returns>
    IScopeParameters ShallowCopyScopeParametersInterface();

    /// <summary>
    /// Gets the ordered scopes parameters.
    /// <seealso cref="ScopeType"/>
    /// </summary>
    IEnumerable<ScopeType> OrderedScopeSequence { get; }

    /// <summary>
    /// Gets a value indicating whether Parent Scope is set.
    /// </summary>
    /// <value>
    ///   <c>true</c> if Parent Scope is set otherwise, <c>false</c>.
    /// </value>
    bool ParentScopeIsSet { get; }

    /// <summary>
    /// Gets a value indicating whether Self Scope is set.
    /// </summary>
    /// <value>
    ///   <c>true</c> if Self Scope is set otherwise, <c>false</c>.
    /// </value>
    bool SelfScopeIsSet { get; }

    /// <summary>
    /// Gets a value indicating whether Children Scope is set.
    /// </summary>
    /// <value>
    ///   <c>true</c> if Children Scope is set otherwise, <c>false</c>.
    /// </value>
    bool ChildrenScopeIsSet { get; }
  }
}

