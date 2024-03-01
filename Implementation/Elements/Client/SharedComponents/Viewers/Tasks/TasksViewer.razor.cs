using CommonSolution.Gateways;

using Client.SharedState;

using Client.Pages.Task.Management.Localizations;

using Client.Pages.Task.Management.ModalDialogs;
using Client.SharedComponents.Viewers.Tasks.Localizations;
using FrontEndFramework.Components.BlockingLoadingOverlay;
using FrontEndFramework.Components.Snackbar;

using System.Diagnostics;
using System.Globalization;


namespace Client.SharedComponents.Viewers.Tasks;


public partial class TasksViewer : Microsoft.AspNetCore.Components.ComponentBase 
{

  /* ━━━ Component Parameters ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Components.Parameter]
  public required Microsoft.AspNetCore.Components.EventCallback<string> onClickTaskAddingButtonEventHandler { get; set; }
  
  [Microsoft.AspNetCore.Components.Parameter] 
  public string? rootElementModifierCSS_Class { get; set; }
 
  
  /* ━━━ Fields ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  private readonly TasksViewerLocalization localization = 
      ClientConfigurationRepresentative.MustForceDefaultLocalization ?
          new TasksViewerEnglishLocalization() :
          CultureInfo.CurrentCulture.Name switch
          {
            SupportedCultures.JAPANESE => new TasksViewerJapaneseLocalization(),
            _ => new TasksViewerEnglishLocalization()
          };


  /* ━━━ Livecycle Hooks ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  protected override async System.Threading.Tasks.Task OnInitializedAsync()
  {
    TasksSharedState.onStateChanged += base.StateHasChanged;
    await TasksSharedState.retrieveTasksSelection();
  }


  /* ━━━ Actions Handling ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  /* ─── Tasks Retrieving ─────────────────────────────────────────────────────────────────────────────────────────── */
  private async System.Threading.Tasks.Task onNewTaskSearchingByFullOrPartialTitleOrDescriptionRequestHasBeenEmitted(
    string? newSearchingOfTaskByFullOrPartialTitleOrDescriptionRequest 
  )
  {
    await TasksSharedState.retrieveTasksSelection(
      new TaskGateway.SelectionRetrieving.RequestParameters
      {
        SearchingByFullOrPartialTitleOrDescription = newSearchingOfTaskByFullOrPartialTitleOrDescriptionRequest
      },
      mustResetSearchingByFullOrPartialTitleOrDescription: 
          newSearchingOfTaskByFullOrPartialTitleOrDescriptionRequest is null
    );
  }

  private async System.Threading.Tasks.Task onClickTasksSelectionRetrievingRetryingButton()
  {
    await TasksSharedState.retrieveTasksSelection();
  }

  private async void onClickTasksFilteringResettingButton()
  {
    await TasksSharedState.retrieveTasksSelection(mustResetSearchingByFullOrPartialTitleOrDescription: true);
  }
  
  private void openTasksFilteringModalDialog()
  {
    TasksFilteringModalDialogService.displayModalDialog();
  }
  
  
  /* ─── Adding of New Task ───────────────────────────────────────────────────────────────────────────────────────── */
  private System.Threading.Tasks.Task onClickTaskAddingButton()
  {
    return this.onClickTaskAddingButtonEventHandler.InvokeAsync(null);
  }
  
  
  /* ─── Task Selecting ───────────────────────────────────────────────────────────────────────────────────────────── */
  private void onSelectTask(CommonSolution.Entities.Task targetTask)
  {
    TasksSharedState.currentlySelectedTask = targetTask;
  }

  
  /* ─── Toggling of Task Completion ──────────────────────────────────────────────────────────────────────────────── */
  private async void onToggleTaskCompletion(CommonSolution.Entities.Task targetTask)
  {

    BlockingLoadingOverlayService.displayBlockingLoadingOverlay();
    
    try
    {
      await TasksSharedState.toggleTaskCompletion(targetTask.ID);
    }
    catch (Exception exception)
    {
      Debug.WriteLine(exception);
    }

    BlockingLoadingOverlayService.dismissBlockingLoadingOverlay();
    
  }

  
  /* ━━━ Rendering ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  private bool mustDisableSearchBox =>
      TasksSharedState.isTasksRetrievingInProgressOrNotStartedYet ||
      TasksSharedState.totalTasksCountInDataSource == 0;

  private const byte LOADING_PLACEHOLDER_CARDS_COUNT = 12;

}
