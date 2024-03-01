namespace Client.SharedComponents.OnePerPage.Header;


public partial class Header : Microsoft.AspNetCore.Components.ComponentBase
{

  [Microsoft.AspNetCore.Components.Parameter]
  public string? rootElementModifierCSS_Class { get; set; } = null;
  
  [Microsoft.AspNetCore.Components.Parameter]
  public string? hamburgerMenuButtonAdditionalCSS_Class { get; set; } = null;

}