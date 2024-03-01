// ReSharper disable ReplaceAutoPropertyWithComputedProperty
// To avoid generating a backing field https://stackoverflow.com/a/77935080/4818123

using Client.Resources.Localizations;


namespace Client.Pages.Task.Management.Localizations;


internal record TasksManagementPageEnglishLocalization : TasksManagementPageLocalization
{

  internal override FilteringModalDialog filteringModalDialog { get; init; } = new()
  {
    title = "Filter Tasks"
  };
  
  internal override string taskManagerActivationGuidance { get; } = 
      "Click or tap the task card to view details or edit the dedicated task.";

  internal override SuccessMessages successMessages { get; init; } = new()
  {
    taskAddingSucceeded = "Task has been added.",
    taskUpdatingSucceeded = "Task has been updated",
    taskDeletingSucceeded = "Task has been deleted."
  };
  
  internal override ErrorMessages errorMessages { get; init; } = new()
  {
    taskAddingFailed = SharedStaticEnglishStrings.SingleInstance.
        buildDataRetrievingOrSubmittingFailedPoliteMessage("The malfunction has occurred during adding of new task."),
    taskUpdatingFailed = SharedStaticEnglishStrings.SingleInstance.
        buildDataRetrievingOrSubmittingFailedPoliteMessage("The malfunction has occurred during updating of task."),
    taskDeletingFailed = SharedStaticEnglishStrings.SingleInstance.
      buildDataRetrievingOrSubmittingFailedPoliteMessage("The malfunction has occurred during deleting of task.")
  };

}
