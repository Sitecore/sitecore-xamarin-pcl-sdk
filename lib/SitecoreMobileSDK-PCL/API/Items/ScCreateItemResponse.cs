namespace Sitecore.MobileSDK.API.Items
{
  using System;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Exceptions;
  using Sitecore.MobileSDK.Session;

  public class ScCreateItemResponse 
  {

    public ScCreateItemResponse(string number)
    {
      int result;
      if (Int32.TryParse(number, out result)) {
        this.StatusCode = result;
      } else {
        throw new ParserException(TaskFlowErrorMessages.PARSER_EXCEPTION_MESSAGE);
      }

    }

    public bool Created {
        get{
          return this.StatusCode == 201;
        }
    }

    public int StatusCode {
      get;
      private set;
    }
  }
}
