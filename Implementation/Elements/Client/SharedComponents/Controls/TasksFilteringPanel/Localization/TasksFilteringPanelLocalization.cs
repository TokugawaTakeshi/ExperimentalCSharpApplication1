namespace Client.SharedComponents.Controls.TasksFilteringPanel.Localization;


internal abstract record TasksFilteringPanelLocalization
{
  
  internal record Titles
  {
    internal required string summary { get; init; }
    internal required string custom { get; init; }
  } 
  
  internal abstract Titles titles { get; init; }


  internal record SummaryCategories
  {
    internal required string all { get; init; }
    internal required string associatedWithDateAndTime { get; init; }
    internal required string associatedWithDate { get; init; }
  }
  
  internal abstract SummaryCategories summaryCategories { get; init; }
  
}