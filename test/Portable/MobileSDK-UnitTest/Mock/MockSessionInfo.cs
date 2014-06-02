
namespace MobileSDKUnitTest.Mock
{
	using System;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder;

    public class SessionConfigPOD : ISessionConfig
    {
		private string site;

        public string InstanceUrl { get; set; }

        public string ItemWebApiVersion { get; set; }

        public string Site 
		{ 
			get 
			{ 
				return this.site;
			}
			set
			{ 
				if (string.IsNullOrEmpty(value))
				{
					this.site = null;
				}
				else
				{
					string separator = "/";
					string siteValue = value;
					bool firstSymbolIsNotSlash = !siteValue.StartsWith (separator);
					if (firstSymbolIsNotSlash)
					{
						throw new ArgumentException ("SessionConfigPOD.Site : site must starts with '/'"); 
					}
					else
					{
						this.site = value;
					}
				}
			}
		}
    }
}
