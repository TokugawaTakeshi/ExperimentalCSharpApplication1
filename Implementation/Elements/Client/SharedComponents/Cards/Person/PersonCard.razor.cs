using System.Diagnostics;
using YamatoDaiwa.Frontend.Components.Abstractions;


namespace Client.SharedComponents.Cards.Person;


public partial class PersonCard : Microsoft.AspNetCore.Components.ComponentBase, ISupportsFlexibleExternalCSS_ClassesSpecifyingForRootElement 
{

  [Microsoft.AspNetCore.Components.Parameter] 
  public required CommonSolution.Entities.Person targetPerson { get; set; }

  [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.EventCallback<CommonSolution.Entities.Person> onClick { get; set; }

  [Microsoft.AspNetCore.Components.Parameter] 
  public string rootElementTag { get; set; } = "div";

  [Microsoft.AspNetCore.Components.Parameter]
  public string? rootElementModifierCSS_Class { get; set; } = null;

  [Microsoft.AspNetCore.Components.Parameter]
  public string[]? rootElementModifierCSS_Classes { get; set; } = null;

  [Microsoft.AspNetCore.Components.Parameter]
  public string? rootElementSpaceSeparatedModifierCSS_Classes { get; set; } = null;
  
  
  private async System.Threading.Tasks.Task onClickOutermostElement()
  {

    if (this.onClick.HasDelegate)
    {
      
      try
      {
        await this.onClick.InvokeAsync(this.targetPerson);
      }
      catch (Exception exception)
      {
        Debug.WriteLine(exception);
        throw;
      }
      
    }
    
  }

}
