namespace FrontEndFramework.InputtedValueValidation.PreMadeRules.Strings;


public class NumericMaximumInputtedValueValidationRuleEnglishLocalization: 
    NumericMaximumInputtedValueValidationRule.ILocalization
{
  public Func<
    NumericMaximumInputtedValueValidationRule.ILocalization.ErrorMessage.TemplateVariables, string
  > ErrorMessageBuilder => (
    NumericMaximumInputtedValueValidationRule.ILocalization.ErrorMessage.TemplateVariables templateVariables) =>
      $"The inputted numeric value is exceeding the { templateVariables.MaximalValue }, the maximal allowed value. " +
        $"Please input the number not greater than { templateVariables.MaximalValue }.";
}
