namespace Client.Services;


public abstract class DrawerMenuService
{
 
  public static event Action? onStateChanged;
  private static void NotifyStateChanged() => DrawerMenuService.onStateChanged?.Invoke();


  private static bool _isDrawerDisplaying = false;

  public static bool isDrawerDisplaying
  {
    get => DrawerMenuService._isDrawerDisplaying;
    private set
    {
      DrawerMenuService._isDrawerDisplaying = value;
      DrawerMenuService.NotifyStateChanged();
    } 
  }


  public static void toggleDisplaying()
  {
    DrawerMenuService.isDrawerDisplaying = !DrawerMenuService.isDrawerDisplaying;
  }

}