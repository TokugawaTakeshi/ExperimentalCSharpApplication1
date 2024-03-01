using Client.Pages.Task.Management.Localizations;

using Client.SharedState;

using Client.SharedComponents.Managers.Task;

using FrontEndFramework.Components.BlockingLoadingOverlay;
using FrontEndFramework.Components.Snackbar;

using System.Globalization;
using System.Diagnostics;


namespace Client.Pages.Task.Management;


public partial class TasksManagementPageContent : Microsoft.AspNetCore.Components.ComponentBase
{

  /* ━━━ Fields ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  private CommonSolution.Entities.Task? activeTask = null;

  private string taskManagerAdditionalCSS_Class =>
      this.activeTask is not null ? "TasksManagementPage-TaskManager__VisibleAtNarrowScreens" : "";
  
  private readonly TasksManagementPageLocalization localization = 
      ClientConfigurationRepresentative.MustForceDefaultLocalization ?
          new TasksManagementPageEnglishLocalization() :
          CultureInfo.CurrentCulture.Name switch
          {
            SupportedCultures.JAPANESE => new TasksManagementPageJapaneseLocalization(),
            _ => new TasksManagementPageEnglishLocalization()
          };


  /* ━━━ Livecycle Hooks ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  protected override void OnInitialized()
  {
    TasksSharedState.onStateChanged += base.StateHasChanged;
    TasksSharedState.onSelectedTaskHasChanged += this.onTaskHasBeenSelected;
  }


  /* ━━━ Actions Handling ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  /* ─── Task Selecting ───────────────────────────────────────────────────────────────────────────────────────────── */
  private void onTaskHasBeenSelected(CommonSolution.Entities.Task newTask)
  {
    this.activeTask = newTask;
  }
  
  
  /* ─── Adding of New Task ───────────────────────────────────────────────────────────────────────────────────────── */
  private TaskManager taskManager = null!;
  
  private void onClickStartingOfNewTaskInputtingButton()
  {
    this.activeTask = null;
    this.taskManager.beginInputNewTaskData();
  }
  
  private async System.Threading.Tasks.Task onNewTaskInputtingCompleted(
    CommonSolution.Gateways.TaskGateway.Adding.RequestData newTaskData
  )
  {
    
    BlockingLoadingOverlayService.displayBlockingLoadingOverlay();

    CommonSolution.Entities.Task newTask;
    
    try
    {
      newTask = await TasksSharedState.addTask(
        newTaskData, 
        mustRetrieveUnfilteredTasksIfNewOneDoesNotSatisfyingTheCurrentFilteringConditions: true
      );
    }
    catch (Exception exception)
    {

      _ = SnackbarService.displaySnackbarForAWhile(
        message: this.localization.errorMessages.taskAddingFailed,
        decorativeVariation: Snackbar.StandardDecorativeVariations.error
      );

      Debug.WriteLine(exception);
      
      return;

    }
    finally
    {
      BlockingLoadingOverlayService.dismissBlockingLoadingOverlay();  
    }

    this.taskManager.utilizeTaskEditing();
    this.activeTask = newTask;
    
    _ = SnackbarService.displaySnackbarForAWhile(
      message: this.localization.successMessages.taskUpdatingSucceeded,
      decorativeVariation: Snackbar.StandardDecorativeVariations.success
    );
    
  }


  /* ─── Editing of Existing Task ─────────────────────────────────────────────────────────────────────────────────── */
  private async System.Threading.Tasks.Task onExistingTaskEditingCompleted(
    CommonSolution.Gateways.TaskGateway.Updating.RequestData updatedTaskData
  )
  {
    
    BlockingLoadingOverlayService.displayBlockingLoadingOverlay();

    try
    {
      await TasksSharedState.updateTask(updatedTaskData);
    }
    catch (Exception exception)
    {

      _ = SnackbarService.displaySnackbarForAWhile(
        message: this.localization.errorMessages.taskUpdatingFailed,
        decorativeVariation: Snackbar.StandardDecorativeVariations.error
      );
      
      Debug.WriteLine(exception.ToString());
      return;

    }
    finally
    {
      BlockingLoadingOverlayService.dismissBlockingLoadingOverlay();  
    }
  
    this.taskManager.utilizeTaskEditing();
    
    _ = SnackbarService.displaySnackbarForAWhile(
      message: this.localization.successMessages.taskUpdatingSucceeded,
      decorativeVariation: Snackbar.StandardDecorativeVariations.success
    );
    
  }
  
  
  /* ─── Deleting of Existing Task ────────────────────────────────────────────────────────────────────────────────── */
  private async System.Threading.Tasks.Task onDeleteTask()
  {

    if (this.activeTask is null)
    {
      throw new Exception("\"onDeleteTask\" method has been called while \"activeTask\" is still \"null\".");
    }
    
    
    BlockingLoadingOverlayService.displayBlockingLoadingOverlay();

    try
    {
      await TasksSharedState.deleteTask(this.activeTask.ID);
    } catch (Exception exception)
    {
      
      _ = SnackbarService.displaySnackbarForAWhile(
        message: this.localization.errorMessages.taskDeletingFailed,
        decorativeVariation: Snackbar.StandardDecorativeVariations.error
      );
      
      Debug.WriteLine(exception.ToString());
      return;
      
    }
    finally
    {
      BlockingLoadingOverlayService.dismissBlockingLoadingOverlay();  
    }

    this.taskManager.utilizeTaskEditing();

    this.activeTask = null;
    
    _ = SnackbarService.displaySnackbarForAWhile(
      message: this.localization.successMessages.taskDeletingSucceeded,
      decorativeVariation: Snackbar.StandardDecorativeVariations.success
    );
    
  }

}