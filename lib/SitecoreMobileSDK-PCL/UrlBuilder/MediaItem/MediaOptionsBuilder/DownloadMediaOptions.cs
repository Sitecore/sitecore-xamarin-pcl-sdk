﻿using System;
using System.Globalization;

namespace Sitecore.MobileSDK.UrlBuilder.MediaItem
{
	using System.Collections.Generic;

  public class DownloadMediaOptions : IDownloadMediaOptions
	{
    public DownloadMediaOptions ()
    {
    }

    public IDownloadMediaOptions ShallowCopy()
    {
      DownloadMediaOptions copy = new DownloadMediaOptions();

      copy.Width              = this.width;
      copy.Height             = this.height;
      copy.MaxWidth           = this.maxWidth;
      copy.MaxHeight          = this.maxHeight;
      copy.BackgroundColor    = this.backgroundColor;
      copy.DisableMediaCache  = this.disableMediaCache;
      copy.AllowStrech        = this.allowStrech;
      copy.Scale              = this.scale;
      copy.DisplayAsThumbnail = this.displayAsThumbnail;

      return copy;
    }

    public bool IsEmpty
		{
      get
      {
          return  	
              null == this.Width
          &&  null == this.Height
          &&	null == this.MaxWidth
          &&	null == this.MaxHeight
          &&	null == this.BackgroundColor
          &&	null == this.DisableMediaCache
          &&	null == this.AllowStrech
          &&	null == this.Scale
          &&	null == this.DisplayAsThumbnail;
      }
		}

		public string Width
		{
      private set 
      {
        this.width = value;
      }
			get
			{ 
				return this.width;
			}
		}

		public void SetWidth(int width)
		{
			if (width <= 0)
			{
				throw new ArgumentException("[DownloadMediaOptions] width must be > 0");
			}

			this.width = width.ToString ();
		}

		public string Height
		{
      private set
      {
        this.height = value;
      }
			get
			{ 
				return this.height;
			}
		}

		public void SetHeight(int height)
		{
			if (height <= 0)
			{
				throw new ArgumentException("[DownloadMediaOptions] height must be > 0");
			}

			this.height = height.ToString ();
		}

		public string MaxWidth
		{
      private set
      {
        this.maxWidth = value;
      }
			get
			{ 
				return this.maxWidth;
			}
		}

		public void SetMaxWidth(int maxWidth)
		{
			if (maxWidth <= 0)
			{
				throw new ArgumentException("[DownloadMediaOptions] maxWidth must be > 0");
			}

			this.maxWidth = maxWidth.ToString ();
		}

		public string MaxHeight
		{
      private set
      {
        this.maxHeight = value;
      }
			get
			{ 
				return this.maxHeight;
			}
		}

		public void SetMaxHeight(int maxHeight)
		{
			if (maxHeight <= 0)
			{
				throw new ArgumentException("[DownloadMediaOptions] maxHeight must be > 0");
			}

			this.maxHeight = maxHeight.ToString ();
		}

		public string BackgroundColor
		{
      private set
      {
        this.backgroundColor = value;
      }
			get
			{ 
				return this.backgroundColor;
			}
		}

		public void SetBackgroundColor(string color)
		{
			this.backgroundColor = color;
		}

		public string DisableMediaCache
		{
      private set
      {
        this.disableMediaCache = value;
      }
			get
			{ 
				if (this.disableMediaCache != null)
					return DownloadMediaOptions.PositiveBoolValue;

				return null;
			}
		}

		public void SetDisableMediaCache(bool value)
		{
			if (value)
			{
				this.disableMediaCache = DownloadMediaOptions.PositiveBoolValue;
			}
			else
			{
        this.disableMediaCache = DownloadMediaOptions.NegativeBoolValue;
			}
		}

		public string AllowStrech
		{
      private set
      {
        this.allowStrech = value;
      }
			get
			{ 
				if (this.allowStrech != null)
					return DownloadMediaOptions.PositiveBoolValue;

				return null;
			}
		}

		public void SetAllowStrech(bool value)
		{
			if (value)
			{
				this.allowStrech = DownloadMediaOptions.PositiveBoolValue;
			}
			else
			{
        this.allowStrech = DownloadMediaOptions.NegativeBoolValue;
			}
		}

		public string DisplayAsThumbnail
		{
      private set
      {
        this.displayAsThumbnail = value;
      }
			get
			{ 
				if (this.displayAsThumbnail != null)
					return DownloadMediaOptions.PositiveBoolValue;

				return null;
			}
		}

		public void SetDisplayAsThumbnail(bool value)
		{
			if (value)
			{
				this.displayAsThumbnail = DownloadMediaOptions.PositiveBoolValue;
			}
			else
			{
        this.displayAsThumbnail = DownloadMediaOptions.NegativeBoolValue;
			}
		}

		public string Scale
		{
      private set
      {
        this.scale = value;
      }
			get
			{ 
				return this.scale;
			}
		}

		public void SetScale(float scale)
		{
			if (scale <= 0)
			{
				throw new ArgumentException("[DownloadMediaOptions] scale must be > 0");
			}

      string convertedScale = scale.ToString(CultureInfo.InvariantCulture);
      this.scale = convertedScale;
		}

		public Dictionary<string, string> OptionsDictionary
		{
			get
			{
				Dictionary<string, string> result = new Dictionary<string, string> ();

				if (this.Width != null)
				{
					result.Add (DownloadMediaOptions.widthKey, this.Width);
				}

				if (this.Height != null)
				{
					result.Add (DownloadMediaOptions.heightKey, this.Height);
				}

				if (this.MaxWidth != null)
				{
					result.Add (DownloadMediaOptions.maxWidthKey, this.MaxWidth);
				}

				if (this.MaxHeight != null)
				{
					result.Add (DownloadMediaOptions.maxHeightKey, this.MaxHeight);
				}

				if (this.BackgroundColor != null)
				{
					result.Add (DownloadMediaOptions.backgroundColorKey, this.BackgroundColor);
				}

				if (this.DisableMediaCache != null)
				{
					result.Add (DownloadMediaOptions.mediaCacheKey, this.DisableMediaCache);
				}

				if (this.AllowStrech != null)
				{
					result.Add (DownloadMediaOptions.strechKey, this.AllowStrech);
				}

				if (this.Scale != null)
				{
					result.Add (DownloadMediaOptions.scaleKey, this.Scale);
				}

				if (this.DisplayAsThumbnail != null)
				{
					result.Add (DownloadMediaOptions.thumbnailKey, this.DisplayAsThumbnail);
				}

				return result;
			}
		}

		private string width;
		private string height;
		private string maxWidth;
		private string maxHeight;
		private string backgroundColor;
		private string disableMediaCache;
		private string allowStrech;
		private string scale;
		private string displayAsThumbnail;

		private const string PositiveBoolValue = "1";
    private const string NegativeBoolValue = "0";

		private const string widthKey 			    = "w";
		private const string heightKey 			    = "h";
		private const string maxWidthKey 		    = "mw";
		private const string maxHeightKey 	    = "mh";
		private const string strechKey 			    = "as";
		private const string mediaCacheKey 	    = "dmc";
		private const string scaleKey 			    = "sc";
		private const string thumbnailKey 		  = "thn";
		private const string backgroundColorKey = "bc";
	}
}

