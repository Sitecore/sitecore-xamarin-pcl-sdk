namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using System;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public abstract class AbstractDeleteItemRequestBuilder<T> : IBaseDeleteRequestParametersBuilder<T>
    where T : class
  {
    protected string database;
    protected ScopeParameters scopeParameters;
    protected ISessionConfig sessionConfig;

    public abstract T Build();

    public IBaseDeleteRequestParametersBuilder<T> Database(string sitecoreDatabase)
    {
      if (string.IsNullOrWhiteSpace(sitecoreDatabase))
      {
        throw new ArgumentException("AbstractDeleteItemRequestBuilder.Database : " +
                                    "The input cannot be null or empty");
      }

      if (!string.IsNullOrEmpty(this.database))
      {
        throw new InvalidOperationException("AbstractDeleteItemRequestBuilder.Database : " +
                                            "The database cannot be assigned twice");
      }

      this.database = sitecoreDatabase;
      return this;
    }

    public IBaseDeleteRequestParametersBuilder<T> AddScope(ICollection<ScopeType> scope)
    {
      var scopeParams = new ScopeParameters(this.scopeParameters);

      foreach (var singleScope in scope)
      {
        scopeParams.AddScope(singleScope);
      }
      this.scopeParameters = scopeParams;

      return this;
    }

    public IBaseDeleteRequestParametersBuilder<T> AddScope(params ScopeType[] scope)
    {
      var castedScope = (ICollection<ScopeType>)scope;
      return this.AddScope(castedScope);
    }
  }
}
