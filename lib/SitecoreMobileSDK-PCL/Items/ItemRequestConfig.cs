using System;

namespace Sitecore.MobileSDK
{
	public class ItemRequestConfig
	{
		public string Id { get; set; }

		public SessionConfig SessionConfig { get; private set; }

		public PublicKeyX509Certificate Certificate { get; private set; }

		public ItemRequestConfig (SessionConfig config, PublicKeyX509Certificate certificate)
		{
			this.SessionConfig = config;
			this.Certificate = certificate;
		}

		public ItemRequestConfig (SessionConfig config)
		{
			this.SessionConfig = config;
		}
	}
}

