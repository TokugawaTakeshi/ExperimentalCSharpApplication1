using YamatoDaiwa.Frontend.Components.Abstractions;
using YamatoDaiwa.CSharpExtensions;


namespace FrontEndFramework.Components;


public abstract class YDF_Component : Microsoft.AspNetCore.Components.ComponentBase, ISupportsExternalCSS_ClassesForRootElement
{

  [Microsoft.AspNetCore.Components.Parameter]
  public string? modifierCSS_Class { get; set; } = null;

  [Microsoft.AspNetCore.Components.Parameter]
  public string? spaceSeparatedModifierCSS_Classes { get; set; } = null;

  [Microsoft.AspNetCore.Components.Parameter]
  public string[]? modifierCSS_Classes { get; set; } = null;


  public string rootElementSpaceSeparatedModifierCSS_Classes => new List<string>().
        
      AddElementToEndIf(this.modifierCSS_Class,  String.IsNullOrEmpty(this.modifierCSS_Class)).
        
      AddElementsToEnd(this.spaceSeparatedModifierCSS_Classes?.Split(" ") ?? Array.Empty<string>()).
        
      AddElementsToEnd(this.modifierCSS_Classes ?? Array.Empty<string>()).
        
      StringifyEachElementAndJoin(" ");
  
}
