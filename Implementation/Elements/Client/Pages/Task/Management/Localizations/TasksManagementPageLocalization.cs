namespace Client.Pages.Task.Management.Localizations;


internal abstract record TasksManagementPageLocalization
{

  internal record FilteringModalDialog
  {
    internal required string title { get; init; }
  }
  
  internal abstract FilteringModalDialog filteringModalDialog { get; init; }
  
  
  internal abstract string taskManagerActivationGuidance { get; }


  internal record SuccessMessages
  {
    internal required string taskAddingSucceeded { get; init; }
    internal required string taskUpdatingSucceeded { get; init; }
    internal required string taskDeletingSucceeded { get; init; }
  }
  
  internal abstract SuccessMessages successMessages { get; init; }
  
  
  internal record ErrorMessages
  {
    internal required string taskAddingFailed { get; init; }
    internal required string taskUpdatingFailed { get; init; }
    internal required string taskDeletingFailed { get; init; }
  }
  
  internal abstract ErrorMessages errorMessages { get; init; }
  
}
