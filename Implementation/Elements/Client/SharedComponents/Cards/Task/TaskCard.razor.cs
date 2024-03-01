using System.Globalization;
using Client.SharedComponents.Cards.Task.Localization;
using YamatoDaiwa.CSharpExtensions;
using Utils;


namespace Client.SharedComponents.Cards.Task;


public partial class TaskCard : Microsoft.AspNetCore.Components.ComponentBase
{

  /* ━━━ Component Parameters ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Components.Parameter]
  public required CommonSolution.Entities.Task targetTask { get; set; }

  [Microsoft.AspNetCore.Components.Parameter]
  public required Microsoft.AspNetCore.Components.EventCallback<
    CommonSolution.Entities.Task
  > onClickRootElementEventHandler { get; set; }
  
  [Microsoft.AspNetCore.Components.Parameter]
  public required Microsoft.AspNetCore.Components.EventCallback<
    CommonSolution.Entities.Task
  > onClickCheckBoxEventHandler { get; set; }

  [Microsoft.AspNetCore.Components.Parameter]
  public string rootElementTag { get; set; } = "div";
  
  [Microsoft.AspNetCore.Components.Parameter]
  public bool disabled { get; set; } = false;


  /* ━━━ Events Handling ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  private async System.Threading.Tasks.Task onClickRootElement()
  {
    await this.onClickRootElementEventHandler.InvokeAsync(this.targetTask);
  }

  private async System.Threading.Tasks.Task onClickCheckbox()
  {
    await this.onClickCheckBoxEventHandler.InvokeAsync(this.targetTask);
  }


  /* ━━━ Auxiliary Getters ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  private string? dateBadgeLabel => this.targetTask.associatedDate?.ToString();
  private string? dateTimeBadgeLabel => this.targetTask.associatedDateTime?.ToString();

  private bool hasAtLeastOneBadge => this.dateBadgeLabel is not null || this.dateTimeBadgeLabel is not null;
  
  
  /* ━━━ CSS Classes ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Components.Parameter]
  public string? rootElementModifierCSS_Class { get; set; } = null;
  
  private string rootElementModifierCSS_Classes => new List<string>().
    
      AddElementToEndIf("TaskCard__DisabledState", this.disabled).
      AddElementToEndIf(
        this.rootElementModifierCSS_Class ?? "", String.IsNullOrEmpty(this.rootElementModifierCSS_Class)
      ).
          
      StringifyEachElementAndJoin(" ");
  
  private string composeClassAttributeValueForRootElement(string namespaceCSS_Class) => new List<string?> { namespaceCSS_Class }.

      AddElementToEndIf("TaskCard__DisabledState", this.disabled).
      
      AddElementToEndIfNotNull(this.rootElementModifierCSS_Class).

      StringifyEachElementAndJoin(" ");
  
  
  /* ━━━ Localization ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  private readonly TaskCardLocalization localization = 
      ClientConfigurationRepresentative.MustForceDefaultLocalization ?
          new TaskCardEnglishLocalization() :
          CultureInfo.CurrentCulture.Name switch
          {
            SupportedCultures.JAPANESE => new TaskCardJapaneseLocalization(),
            _ => new TaskCardEnglishLocalization()
          };

}