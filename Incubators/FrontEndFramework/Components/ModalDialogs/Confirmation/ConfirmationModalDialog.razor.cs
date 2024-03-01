namespace FrontEndFramework.Components.ModalDialogs.Confirmation;


public partial class ConfirmationModalDialog : Microsoft.AspNetCore.Components.ComponentBase
{
  
  /* ━━━ Parameters ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Components.Parameter]
  [Microsoft.AspNetCore.Components.EditorRequired]
  public required string title { get; set; }
  
  [Microsoft.AspNetCore.Components.Parameter]
  [Microsoft.AspNetCore.Components.EditorRequired]
  public required string question { get; set; }

  [Microsoft.AspNetCore.Components.Parameter]
  public string confirmationButtonLabel { get; set; } = "Yes";
  
  [Microsoft.AspNetCore.Components.Parameter]
  public string cancellationButtonLabel { get; set; } = "No";
  
  
  /* ━━━ State ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  private bool isDisplaying = false;
  
  
  /* ━━━ Public methods ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public void display()
  {
    this.isDisplaying = true;
    base.StateHasChanged();
  }

  public void dismiss()
  {
    this.isDisplaying = false;
    base.StateHasChanged();
  }

  
  /* ━━━ Actions handling ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  protected void onConfirmationButtonClicked()
  {
    this.isDisplaying = false;
  }
  
  protected void onCancellationButtonClicked()
  {
    this.isDisplaying = false;
  }
  
}