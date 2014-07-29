
namespace Sitecore.MobileSDK.API.Request.Parameters
{
    public interface IGetMediaItemRequestParametersBuilder<T>
		where T : class
	{
		IGetMediaItemRequestParametersBuilder<T> Database (string database);
		IGetMediaItemRequestParametersBuilder<T> Language (string itemLanguage);
		IGetMediaItemRequestParametersBuilder<T> Version (string itemVersion);

    IGetMediaItemRequestParametersBuilder<T> DownloadOptions (IDownloadMediaOptions downloadMediaOptions);
		T Build();
	}
}

