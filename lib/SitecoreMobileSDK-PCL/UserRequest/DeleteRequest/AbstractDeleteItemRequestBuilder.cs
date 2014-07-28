namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
  using Sitecore.MobileSDK.UserRequest.DeleteRequest;
  using Sitecore.MobileSDK.Validators;

  public abstract class AbstractDeleteItemRequestBuilder<T> : IDeleteItemRequestBuilder<T>
    where T : class
  {
    protected string database;
    protected ScopeParameters scopeParameters;
    protected ISessionConfig sessionConfig;

    public abstract T Build();

    public IDeleteItemRequestBuilder<T> Database(string database)
    {
      if (string.IsNullOrWhiteSpace(database))
      {
        BaseValidator.ThrowNullOrEmptyParameterException(this.GetType().Name + ".database");
      }

      if (!string.IsNullOrEmpty(this.database))
      {
        BaseValidator.ThrowParameterSetTwiceException(this.GetType().Name + ".database");
      }

      this.database = database;
      return this;
    }

    public IDeleteItemRequestBuilder<T> AddScope(ICollection<ScopeType> scope)
    {
      var scopeParams = new ScopeParameters(this.scopeParameters);

      foreach (var singleScope in scope)
      {
        scopeParams.AddScope(singleScope);
      }
      this.scopeParameters = scopeParams;

      return this;
    }

    public IDeleteItemRequestBuilder<T> AddScope(params ScopeType[] scope)
    {
      ICollection<ScopeType> castedScope = scope;
      return this.AddScope(castedScope);
    }
  }
}
