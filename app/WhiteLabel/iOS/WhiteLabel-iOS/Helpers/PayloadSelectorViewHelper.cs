using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

namespace WhiteLabeliOS
{
  using System;
  using MonoTouch.UIKit;


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

      payloadSelectionView.SetTitle("Min", PayloadSelectorViewHelper.PAYLOAD_MIN_BUTTON_INDEX);
      payloadSelectionView.SetTitle("Content", PayloadSelectorViewHelper.PAYLOAD_CONTENT_BUTTON_INDEX);
      payloadSelectionView.SetTitle("Full", PayloadSelectorViewHelper.PAYLOAD_FULL_BUTTON_INDEX);

      payloadSelectionView.SelectedSegment = PayloadSelectorViewHelper.DEFAULT_PAYLOAD_BUTTON_INDEX;
    }
  }
}

