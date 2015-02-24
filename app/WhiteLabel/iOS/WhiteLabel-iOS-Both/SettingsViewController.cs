
namespace WhiteLabeliOS
{
  using System;
  using System.Drawing;

  #if __UNIFIED__
  using UIKit;
  using Foundation;
  #else
  using MonoTouch.UIKit;
  using MonoTouch.Foundation;
  #endif

	public partial class SettingsViewController : BaseTaskViewController
	{
		public SettingsViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString ("Settings", "Settings");
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			this.instanceUrlField.Placeholder	= NSBundle.MainBundle.LocalizedString ("Instance Url", null);
			this.passwordField.Placeholder		= NSBundle.MainBundle.LocalizedString ("Password", null);
			this.loginField.Placeholder    		= NSBundle.MainBundle.LocalizedString ("Login", null);
			this.siteField.Placeholder     		= NSBundle.MainBundle.LocalizedString ("Site", null);
			this.dbField.Placeholder       		= NSBundle.MainBundle.LocalizedString ("Database", null);
			this.languageField.Placeholder 		= NSBundle.MainBundle.LocalizedString ("Language", null);

			this.instanceUrlField.ShouldReturn	= this.HideKeyboard;
			this.passwordField.ShouldReturn		= this.HideKeyboard;
			this.loginField.ShouldReturn    	= this.HideKeyboard;
			this.siteField.ShouldReturn     	= this.HideKeyboard;
			this.dbField.ShouldReturn       	= this.HideKeyboard;
			this.languageField.ShouldReturn 	= this.HideKeyboard;
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear (animated);

			this.instanceUrlField.Text = this.instanceSettings.InstanceUrl;
			this.passwordField.Text = this.instanceSettings.InstancePassword;
			this.loginField.Text = this.instanceSettings.InstanceLogin;
			this.siteField.Text = this.instanceSettings.InstanceSite;
			this.dbField.Text = this.instanceSettings.InstanceDataBase;
			this.languageField.Text = this.instanceSettings.InstanceLanguage;
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear (animated);

			InstanceSettings settings = this.instanceSettings;

			settings.InstanceUrl 		= this.instanceUrlField.Text;
			settings.InstancePassword 	= this.passwordField.Text;
			settings.InstanceLogin 		= this.loginField.Text;
			settings.InstanceSite 		= this.siteField.Text;
			settings.InstanceDataBase 	= this.dbField.Text;
			settings.InstanceLanguage 	= this.languageField.Text;
		}
	}
}

