namespace FrontEndFramework.Components.Controls.Buttons.Closing;


public partial class ClosingButton : Microsoft.AspNetCore.Components.ComponentBase
// public partial class ClosingButton : Microsoft.AspNetCore.Components.ComponentBase, ISupportsFlexibleCustomCSS_ClassesSpecifyingForRootElement
{

  [Microsoft.AspNetCore.Components.Parameter] 
  public string? modifierCSS_Class { get; set; }
  
  [Microsoft.AspNetCore.Components.Parameter] 
  public string? rootElementModifierCSS_Class { get; set; }
  
  [Microsoft.AspNetCore.Components.Parameter] 
  public string[]? modifierCSS_Classes { get; set; }
  
  [Microsoft.AspNetCore.Components.Parameter] 
  public string? spaceSeparatedAdditionalCSS_Classes { get; set; }
  
  [Microsoft.AspNetCore.Components.Parameter] 
  public Microsoft.AspNetCore.Components.EventCallback<Microsoft.AspNetCore.Components.Web.MouseEventArgs> onClick { get; set; }

  
}
