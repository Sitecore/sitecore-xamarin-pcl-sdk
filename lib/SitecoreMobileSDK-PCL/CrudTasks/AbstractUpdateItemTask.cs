﻿namespace Sitecore.MobileSDK.CrudTasks
{
  using System.Threading.Tasks;
  using System.Net.Http;
  using System.Threading;
  using System.Text;
  using Sitecore.MobileSDK.PublicKey;

  internal abstract class AbstractUpdateItemTask<TRequest> : AbstractGetItemTask<TRequest>
    where TRequest : class
  {
    public AbstractUpdateItemTask(HttpClient httpClient)
      : base(httpClient)
    {

    }

    public override HttpRequestMessage BuildRequestUrlForRequestAsync(TRequest request, CancellationToken cancelToken)
    {
      string url = this.UrlToGetItemWithRequest(request);
      HttpRequestMessage result = new HttpRequestMessage(new HttpMethod("PATCH"), url);

      string fieldsList = this.GetFieldsListString(request);
      StringContent bodycontent = new StringContent(fieldsList, Encoding.UTF8, "application/json");
      result.Content = bodycontent;

      return result;
    }

    public abstract string GetFieldsListString(TRequest request);

  }
}

