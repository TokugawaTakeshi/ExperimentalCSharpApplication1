using YamatoDaiwa.Frontend.Components.Controls.Validation;


namespace Client.Data.FromUser.Entities.Person;


internal class PersonGenderInputtedDataValidation : InputtedValueValidation
{
  
  internal PersonGenderInputtedDataValidation(
    bool? isInputRequired = CommonSolution.Entities.Person.Gender.IS_REQUIRED,
    string? requiredInputIsMissingValidationErrorMessage = "性別は必須ですから、お手数ですが、指定して下さい。"
  ) : base(
    omittedValueChecker: rawValue => rawValue == null,
    isInputRequired: isInputRequired, 
    requiredInputIsMissingValidationErrorMessage: requiredInputIsMissingValidationErrorMessage
  ) {}
  
}