namespace FrontEndFramework.InputtedValueValidation.PreMadeRules.Strings;


public struct MaximalCharactersCountInputtedValueValidationRuleEnglishLocalization : 
    MaximalCharactersCountInputtedValueValidationRule.ILocalization
{
  
  public Func<
    MaximalCharactersCountInputtedValueValidationRule.ILocalization.ErrorMessage.TemplateVariables, string
  > ErrorMessageBuilder => (
    MaximalCharactersCountInputtedValueValidationRule.ILocalization.ErrorMessage.TemplateVariables templateVariables
  ) =>
      "Too much characters has been inputted. " +
      $"Please input maximum { templateVariables.MaximalCharactersCount } characters.";

}