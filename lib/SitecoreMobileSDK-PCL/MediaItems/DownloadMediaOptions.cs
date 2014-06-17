using System;

namespace Sitecore.MobileSDK.MediaItems
{
	public class DownloadMediaOptions
	{
		private string width;
		private string height;
		private string maxWidth;
		private string maxHeight;
		private string backgroundColor;
		private string disableMediaCache;
		private string allowStrech;
		private string scale;
		private string displayAsThumbnail;

		public DownloadMediaOptions ()
		{
		}

		public string Width
		{
			get
			{ 
				return this.width;
			}
		}

		public void SetWidth(int width)
		{
			if (width > 0)
			{
				throw new ArgumentException("[DownloadMediaOptions] width must be > 0");
			}

			this.width = width.ToString ();
		}

		public string Height
		{
			get
			{ 
				return this.height;
			}
		}

		public void SetHeight(int height)
		{
			if (height > 0)
			{
				throw new ArgumentException("[DownloadMediaOptions] height must be > 0");
			}

			this.height = height.ToString ();
		}

		public string MaxWidth
		{
			get
			{ 
				return this.maxWidth;
			}
		}

		public void SetMaxWidth(int maxWidth)
		{
			if (maxWidth > 0)
			{
				throw new ArgumentException("[DownloadMediaOptions] maxWidth must be > 0");
			}

			this.maxWidth = maxWidth.ToString ();
		}

		public string MaxHeight
		{
			get
			{ 
				return this.maxHeight;
			}
		}

		public void SetMaxHeight(int maxHeight)
		{
			if (maxHeight > 0)
			{
				throw new ArgumentException("[DownloadMediaOptions] maxHeight must be > 0");
			}

			this.maxHeight = maxHeight.ToString ();
		}

	}
}

