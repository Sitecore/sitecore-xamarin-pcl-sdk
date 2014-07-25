
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
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.SessionSettings;

  using Sitecore.MobileSDK.Authenticate;
  using Sitecore.MobileSDK.CrudTasks;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.TaskFlow;
  using Sitecore.MobileSDK.UrlBuilder.DeleteItem;
  using Sitecore.MobileSDK.UserRequest;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;
  using Sitecore.MobileSDK.UrlBuilder.ItemByPath;
  using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;


  public class ScApiSession : ISitecoreWebApiSession
  {
    public ScApiSession(SessionConfig config, ItemSource defaultSource = null)
    {
      if (null == config)
      {
        throw new ArgumentNullException("ScApiSession.config cannot be null");
      }

      this.requestMerger = new UserRequestMerger(config, defaultSource);
      this.sessionConfig = config.ShallowCopy();

      this.httpClient = new HttpClient();
    }

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

    public IWebApiCredentials Credentials
    {
      get
      {
        return this.sessionConfig;
      }
    }

    #region Forbidden Methods

    private ScApiSession()
    {
    }

    #endregion Forbidden Methods

    #region Encryption

    protected virtual async Task<PublicKeyX509Certificate> GetPublicKeyAsync(CancellationToken cancelToken = default(CancellationToken))
    {
      try
      {
        var sessionConfigBuilder = new SessionConfigUrlBuilder(this.restGrammar, this.webApiGrammar);
        var taskFlow = new GetPublicKeyTasks(sessionConfigBuilder, this.restGrammar, this.webApiGrammar, this.httpClient);

        PublicKeyX509Certificate result = await RestApiCallFlow.LoadRequestFromNetworkFlow(this.sessionConfig, taskFlow, cancelToken);
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
      if (this.sessionConfig.IsAnonymous())
      {
        return new AnonymousSessionCryptor();
      }
      else
      {
        // TODO : flow should be responsible for caching. Do not hard code here
        this.publicCertifiacte = await this.GetPublicKeyAsync(cancelToken);
        return new AuthenticedSessionCryptor(this.sessionConfig.Username, this.sessionConfig.Password, this.publicCertifiacte);
      }
    }

    #endregion Encryption

    #region GetItems

    public async Task<ScItemsResponse> ReadItemAsync(IReadItemsByIdRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      IReadItemsByIdRequest requestCopy = request.DeepCopyGetItemByIdRequest();

      ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken);
      IReadItemsByIdRequest autocompletedRequest = this.requestMerger.FillReadItemByIdGaps(requestCopy);

      var taskFlow = new GetItemsByIdTasks(new ItemByIdUrlBuilder(this.restGrammar, this.webApiGrammar), this.httpClient, cryptor);

      return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
    }

    public async Task<ScItemsResponse> ReadItemAsync(IReadItemsByPathRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      IReadItemsByPathRequest requestCopy = request.DeepCopyGetItemByPathRequest();

      ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken);
      IReadItemsByPathRequest autocompletedRequest = this.requestMerger.FillReadItemByPathGaps(requestCopy);

      var taskFlow = new GetItemsByPathTasks(new ItemByPathUrlBuilder(this.restGrammar, this.webApiGrammar), this.httpClient, cryptor);

      return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
    }

    public async Task<ScItemsResponse> ReadItemAsync(IReadItemsByQueryRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      IReadItemsByQueryRequest requestCopy = request.DeepCopyGetItemByQueryRequest();

      ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken);
      IReadItemsByQueryRequest autocompletedRequest = this.requestMerger.FillReadItemByQueryGaps(requestCopy);

      var taskFlow = new GetItemsByQueryTasks(new ItemByQueryUrlBuilder(this.restGrammar, this.webApiGrammar), this.httpClient, cryptor);
      return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
    }

    public async Task<Stream> DownloadResourceAsync(IReadMediaItemRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      IReadMediaItemRequest requestCopy = request.DeepCopyReadMediaRequest();
      IReadMediaItemRequest autocompletedRequest = this.requestMerger.FillReadMediaItemGaps(requestCopy);

      MediaItemUrlBuilder urlBuilder = new MediaItemUrlBuilder(this.restGrammar, this.sessionConfig, autocompletedRequest.ItemSource);

      var taskFlow = new GetResourceTask(urlBuilder, this.httpClient);
      return await RestApiCallFlow.LoadResourceFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
    }

    #endregion GetItems

    #region CreateItems

    public async Task<ScItemsResponse> CreateItemAsync(ICreateItemByIdRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      ICreateItemByIdRequest requestCopy = request.DeepCopyCreateItemByIdRequest();

      ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken);

      ICreateItemByIdRequest autocompletedRequest = this.requestMerger.FillCreateItemByIdGaps(requestCopy);

      var taskFlow = new CreateItemByIdTask(new CreateItemByIdUrlBuilder(this.restGrammar, this.webApiGrammar), this.httpClient, cryptor);

      return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
    }

    public async Task<ScItemsResponse> CreateItemAsync(ICreateItemByPathRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      ICreateItemByPathRequest requestCopy = request.DeepCopyCreateItemByPathRequest();

      ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken);

      ICreateItemByPathRequest autocompletedRequest = this.requestMerger.FillCreateItemByPathGaps(requestCopy);

      var taskFlow = new CreateItemByPathTask(new CreateItemByPathUrlBuilder(this.restGrammar, this.webApiGrammar), this.httpClient, cryptor);

      return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
    }

    #endregion CreateItems

    #region DeleteItems

    public async Task<ScDeleteItemsResponse> DeleteItemAsync(IDeleteItemsByIdRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      IDeleteItemsByIdRequest requestCopy = request.DeepCopyDeleteItemRequest();

      ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken);

      IDeleteItemsByIdRequest autocompletedRequest = this.requestMerger.FillDeleteItemByIdGaps(requestCopy);

      var taskFlow = new DeleteItemTasks<IDeleteItemsByIdRequest>(new DeleteItemByIdUrlBuilder(this.restGrammar, this.webApiGrammar),
        this.httpClient, cryptor);

      return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
    }

    public async Task<ScDeleteItemsResponse> DeleteItemAsync(IDeleteItemsByPathRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      IDeleteItemsByPathRequest requestCopy = request.DeepCopyDeleteItemRequest();

      ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken);

      IDeleteItemsByPathRequest autocompletedRequest = this.requestMerger.FillDeleteItemByPathGaps(requestCopy);

      var taskFlow = new DeleteItemTasks<IDeleteItemsByPathRequest>(new DeleteItemByPathUrlBuilder(this.restGrammar, this.webApiGrammar),
        this.httpClient, cryptor);

      return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
    }

    public async Task<ScDeleteItemsResponse> DeleteItemAsync(IDeleteItemsByQueryRequest request, CancellationToken cancelToken = default(CancellationToken))
    {
      IDeleteItemsByQueryRequest requestCopy = request.DeepCopyDeleteItemRequest();

      ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync(cancelToken);

      IDeleteItemsByQueryRequest autocompletedRequest = this.requestMerger.FillDeleteItemByQueryGaps(requestCopy);

      var taskFlow = new DeleteItemTasks<IDeleteItemsByQueryRequest>(new DeleteItemByQueryUrlBuilder(this.restGrammar, this.webApiGrammar), 
        this.httpClient, cryptor);

      return await RestApiCallFlow.LoadRequestFromNetworkFlow(autocompletedRequest, taskFlow, cancelToken);
    }

    #endregion DeleteItems


    #region Authentication

    public async Task<bool> AuthenticateAsync(CancellationToken cancelToken = default(CancellationToken))
    {
      var sessionUrlBuilder = new SessionConfigUrlBuilder(this.restGrammar, this.webApiGrammar);
      var cryptor = await this.GetCredentialsCryptorAsync(cancelToken);

      var taskFlow = new AuthenticateTasks(this.restGrammar, this.webApiGrammar, sessionUrlBuilder, this.httpClient, cryptor);

      WebApiJsonStatusMessage result = await RestApiCallFlow.LoadRequestFromNetworkFlow(this.sessionConfig, taskFlow, cancelToken);

      return result.StatusCode == 200;
    }

    #endregion Authentication


    #region Private Variables

    private readonly UserRequestMerger requestMerger;
    private readonly HttpClient httpClient;

    protected readonly SessionConfig sessionConfig;

    private readonly IRestServiceGrammar restGrammar = RestServiceGrammar.ItemWebApiV2Grammar();
    private readonly IWebApiUrlParameters webApiGrammar = WebApiUrlParameters.ItemWebApiV2UrlParameters();


    private PublicKeyX509Certificate publicCertifiacte;

    #endregion Private Variables
  }
}
