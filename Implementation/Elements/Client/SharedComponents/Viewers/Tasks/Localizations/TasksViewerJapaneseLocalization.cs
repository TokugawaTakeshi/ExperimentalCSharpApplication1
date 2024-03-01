using Client.Resources.Localizations;


namespace Client.SharedComponents.Viewers.Tasks.Localizations;


internal record TasksViewerJapaneseLocalization : TasksViewerLocalization
{
  
  internal override SearchBox searchBox { get; init; } = new()
  {
    placeholder = "課題の見出しや記述で検索",
    accessibilityGuidance = "課題の見出しや記述で検索"
  };
  
  internal override Buttons buttons { get; init; } = new()
  {
    taskAdding = new SharedStaticStrings.ButtonWithVisibleLabel { label = "課題追加" },
    retryingOfDataRetrieving = new SharedStaticStrings.ButtonWithVisibleLabel { label = "取得再試験" },
    immediateAddingOfFirstTask = new SharedStaticStrings.ButtonWithVisibleLabel { label = "一件目の課題を追加しましょう" },
    resettingOfFiltering = new SharedStaticStrings.ButtonWithVisibleLabel { label = "絞り込みを解除" },
    tasksFiltering = new SharedStaticStrings.ButtonWithVisibleLabel { label = "課題をフィルタリング" }
  };
  
  internal override Errors errors { get; init; } = new()
  {
    tasksRetrieving = new SharedStaticStrings.MessageWithTitleAndDescription
    {
      title = "課題一覧取得失敗",
      description = SharedStaticJapaneseStrings.SingleInstance.
        buildDataRetrievingOrSubmittingFailedPoliteMessage(
          "課題一覧取得中、不具合が発生しました。"
        )
    }
  };
  
  internal override Guidances guidances { get; init; } = new()
  {
    noItemsAvailable = "現在、課題が一件も作られていません。",
    noItemsFound = "該当する課題が見つかりませんでした。"
  };

}