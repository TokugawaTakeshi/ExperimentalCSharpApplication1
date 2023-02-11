using Microsoft.AspNetCore.Components;

namespace FrontEndFramework.Mixins;


public interface IAcceptsSpaceSeparatedAdditionalCSS_Classes
{
  
  [Parameter] 
  public string? spaceSeparatedAdditionalCSS_Classes
  {
    get => null;
    set {}
  }
  
}