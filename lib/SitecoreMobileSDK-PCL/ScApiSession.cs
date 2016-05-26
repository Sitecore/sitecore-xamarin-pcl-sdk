using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Sitecore.MobileSDK.UrlBuilder.Children;
using Sitecore.MobileSDK.UrlBuilder.Search;

namespace Sitecore.MobileSDK
{
  using System;
  using System.IO;
  using System.Net.Http;
  using System.Threading;
  using System.Threading.Tasks;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Exceptions;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.API.MediaItem;
  using Sitecore.MobileSDK.PasswordProvider;
  using Sitecore.MobileSDK.PasswordProvider.Interface;
  using Sitecore.MobileSDK.Session;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.TaskFlow;
  using Sitecore.MobileSDK.CrudTasks;
  using Sitecore.MobileSDK.CrudTasks.Resource;
  using Sitecore.MobileSDK.Validators;
  using Sitecore.MobileSDK.Authenticate;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UserRequest;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;
  using Sitecore.MobileSDK.UrlBuilder.ItemByPath;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;
  using Sitecore.MobileSDK.UrlBuilder.UpdateItem;
  using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;
  using Sitecore.MobileSDK.UrlBuilder.DeleteItem;
  using Sitecore.MobileSDK.UrlBuilder.RenderingHtml;

  public class ScApiSession : ISitecoreSSCSession
  {
    public ScApiSession(
      ISessionConfig config,
      ISSCCredentials credentials,
      IMediaLibrarySettings mediaSettings,
      ItemSource defaultSource = null)
    {
      if (null == config)
      {
        throw new ArgumentNullException("ScApiSession.config cannot be null");
      }

      this.sessionConfig = config.SessionConfigShallowCopy();
      this.requestMerger = new UserRequestMerger(this.sessionConfig, defaultSource);

      if (null != credentials)
      {
        this.credentials = credentials.CredentialsShallowCopy();
      }

      if (null != mediaSettings)
      {
        this.mediaSettings = mediaSettings.MediaSettingsShallowCopy();
      }

      this.cookies = new CookieContainer();
      this.handler = new HttpClientHandler();
      this.handler.CookieContainer = cookies;
      this.httpClient = new HttpClient(this.handler);

    }

    #region IDisposable
    void ReleaseResources()
    {
      Exception credentialsException = null;
      Exception httpClientException = null;

      if (null != this.credentials)
      {
        try
        {
          this.credentials.Dispose();
        }
        catch (Exception ex)
        {
          credentialsException = ex;
        }
        this.credentials = null;
      }

      if (null != this.httpClient)
      {
        try
        {
          this.httpClient.Dispose();
        }
        catch (Exception ex)
        {
          httpClientException = ex;
        }
        this.httpClient = null;
      }

      if (null != credentialsException)
      {
        throw credentialsException;
      }
      else if (null != httpClientException)
      {
        throw httpClientException;
      }
    }
      
    public virtual void Dispose()
    {
      this.ReleaseResources();
    }

    ~ScApiSession() 
    {

    }
    #endregion IDisposable

    #region ISitecoreSSCSessionState
    public IItemSource DefaultSource
    {
      get
      {
        return this.requestMerger.ItemSourceMerger.DefaultSource;
      }
    }

    public ISessionConfig Config
    {
      get
      {
        return this.sessionConfig;
      }
    }

    public ISSCCredentials Credentials
    {
      get
      {
        return this.credentials;
      }
    }

    public IMediaLibrarySettings MediaLibrarySettings 
    { 
      get
      {
        return this.mediaSettings;
      }
    }
    #endregion

    #region Forbidden Methods

    private ScApiSession()
    {
    }

    #endregion Forbidden Methods

    #region Encryption

