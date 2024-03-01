using CommonSolution.Entities;
using System.Diagnostics;
using CommonSolution.Gateways;


namespace Client.SharedState;


internal abstract class PeopleSharedState
{

  public static event Action? onStateChanged;
  private static void NotifyStateChanged() => PeopleSharedState.onStateChanged?.Invoke();
  
  
  /* ━━━ 選択 ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  private static Person? _currentlySelectedPerson = null;
  public static Person? currentlySelectedPerson
  {
    get => PeopleSharedState._currentlySelectedPerson;
    set
    {
      PeopleSharedState._currentlySelectedPerson = value;
      PeopleSharedState.NotifyStateChanged();
    }
  }
  
  
  /* ━━━ 取得 ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  private static List<Person> _peopleSelection = new ();
  public static List<Person> peopleSelection
  {
    get => PeopleSharedState._peopleSelection;
    private set
    {
      PeopleSharedState._peopleSelection = value;
      PeopleSharedState.NotifyStateChanged();
    }
  }


  private static uint _totalPeopleCountInDataSource = 0;
  public static uint totalPeopleCountInDataSource
  {
    get => PeopleSharedState._totalPeopleCountInDataSource;
    private set {
      PeopleSharedState._totalPeopleCountInDataSource = value;
      PeopleSharedState.NotifyStateChanged();
    }
  }
  
  private static uint _totalPeopleCountInSelection = 0;
  public static uint totalPeopleCountInSelection
  {
    get => PeopleSharedState._totalPeopleCountInSelection;
    private set
    {
      PeopleSharedState._totalPeopleCountInSelection = value;
      PeopleSharedState.NotifyStateChanged();
    }
  }

  private static string? _searchingByFullOrPartialNameOrItsSpell = null;
  public static string? searchingByFullOrPartialNameOrItsSpell
  {
    get => PeopleSharedState._searchingByFullOrPartialNameOrItsSpell;
    private set
    {
      PeopleSharedState._searchingByFullOrPartialNameOrItsSpell = value;
      PeopleSharedState.NotifyStateChanged();
    }
  }

  private static bool _isWaitingForPeopleSelectionRetrieving = true;
  public static bool isWaitingForPeopleSelectionRetrieving
  {
    get => PeopleSharedState._isWaitingForPeopleSelectionRetrieving;
    private set
    {
      PeopleSharedState._isWaitingForPeopleSelectionRetrieving = value;
      PeopleSharedState.NotifyStateChanged();
    }
  }
  
  private static bool _isPeopleSelectionBeingRetrievedNow = false;
  public static bool isPeopleSelectionBeingRetrievedNow
  {
    get => PeopleSharedState._isPeopleSelectionBeingRetrievedNow;
    private set
    {
      PeopleSharedState._isPeopleSelectionBeingRetrievedNow = value;
      PeopleSharedState.NotifyStateChanged();
    }
  }
  
  private static bool _hasPeopleSelectionRetrievingErrorOccurred = false;
  public static bool hasPeopleSelectionRetrievingErrorOccurred
  {
    get => PeopleSharedState._hasPeopleSelectionRetrievingErrorOccurred;
    private set
    {
      PeopleSharedState._hasPeopleSelectionRetrievingErrorOccurred = value;
      PeopleSharedState.NotifyStateChanged();
    }
  }

  public static bool isPeopleRetrievingInProgressOrNotStartedYet => 
      PeopleSharedState.isWaitingForPeopleSelectionRetrieving || PeopleSharedState.isPeopleSelectionBeingRetrievedNow;
  
  public static async System.Threading.Tasks.Task retrievePeopleSelection(
    IPersonGateway.SelectionRetrieving.RequestParameters? requestParameters = null,
    bool mustResetSearchingByFullOrPartialNameOrItsSpell = false
  ) {

    if (PeopleSharedState.isPeopleSelectionBeingRetrievedNow)
    {
      return;      
    }


    PeopleSharedState.currentlySelectedPerson = null; 
    PeopleSharedState.isWaitingForPeopleSelectionRetrieving = false;
    PeopleSharedState.isPeopleSelectionBeingRetrievedNow = true;
    PeopleSharedState.hasPeopleSelectionRetrievingErrorOccurred = false;

    if (mustResetSearchingByFullOrPartialNameOrItsSpell)
    {
      PeopleSharedState.searchingByFullOrPartialNameOrItsSpell = null;
    }
    else
    {
      PeopleSharedState.searchingByFullOrPartialNameOrItsSpell =
          requestParameters?.SearchingByFullOrPartialNameOrItsSpell ??
          PeopleSharedState.searchingByFullOrPartialNameOrItsSpell;  
    }
    
    
    IPersonGateway.SelectionRetrieving.ResponseData responseData;
    
    try
    {

      responseData = await ClientDependencies.Injector.gateways().Person.RetrieveSelection(
        new IPersonGateway.SelectionRetrieving.RequestParameters
        { SearchingByFullOrPartialNameOrItsSpell = PeopleSharedState.searchingByFullOrPartialNameOrItsSpell }
      );

    }
    catch (Exception exception)
    {
      
      PeopleSharedState.hasPeopleSelectionRetrievingErrorOccurred = true;
      PeopleSharedState.isPeopleSelectionBeingRetrievedNow = false;
      Debug.WriteLine(exception);
      
      return;
      
    }

    
    PeopleSharedState._peopleSelection = responseData.Items.ToList();
    PeopleSharedState._totalPeopleCountInSelection = responseData.TotalItemsCountInSelection;
    PeopleSharedState._totalPeopleCountInDataSource = responseData.TotalItemsCount;
    
    PeopleSharedState.isPeopleSelectionBeingRetrievedNow = false;
    
  }

}
