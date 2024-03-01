using FrontEndFramework.InputtedValueValidation.PreMadeRules.Strings;
using YamatoDaiwa.Frontend.Components.Controls.Validation;


namespace Client.Data.FromUser.Entities.Person;


internal class PersonFamilyNameSpellInputtedDataValidation : InputtedValueValidation
{
  
  internal PersonFamilyNameSpellInputtedDataValidation(
    bool? isInputRequired = CommonSolution.Entities.Person.FamilyNameSpell.IS_REQUIRED,
    string? requiredInputIsMissingValidationErrorMessage = "上の名前の読み方は必須ですから、お手数ですが、入力して下さい。"
  ) : base(
    omittedValueChecker: rawValue => String.IsNullOrEmpty(rawValue as string),
    isInputRequired: isInputRequired, 
    requiredInputIsMissingValidationErrorMessage: requiredInputIsMissingValidationErrorMessage,
    staticRules:
    [
      new MinimalCharactersCountInputtedValueValidationRule
      {
        MinimalCharactersCount = CommonSolution.Entities.Person.FamilyNameSpell.MINIMAL_CHARACTERS_COUNT,
        ErrorMessage = $"上の名前の読み方は最少{ CommonSolution.Entities.Person.FamilyNameSpell.MINIMAL_CHARACTERS_COUNT }を指定して下さい。",
        MustFinishValidationIfValueIsInvalid = true
      }
    ]
  ) {}
  
}