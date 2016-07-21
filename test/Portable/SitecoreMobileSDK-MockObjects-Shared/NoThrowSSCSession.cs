﻿namespace Sitecore.MobileSDK.MockObjects
{
  using System;
  using System.Diagnostics;
  using System.IO;
  using System.Threading;
  using System.Threading.Tasks;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.MediaItem;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.PasswordProvider;
  using Sitecore.MobileSDK.PasswordProvider.Interface;

  public class NoThrowSSCSession : ISitecoreSSCSession
  {
    private ISitecoreSSCSession workerSession;

    public NoThrowSSCSession(ISitecoreSSCSession workerSession)
    {
      this.workerSession = workerSession;
    }

    public void Dispose()
    {
      if (null != this.workerSession)
      {
        this.workerSession.Dispose();
        this.workerSession = null;
      }
    }

    private async Task<TResult> InvokeNoThrow<TResult>(Task<TResult> task)
      where TResult : class
    {
      try
      {
        return await task;
      }
      catch (Exception ex)
      {
        Debug.WriteLine("Suppressed exception : " + Environment.NewLine + ex.ToString());
        return null;
      }
    }

    #region Getter Properties
    public IItemSource DefaultSource
    {
      get
      {
        return this.workerSession.DefaultSource;
      }
    }

    public ISessionConfig Config
    {
      get
      {
        return this.workerSession.Config;
      }
    }

    public IWebApiCredentials Credentials
    {
      get
      {
        return this.workerSession.Credentials;
      }
    }

    public IMediaLibrarySettings MediaLibrarySettings 
    {
      get
      {
        return this.workerSession.MediaLibrarySettings;
      }
    }
    #endregion 
   
    #region GetItems

    public async Task<ScItemsResponse> ReadItemAsync(IReadItemsByIdRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      return await this.InvokeNoThrow(this.workerSession.ReadItemAsync(request, cancelToken));
    }

    public async Task<ScItemsResponse> ReadItemAsync(IReadItemsByPathRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      return await this.InvokeNoThrow(this.workerSession.ReadItemAsync(request, cancelToken));
    }

    public async Task<Stream> DownloadMediaResourceAsync(IMediaResourceDownloadRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      return await this.InvokeNoThrow(this.workerSession.DownloadMediaResourceAsync(request, cancelToken));
    }

    #endregion GetItems

    #region CreateItems

    public async Task<ScCreateItemResponse> CreateItemAsync(ICreateItemByPathRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      return await this.InvokeNoThrow(this.workerSession.CreateItemAsync(request, cancelToken));
    }

    public async Task<ScItemsResponse> ReadChildrenAsync(IReadItemsByIdRequest request, CancellationToken cancelToken = default(CancellationToken))
    { 
      return await this.InvokeNoThrow(this.workerSession.ReadChildrenAsync(request, cancelToken));
    }

    #endregion CreateItems

    #region Update Items

    public async Task<ScItemsResponse> UpdateItemAsync(IUpdateItemByIdRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      return await this.InvokeNoThrow(this.workerSession.UpdateItemAsync(request, cancelToken));
    }

    public async Task<ScItemsResponse> RunStoredSearchAsync(ISitecoreStoredSearchRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      return await this.InvokeNoThrow(this.workerSession.RunStoredSearchAsync(request, cancelToken));
    }

    public async Task<ScItemsResponse> RunStoredQuerryAsync(IReadItemsByIdRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      return await this.InvokeNoThrow(this.workerSession.RunStoredQuerryAsync(request, cancelToken));
    }

    public async Task<ScItemsResponse> RunSitecoreSearchAsync(ISitecoreSearchRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      return await this.InvokeNoThrow(this.workerSession.RunSitecoreSearchAsync(request, cancelToken));
    }

    #endregion Update Items

    #region DeleteItems

    public async Task<ScDeleteItemsResponse> DeleteItemAsync(IDeleteItemsByIdRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      return await this.InvokeNoThrow(this.workerSession.DeleteItemAsync(request, cancelToken));
    }

    #endregion DeleteItems

    #region Authentication

    public async Task<bool> AuthenticateAsync(CancellationToken cancelToken = default(CancellationToken))
    {
      try
      {
        return await this.workerSession.AuthenticateAsync(cancelToken);
      }
      catch (Exception ex)
      {
        Debug.WriteLine("Suppressed exception : " + Environment.NewLine + ex.ToString());
        return false;
      }
    }

    #endregion Authentication

  }
}

