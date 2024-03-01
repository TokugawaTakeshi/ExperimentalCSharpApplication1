using YamatoDaiwa.CSharpExtensions.OfficialLocalizations.Japanese;

namespace FrontEndFramework.InputtedValueValidation.PreMadeRules.Strings;


public class JapanesePhoneNumberInputtedValueValidationRule : YamatoDaiwa.Frontend.Components.Controls.Validation.InputtedValueValidation.IRule
{
  
  public interface ILocalization
  {

    public Func<ErrorMessage.TemplateVariables, string> ErrorMessageBuilder { get; }

    static class ErrorMessage
    {

      public struct TemplateVariables
      {
        public string RawValue { get; init; }
      }

    }

  }
  
  public static ILocalization Localization = new JapanesePhoneNumberInputtedValueValidationRuleEnglishLocalization();
  
  
  public bool MustFinishValidationIfValueIsInvalid { get; init; }
  
  public Func<ILocalization.ErrorMessage.TemplateVariables, string>? ErrorMessageBuilder { get; init; }
  public string? ErrorMessage { get; init; }
  
  
  public YamatoDaiwa.Frontend.Components.Controls.Validation.InputtedValueValidation.IRule.CheckingResult Check(object rawValue) =>
      new()
      {
        ErrorMessage = rawValue is String stringValue && JapanesePhoneNumber.IsValid(stringValue) ?
          null :
          this.buildErrorMessage(
            new ILocalization.ErrorMessage.TemplateVariables
            {
              RawValue = rawValue.ToString() ?? "null"
            }
          )
      };
  
  
  private string buildErrorMessage(ILocalization.ErrorMessage.TemplateVariables templateVariables)
  {

    if (this.ErrorMessageBuilder is not null)
    {
      return this.ErrorMessageBuilder(templateVariables);
    }


    if (this.ErrorMessage is not null)
    {
      return this.ErrorMessage;
    }


    return JapanesePhoneNumberInputtedValueValidationRule.Localization.ErrorMessageBuilder(templateVariables);

  }
  
}