namespace FrontEndFramework.InputtedValueValidation.PreMadeRules.Strings;


public class JapanesePhoneNumberInputtedValueValidationRuleEnglishLocalization: 
    JapanesePhoneNumberInputtedValueValidationRule.ILocalization
{
    
  public Func<
    JapanesePhoneNumberInputtedValueValidationRule.ILocalization.ErrorMessage.TemplateVariables, string
  > ErrorMessageBuilder =>
      (JapanesePhoneNumberInputtedValueValidationRule.ILocalization.ErrorMessage.TemplateVariables _) =>
          "The inputted phone number is not the Japanese format." +
          "It must the mistyping. " +
          "Please check the correct phone number. ";

}