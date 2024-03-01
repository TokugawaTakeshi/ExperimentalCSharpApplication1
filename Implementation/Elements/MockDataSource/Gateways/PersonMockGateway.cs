using Person = CommonSolution.Entities.Person;
using CommonSolution.Gateways;
using YamatoDaiwa.CSharpExtensions.DataMocking;


namespace MockDataSource.Gateways;


public class PersonMockGateway : IPersonGateway
{

  private readonly MockDataSource mockDataSource = MockDataSource.GetInstance();
  
  /* 【 用途 】 手動変更専用 */
  private static readonly bool NO_ITEMS_SIMULATION_MODE = false;


  public Task<Person[]> RetrieveAll()
  {
    return MockGatewayHelper.SimulateDataRetrieving<object, Person[]>(
      requestParameters: null,
      getResponseData: PersonMockGateway.NO_ITEMS_SIMULATION_MODE ? Array.Empty<Person> : mockDataSource.RetrieveAllPeople,
      new MockGatewayHelper.SimulationOptions
      {
        MinimalPendingPeriod__Seconds = 2,
        MaximalPendingPeriod__Seconds = 3,
        MustSimulateError = false,
        GatewayName = nameof(PersonMockGateway),
        TransactionName = "RetrievingOfAll"
      }
    );
  }

  public Task<IPersonGateway.SelectionRetrieving.ResponseData> RetrieveSelection(
    IPersonGateway.SelectionRetrieving.RequestParameters? requestParameters
  )
  {

    string? searchingByFullOrPartialNameOrItsSpell = requestParameters?.SearchingByFullOrPartialNameOrItsSpell; 
    
    return MockGatewayHelper.SimulateDataRetrieving(
      requestParameters,
      getResponseData: () => {

        if (PersonMockGateway.NO_ITEMS_SIMULATION_MODE)
        {
          return new IPersonGateway.SelectionRetrieving.ResponseData
          {
            Items = Array.Empty<Person>(),
            TotalItemsCountInSelection = 0,
            TotalItemsCount = 0
          }; 
        }
        
        
        Person[] filteredPeople;
        
        if (!String.IsNullOrEmpty(searchingByFullOrPartialNameOrItsSpell))
        {

          filteredPeople = mockDataSource.People.
              Where(
                person => 
                    person.familyName.Contains(searchingByFullOrPartialNameOrItsSpell) ||
                    (person.givenName?.Contains(searchingByFullOrPartialNameOrItsSpell) ?? false) ||
                    (person.familyNameSpell?.Contains(searchingByFullOrPartialNameOrItsSpell) ?? false) ||
                    (person.givenNameSpell?.Contains(searchingByFullOrPartialNameOrItsSpell) ?? false)
              ).
              ToArray();

        }
        else
        {
          filteredPeople = mockDataSource.People.ToArray();
        }

        return new IPersonGateway.SelectionRetrieving.ResponseData
        {
          Items = filteredPeople,
          TotalItemsCountInSelection = Convert.ToUInt32(filteredPeople.Length),
          TotalItemsCount = Convert.ToUInt32(mockDataSource.People.Count)
        };
        
      },
      new MockGatewayHelper.SimulationOptions
      {
        MinimalPendingPeriod__Seconds = 1,
        MaximalPendingPeriod__Seconds = 2,
        MustSimulateError = false,
        GatewayName = nameof(PersonMockGateway),
        TransactionName = "RetrievingOfSelection"
      }
    );
    
  }

  public Task<IPersonGateway.Adding.ResponseData> Add(IPersonGateway.Adding.RequestData requestData)
  {
    return MockGatewayHelper.SimulateDataSubmitting(
      requestData,
      getResponseData: () => mockDataSource.AddPerson(requestData),
      new MockGatewayHelper.SimulationOptions
      {
        MinimalPendingPeriod__Seconds = 1,
        MaximalPendingPeriod__Seconds = 2,
        MustSimulateError = false,
        GatewayName = nameof(PersonMockGateway),
        TransactionName = "Adding"
      }
    );
  }

  public Task Update(IPersonGateway.Updating.RequestData requestData)
  {
    return MockGatewayHelper.SimulateDataSubmitting<IPersonGateway.Updating.RequestData, object>(
      requestData,
      getResponseData: () =>
      {
        mockDataSource.UpdatePerson(requestData);
        return null;
      },
      new MockGatewayHelper.SimulationOptions
      {
        MinimalPendingPeriod__Seconds = 1,
        MaximalPendingPeriod__Seconds = 2,
        MustSimulateError = false,
        GatewayName = nameof(PersonMockGateway),
        TransactionName = nameof(IPersonGateway.Adding)
      }
    );
  }

  public Task Delete(string targetPersonID)
  {
    return MockGatewayHelper.SimulateDataSubmitting<string, object>(
      targetPersonID,
      getResponseData: () =>
      {
        this.mockDataSource.DeleteTask(targetPersonID);
        return null;
      },
      new MockGatewayHelper.SimulationOptions
      {
        MinimalPendingPeriod__Seconds = 1,
        MaximalPendingPeriod__Seconds = 2,
        MustSimulateError = false,
        GatewayName = nameof(PersonMockGateway),
        TransactionName = nameof(IPersonGateway.Adding)
      }
    );
  }
}