using YamatoDaiwa.Frontend.Helpers;


namespace FrontEndFramework.Components.Snackbar;


public abstract class SnackbarService
{

  public static event Action? onStateChanged;
  protected static void NotifyStateChanged() => SnackbarService.onStateChanged?.Invoke();


  protected const ushort MESSAGE_DISPLAYING_DURATION__SECONDS = 5;
  
  
  protected static bool _isSnackbarDisplaying = false;
  public static bool isSnackbarDisplaying
  {
    get => SnackbarService._isSnackbarDisplaying;
    protected set
    {
      SnackbarService._isSnackbarDisplaying = value;
      SnackbarService.NotifyStateChanged();
    }
  }
  
  protected static string _message = "";
  public static string message
  {
    get => SnackbarService._message;
    protected set
    {
      SnackbarService._message = value;
      SnackbarService.NotifyStateChanged();
    }
  }


  protected static string _decoration = Snackbar.StandardDecorativeVariations.error.ToString();
  public static string decoration
  {
    get => SnackbarService._decoration;
    set
    {
      SnackbarService._decoration = value;
      SnackbarService.NotifyStateChanged();
    }
  }
  

  public static async System.Threading.Tasks.Task displaySnackbarForAWhile(
    string message,
    object decorativeVariation,
    ushort displayingDuration__seconds = SnackbarService.MESSAGE_DISPLAYING_DURATION__SECONDS
  )
  {

    SnackbarService.message = message;
    
    string validDecoration = ""; 
    
    YDF_ComponentsHelper.AssignDecorativeVariationIfItIsValid<Snackbar.StandardDecorativeVariations>(
      decorativeVariation, Snackbar.CustomDecorativeVariations, ref validDecoration
    );

    SnackbarService.decoration = validDecoration;
    SnackbarService.isSnackbarDisplaying = true;

    await Task.Delay(
      (int)TimeSpan.FromSeconds(displayingDuration__seconds).TotalMilliseconds
    );

    SnackbarService.hideSnackbar();
    
  }

  public static void hideSnackbar()
  {
    SnackbarService.isSnackbarDisplaying = false;
    SnackbarService.utilizeStateAfterHiding();
  }


  protected static void utilizeStateAfterHiding()
  {
    SnackbarService.message = "";
  }
  
}
