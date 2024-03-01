using YamatoDaiwa.CSharpExtensions;


namespace FrontEndFramework.Components.ModalDialogs.Common;


public partial class ModalDialog : Microsoft.AspNetCore.Components.ComponentBase
{

  /* ━━━ Parameters ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Components.Parameter]
  public string? title { get; set; }

  [Microsoft.AspNetCore.Components.Parameter]
  public required bool isInitiallyDisplaying { get; set; } = false;

  [Microsoft.AspNetCore.Components.Parameter]
  public bool noDismissingButton { get; set; } = false;
  
  [Microsoft.AspNetCore.Components.Parameter]
  public required Microsoft.AspNetCore.Components.EventCallback<ModalDialog> onInitializedEventCallback { get; set; }
  
  [Microsoft.AspNetCore.Components.Parameter]
  public required Microsoft.AspNetCore.Components.RenderFragment ChildContent { get; set; }
  
  
  /* ━━━ State ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  private bool isDisplaying = false;
  
  
  /* ━━━ Public methods ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public void display()
  {
    this.isDisplaying = true;
  }

  public void dismiss()
  {
    this.isDisplaying = false;
  }
   
  
  /* ━━━ Lifecycle hooks ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  protected override void OnInitialized()
  {
    
    base.OnInitialized();
    
    this.isDisplaying = this.isInitiallyDisplaying;
    this.onInitializedEventCallback.InvokeAsync(this);
    
  }


  /* ━━━ CSS classes ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Components.Parameter]
  public string? rootElementModifierCSS_Class { get; set; }

  private string rootElementModifierCSS_Classes => new List<string> { "ModalDialog--YDF__FillingOfScreenWithSmallGapGeometry" }.
    
      AddElementToEndIf(
        this.rootElementModifierCSS_Class ?? "", String.IsNullOrEmpty(this.rootElementModifierCSS_Class)
      ).
          
      StringifyEachElementAndJoin(" ");

}
