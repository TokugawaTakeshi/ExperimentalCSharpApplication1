// ReSharper disable ReplaceAutoPropertyWithComputedProperty
// To avoid generating a backing field https://stackoverflow.com/a/77935080/4818123

using Client.Resources.Localizations;


namespace Client.Pages.Task.Management.Localizations;


internal record TasksManagementPageJapaneseLocalization : TasksManagementPageLocalization
{

  internal override FilteringModalDialog filteringModalDialog { get; init; } = new()
  {
    title = "課題の絞り込み"
  };
  
  internal override string taskManagerActivationGuidance { get; } = 
      "課題の詳細を閲覧する事や編集するにはカードをクリック・タップして下さい。";
 
  internal override SuccessMessages successMessages { get; init; } = new()
  {
    taskAddingSucceeded = "新規課題が追加されました。",
    taskUpdatingSucceeded = "課題の更新が完了しました。",
    taskDeletingSucceeded = "課題を削除しました。"
  };
  
  internal override ErrorMessages errorMessages { get; init; } = new()
  {
    taskAddingFailed = SharedStaticJapaneseStrings.SingleInstance.
        buildDataRetrievingOrSubmittingFailedPoliteMessage("新規課題の追加中不具合が発生しました。"),
    taskUpdatingFailed = SharedStaticJapaneseStrings.SingleInstance.
        buildDataRetrievingOrSubmittingFailedPoliteMessage("課題の更新中不具合が発生しました。"),
    taskDeletingFailed = SharedStaticJapaneseStrings.SingleInstance.
      buildDataRetrievingOrSubmittingFailedPoliteMessage("課題の削除中不具合が発生しました。")
  };

}
