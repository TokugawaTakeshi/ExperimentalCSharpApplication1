using CommonSolution.Gateways;
using YamatoDaiwa.CSharpExtensions.DataMocking;


namespace MockDataSource.Gateways;


public class TaskMockGateway : TaskGateway
{
  
  private readonly MockDataSource mockDataSource = MockDataSource.GetInstance();

  /* [ Usage ] Intended to be changed manually during prototype development stage.*/
  private static readonly bool NO_ITEMS_SIMULATION_MODE = false;
  
  
  public override System.Threading.Tasks.Task<CommonSolution.Entities.Task[]> RetrieveAll()
  {
    return MockGatewayHelper.SimulateDataRetrieving<object, CommonSolution.Entities.Task[]>(
      requestParameters: null,
      getResponseData: mockDataSource.RetrieveAllTasks,
      new MockGatewayHelper.SimulationOptions
      {
        MinimalPendingPeriod__Seconds = 1,
        MaximalPendingPeriod__Seconds = 2,
        MustSimulateError = false,
        GatewayName = nameof(TaskMockGateway),
        TransactionName = "RetrievingOfAll"
      }
    );
  }

  public override System.Threading.Tasks.Task<TaskGateway.SelectionRetrieving.ResponseData> RetrieveSelection(
    TaskGateway.SelectionRetrieving.RequestParameters requestParameters
  )
  {
    return MockGatewayHelper.SimulateDataRetrieving(
      requestParameters,
      getResponseData: () =>
      {

        if (TaskMockGateway.NO_ITEMS_SIMULATION_MODE)
        {
          return new TaskGateway.SelectionRetrieving.ResponseData
          {
            Items = Array.Empty<CommonSolution.Entities.Task>(),
            TotalItemsCountInSelection = 0,
            TotalItemsCount = 0
          };
        }


        CommonSolution.Entities.Task[] arrangedTasksSelection = TaskGateway.ArrangeTasks(
          TaskGateway.FilterTasks(mockDataSource.Tasks.ToArray(), requestParameters)
        ); 
        
        return new TaskGateway.SelectionRetrieving.ResponseData
        {
          Items = arrangedTasksSelection,
          TotalItemsCountInSelection = Convert.ToUInt32(arrangedTasksSelection.Length),
          TotalItemsCount = Convert.ToUInt32(mockDataSource.Tasks.Count)
        };
        
      },
      new MockGatewayHelper.SimulationOptions
      {
        MinimalPendingPeriod__Seconds = 1,
        MaximalPendingPeriod__Seconds = 2,
        MustSimulateError = false,
        GatewayName = nameof(TaskMockGateway),
        TransactionName = "RetrievingOfSelection"
      }
    );
    
  }
  
  
  /* ━━━ Adding ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public override Task<TaskGateway.Adding.ResponseData> Add(TaskGateway.Adding.RequestData requestData)
  {
    return MockGatewayHelper.SimulateDataSubmitting(
      requestData,
      getResponseData: () => mockDataSource.AddTask(requestData),
      new MockGatewayHelper.SimulationOptions
      {
        MinimalPendingPeriod__Seconds = 1,
        MaximalPendingPeriod__Seconds = 2,
        MustSimulateError = false,
        GatewayName = nameof(TaskMockGateway),
        TransactionName = "Adding"
      }
    );
  }
  
  
  /* ━━━ Updating ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public override System.Threading.Tasks.Task Update(TaskGateway.Updating.RequestData requestData)
  {
    return MockGatewayHelper.SimulateDataSubmitting<TaskGateway.Updating.RequestData, object>(
      requestData,
      getResponseData: () =>
      {
        mockDataSource.UpdateTask(requestData);
        return null;
      },
      new MockGatewayHelper.SimulationOptions
      {
        MinimalPendingPeriod__Seconds = 1,
        MaximalPendingPeriod__Seconds = 2,
        MustSimulateError = false,
        GatewayName = nameof(TaskMockGateway),
        TransactionName = nameof(TaskGateway.Adding)
      }
    );
  }

  public override System.Threading.Tasks.Task ToggleCompletion(string targetTaskID)
  {
    return MockGatewayHelper.SimulateDataRetrieving<string, object>(
      targetTaskID,
      getResponseData: () =>
      {
        
        CommonSolution.Entities.Task targetTask = this.mockDataSource.
            RetrieveAllTasks().
            Single(task => task.ID == targetTaskID);
        
        targetTask.isComplete = !targetTask.isComplete;
        
        return null;
        
      },
      new MockGatewayHelper.SimulationOptions
      {
        MinimalPendingPeriod__Seconds = 1,
        MaximalPendingPeriod__Seconds = 2,
        MustSimulateError = false,
        GatewayName = nameof(TaskMockGateway),
        TransactionName = nameof(TaskGateway.Adding)
      }
    );
  }

  
  /* ━━━ Deleting ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public override System.Threading.Tasks.Task Delete(string targetTaskID)
  {
    return MockGatewayHelper.SimulateDataSubmitting<string, object>(
      targetTaskID,
      getResponseData: () =>
      {
        this.mockDataSource.DeleteTask(targetTaskID);
        return null;
      },
      new MockGatewayHelper.SimulationOptions
      {
        MinimalPendingPeriod__Seconds = 1,
        MaximalPendingPeriod__Seconds = 2,
        MustSimulateError = false,
        GatewayName = nameof(TaskMockGateway),
        TransactionName = nameof(TaskGateway.Adding)
      }
    );
  }
  
}