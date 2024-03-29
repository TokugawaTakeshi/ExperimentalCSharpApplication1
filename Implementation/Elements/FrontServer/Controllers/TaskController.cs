﻿using System.ComponentModel.DataAnnotations;
using Task = CommonSolution.Entities.Task;
using CommonSolution.Gateways;
using Microsoft.AspNetCore.Mvc;


namespace FrontServer.Controllers;


[Route("api/tasks")]
[ApiController]
public class TaskController : ControllerBase
{

  private readonly ITaskGateway taskGateway = FrontServerDependencies.Injector.gateways().Task;

  
  /* === 取得 ======================================================================================================== */
  [HttpGet("all")]
  public async System.Threading.Tasks.Task<ActionResult<Task[]>> retrieveAllTasks()
  {
    
    return base.Ok(await this.taskGateway.RetrieveAll());
  }
  
  /* --- 標本 -------------------------------------------------------------------------------------------------------- */
  [HttpGet("selection")]
  public async System.Threading.Tasks.Task<ActionResult<ITaskGateway.SelectionRetrieving.ResponseData>> Get(
    [FromQuery(Name="pagination_page_number")] [Required] uint paginationPageNumber,
    [FromQuery(Name="items_count_per_pagination_page")] [Required] uint itemsCountPerPaginationPage,
    [FromQuery(Name="searching_by_full_or_partial_title")] string? searchingByFullOrPartialTitle
  ) {
    return base.Ok(
      await this.taskGateway.RetrieveSelection(
        new ITaskGateway.SelectionRetrieving.RequestParameters
        {
          PaginationPageNumber = paginationPageNumber,
          ItemsCountPerPaginationPage = itemsCountPerPaginationPage,
          SearchingByFullOrPartialTitle = searchingByFullOrPartialTitle
        })
    );
  }
  
   
  /* === 追加 ======================================================================================================== */
  [HttpPost]
  public async System.Threading.Tasks.Task<ActionResult<ITaskGateway.Adding.ResponseData>> Add(
    [FromBody] ITaskGateway.Adding.RequestData requestData
  ) {
    return base.Ok(await this.taskGateway.Add(requestData));
  }

  
  /* === 更新 ======================================================================================================= */
  [HttpPut]
  public async System.Threading.Tasks.Task<ActionResult> Update(
    [FromBody] ITaskGateway.Updating.RequestData requestData
  ) {
    await this.taskGateway.Update(requestData);
    return base.Ok();
  }
  
  
  /* === 削除 ======================================================================================================= */
  [HttpDelete("(id)")]
  public async System.Threading.Tasks.Task<ActionResult> Delete(string targetTaskID) {
    await this.taskGateway.Delete(targetTaskID);
    return base.Ok();
  }

}