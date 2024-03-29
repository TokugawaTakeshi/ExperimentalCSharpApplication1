﻿using Client.Data.FromUser.Entities.Task;

using FrontEndFramework.InputtedValueValidation;

using System.Diagnostics;
using Microsoft.AspNetCore.Components;


namespace Client.Components.SharedReusable.Managers.Task;


using ValidatableControl = FrontEndFramework.ValidatableControl;


public partial class TaskManager : ComponentBase
{
  
  /* === Blazorコンポーネント引数 ========================================================================================= */
  [Parameter] public CommonSolution.Entities.Task? targetTask { get; set; }

  [Parameter] public string? activationGuidance { get; set; }
  
  [Parameter] public string? spaceSeparatedAdditionalCSS_Classes { get; set; }


  /* === Blazorコンポーネントステート ======================================================================================= */
  private bool isViewingMode = true;
  
  private readonly string ID = TaskManager.generateComponentID();
  private string HEADING_ID => $"{ this.ID }-HEADING";

  private (ValidatableControl.Payload taskTitle, ValidatableControl.Payload taskDescription) controlsPayload = (
    taskTitle: new ValidatableControl.Payload(
      initialValue: "", 
      new TaskTitleInputtedDataValidation()),
    taskDescription: new ValidatableControl.Payload(
      initialValue: "", 
      new TaskTitleInputtedDataValidation()
      )
  );
  
  
  /* === 行動処理 ==================================================================================================== */
  private void beginTaskEditing()
  {

    if (this.targetTask == null)
    {
      throw new Exception("「beginTaskEditing」メソッドは呼び出されたが、「targetTask」は「null」のまま。");
    }

    
    this.controlsPayload.taskTitle.Value = this.targetTask.Title;
    this.controlsPayload.taskDescription.Value = this.targetTask.Description ?? "";
    
    this.isViewingMode = false;

  }

  private void displayTaskDeletingConfirmationDialog()
  {
    // TODO 【 次のプールリクエスト 】 実装
  }

  private void updateTask()
  {

    if (this.targetTask == null)
    {
      throw new Exception("「updateTask」メソッドは呼び出されたが、「targetTask」は「null」のまま。");
    }
    
    if (ValidatableControlsGroup.HasInvalidInputs(this.controlsPayload))
    {
      // ValidatableControlsGroup.PointOutValidationErrors();
      return;
    }

    this.targetTask.Title = this.controlsPayload.taskTitle.GetExpectedToBeValidValue();
    this.targetTask.Description = this.controlsPayload.taskDescription.GetExpectedToBeValidValue();
    
    // TODO 【 次のプールリクエスト 】 実装
  }

  private void utilizeTaskEditing()
  {
    
    this.isViewingMode = true;

    this.controlsPayload.taskTitle.Value = "";
    this.controlsPayload.taskDescription.Value = "";

  }


  /* === ルーチン ====================================================================================================== */
  /* --- ID生成 ------------------------------------------------------------------------------------------------------ */
  private static uint counterForComponentID_Generating = 0;
  
  private static string generateComponentID()
  {
    TaskManager.counterForComponentID_Generating++;
    return $"TASK_MANAGER-{ TaskManager.counterForComponentID_Generating }";
  }

}