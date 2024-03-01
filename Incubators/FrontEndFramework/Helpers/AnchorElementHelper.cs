namespace FrontEndFramework.Helpers;


public abstract class AnchorElementHelper
{

  public static string BuildEmailLinkHrefAttributeValue(string emailAddress)
  {
    return $"mailto:{ emailAddress }";
  }
  
  public static string BuildPhoneNumberLinkHrefAttributeValue(string phoneNumber)
  {
    return $"tel:{ phoneNumber }";
  }
  
}