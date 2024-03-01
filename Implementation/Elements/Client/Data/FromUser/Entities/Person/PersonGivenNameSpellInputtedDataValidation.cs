using FrontEndFramework.InputtedValueValidation.PreMadeRules.Strings;
using YamatoDaiwa.Frontend.Components.Controls.Validation;


namespace Client.Data.FromUser.Entities.Person;


internal class PersonGivenNameSpellInputtedDataValidation : InputtedValueValidation
{
  
  internal PersonGivenNameSpellInputtedDataValidation(
    bool? isInputRequired = CommonSolution.Entities.Person.GivenNameSpell.IS_REQUIRED,
    string? requiredValueIsMissingValidationErrorMessage = "下の名前の読み方は必須ですから、お手数ですが、入力して下さい。"
  ) : base(
    omittedValueChecker: rawValue => String.IsNullOrEmpty(rawValue as string),
    isInputRequired: isInputRequired,
    requiredInputIsMissingValidationErrorMessage: requiredValueIsMissingValidationErrorMessage,
    staticRules:
    [
      new MinimalCharactersCountInputtedValueValidationRule
      {
        MinimalCharactersCount = CommonSolution.Entities.Person.GivenNameSpell.MINIMAL_CHARACTERS_COUNT,
        ErrorMessage = $"下の名前の読み方は最少{ CommonSolution.Entities.Person.GivenNameSpell.MINIMAL_CHARACTERS_COUNT }を指定して下さい。",
        MustFinishValidationIfValueIsInvalid = true
      }
    ]
  ) {}
  
}