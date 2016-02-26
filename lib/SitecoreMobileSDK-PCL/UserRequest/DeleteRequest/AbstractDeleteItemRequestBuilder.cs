namespace Sitecore.MobileSDK.UserRequest.DeleteRequest
{
  using System;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
  using Sitecore.MobileSDK.Validators;

  public abstract class AbstractDeleteItemRequestBuilder<T> : IDeleteItemRequestBuilder<T>
    where T : class
  {
    protected string database;
    protected ISessionConfig sessionConfig;

    public abstract T Build();

    public IDeleteItemRequestBuilder<T> Database(string database)
    {
      if (string.IsNullOrEmpty(database))
      {
        return this;
      }

      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(database, this.GetType().Name + ".Database");

      BaseValidator.CheckForTwiceSetAndThrow(this.database, this.GetType().Name + ".Database");

      this.database = database;
      return this;
    }

  }
}
