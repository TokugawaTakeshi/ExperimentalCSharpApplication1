using FrontEndFramework.InputtedValueValidation.PreMadeRules.Strings;
using YamatoDaiwa.Frontend.Components.Controls.Validation;


namespace Client.Data.FromUser.Entities.Person;


internal class PersonFamilyNameInputtedDataValidation : InputtedValueValidation
{
  
  internal PersonFamilyNameInputtedDataValidation(
    bool? isInputRequired = CommonSolution.Entities.Person.FamilyName.IS_REQUIRED,
    string? requiredInputIsMissingValidationErrorMessage = "上の名前は必須ですから、お手数ですが、入力して下さい。"
  ) : base(
    omittedValueChecker: rawValue => String.IsNullOrEmpty(rawValue as string),
    isInputRequired: isInputRequired, 
    requiredInputIsMissingValidationErrorMessage: requiredInputIsMissingValidationErrorMessage,
    staticRules:
    [
      new MinimalCharactersCountInputtedValueValidationRule
      {
        MinimalCharactersCount = CommonSolution.Entities.Person.FamilyName.MINIMAL_CHARACTERS_COUNT,
        ErrorMessage = $"上の名前は最少{ CommonSolution.Entities.Person.FamilyName.MINIMAL_CHARACTERS_COUNT }を指定して下さい。",
        MustFinishValidationIfValueIsInvalid = true
      }
    ]
  ) {}
  
}