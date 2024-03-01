using LoadingIndicatorTypes = YamatoDaiwa.Frontend.Components.LoadingIndicator.LoadingIndicator.Types;


namespace FrontEndFramework.Components.BlockingLoadingOverlay;


public abstract class BlockingLoadingOverlayService
{

  public static event Action? onStateChanged;
  protected static void NotifyStateChanged() => BlockingLoadingOverlayService.onStateChanged?.Invoke();
  
  
  protected static LoadingIndicatorTypes _loadingIndicatorType = LoadingIndicatorTypes.variableWidthArcSpinner;
  public static LoadingIndicatorTypes loadingIndicatorType => BlockingLoadingOverlayService._loadingIndicatorType;


  protected static bool _isBlockingLoadingOverlayDisplaying = false;
  public static bool isBlockingLoadingOverlayDisplaying
  {
    get => BlockingLoadingOverlayService._isBlockingLoadingOverlayDisplaying;
    protected set
    {
      BlockingLoadingOverlayService._isBlockingLoadingOverlayDisplaying = value;
      BlockingLoadingOverlayService.NotifyStateChanged();
    }
  }


  public static void displayBlockingLoadingOverlay(
    LoadingIndicatorTypes loadingIndicatorType = LoadingIndicatorTypes.variableWidthArcSpinner
  )
  {
    BlockingLoadingOverlayService._loadingIndicatorType = loadingIndicatorType;
    BlockingLoadingOverlayService.isBlockingLoadingOverlayDisplaying = true;
  }

  public static void dismissBlockingLoadingOverlay()
  {
    BlockingLoadingOverlayService.isBlockingLoadingOverlayDisplaying = false;
  }
  
}