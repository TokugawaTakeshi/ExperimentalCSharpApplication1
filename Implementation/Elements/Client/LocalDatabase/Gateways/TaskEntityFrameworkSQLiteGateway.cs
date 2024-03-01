using CommonSolution.Gateways;
using Client.LocalDataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace Client.LocalDataBase.Gateways;


public class TaskEntityFrameworkSQLiteGateway : TaskGateway
{
  
  private readonly LocalDatabaseContext databaseContext = LocalDatabaseContext.GetInstance();
  
  
  /* ━━━ Retrieving ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public override System.Threading.Tasks.Task<CommonSolution.Entities.Task[]> RetrieveAll()
  {
    return System.Threading.Tasks.Task.FromResult(
      this.databaseContext.TasksModels.Select(taskModel => taskModel.ToEntity()).ToArray()
    );
  }

  public override System.Threading.Tasks.Task<TaskGateway.SelectionRetrieving.ResponseData> RetrieveSelection(
    TaskGateway.SelectionRetrieving.RequestParameters requestParameters
  )
  {

    CommonSolution.Entities.Task[] allTasks = this.databaseContext.
        TasksModels.
        Select(taskModel => taskModel.ToEntity()).
        ToArray();

    uint totalItemsCount = Convert.ToUInt32(allTasks.Length);
    
    CommonSolution.Entities.Task[] arrangedTasksSelection = 
        TaskGateway.ArrangeTasks(
          TaskGateway.FilterTasks(allTasks, requestParameters)
        ); 
    
    return System.Threading.Tasks.Task.FromResult(
      new TaskGateway.SelectionRetrieving.ResponseData
      {
        Items = arrangedTasksSelection,
        TotalItemsCountInSelection = Convert.ToUInt32(arrangedTasksSelection.Length),
        TotalItemsCount = Convert.ToUInt32(totalItemsCount)
      }
    );
    
  }
  
  
  /* ━━━ Adding ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public override async Task<TaskGateway.Adding.ResponseData> Add(TaskGateway.Adding.RequestData requestData)
  {
    
    EntityEntry<TaskModel> newTask = this.databaseContext.TasksModels.Add(
      new TaskModel
      {
        Title = requestData.Title,
        Description = requestData.Description,
        IsComplete = requestData.IsComplete
      }
    );

    await this.databaseContext.SaveChangesAsync();

    return new TaskGateway.Adding.ResponseData { AddedTaskID = newTask.Entity.ID };

  }
  
  
  /* ━━━ Updating ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public override System.Threading.Tasks.Task Update(TaskGateway.Updating.RequestData requestData)
  {

    TaskModel targetTaskModel = this.databaseContext.TasksModels.
        First(taskModel => taskModel.ID == requestData.ID);

    targetTaskModel.Title = requestData.Title;
    targetTaskModel.Description = requestData.Description;

    return databaseContext.SaveChangesAsync();

  }

  public override System.Threading.Tasks.Task ToggleCompletion(string targetTaskID)
  {
    
    TaskModel targetTaskModel = this.databaseContext.TasksModels.
        First(taskModel => taskModel.ID == targetTaskID);

    targetTaskModel.IsComplete = !targetTaskModel.IsComplete;

    return databaseContext.SaveChangesAsync();

  }

  
  /* ━━━ Deleting ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public override System.Threading.Tasks.Task Delete(string targetTaskID)
  {
    this.databaseContext.TasksModels.Where(taskModel => taskModel.ID == targetTaskID).ExecuteDelete();
    return System.Threading.Tasks.Task.CompletedTask;
  }
  
}