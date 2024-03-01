using Microsoft.AspNetCore.Components;


namespace FrontEndFramework.Components.Controls;


public abstract class InputtableControl : ComponentBase
{
    
  /* === Blazor component parameters ================================================================================ */
  [Microsoft.AspNetCore.Components.Parameter] 
  public string? label { get; set; } = null;
  
  [Microsoft.AspNetCore.Components.Parameter] 
  public string? accessibilityGuidance { get; set; } = null;
  
  [Microsoft.AspNetCore.Components.Parameter] 
  public string? externalLabelHTML_ID { get; set; } = null;
  
  [Microsoft.AspNetCore.Components.Parameter] 
  public string? guidance { get; set; } = null;
  
  [Microsoft.AspNetCore.Components.Parameter] 
  public bool required { get; set; } = false;

  [Microsoft.AspNetCore.Components.Parameter] 
  public bool mustDisplayAppropriateBadgeIfInputIsRequired { get; set; } = false;

  [Microsoft.AspNetCore.Components.Parameter] 
  public bool mustDisplayAppropriateBadgeIfInputIsOptional { get; set; } = false;

  [Microsoft.AspNetCore.Components.Parameter] 
  public bool mustAddInvisibleBadgeForHeightEqualizingWhenNoBadge { get; set; } = false;
 
  [Microsoft.AspNetCore.Components.Parameter]  
  public bool @readonly { get; set; } = false;
  
  [Microsoft.AspNetCore.Components.Parameter] 
  public string? coreElementHTML_ID { get; set; }
  
  [Microsoft.AspNetCore.Components.Parameter] 
  public bool disabled { get; set; } = false;
  
  
  /* === State ====================================================================================================== */
  protected bool invalidInputHighlightingIfAnyValidationErrorsMessages = false;
  protected bool validInputHighlightingIfAnyErrorsMessages = false;
  
  
  /* === Methods ==================================================================================================== */
  public InputtableControl HighlightInvalidInput()
  {
    this.invalidInputHighlightingIfAnyValidationErrorsMessages = true;
    return this;
  }
  
}