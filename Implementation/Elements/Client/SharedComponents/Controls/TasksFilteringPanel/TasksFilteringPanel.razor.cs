using CommonSolution.Gateways;
using Client.SharedComponents.Controls.TasksFilteringPanel.Localization;
using Client.SharedState;
using System.Globalization;
using YamatoDaiwa.CSharpExtensions;
using Utils;


namespace Client.SharedComponents.Controls.TasksFilteringPanel;


public partial class TasksFilteringPanel : Microsoft.AspNetCore.Components.ComponentBase
{
  
  [Microsoft.AspNetCore.Components.Parameter] 
  public string? rootElementModifierCSS_Class { get; set; }
  
  private readonly TasksFilteringPanelLocalization localization = ClientConfigurationRepresentative.
        MustForceDefaultLocalization ?
        new TasksFilteringPanelEnglishLocalization() :
        CultureInfo.CurrentCulture.Name switch
        {
          SupportedCultures.JAPANESE => new TasksFilteringPanelJapaneseLocalization(),
          _ => new TasksFilteringPanelEnglishLocalization()
        };
 

  /* ━━━ Lifecycle Hooks ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  protected override void OnInitialized()
  {
    TasksSharedState.onStateChanged += base.StateHasChanged;
  }
  
  
  /* ━━━ Actions Handling ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  private System.Threading.Tasks.Task retrieveAllTasks()
  {
    return TasksSharedState.retrieveTasksSelection(
      mustResetFilteringByAssociatedDate: true,
      mustResetFilteringByAssociatedDateTime: true
    );
  }
  
  private System.Threading.Tasks.Task retrieveTasksWithAssociatedDateTime()
  {
    return TasksSharedState.retrieveTasksSelection(
      requestParameters: new TaskGateway.SelectionRetrieving.RequestParameters
      {
        OnlyTasksWithAssociatedDateTime = true
      }
    );
  }
  
  private System.Threading.Tasks.Task retrieveTasksWithAssociatedDate()
  {
    return TasksSharedState.retrieveTasksSelection(
      requestParameters: new TaskGateway.SelectionRetrieving.RequestParameters
      {
        OnlyTasksWithAssociatedDate = true
      }
    );
  }
  
  
  /* ━━━ Conditional Rendering ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  private bool isDisabled => TasksSharedState.isTasksRetrievingInProgressOrNotStartedYet;

  private string retrievingOfAllTasksButtonAdditionalCSS_Classes => new List<string>().
      AddElementToEndIf(
        "TasksFilteringPanel-List-Item-Button__SelectedState",
        !TasksSharedState.onlyTasksWithAssociatedDate && !TasksSharedState.onlyTasksWithAssociatedDateTime
      ).

      StringifyEachElementAndJoin(" ");
  
  private string retrievingOfTasksAssociatedWithDateButtonAdditionalCSS_Classes => new List<string>().
    AddElementToEndIf(
      "TasksFilteringPanel-List-Item-Button__SelectedState",
      TasksSharedState.onlyTasksWithAssociatedDate
    ).

    StringifyEachElementAndJoin(" ");

  private string retrievingOfTasksAssociatedWithDateTimeButtonAdditionalCSS_Classes => new List<string>().
    AddElementToEndIf(
      "TasksFilteringPanel-List-Item-Button__SelectedState",
      TasksSharedState.onlyTasksWithAssociatedDateTime
    ).
    StringifyEachElementAndJoin(" ");

}