using FrontEndFramework.InputtedValueValidation.PreMadeRules.Strings;
using YamatoDaiwa.Frontend.Components.Controls.Validation;


namespace Client.Data.FromUser.Entities.Task;


internal class TaskDescriptionInputtedDataValidation : InputtedValueValidation
{
  
  internal TaskDescriptionInputtedDataValidation(
    bool? isInputRequired = CommonSolution.Entities.Task.Description.IS_REQUIRED,
    string? requiredValueIsMissingValidationErrorMessage = "課題の詳細は必須となります。お手数ですが、入力してください。"
  ) : base(
    omittedValueChecker: rawValue => String.IsNullOrEmpty(rawValue as string),
    isInputRequired: isInputRequired,
    requiredInputIsMissingValidationErrorMessage: requiredValueIsMissingValidationErrorMessage,
    staticRules:
    [
      new MinimalCharactersCountInputtedValueValidationRule
      {
        MinimalCharactersCount = CommonSolution.Entities.Task.Description.MINIMAL_CHARACTERS_COUNT,
        ErrorMessage = $"課題の詳細は最少{ CommonSolution.Entities.Task.Description.MINIMAL_CHARACTERS_COUNT }を指定して下さい。",
        MustFinishValidationIfValueIsInvalid = true
      }
    ]
  ) {}
  
}