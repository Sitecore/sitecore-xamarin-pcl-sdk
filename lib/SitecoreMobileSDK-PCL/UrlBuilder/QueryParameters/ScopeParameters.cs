using System;

namespace Sitecore.MobileSDK.UrlBuilder.QueryParameters
{
  public class ScopeParameters
  {
    public ScopeParameters()
    {
    }

    public ScopeParameters ShallowCopy()
    {
      ScopeParameters copy = new ScopeParameters();

      copy.parentScopeIsSet    = this.parentScopeIsSet;
      copy.selfScopeIsSet      = this.selfScopeIsSet;
      copy.childrenScopeIsSet  = this.childrenScopeIsSet;

      return copy;
    }



    public void AddScope(ScopeType scope)
    {
      switch (scope)
      {
        case ScopeType.Self:
        {
          if (this.SelfScopeIsSet)
          {
            this.OnValidationError();
          }

          this.SelfScopeIsSet = true;
        }
        break;
        case ScopeType.Parent:
        {
          if (this.ParentScopeIsSet)
          {
            this.OnValidationError();
          }

          this.ParentScopeIsSet = true;
        }
        break;
        case ScopeType.Children:
        {
          if (this.ChildrenScopeIsSet)
          {
            this.OnValidationError();
          }

          this.ChildrenScopeIsSet = true;
        }
        break;
      }

    }

    public bool ParentScopeIsSet
    {
      private set
      { 
        this.parentScopeIsSet = value;
      }

      get
      { 
        return this.parentScopeIsSet;
      }
    }

    public bool SelfScopeIsSet
    {
      private set
      { 
        this.selfScopeIsSet = value;
      }

      get
      { 
        return this.selfScopeIsSet;
      }
    }

    public bool ChildrenScopeIsSet
    {
      private set
      { 
        this.childrenScopeIsSet = value;
      }

      get
      { 
        return this.childrenScopeIsSet;
      }
    }

    private void OnValidationError()
    {
      throw new InvalidOperationException("Adding scope parameter duplicates is forbidden");
    }

    private bool parentScopeIsSet    = false;
    private bool selfScopeIsSet      = false;
    private bool childrenScopeIsSet  = false;
  }
}

