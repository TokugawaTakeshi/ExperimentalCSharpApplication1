using CommonSolution.Gateways;
using ClientAndFrontServer;


namespace FrontServer.Controllers;


[Microsoft.AspNetCore.Mvc.ApiController]
public class TaskController : Microsoft.AspNetCore.Mvc.ControllerBase
{

  private readonly TaskGateway taskGateway = FrontServerDependencies.Injector.gateways().Task;

  
  /* ━━━ Retrieving ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Mvc.HttpGet(TasksTransactions.RetrievingOfAll.URN_PATH)]
  public async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.ActionResult<Task[]>> retrieveAllTasks()
  {
    return base.Ok(await this.taskGateway.RetrieveAll());
  }
  
  [Microsoft.AspNetCore.Mvc.HttpGet(TasksTransactions.RetrievingOfSelection.URN_PATH)]
  public async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.ActionResult<TaskGateway.SelectionRetrieving.ResponseData>> Get(
    [Microsoft.AspNetCore.Mvc.FromQuery(Name="only_tasks_with_associated_date")] bool onlyTasksWithAssociatedDate,
    [Microsoft.AspNetCore.Mvc.FromQuery(Name="only_tasks_with_associated_date_time")] bool onlyTasksWithAssociatedDateTime,
    [Microsoft.AspNetCore.Mvc.FromQuery(Name="searching_by_full_or_partial_title")] string? searchingByFullOrPartialTitle
  ) {
    return base.Ok(
      await this.taskGateway.RetrieveSelection(
      new TaskGateway.SelectionRetrieving.RequestParameters
      {
        OnlyTasksWithAssociatedDate = onlyTasksWithAssociatedDate,
        OnlyTasksWithAssociatedDateTime = onlyTasksWithAssociatedDateTime,
        SearchingByFullOrPartialTitleOrDescription = searchingByFullOrPartialTitle
      }
    ));
    
  }
  
  
  /* ━━━ Adding ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Mvc.HttpPost(TasksTransactions.Adding.URN_PATH)]
  public async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.ActionResult<CommonSolution.Entities.Task>> Add(
    [Microsoft.AspNetCore.Mvc.FromBody] TaskGateway.Adding.RequestData requestData
  ) {
    return base.Ok(
      await this.taskGateway.Add(requestData)
    );
  }
  
  
  /* ━━━ Updating ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Mvc.HttpPut(TasksTransactions.Updating.URN_PATH)]
  public async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.ActionResult> Update(
    [Microsoft.AspNetCore.Mvc.FromBody] TaskGateway.Updating.RequestData requestData
  ) {
    return base.Ok(
      await this.taskGateway.Update(requestData)
    );
  }

  
  /* ━━━ Deleting ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Mvc.HttpDelete(TasksTransactions.Deleting.URN_PATH_TEMPLATE)]
  public async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.ActionResult> Delete(string targetTaskID) {
    await this.taskGateway.Delete(targetTaskID);
    return base.Ok();
  }

}