using Client.SharedState;
using Microsoft.AspNetCore.Components;


namespace Client.Pages.Person.Management;


public partial class PeopleManagementPageContent : ComponentBase
{

  /* ━━━ フィルド ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  private CommonSolution.Entities.Person? activePerson => PeopleSharedState.currentlySelectedPerson;
  
  private readonly string personManagerActivationGuidance = "人の詳細を閲覧する事や編集するにはカードをクリック・タップして下さい。";
  
  private string personManagerAdditionalCSS_Class =>
      this.activePerson is not null ? "PeopleManagementPage-PersonManager__VisibleAtNarrowScreens" : "";

  
  /* ━━━ ライフサイクルフック ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  protected override void OnInitialized()
  {
    PeopleSharedState.onStateChanged += base.StateHasChanged;
  }

}