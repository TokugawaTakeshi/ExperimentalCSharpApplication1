using CommonSolution.Gateways;
using Client.SharedState;
using Microsoft.AspNetCore.Components;


namespace Client.SharedComponents.Viewers.People;


public partial class PeopleViewer : ComponentBase
{

  [Microsoft.AspNetCore.Components.Parameter] 
  public string? spaceSeparatedAdditionalCSS_Classes { get; set; }


  /* ━━━ ライフサイクルフック ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  protected override async System.Threading.Tasks.Task OnInitializedAsync()
  {
    PeopleSharedState.onStateChanged += base.StateHasChanged;
    await PeopleSharedState.retrievePeopleSelection();
  }


  /* ━━━ 操作の処理 ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  private async System.Threading.Tasks.Task onNewPersonSearchingRequestByFullOrPartialNameOrItsSpellHasBeenEmitted(
    string? newPersonSearchingRequestByFullOrPartialNameOrItsSpell
  ) 
  {
    await PeopleSharedState.retrievePeopleSelection(
      new IPersonGateway.SelectionRetrieving.RequestParameters
      {
        SearchingByFullOrPartialNameOrItsSpell = newPersonSearchingRequestByFullOrPartialNameOrItsSpell
      },
      mustResetSearchingByFullOrPartialNameOrItsSpell: newPersonSearchingRequestByFullOrPartialNameOrItsSpell is null
    );
  }

  private async System.Threading.Tasks.Task onClickPeopleSelectionRetrievingRetryingButton()
  {
    await PeopleSharedState.retrievePeopleSelection();
  }

  private void onClickPersonAddingButton()
  {
    // TODO
  }

  private async void onClickFilteringResettingButton()
  {
    await PeopleSharedState.retrievePeopleSelection(mustResetSearchingByFullOrPartialNameOrItsSpell: true);
  }

  private void onSelectPerson(CommonSolution.Entities.Person targetPerson)
  {
    PeopleSharedState.currentlySelectedPerson = targetPerson;
  }
  
  
  /* ━━━ 条件的表示 ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  private bool mustDisableSearchBox =>
      PeopleSharedState.isPeopleRetrievingInProgressOrNotStartedYet ||
      PeopleSharedState.totalPeopleCountInDataSource == 0;
  
}
