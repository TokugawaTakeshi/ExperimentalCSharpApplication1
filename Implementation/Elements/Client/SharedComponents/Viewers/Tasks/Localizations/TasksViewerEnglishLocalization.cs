using Client.Resources.Localizations;


namespace Client.SharedComponents.Viewers.Tasks.Localizations;


internal record TasksViewerEnglishLocalization : TasksViewerLocalization
{
  
  internal override SearchBox searchBox { get; init; } = new()
  {
    placeholder = "Search by Title or Description",
    accessibilityGuidance = "Search by task title or description"
  };
  
  internal override Buttons buttons { get; init; } = new()
  {
    taskAdding = new SharedStaticStrings.ButtonWithVisibleLabel { label = "Add Task" },
    retryingOfDataRetrieving = new SharedStaticStrings.ButtonWithVisibleLabel { label = "Retry to Retrieve" },
    immediateAddingOfFirstTask = new SharedStaticStrings.ButtonWithVisibleLabel { label = "Let add first one" },
    resettingOfFiltering = new SharedStaticStrings.ButtonWithVisibleLabel { label = "Reset Filtering" },
    tasksFiltering = new SharedStaticStrings.ButtonWithVisibleLabel { label = "Filter Tasks" }
  };

  internal override Errors errors { get; init; } = new()
  {
    tasksRetrieving = new SharedStaticStrings.MessageWithTitleAndDescription
    {
      title = "Tasks Retrieving Failure",
      description = SharedStaticEnglishStrings.SingleInstance.
          buildDataRetrievingOrSubmittingFailedPoliteMessage(
            "The tasks selection retrieving has failed."
          )
    }
  };
  
  internal override Guidances guidances { get; init; } = new()
  {
    noItemsAvailable = "No tasks has been added yet.",
    noItemsFound = "No tasks matching with filtering conditions."
  };
  
}