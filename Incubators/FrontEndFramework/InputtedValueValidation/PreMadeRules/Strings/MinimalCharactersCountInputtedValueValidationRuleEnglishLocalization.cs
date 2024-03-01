namespace FrontEndFramework.InputtedValueValidation.PreMadeRules.Strings;


public struct MinimalCharactersCountInputtedValueValidationRuleEnglishLocalization : 
    MinimalCharactersCountInputtedValueValidationRule.ILocalization
{
  
  public Func<
    MinimalCharactersCountInputtedValueValidationRule.ILocalization.ErrorMessage.TemplateVariables, string
  > ErrorMessageBuilder => (
      MinimalCharactersCountInputtedValueValidationRule.ILocalization.ErrorMessage.TemplateVariables templateVariables
    ) =>
        "Not enough characters has been inputted. " +
        $"Please input at least { templateVariables.MinimalCharactersCount } characters.";

}