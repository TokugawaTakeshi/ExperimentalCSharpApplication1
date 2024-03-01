using Microsoft.AspNetCore.Components;
using YamatoDaiwa.CSharpExtensions;


namespace FrontEndFramework.Components.Controls.Buttons.Plain.LoadingPlaceholder;


public partial class ButtonLoadingPlaceholder : ComponentBase
{

  /* --- Theme ------------------------------------------------------------------------------------------------------ */
  protected string _theme = Buttons.Plain.Button.StandardThemes.regular.ToString();
  
  [Parameter] public object theme
  {
    get => this._theme;
    set
    {

      if (value is Buttons.Plain.Button.StandardThemes standardTheme)
      {
        this._theme = standardTheme.ToString();
        return;
      }
      
      
      // TODO CustomThemes確認 https://github.com/TokugawaTakeshi/ExperimentalCSharpApplication1/issues/34#issuecomment-1500788874
      
      this._theme = value.ToString();

    }
  }

  [Parameter]
  public bool areThemesExternal { get; set; } = Buttons.Plain.Button.mustConsiderThemesAsExternal;
  
  
  /* --- Geometry --------------------------------------------------------------------------------------------------- */
  protected string _geometry = Buttons.Plain.Button.StandardGeometricVariations.regular.ToString();

  [Parameter] public object geometry
  {
    get => this._geometry;
    set
    {

      if (value is Buttons.Plain.Button.StandardGeometricVariations standardGeometricVariation)
      {
        this._geometry = standardGeometricVariation.ToString();
        return;
      }
      
      
      // TODO CustomGeometricVariations https://github.com/TokugawaTakeshi/ExperimentalCSharpApplication1/issues/34#issuecomment-1500788874

      this._geometry = value.ToString();

    }
  }
  
  
  /* --- CSS classes ------------------------------------------------------------------------------------------------ */
  [Parameter] public string? spaceSeparatedAdditionalCSS_Classes { get; set; }
  
  private string rootElementModifierCSS_Classes => new List<string>().
      AddElementToEndIf(
        $"Button--YDF__{ this._theme.ToLowerCamelCase() }Theme",
        _ => Enum.GetNames(typeof(Buttons.Plain.Button.StandardThemes)).Length > 1 && !this.areThemesExternal
      ).
      AddElementToEndIf(
        $"Button--YDF__{ this._geometry.ToLowerCamelCase() }Geometry",
        _ => Enum.GetNames(typeof(Buttons.Plain.Button.StandardGeometricVariations)).Length > 1
      ).
      StringifyEachElementAndJoin("");
  
}