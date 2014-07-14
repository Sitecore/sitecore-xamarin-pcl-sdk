

namespace Sitecore.MobileSDK.UrlBuilder.QueryParameters
{
  using System;
  using System.Collections;
  using System.Collections.Generic;


  public class ScopeParameters
  {
    public ScopeParameters()
    {
    }

    #region Copy Constructor
    public ScopeParameters ShallowCopy()
    {
      ScopeParameters copy = new ScopeParameters();
      copy.accumulatedScope = this.CopyAccumulatedScope();

      return copy;
    }

    private List<ScopeType> CopyAccumulatedScope()
    {
      if (null == this.accumulatedScope)
      {
        return null;
      }

      var result = new List<ScopeType>(this.accumulatedScope);
      return result;
    }
    #endregion Copy Constructor


    public void AddScope(ScopeType scope)
    {
      if (this.accumulatedScope.Contains(scope))
      {
        throw new InvalidOperationException("Adding scope parameter duplicates is forbidden");
      }

      this.accumulatedScope.Add(scope);
    }

    #region Properties
    public IEnumerable<ScopeType> AccumulatedScope
    {
      get
      {
        return this.accumulatedScope;
      }
    }

    public bool ParentScopeIsSet
    {
      get
      { 
        return this.accumulatedScope.Contains(ScopeType.Parent);
      }
    }

    public bool SelfScopeIsSet
    {
      get
      { 
        return this.accumulatedScope.Contains(ScopeType.Self);
      }
    }

    public bool ChildrenScopeIsSet
    {
      get
      { 
        return this.accumulatedScope.Contains(ScopeType.Children);
      }
    }
    #endregion Properties

    #region Instance Variables
    private const int MAX_SCOPE_ARGS_COUNT = 3;
    private List<ScopeType> accumulatedScope = new List<ScopeType>(MAX_SCOPE_ARGS_COUNT);
    #endregion Instance Variables
  }
}

