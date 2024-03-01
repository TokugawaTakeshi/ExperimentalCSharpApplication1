using System.Reflection;
using FrontEndFramework.ValidatableControl;


namespace FrontEndFramework.InputtedValueValidation;


public class ValidatableControlsGroup
{

  public static bool HasInvalidInputs(object controlsPayload)
  {

    Type parameterType = controlsPayload.GetType();

    foreach (FieldInfo fieldInfo in parameterType.GetFields())
    {

      object? potentialControlPayload = fieldInfo.GetValue(controlsPayload); 
      
      if (potentialControlPayload is not ValidatableControl.Payload controlPayload)
      {
        throw new ArgumentException(
          "The controlsPayload must be tuple with values of \"ValidatableControl.Payload\" type."
        );
      }

      
      if (controlPayload.IsInvalid)
      {
        return true;
      }
      
    }
    
    return false;
    
  }

  public static void PointOutValidationErrors(object controlsPayload, string? scrollingContainerHTML_ID = null) {

    Type parameterType = controlsPayload.GetType();
    bool isCurrentControlTheFirstInvalidOne = true;
    

    foreach (FieldInfo fieldInfo in parameterType.GetFields())
    {

      object? potentialControlPayload = fieldInfo.GetValue(controlsPayload); 
      
      if (potentialControlPayload is not ValidatableControl.Payload controlPayload)
      {
        throw new ArgumentException(
          "The controlsPayload must be tuple with values of \"ValidatableControl.Payload\" type."
        );
      }


      if (!controlPayload.IsInvalid)
      {
        continue;
      }


      IValidatableControl componentInstance = controlPayload.GetComponentInstance();

      componentInstance.HighlightInvalidInput();

      if (isCurrentControlTheFirstInvalidOne)
      {
        
        componentInstance.Focus();

        if (scrollingContainerHTML_ID is not null)
        {
          // TODO Scroll
        }

        isCurrentControlTheFirstInvalidOne = false;

      }

    }
    
  }
  
}