using CommonSolution.Gateways;

using ClientAndFrontServer;

using System.Text;
using System.Text.Json;
using Utils;
using YamatoDaiwa.CSharpExtensions.Exceptions;


namespace Client.Data.FromServer;


public class TaskHTTP_ClientGateway : TaskGateway
{
  
  private HttpClient httpClient = new();
  private JsonSerializerOptions serializerOptions = new() { PropertyNamingPolicy = null };
  
  
  /* ━━━ Retrieving ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public override async System.Threading.Tasks.Task<CommonSolution.Entities.Task[]> RetrieveAll()
  {

    HttpResponseMessage response = await this.httpClient.GetAsync(
      new Uri(String.Format(TasksTransactions.RetrievingOfAll.URN_PATH))
    );
    
    if (response.IsSuccessStatusCode)
    {
      return JsonSerializer.Deserialize<CommonSolution.Entities.Task[]>(
        await response.Content.ReadAsStringAsync(),
        this.serializerOptions
      )!;
    }
    
    
    throw new Exception(response.ReasonPhrase);        
    
  }

  public override async System.Threading.Tasks.Task<TaskGateway.SelectionRetrieving.ResponseData> RetrieveSelection(
    TaskGateway.SelectionRetrieving.RequestParameters requestParameters
  )
  {
    
    HttpResponseMessage response = await this.httpClient.GetAsync(
      URI_Builder.Build(
        new URI_Builder.SourceData
        {
          origin = ClientToFrontServerConnection.ORIGIN,
          path = TasksTransactions.RetrievingOfSelection.URN_PATH,
          queryParameters = new Dictionary<string, object>().
              SetPairIf(
                TasksTransactions.RetrievingOfSelection.QueryParameters.onlyTasksWithAssociatedDateTime, 
                true,
                requestParameters.OnlyTasksWithAssociatedDateTime is true
              ).
              SetPairIf(
                TasksTransactions.RetrievingOfSelection.QueryParameters.onlyTasksWithAssociatedDate, 
                true,
                requestParameters.OnlyTasksWithAssociatedDate is true
              ).
              SetPairIfValueNotIsNull(
                TasksTransactions.RetrievingOfSelection.QueryParameters.searchingByFullOrPartialTitleOrDescription, 
                requestParameters.SearchingByFullOrPartialTitleOrDescription
              )
        }
      )
    );
    
    if (response.IsSuccessStatusCode)
    {
      return JsonSerializer.Deserialize<TaskGateway.SelectionRetrieving.ResponseData>(
        await response.Content.ReadAsStringAsync(),
        this.serializerOptions
      );
    }

    
    throw new DataRetrievingFailedException(
      $"Failed to retrieve tasks selection.\n{ response.ReasonPhrase }"
    );

  }


  /* ━━━ Adding ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public override async System.Threading.Tasks.Task<CommonSolution.Entities.Task> Add(
    TaskGateway.Adding.RequestData requestData
  )
  {
    
    HttpResponseMessage response = await this.httpClient.PostAsync(
      URI_Builder.Build(
        new URI_Builder.SourceData
        {
          origin = ClientToFrontServerConnection.ORIGIN,
          path = TasksTransactions.Adding.URN_PATH
        }
      ),
      new StringContent(
        JsonSerializer.Serialize(requestData, this.serializerOptions),
        Encoding.UTF8,
        "application/json"
      )
    );

    if (response.IsSuccessStatusCode)
    {
      return JsonSerializer.Deserialize<CommonSolution.Entities.Task>(
        await response.Content.ReadAsStringAsync(),
        this.serializerOptions
      );
    }
    
    
    throw new DataRetrievingFailedException($"Failed add new task.\n{ response.ReasonPhrase }");
    
  }
  
  
  /* ━━━ Updating ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public override async System.Threading.Tasks.Task<CommonSolution.Entities.Task> Update(
    Updating.RequestData requestData
  )
  {
    
    HttpResponseMessage response = await this.httpClient.PutAsync(
      URI_Builder.Build(
        new URI_Builder.SourceData
        {
          origin = ClientToFrontServerConnection.ORIGIN,
          path = TasksTransactions.Updating.URN_PATH
        }
      ),
      new StringContent(
        JsonSerializer.Serialize(requestData, this.serializerOptions),
        Encoding.UTF8,
        "application/json"
      )
    );
    
    if (response.IsSuccessStatusCode)
    {
      return JsonSerializer.Deserialize<CommonSolution.Entities.Task>(
        await response.Content.ReadAsStringAsync(),
        this.serializerOptions
      );
    }
    
    throw new DataRetrievingFailedException(
      $"Failed add new task.\n{ response.ReasonPhrase }"
      $"Failed to update the task with \"{ requestData.ID }\" new task.\n{ response.ReasonPhrase }"
    );
    
  }
    );
    
  }
}
