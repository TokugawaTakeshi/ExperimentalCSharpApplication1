using Client.Services;


namespace Client.Layouts.Main;


public partial class MainLayout : Microsoft.AspNetCore.Components.LayoutComponentBase
{
  
  protected override void OnInitialized()
  {
    DrawerMenuService.onStateChanged += base.StateHasChanged;
  }


  protected void onClickDrawerClosingButton()
  {
    DrawerMenuService.toggleDisplaying();
  }
  
}