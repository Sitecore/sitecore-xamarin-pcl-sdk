using Sitecore.MobileSDK.API.Request.Parameters;

namespace Sitecore.MobileSDK.UserRequest.SearchRequest
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Validators;

  public class RunStoredSearchRequestBuilder : AbstractGetVersionedItemRequestBuilder<ISitecoreStoredSearchRequest>
  {
    public RunStoredSearchRequestBuilder(string itemId)
    {
      ItemIdValidator.ValidateItemId(itemId, this.GetType().Name + ".ItemId");

      this.itemId = itemId;
    }

    public override ISitecoreStoredSearchRequest Build()
    {
      IPagingParameters pagingSettings = this.AccumulatedPagingParameters;

      BaseValidator.CheckNullAndThrow(this.term, "Search term can not be null");

      StoredSearchParameters result = new StoredSearchParameters(
        null, 
        this.itemSourceAccumulator, 
        this.queryParameters, 
        pagingSettings,
        this.itemId,
        this.term);

      return result;
    }

    public RunStoredSearchRequestBuilder Term(string term)
    {
      BaseValidator.CheckNullAndThrow(term, this.GetType().Name + ".Term");

      this.term = term;

      return this;
    }

    private string term;
    private readonly string itemId;
  }
}

