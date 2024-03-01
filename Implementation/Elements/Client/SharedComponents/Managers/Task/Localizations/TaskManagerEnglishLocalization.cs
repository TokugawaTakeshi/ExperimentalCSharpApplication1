using Client.Resources.Localizations;


namespace Client.SharedComponents.Managers.Task.Localizations;


internal record TaskManagerEnglishLocalization : TaskManagerLocalization
{
  
  internal override string heading { get; } = "Task Manager";

  internal override Buttons buttons { get; init; } = new()
  {
    taskEditing = new SharedStaticStrings.ButtonWithVisibleLabel { label = "Edit Task"},
    taskDeleting = new SharedStaticStrings.ButtonWithVisibleLabel { label = "Delete Task"},
    taskSaving = new SharedStaticStrings.ButtonWithVisibleLabel { label = "Save Task"},
    terminatingOfEditing = new SharedStaticStrings.ButtonWithVisibleLabel { label = "Cancel editing"}
  };

  internal override MetadataKeys metadataKeys { get; } = new()
  {
    title = "Title",
    description = "Description"
  };

  internal override Controls controls { get; } = new()
  {
    taskTitle = new SharedStaticStrings.ControlWithLabelAndGuidance
    {
      label = "Title",
      guidance = $"Please input **{ CommonSolution.Entities.Task.Title.MINIMAL_CHARACTERS_COUNT } - " + 
          $"{ CommonSolution.Entities.Task.Title.MAXIMAL_CHARACTERS_COUNT }** characters." 
          
    },
    taskDescription = new SharedStaticStrings.ControlWithLabelAndGuidance
    {
      label = "Description",
      guidance = $"Please input **at least { CommonSolution.Entities.Task.Description.MINIMAL_CHARACTERS_COUNT }**" + 
          "characters" 
    }
  };

}
