

namespace WhiteLabeliOS
{
  using System;
  using UIKit;
  using Foundation;

  using Sitecore.MobileSDK.API.Request.Parameters;

  public class PayloadSelectorViewHelper
  {
    public const int PAYLOAD_MIN_BUTTON_INDEX  = 0;
    public const int PAYLOAD_CONTENT_BUTTON_INDEX = 1;
    public const int PAYLOAD_FULL_BUTTON_INDEX    = 2;

    public const PayloadType DEFAULT_PAYLOAD = PayloadType.Content;
    private const int DEFAULT_PAYLOAD_BUTTON_INDEX = PAYLOAD_CONTENT_BUTTON_INDEX;

    private const int PAYLOAD_SEGMENTS_COUNT   = 3;

    public static void ConfigureSegmentedControllerForPayload(UISegmentedControl payloadSelectionView)
    {
      if (PayloadSelectorViewHelper.PAYLOAD_SEGMENTS_COUNT != payloadSelectionView.NumberOfSegments)
      {
        throw new Exception("[PAYLOAD] Unexpected segments count");
      }

      payloadSelectionView.SetTitle(PayloadSelectorViewHelper.MinPayloadButtonName(), PayloadSelectorViewHelper.PAYLOAD_MIN_BUTTON_INDEX);
      payloadSelectionView.SetTitle(PayloadSelectorViewHelper.ContentPayloadButtonName(), PayloadSelectorViewHelper.PAYLOAD_CONTENT_BUTTON_INDEX);
      payloadSelectionView.SetTitle(PayloadSelectorViewHelper.FullPayloadButtonName(), PayloadSelectorViewHelper.PAYLOAD_FULL_BUTTON_INDEX);

      payloadSelectionView.SelectedSegment = PayloadSelectorViewHelper.DEFAULT_PAYLOAD_BUTTON_INDEX;
    }

    public static string MinPayloadButtonName()
    {
      return NSBundle.MainBundle.LocalizedString("PAYLOAD_BUTTON_MIN", null);
    }

    public static string ContentPayloadButtonName()
    {
      return NSBundle.MainBundle.LocalizedString("PAYLOAD_BUTTON_CONTENT", null);
    }

    public static string FullPayloadButtonName()
    {
      return NSBundle.MainBundle.LocalizedString("PAYLOAD_BUTTON_FULL", null);
    }
  }
}

