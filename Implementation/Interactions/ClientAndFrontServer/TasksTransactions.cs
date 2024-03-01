namespace ClientAndFrontServer;


public record TasksTransactions
{

  public record RetrievingOfAll
  {
    public const string URN_PATH = "api/tasks/all";
  }
  
  public record RetrievingOfSelection
  {
    
    public const string URN_PATH = "/api/tasks/selection";
    
    public static readonly (
      string onlyTasksWithAssociatedDate,
      string onlyTasksWithAssociatedDateTime,
      string searchingByFullOrPartialTitleOrDescription
    ) QueryParameters = (
      onlyTasksWithAssociatedDate: "only_tasks_with_associated_date",
      onlyTasksWithAssociatedDateTime: "only_tasks_with_associated_date_time",
      searchingByFullOrPartialTitleOrDescription: "searching_by_full_or_partial_title"
    ); 
    
  }

  public record Adding
  {
    public const string URN_PATH = "api/tasks/all";
  }
  
}