    protected virtual async Task<string> GetPublicKeyAsync(CancellationToken cancelToken = default(CancellationToken))
    {
      try
      {
        var sessionConfigBuilder = new SessionConfigUrlBuilder(this.restGrammar, this.sscGrammar);
        var taskFlow = new GetPublicKeyTasks(sessionConfigBuilder, this.restGrammar, this.sscGrammar, this.httpClient);

        string result = await RestApiCallFlow.LoadRequestFromNetworkFlow(this.sessionConfig, taskFlow, cancelToken);
        this.publicCertifiacte = result;
      }
      catch (ObjectDisposedException)
      {
        // CancellationToken.ThrowIfCancellationRequested()
        throw;
      }
      catch (OperationCanceledException)
      {
        // CancellationToken.ThrowIfCancellationRequested()
        // and TaskCanceledException
        throw;
      }
      catch (SitecoreMobileSdkException ex)
      {
        // throw unwrapped exception as if GetPublicKeyAsync() is an atomic phase
        throw new RsaHandshakeException("[Sitecore Mobile SDK] Public key not received properly", ex.InnerException);
      }
      catch (Exception ex)
      {
        throw new RsaHandshakeException("[Sitecore Mobile SDK] Public key not received properly", ex);
      }

      return this.publicCertifiacte;
    }

    protected virtual async Task<ICredentialsHeadersCryptor> GetCredentialsCryptorAsync(CancellationToken cancelToken = default(CancellationToken))
    {
      bool isAnonymous = (null == this.Credentials) || SSCCredentialsValidator.IsAnonymousSession(this.Credentials);

      if (isAnonymous)
      {
        return new AnonymousSessionCryptor();
      }
      else if (SSCCredentialsValidator.IsValidCredentials(this.Credentials))
      {
        #if !ENCRYPTION_DISABLED
        // TODO : flow should be responsible for caching. Do not hard code here
        this.publicCertifiacte = await this.GetPublicKeyAsync(cancelToken);
        #else
        this.publicCertifiacte = null;
        #endif

        // TODO : credentials should not be passed as plain text strings. 
        // TODO : Use ```SecureString``` class
        return new AuthenticatedSessionCryptor(this.credentials.Username, this.credentials.Password, this.publicCertifiacte);
      }
      else
      {
        throw new ArgumentException(this.GetType().Name + " : web API credentials are not valid");
      }
    }

    #endregion Encryption

    #region SearchItems

    public async Task<ScItemsResponse> RunStoredSearchAsync(ISitecoreStoredSearchRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      ISitecoreStoredSearchRequest requestCopy = request.DeepCopySitecoreStoredSearchRequest();

      using (ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken))
      {
        ISitecoreStoredSearchRequest autocompletedRequest = this.requestMerger.FillSitecoreStoredSearchGaps(requestCopy);

        var urlBuilder = new RunStoredSearchUrlBuilder(this.restGrammar, this.sscGrammar);
        var taskFlow = new RunStoredSearchTasks(urlBuilder, this.httpClient, cryptor);

        return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
      }
    }

