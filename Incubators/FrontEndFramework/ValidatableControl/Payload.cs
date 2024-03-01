namespace FrontEndFramework.ValidatableControl;


using InputtedValueValidation = YamatoDaiwa.Frontend.Components.Controls.Validation.InputtedValueValidation; 


public class Payload
{

  /* TODO 検討中
  public readonly string ID;*/ 

  public readonly InputtedValueValidation Validation;

  protected object value;
  protected InputtedValueValidation.Result validationResult;
  protected InputtedValueValidation.AsynchronousChecks.Status? asynchronousChecksStatus = null;
  protected readonly Func<IValidatableControl> componentInstanceAccessor;
  protected System.Timers.Timer? waitingForStaringOfAsynchronousValidationTimer = null;

  
  public object Value => this.value;

  public void SetValue(object value, byte? asynchronousValidationDelay__seconds = null)
  {

    this.value = value;
    this.validationResult = this.Validation.Validate(
      this.value,
      mustPostponeAsynchronousValidation: asynchronousValidationDelay__seconds is not null,
      asynchronousChecksCallback: this.onAsynchronousChecksStatusChanged
    );


    if (asynchronousValidationDelay__seconds is null)
    {
      return;
    }


    if (this.waitingForStaringOfAsynchronousValidationTimer is not null)
    {
      this.waitingForStaringOfAsynchronousValidationTimer.Stop();
      this.waitingForStaringOfAsynchronousValidationTimer.Close();
      this.waitingForStaringOfAsynchronousValidationTimer.Dispose();
    }


    /* [ Approach ] No need in asynchronous validations if the value has not passed the static validations. */
    if (this.IsInvalid)
    {
      return;
    }


    this.waitingForStaringOfAsynchronousValidationTimer = new System.Timers.Timer(
      TimeSpan.FromSeconds((double)asynchronousValidationDelay__seconds)
    );

    this.waitingForStaringOfAsynchronousValidationTimer.Elapsed += (_, _) =>
    {
      this.Validation.executeAsynchronousChecksIfAny(
        this.value,
        currentValidationResult: this.validationResult,
        this.onAsynchronousChecksStatusChanged
      );
    };
    
    this.waitingForStaringOfAsynchronousValidationTimer.Start();

  }


  public Payload(
    object initialValue, 
    InputtedValueValidation validation,
    Func<IValidatableControl> componentInstanceAccessor
  ) {

    this.Validation = validation;

    this.value = initialValue;
    this.validationResult = this.Validation.Validate(initialValue);
    this.componentInstanceAccessor = componentInstanceAccessor;

  }


  public TValue GetExpectedToBeValidValue<TValue>()
  {

    if (this.IsInvalid)
    {
      throw new Exception("Contrary os expectations, the value is invalid.");
    }


    if (this.Value is TValue narrowedValidValue)
    {
      return narrowedValidValue;
    }


    throw new Exception("The actual type of value is different with one passed via generic parameter.");

  }

  
  public bool IsEmpty => this.Validation.HasValueBeenOmitted(this.value);
  public bool IsInvalid => this.validationResult.IsValid;
  public string[] ValidationErrorsMessages => this.validationResult.ErrorsMessages;


  public InputtedValueValidation.Result ValidationResult
  {
    get => this.validationResult;
    set
    {
  
      bool wasValidPreviously = this.validationResult.IsValid;

      this.validationResult = value;

    }
  }

  
  public IValidatableControl GetComponentInstance()
  {

    IValidatableControl componentInstance = this.componentInstanceAccessor.Invoke();

    // if (componentInstance is null)
    // {
    //   throw new Exception("The \"componentInstanceAccessor\" is still \"null\"");
    // }

    
    return componentInstance;
    
  }
  
  /* ━━━ Routines ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  /* ─── Event handlers ─────────────────────────────────────────────────────────────────────────────────────────── */
  protected void onAsynchronousChecksStatusChanged(
    InputtedValueValidation.AsynchronousChecks.Status asynchronousChecksStatus,
    InputtedValueValidation.Result newestValidationResult 
  )
  {
    this.validationResult = newestValidationResult;
    this.asynchronousChecksStatus = asynchronousChecksStatus;
  }
  
}