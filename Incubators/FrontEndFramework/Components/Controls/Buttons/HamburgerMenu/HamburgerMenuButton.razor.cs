using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;


namespace FrontEndFramework.Components.Controls.Buttons.HamburgerMenu;


public partial class HamburgerMenuButton :  Microsoft.AspNetCore.Components.ComponentBase
{
  
  [Microsoft.AspNetCore.Components.Parameter]
  public EventCallback<MouseEventArgs> onClick { get; set; }
 
  [Microsoft.AspNetCore.Components.Parameter]
  public string? spaceSeparatedAdditionalCSS_Classes { get; set; }
  
}
