using Client.Resources.Localizations;


namespace Client.SharedComponents.Managers.Task.Localizations;


internal record TaskManagerJapaneseLocalization : TaskManagerLocalization
{
  
  internal override string heading { get; } = "タスクマネージャー";
  
  internal override Buttons buttons { get; init; } = new()
  {
    taskEditing = new SharedStaticStrings.ButtonWithVisibleLabel { label = "課題を編集"},
    taskDeleting = new SharedStaticStrings.ButtonWithVisibleLabel { label = "課題を削除"},
    taskSaving = new SharedStaticStrings.ButtonWithVisibleLabel { label = "課題を保存"},
    terminatingOfEditing = new SharedStaticStrings.ButtonWithVisibleLabel { label = "編集停止"}
  };
  
  internal override MetadataKeys metadataKeys { get; } = new()
  {
    title = "見出し",
    description = "詳細"
  };
  
  internal override Controls controls { get; } = new()
  {
    taskTitle = new SharedStaticStrings.ControlWithLabelAndGuidance
    {
      label = "見出し",
      guidance = $"見出しを**{ CommonSolution.Entities.Task.Title.MINIMAL_CHARACTERS_COUNT }~" + 
          $"{ CommonSolution.Entities.Task.Title.MAXIMAL_CHARACTERS_COUNT }**文字内入力して下さい。" 
          
    },
    taskDescription = new SharedStaticStrings.ControlWithLabelAndGuidance
    {
      label = "記述",
      guidance = $"詳細を**{ CommonSolution.Entities.Task.Description.MINIMAL_CHARACTERS_COUNT }文字以上**" + 
          "入力して下さい。" 
    }
  };
  
}
