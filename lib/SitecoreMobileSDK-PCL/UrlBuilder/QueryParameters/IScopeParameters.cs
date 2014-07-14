namespace Sitecore.MobileSDK.UrlBuilder.QueryParameters
{
  using System;
  using System.Collections;
  using System.Collections.Generic;


  public interface IScopeParameters
  {
    IScopeParameters ShallowCopyScopeParametersInterface();
    IEnumerable<ScopeType> OrderedScopeSequence { get; }


    bool ParentScopeIsSet   { get; }
    bool SelfScopeIsSet     { get; }
    bool ChildrenScopeIsSet { get; }
  }
}

