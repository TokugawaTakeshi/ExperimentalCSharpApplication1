namespace FrontEndFramework.ValidatableControl;


public interface IValidatableControl
{
  
  IValidatableControl HighlightInvalidInput();
  
  
  ValueTask<IValidatableControl.RootElementOffsetCoordinates> GetRootElementOffsetCoordinates();

  struct RootElementOffsetCoordinates
  {
    public required double Top { get; init; }
    public required double Left { get; init; }
  }
  
  
  IValidatableControl Focus();

}