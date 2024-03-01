namespace CommonSolution.Gateways;


public abstract class TaskGateway
{
 
  /* ━━━ Retrieving ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public abstract System.Threading.Tasks.Task<CommonSolution.Entities.Task[]> RetrieveAll();
  
  public abstract Task<SelectionRetrieving.ResponseData> RetrieveSelection(
    SelectionRetrieving.RequestParameters requestParameters
  );
  
  public abstract class SelectionRetrieving
  {
    
    public struct RequestParameters
    {
      public string? SearchingByFullOrPartialTitleOrDescription { get; init; }
      public bool? OnlyTasksWithAssociatedDate { get; init; }
      public bool? OnlyTasksWithAssociatedDateTime { get; init; }
    }

    public struct ResponseData
    {
      public required uint TotalItemsCount;
      public required uint TotalItemsCountInSelection;
      public required CommonSolution.Entities.Task[] Items;
    }
    
  }

  
  /* ─── Filtering & arranging ────────────────────────────────────────────────────────────────────────────────────── */
  public static CommonSolution.Entities.Task[] FilterTasks(
    CommonSolution.Entities.Task[] tasks, SelectionRetrieving.RequestParameters filtering
  )
  {

    CommonSolution.Entities.Task[] workpiece = tasks;
    
    if (filtering.OnlyTasksWithAssociatedDate == true)
    {
      workpiece = workpiece.Where(
        task => task.associatedDate is not null
      ).ToArray();
    } else if (filtering.OnlyTasksWithAssociatedDateTime == true)
    {
      workpiece = workpiece.Where(
        task => task.associatedDateTime is not null
      ).ToArray();
    }

    if (!String.IsNullOrEmpty(filtering.SearchingByFullOrPartialTitleOrDescription))
    {
      workpiece = workpiece.
          Where(
            (CommonSolution.Entities.Task task) => 
                task.title.Contains(filtering.SearchingByFullOrPartialTitleOrDescription) ||
                (task.description?.Contains(filtering.SearchingByFullOrPartialTitleOrDescription) ?? false)
          ).
          ToArray();
    }

    return workpiece;

  }

  public static bool IsTaskSatisfyingToFilteringConditions(
    CommonSolution.Entities.Task task, SelectionRetrieving.RequestParameters filtering
  )
  {
    return TaskGateway.FilterTasks([ task ], filtering).Length == 1;
  }
  
  public static CommonSolution.Entities.Task[] ArrangeTasks(CommonSolution.Entities.Task[] tasks)
  {
    return tasks.
        OrderByDescending((CommonSolution.Entities.Task task) => task.associatedDateTime is not null).
        ThenByDescending((CommonSolution.Entities.Task task) => task.associatedDate is not null).
        ToArray();
  }
  
  
  /* ━━━ Adding ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public abstract Task<Adding.ResponseData> Add(Adding.RequestData requestData);

  public abstract class Adding
  {
    
    public struct RequestData
    {
      public required string Title { get; init; }
      public string? Description { get; init; }
      public bool IsComplete { get; init; }
      public string[]? SubtasksIDs { get; init; }
      public string? AssociatedDateTime__ISO8601 { get; init; }
      public string? AssociatedDate__ISO8601 { get; init; }
      public CommonSolution.Entities.Location? AssociatedLocation { get; init; }
    }

    public struct ResponseData
    {
      public required string AddedTaskID { get; init; }
    }
    
  }
  
  
  /* ━━━ Updating ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public abstract System.Threading.Tasks.Task Update(Updating.RequestData requestData);
  
  public abstract class Updating
  {
    public struct RequestData
    {
      public required string ID { get; init; }
      public string Title { get; init; }
      public string? Description { get; init; }
      public bool IsComplete { get; init; }
      public string[]? SubtasksIDs { get; init; }
      public string? AssociatedDateTime__ISO8601 { get; init; }
      public string? AssociatedDate__ISO8601 { get; init; }
      public CommonSolution.Entities.Location? AssociatedLocation { get; init; }
    }
  }

  
  public abstract System.Threading.Tasks.Task ToggleCompletion(string targetTaskID);
  

  /* ━━━ Deleting ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public abstract System.Threading.Tasks.Task Delete(string targetTaskID);
  
}
