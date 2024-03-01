namespace Client.SharedComponents.Controls.TasksFilteringPanel.Localization;


internal record TasksFilteringPanelEnglishLocalization : TasksFilteringPanelLocalization
{
  
  internal override Titles titles { get; init; } = new()
  {
    summary = "Summary",
    custom = "Custom"
  };

  internal override SummaryCategories summaryCategories { get; init; } = new SummaryCategories()
  {
    all = "All Tasks",
    associatedWithDateAndTime = "Date & Time",
    associatedWithDate = "Date"
  };

}