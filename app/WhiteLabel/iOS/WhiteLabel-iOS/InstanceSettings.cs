using System;
using MonoTouch.Foundation;
using Sitecore.MobileSDK.Items;
using Sitecore.MobileSDK;

namespace WhiteLabeliOS
{
	public class InstanceSettings
	{
		private string instanceUrl;
		private string instanceLogin;
		private string instancePassword;
		private string instanceSite;
		private string instanceDataBase;

		public InstanceSettings ()
		{
			this.ReadValuesFromStorage ();
		}

		private void ReadValuesFromStorage ()
		{
			NSUserDefaults userDefaults = NSUserDefaults.StandardUserDefaults;
			this.instanceUrl 		= userDefaults.StringForKey ("instanceUrl");
			this.instanceLogin 		= userDefaults.StringForKey ("instanceLogin");
			this.instancePassword 	= userDefaults.StringForKey ("instancePassword");
			this.instanceSite 		= userDefaults.StringForKey ("instanceSite");
			this.instanceDataBase 	= userDefaults.StringForKey ("instanceDataBase");
		}

		public ScApiSession GetSession()
		{
			SessionConfig config = new SessionConfig (this.instanceUrl, this.instanceLogin, this.instancePassword, this.instanceSite);
			ItemSource defaultSource = ItemSource.DefaultSource ();

			return new ScApiSession (config, defaultSource);
		}

		private void SaveValueToStorage(string value, string key)
		{
			NSUserDefaults userDefaults = NSUserDefaults.StandardUserDefaults;
			userDefaults.SetString (value, key);
			userDefaults.Synchronize ();
		}

		public string InstanceUrl		
		{ 
			get
			{ 
				if (instanceUrl == null) {
					instanceUrl = "http://mobiledev1ua1.dk.sitecore.net:722/";
				}
				return instanceUrl;
			}
			set
			{ 
				this.instanceUrl = value;
				this.SaveValueToStorage (value, "instanceUrl");
			}
		}

		public string InstanceLogin 	
		{ 
			get
			{ 
				if (instanceLogin == null) {
					instanceLogin = "admin";
				}
				return instanceLogin;
			}
			set
			{ 
				this.instanceLogin = value;
				this.SaveValueToStorage (value, "instanceLogin");
			} 
		}

		public string InstancePassword 	
		{ 
			get
			{ 
				if (instancePassword == null) {
					instancePassword = "b";
				}
				return instancePassword;
			} 
			set
			{ 
				//TODO: @igk keychain?
				this.instancePassword = value;
				this.SaveValueToStorage (value, "instancePassword");
			} 
		}

		public string InstanceSite 		
		{ 
			get
			{ 
				if (instanceSite == null) {
					instanceSite = "sitecore/shell";
				}
				return instanceSite;
			}
			set
			{ 
				this.instanceSite = value;
				this.SaveValueToStorage (value, "instanceSite");
			} 
		}

		public string InstanceDataBase	
		{ 
			get
			{ 
				if (instanceDataBase == null) {
					instanceDataBase = "web";
				}
				return instanceDataBase;
			}
			set
			{ 
				this.instanceDataBase = value;
				this.SaveValueToStorage (value, "instanceDataBase");
			} 
		}
	}
}

