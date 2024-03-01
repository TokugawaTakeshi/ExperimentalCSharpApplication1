using YamatoDaiwa.Frontend.Components.Controls.Validation;
using FrontEndFramework.InputtedValueValidation.PreMadeRules.Strings;


namespace Client.Data.FromUser.Entities.Person;


internal class PersonEmailInputtedDataValidation : InputtedValueValidation
{
  
  internal PersonEmailInputtedDataValidation(
    bool? isInputRequired = CommonSolution.Entities.Person.EmailAddress.IS_REQUIRED, 
    string? requiredInputIsMissingValidationErrorMessage = "メールアドレスは必須となります。お手数ですがメールアドレスを入力して下さい。"
  ): base(
    omittedValueChecker: rawValue => String.IsNullOrEmpty(rawValue as string),
    isInputRequired: isInputRequired,
    requiredInputIsMissingValidationErrorMessage: requiredInputIsMissingValidationErrorMessage,
    staticRules:
    [
      new EmailAddressInputtedValueValidationRule
      {
        ErrorMessage = "入力されまメールアドレスが無理です。御手数ですが、メールアドレスをご確認ください。"
      }
    ]
  ) {}
  
}