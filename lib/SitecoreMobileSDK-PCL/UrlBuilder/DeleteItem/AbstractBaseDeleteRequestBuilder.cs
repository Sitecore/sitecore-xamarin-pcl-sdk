namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using System;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public abstract class AbstractBaseDeleteRequestBuilder<T> : IBaseDeleteRequestParametersBuilder<T>
    where T : class
  {
    public IBaseDeleteRequestParametersBuilder<T> Database(string sitecoreDatabase)
    {
      if (string.IsNullOrWhiteSpace(sitecoreDatabase))
      {
        throw new ArgumentException("AbstractBaseDeleteRequestBuilder.Database : The input cannot be null or empty");
      }
      else if (!string.IsNullOrEmpty(this.database))
      {
        throw new InvalidOperationException("AbstractBaseDeleteRequestBuilder.Database : The database cannot be assigned twice");
      }

      return this;
    }

    public IBaseDeleteRequestParametersBuilder<T> AddScope(ICollection<ScopeType> scope)
    {
      ScopeParameters scopeParams = new ScopeParameters(this.scopeParameters);

      foreach (ScopeType singleScope in scope)
      {
        scopeParams.AddScope(singleScope);
      }
      this.scopeParameters = scopeParams;

      return this;
    }

    public IBaseDeleteRequestParametersBuilder<T> AddScope(params ScopeType[] scope)
    {
      ICollection<ScopeType> castedScope = (ICollection<ScopeType>)scope;
      return this.AddScope(castedScope);
    }

    public abstract T Build();

    protected string database;
    protected ScopeParameters scopeParameters;
    protected ISessionConfig sessionConfig;
  }
}
