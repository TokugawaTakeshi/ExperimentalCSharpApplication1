using YamatoDaiwa.Frontend.Components.Controls.Validation;


namespace Client.Data.FromUser.Entities.Person;


internal class PersonAvatarURI_InputtedDataValidation : InputtedValueValidation
{
  
  internal PersonAvatarURI_InputtedDataValidation(
    bool? isInputRequired = CommonSolution.Entities.Person.EmailAddress.IS_REQUIRED, 
    string? requiredInputIsMissingValidationErrorMessage = "アバターは必須となります。画像を選択して下さい。"
  ): base(
    omittedValueChecker: rawValue => String.IsNullOrEmpty(rawValue as string),
    isInputRequired: isInputRequired,
    requiredInputIsMissingValidationErrorMessage: requiredInputIsMissingValidationErrorMessage
  ) {}
  
}