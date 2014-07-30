namespace Sitecore.MobileSDK.UrlBuilder.QueryParameters
{
  using System;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public class ScopeParameters : IScopeParameters
  {
    #region Copy Constructor
    public ScopeParameters(IScopeParameters other = null)
    {
      if (null == other)
      {
        return;
      }

      this.accumulatedScope = new List<ScopeType>(other.OrderedScopeSequence);
    }

    public ScopeParameters ShallowCopyScopeParametersClass()
    {
      return new ScopeParameters(this);
    }

    public IScopeParameters ShallowCopyScopeParametersInterface()
    {
      return this.ShallowCopyScopeParametersClass();
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


    public bool AddScope(ScopeType scope)
    {
      if (this.accumulatedScope.Contains(scope))
      {
        return false;
        //        throw new InvalidOperationException("Adding scope parameter duplicates is forbidden");
      }

      this.accumulatedScope.Add(scope);
      return true;
    }

    #region Properties
    public IEnumerable<ScopeType> OrderedScopeSequence
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

