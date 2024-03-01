namespace Client.SharedComponents.Controls.TasksFilteringPanel.Localization;


internal record TasksFilteringPanelJapaneseLocalization : TasksFilteringPanelLocalization
{
 
  internal override Titles titles { get; init; } = new()
  {
    summary = "まとめ",
    custom = "カスタム"
  };
  
  internal override SummaryCategories summaryCategories { get; init; } = new SummaryCategories()
  {
    all = "全課題",
    associatedWithDateAndTime = "日時連想",
    associatedWithDate = "日付連想"
  };
  
}