using Client.Resources.Localizations;


namespace Client.SharedComponents.Managers.Task.Localizations;


internal abstract record TaskManagerLocalization
{
  
  internal abstract string heading { get; }
  
  
  internal record Buttons
  {
    internal required SharedStaticStrings.ButtonWithVisibleLabel taskEditing { get; init; }
    internal required SharedStaticStrings.ButtonWithVisibleLabel taskDeleting { get; init; }
    internal required SharedStaticStrings.ButtonWithVisibleLabel taskSaving { get; init; }
    internal required SharedStaticStrings.ButtonWithVisibleLabel terminatingOfEditing { get; init; }
  }
  
  internal abstract Buttons buttons { get; init; }


  internal record MetadataKeys
  {
    internal required string title { get; init; }
    internal required string description { get; init; }
  }
  
  internal abstract MetadataKeys metadataKeys { get; }


  internal record Controls
  {

    internal required SharedStaticStrings.ControlWithLabelAndGuidance taskTitle { get; init; }
    internal required SharedStaticStrings.ControlWithLabelAndGuidance taskDescription { get; init; }
    
  }
  
  internal abstract Controls controls { get; }
  
}