    public async Task<ScItemsResponse> RunStoredQuerryAsync(IReadItemsByIdRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      IReadItemsByIdRequest requestCopy = request.DeepCopyGetItemByIdRequest();

      using (ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken))
      {
        IReadItemsByIdRequest autocompletedRequest = this.requestMerger.FillReadItemByIdGaps(requestCopy);

        var urlBuilder = new RunStoredQuerryUrlBuilder(this.restGrammar, this.sscGrammar);
        var taskFlow = new RunStoredQuerryTasks(urlBuilder, this.httpClient, cryptor);

        return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
      }
    }

    public async Task<ScItemsResponse> RunSitecoreSearchAsync(ISitecoreSearchRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      ISitecoreSearchRequest requestCopy = request.DeepCopySitecoreSearchRequest();

      using (ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken))
      {
        ISitecoreSearchRequest autocompletedRequest = this.requestMerger.FillSitecoreSearchGaps(requestCopy);

        var urlBuilder = new RunSitecoreSearchUrlBuilder(this.restGrammar, this.sscGrammar);
        var taskFlow = new RunSitecoreSearchTasks(urlBuilder, this.httpClient, cryptor);

        return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
      }
    }

    #endregion SearchItems

    #region GetItems

    public async Task<ScItemsResponse> ReadChildrenAsync(IReadItemsByIdRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      IReadItemsByIdRequest requestCopy = request.DeepCopyGetItemByIdRequest();

      using (ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken))
      {
        IReadItemsByIdRequest autocompletedRequest = this.requestMerger.FillReadItemByIdGaps(requestCopy);

        var urlBuilder = new ChildrenByIdUrlBuilder(this.restGrammar, this.sscGrammar);
        var taskFlow = new GetChildrenByIdTasks(urlBuilder, this.httpClient, cryptor);

        return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
      }
    }

    public async Task<ScItemsResponse> ReadItemAsync(IReadItemsByIdRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      IReadItemsByIdRequest requestCopy = request.DeepCopyGetItemByIdRequest();

      using (ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken))
      {
        IReadItemsByIdRequest autocompletedRequest = this.requestMerger.FillReadItemByIdGaps(requestCopy);

        var urlBuilder = new ItemByIdUrlBuilder(this.restGrammar, this.sscGrammar);
        var taskFlow = new GetItemsByIdTasks(urlBuilder, this.httpClient, cryptor);

        return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
      }
    }

    public async Task<ScItemsResponse> ReadItemAsync(IReadItemsByPathRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      IReadItemsByPathRequest requestCopy = request.DeepCopyGetItemByPathRequest();

      using (ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken))
      {
        IReadItemsByPathRequest autocompletedRequest = this.requestMerger.FillReadItemByPathGaps(requestCopy);

        var urlBuilder = new ItemByPathUrlBuilder(this.restGrammar, this.sscGrammar);
        var taskFlow = new GetItemsByPathTasks(urlBuilder, this.httpClient, cryptor);

        return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
      }
    }

    public async Task<ScItemsResponse> ReadItemAsync(IReadItemsByQueryRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      IReadItemsByQueryRequest requestCopy = request.DeepCopyGetItemByQueryRequest();

      using (ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken))
      {
        IReadItemsByQueryRequest autocompletedRequest = this.requestMerger.FillReadItemByQueryGaps(requestCopy);

        var urlBuilder = new ItemByQueryUrlBuilder(this.restGrammar, this.sscGrammar);
        var taskFlow = new GetItemsByQueryTasks(urlBuilder, this.httpClient, cryptor);

        return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
      }
    }

    public async Task<Stream> DownloadMediaResourceAsync(IMediaResourceDownloadRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      IMediaResourceDownloadRequest requestCopy = request.DeepCopyReadMediaRequest();
      IMediaResourceDownloadRequest autocompletedRequest = this.requestMerger.FillReadMediaItemGaps(requestCopy);
      DownloadStrategy downloadStrategyFromUser = this.mediaSettings.MediaDownloadStrategy;

      if (DownloadStrategy.Plain == downloadStrategyFromUser)
      {
        return await this.DownloadPlainMediaResourceAsync(autocompletedRequest, cancelToken);
      }
      else if (DownloadStrategy.Hashed == downloadStrategyFromUser)
      {
        return await this.DownloadHashedMediaResourceAsync(autocompletedRequest, cancelToken);
      }
      else
      {
        throw new ArgumentException("Unexpected media download strategy specified");
      }
    }

    private async Task<Stream> DownloadPlainMediaResourceAsync(IMediaResourceDownloadRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      MediaItemUrlBuilder urlBuilder = new MediaItemUrlBuilder(
        this.restGrammar,
        this.sscGrammar,
        this.sessionConfig,
        this.mediaSettings,
        request.ItemSource);

      var taskFlow = new GetResourceTask(urlBuilder, this.httpClient);
      return await RestApiCallFlow.LoadResourceFromNetworkFlow(request, taskFlow, cancelToken);
    }

    private async Task<Stream> DownloadHashedMediaResourceAsync(IMediaResourceDownloadRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      var cryptor = await this.GetCredentialsCryptorAsync(cancelToken);

      MediaItemUrlBuilder urlBuilder = new MediaItemUrlBuilder(
        this.restGrammar,
        this.sscGrammar,
        this.sessionConfig,
        this.mediaSettings,
        request.ItemSource);

      var hashUrlGetterFlow = new GetMediaContentHashTask(urlBuilder, this.httpClient, cryptor);
      string hashedMediaUrl = await RestApiCallFlow.LoadRequestFromNetworkFlow(request, hashUrlGetterFlow, cancelToken);

      try
      {
        Stream result = await this.httpClient.GetStreamAsync(hashedMediaUrl);
        return result;
      }
      catch (Exception ex)
      {
        throw new LoadDataFromNetworkException(TaskFlowErrorMessages.NETWORK_EXCEPTION_MESSAGE, ex);
      }
    }
    #endregion GetItems

    #region UploadImage

    public async Task<ScItemsResponse> UploadMediaResourceAsync(IMediaResourceUploadRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      IMediaResourceUploadRequest requestCopy = request.DeepCopyUploadMediaRequest();

      using (ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken))
      {
        IMediaResourceUploadRequest autocompletedRequest = this.requestMerger.FillUploadMediaGaps(requestCopy);

        var urlBuilder = new UploadMediaUrlBuilder(this.restGrammar, this.sscGrammar, this.sessionConfig, this.mediaSettings);
        var taskFlow = new UploadMediaTask(urlBuilder, this.httpClient, cryptor);

        return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
      }
    }

    #endregion UploadImage

    #region GetHTMLRendering

    public async Task<Stream> ReadRenderingHtmlAsync(IGetRenderingHtmlRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      IGetRenderingHtmlRequest requestCopy = request.DeepCopyGetRenderingHtmlRequest();

      using (ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken))
      {
        IGetRenderingHtmlRequest autocompletedRequest = this.requestMerger.FillGetRenderingHtmlGaps(requestCopy);

        var urlBuilder = new RenderingHtmlUrlBuilder(this.restGrammar, this.sscGrammar);
        var taskFlow = new GetRenderingHtmlTasks(urlBuilder, this.httpClient, cryptor);

        return await RestApiCallFlow.LoadResourceFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
      }
    }

    #endregion

    #region CreateItems

    public async Task<ScItemsResponse> CreateItemAsync(ICreateItemByIdRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      ICreateItemByIdRequest requestCopy = request.DeepCopyCreateItemByIdRequest();

      using (ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken))
      {
        ICreateItemByIdRequest autocompletedRequest = this.requestMerger.FillCreateItemByIdGaps(requestCopy);

        var urlBuilder = new CreateItemByIdUrlBuilder(this.restGrammar, this.sscGrammar);
        var taskFlow = new CreateItemByIdTask(urlBuilder, this.httpClient, cryptor);

        return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
      }
    }

    public async Task<ScItemsResponse> CreateItemAsync(ICreateItemByPathRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      ICreateItemByPathRequest requestCopy = request.DeepCopyCreateItemByPathRequest();
     
      using (ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken))
      {
        //TODO: @igk debug info remove later
        IEnumerable<Cookie> responseCookies = cookies.GetCookies(new Uri(this.Config.InstanceUrl)).Cast<Cookie>();
        foreach (Cookie cookie in responseCookies)
        {
          Debug.WriteLine(cookie.Name + ": " + cookie.Value);
        }
        
        ICreateItemByPathRequest autocompletedRequest = this.requestMerger.FillCreateItemByPathGaps(requestCopy);

        var urlBuilder = new CreateItemByPathUrlBuilder(this.restGrammar, this.sscGrammar);
        var taskFlow = new CreateItemByPathTask(urlBuilder, this.httpClient, cryptor);

        return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
      }
    }

    #endregion CreateItems

    #region Update Items

    public async Task<ScItemsResponse> UpdateItemAsync(IUpdateItemByIdRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      IUpdateItemByIdRequest requestCopy = request.DeepCopyUpdateItemByIdRequest();

      using (ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken))
      {
        IUpdateItemByIdRequest autocompletedRequest = this.requestMerger.FillUpdateItemByIdGaps(requestCopy);

        var urlBuilder = new UpdateItemByIdUrlBuilder(this.restGrammar, this.sscGrammar);
        var taskFlow = new UpdateItemByIdTask(urlBuilder, this.httpClient, cryptor);

        return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
      }
    }

    public async Task<ScItemsResponse> UpdateItemAsync(IUpdateItemByPathRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      IUpdateItemByPathRequest requestCopy = request.DeepCopyUpdateItemByPathRequest();

      using (ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken))
      {
        IUpdateItemByPathRequest autocompletedRequest = this.requestMerger.FillUpdateItemByPathGaps(requestCopy);

        var urlBuilder = new UpdateItemByPathUrlBuilder(this.restGrammar, this.sscGrammar);
        var taskFlow = new UpdateItemByPathTask(urlBuilder, this.httpClient, cryptor);

        return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
      }
    }

    #endregion Update Items

    #region DeleteItems

    public async Task<ScDeleteItemsResponse> DeleteItemAsync(IDeleteItemsByIdRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      IDeleteItemsByIdRequest requestCopy = request.DeepCopyDeleteItemRequest();

      using (ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken))
      {
        IDeleteItemsByIdRequest autocompletedRequest = this.requestMerger.FillDeleteItemByIdGaps(requestCopy);

        var urlBuilder = new DeleteItemByIdUrlBuilder(this.restGrammar, this.sscGrammar);
        var taskFlow = new DeleteItemTasks<IDeleteItemsByIdRequest>(urlBuilder, this.httpClient, cryptor);

        return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
      }
    }

    public async Task<ScDeleteItemsResponse> DeleteItemAsync(IDeleteItemsByPathRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      IDeleteItemsByPathRequest requestCopy = request.DeepCopyDeleteItemRequest();

      using (ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken))
      {
        IDeleteItemsByPathRequest autocompletedRequest = this.requestMerger.FillDeleteItemByPathGaps(requestCopy);

        var urlBuilder = new DeleteItemByPathUrlBuilder(this.restGrammar, this.sscGrammar);
        var taskFlow = new DeleteItemTasks<IDeleteItemsByPathRequest>(urlBuilder, this.httpClient, cryptor);

        return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
      }
    }

    public async Task<ScDeleteItemsResponse> DeleteItemAsync(IDeleteItemsByQueryRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      IDeleteItemsByQueryRequest requestCopy = request.DeepCopyDeleteItemRequest();

      using (ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken))
      {
        IDeleteItemsByQueryRequest autocompletedRequest = this.requestMerger.FillDeleteItemByQueryGaps(requestCopy);

        var urlBuilder = new DeleteItemByQueryUrlBuilder(this.restGrammar, this.sscGrammar);
        var taskFlow = new DeleteItemTasks<IDeleteItemsByQueryRequest>(urlBuilder, this.httpClient, cryptor);

        return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
      }
    }

    #endregion DeleteItems

    #region Authentication

    public async Task<bool> AuthenticateAsync(CancellationToken cancelToken = default(CancellationToken))
    {
      var sessionUrlBuilder = new SessionConfigUrlBuilder(this.restGrammar, this.sscGrammar);
      using (var cryptor = await this.GetCredentialsCryptorAsync(cancelToken))
      {
        var taskFlow = new AuthenticateTasks(this.restGrammar, this.sscGrammar, sessionUrlBuilder, this.httpClient, cryptor);

        SSCJsonStatusMessage result = await RestApiCallFlow.LoadRequestFromNetworkFlow(this.sessionConfig, taskFlow, cancelToken);

        return result.StatusCode == 200;
      }
    }

    #endregion Authentication


    #region Private Variables

    private readonly UserRequestMerger requestMerger;
    private HttpClient httpClient;
    private CookieContainer cookies;
    private HttpClientHandler handler;

    protected readonly ISessionConfig sessionConfig;
    private ISSCCredentials credentials;
    private readonly IMediaLibrarySettings mediaSettings;


    private readonly IRestServiceGrammar restGrammar = RestServiceGrammar.ItemSSCV2Grammar();
    private readonly ISSCUrlParameters sscGrammar = SSCUrlParameters.ItemSSCV2UrlParameters();


    private string publicCertifiacte;

    #endregion Private Variables
  }
}
