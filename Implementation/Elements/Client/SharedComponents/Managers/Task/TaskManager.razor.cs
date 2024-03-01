using Client.Data.FromUser.Entities.Task;
using FrontEndFramework.InputtedValueValidation;

using Client.SharedComponents.Managers.Task.Localizations;
using Client.SharedComponents.Viewers.Tasks.Localizations;
using YamatoDaiwa.Frontend.Components.Controls.Validation;
using FrontEndFramework.Components.Controls.TextBox;

using System.Diagnostics;
using System.Globalization;
using FrontEndFramework.Components.ModalDialogs.Confirmation;


namespace Client.SharedComponents.Managers.Task;


public partial class TaskManager : Microsoft.AspNetCore.Components.ComponentBase
{
  
  /* ━━━ Component parameters ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Components.Parameter] 
  public CommonSolution.Entities.Task? targetTask { get; set; }

  [Microsoft.AspNetCore.Components.Parameter]
  [Microsoft.AspNetCore.Components.EditorRequired]
  public required Microsoft.AspNetCore.Components.EventCallback<
    CommonSolution.Gateways.TaskGateway.Adding.RequestData
  > onInputtingDataOfNewTaskCompleteEventHandler { get; set; }
  
  [Microsoft.AspNetCore.Components.Parameter] 
  [Microsoft.AspNetCore.Components.EditorRequired]
  public required Microsoft.AspNetCore.Components.EventCallback<
    CommonSolution.Gateways.TaskGateway.Updating.RequestData
  > onExistingTaskEditingCompleteEventHandler { get; set; }
  
  [Microsoft.AspNetCore.Components.Parameter] 
  [Microsoft.AspNetCore.Components.EditorRequired]
  public required Microsoft.AspNetCore.Components.EventCallback<
    CommonSolution.Entities.Task
  > onDeleteTaskEventHandler { get; set; }
  
  [Microsoft.AspNetCore.Components.Parameter]
  [Microsoft.AspNetCore.Components.EditorRequired]
  public required string activationGuidance { get; set; }
  
  [Microsoft.AspNetCore.Components.Parameter] 
  public string? rootElementModifierCSS_Class { get; set; }
  
  
  /* ━━━ Fields ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  private (
    FrontEndFramework.ValidatableControl.Payload title, 
    FrontEndFramework.ValidatableControl.Payload description
  ) taskControlsPayload;
  
  private TextBox titleTextBox = null!;
  private TextBox descriptionTextBox = null!;
  
  private bool isViewingMode = true;
  private bool isEditingMode => !this.isViewingMode;

  private readonly string ID = TaskManager.generateComponentID();
  private readonly string HEADING_ID;
  
  private TextBox.ValidityHighlightingActivationModes validityHighlightingActivationMode => 
      this.targetTask is null ?
        TextBox.ValidityHighlightingActivationModes.onFocusOut :
        TextBox.ValidityHighlightingActivationModes.immediate;
  
  private readonly TaskManagerLocalization localization = 
      ClientConfigurationRepresentative.MustForceDefaultLocalization ?
          new TaskManagerEnglishLocalization() :
          CultureInfo.CurrentCulture.Name switch
          {
            SupportedCultures.JAPANESE => new TaskManagerJapaneseLocalization(),
            _ => new TaskManagerEnglishLocalization()
          };


  /* ━━━ Constructor ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public TaskManager()
  {
    
    this.HEADING_ID = $"{ this.ID }-HEADING";
    
    this.taskControlsPayload = (
      title: new FrontEndFramework.ValidatableControl.Payload(
        initialValue: "", 
        validation: new TaskTitleInputtedDataValidation(),
        componentInstanceAccessor: () => this.titleTextBox
      ),
      description: new FrontEndFramework.ValidatableControl.Payload(
        initialValue: "", 
        validation: new TaskDescriptionInputtedDataValidation(),
        componentInstanceAccessor: () => this.descriptionTextBox
      ) 
    );
    
  }
  
  
  /* ━━━ Public methods ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public void utilizeTaskEditing()
  {
    
    this.isViewingMode = true;

    this.taskControlsPayload.title.SetValue("");
    this.taskControlsPayload.description.SetValue("");

  }

  
  /* ━━━ Actions handling ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public void beginInputNewTaskData()
  {
    this.isViewingMode = false;
  }
  
  private void beginTaskEditing()
  {

    if (this.targetTask is null)
    {
      throw new Exception("\"beginTaskEditing\" has been called while \"targetTask\" is still \"null\".");
    }


    this.taskControlsPayload.title.SetValue(this.targetTask.title);
    this.taskControlsPayload.description.SetValue(this.targetTask.description ?? ""); 
    
    this.isViewingMode = false;

  }

  private void displayTaskDeletingConfirmationDialog()
  {
    ConfirmationModalDialogService.displayModalDialog(
      new ConfirmationModalDialog.Options
      {
        title = "Confirmation",
        question = "Are you sure?",
        onConfirmationButtonClickedEventHandler = this.deleteTask
      }  
    );
  }

  private async void onClickTaskDataSavingButton()
  {

    if (ValidatableControlsGroup.HasInvalidInputs(this.taskControlsPayload))
    {
      ValidatableControlsGroup.PointOutValidationErrors(this.taskControlsPayload);
      return;
    }
    

    if (this.targetTask == null)
    {

      await this.onInputtingDataOfNewTaskCompleteEventHandler.InvokeAsync(
        new CommonSolution.Gateways.TaskGateway.Adding.RequestData
        {
          Title = this.taskControlsPayload.title.GetExpectedToBeValidValue<string>(),
          Description = this.taskControlsPayload.description.GetExpectedToBeValidValue<string>()
        }
      );
      
      return;

    }
    
    await this.onExistingTaskEditingCompleteEventHandler.InvokeAsync(
      new CommonSolution.Gateways.TaskGateway.Updating.RequestData
      {
        ID = this.targetTask.ID,
        Title = this.taskControlsPayload.title.GetExpectedToBeValidValue<string>(),
        Description = this.taskControlsPayload.description.GetExpectedToBeValidValue<string>()
      }
    );

  }

  private async void deleteTask()
  {

    if (this.targetTask is null)
    {
      throw new Exception("\"deleteTask\" method has been called while \"targetTask\" is still \"null\".");
    }

    
    await this.onDeleteTaskEventHandler.InvokeAsync(this.targetTask);

  }
  
  
  /* ━━━ Routines ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  /* ─── Generating of ID ─────────────────────────────────────────────────────────────────────────────────────────── */
  private static uint counterForComponentID_Generating = 0;
  
  private static string generateComponentID()
  {
    TaskManager.counterForComponentID_Generating++;
    return $"TASK_MANAGER-{ TaskManager.counterForComponentID_Generating }";
  }
  
}