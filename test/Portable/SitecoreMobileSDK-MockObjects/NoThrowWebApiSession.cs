namespace MobileSDKUnitTest.Mock
{
  using System;
  using System.IO;
  using System.Threading;
  using System.Threading.Tasks;
  using System.Diagnostics;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.API.Request;

  using Sitecore.MobileSDK.Items;



  public class NoThrowWebApiSession : ISitecoreWebApiSession
  {
    private ISitecoreWebApiSession workerSession;

    public NoThrowWebApiSession(ISitecoreWebApiSession workerSession)
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
      catch
      {
        Debug.WriteLine("Suppressed unknown exception");
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

    public async Task<ScItemsResponse> ReadItemAsync(IReadItemsByQueryRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      return await this.InvokeNoThrow(this.workerSession.ReadItemAsync(request, cancelToken));
    }

    public async Task<Stream> DownloadResourceAsync(IMediaResourceDownloadRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      return await this.InvokeNoThrow(this.workerSession.DownloadResourceAsync(request, cancelToken));
    }

    public async Task<string> ReadRenderingHTMLAsync(IGetRenderingHtmlRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      return await this.InvokeNoThrow(this.workerSession.ReadRenderingHTMLAsync(request, cancelToken));
    }

    #endregion GetItems

    #region CreateItems

    public async Task<ScItemsResponse> CreateItemAsync(ICreateItemByIdRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      return await this.InvokeNoThrow(this.workerSession.CreateItemAsync(request, cancelToken));
    }

    public async Task<ScItemsResponse> CreateItemAsync(ICreateItemByPathRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      return await this.InvokeNoThrow(this.workerSession.CreateItemAsync(request, cancelToken));
    }

    #endregion CreateItems

    #region Update Items

    public async Task<ScItemsResponse> UpdateItemAsync(IUpdateItemByIdRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      return await this.InvokeNoThrow(this.workerSession.UpdateItemAsync(request, cancelToken));
    }

    public async Task<ScItemsResponse> UpdateItemAsync(IUpdateItemByPathRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      return await this.InvokeNoThrow(this.workerSession.UpdateItemAsync(request, cancelToken));
    }

    #endregion Update Items

    #region DeleteItems

    public async Task<ScDeleteItemsResponse> DeleteItemAsync(IDeleteItemsByIdRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      return await this.InvokeNoThrow(this.workerSession.DeleteItemAsync(request, cancelToken));
    }

    public async Task<ScDeleteItemsResponse> DeleteItemAsync(IDeleteItemsByPathRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      return await this.InvokeNoThrow(this.workerSession.DeleteItemAsync(request, cancelToken));
    }

    public async Task<ScDeleteItemsResponse> DeleteItemAsync(IDeleteItemsByQueryRequest request, CancellationToken cancelToken = default(CancellationToken))
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

