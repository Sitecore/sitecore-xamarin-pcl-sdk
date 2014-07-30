﻿namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using System;
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
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(database, this.GetType().Name + ".Database");

      BaseValidator.CheckForTwiceSetAndThrow(this.database, this.GetType().Name + ".Database");

      this.database = database;
      return this;
    }

    public IDeleteItemRequestBuilder<T> AddScope(ICollection<ScopeType> scope)
    {
      BaseValidator.CheckNullAndThrow(scope, this.GetType().Name + ".Scope");
    
      var scopeParams = new ScopeParameters(this.scopeParameters);

      foreach (var singleScope in scope)
      {
        if (!scopeParams.AddScope(singleScope))
        {
          throw new InvalidOperationException(this.GetType().Name + ".Scope : Adding scope parameter duplicates is forbidden");
        }
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
