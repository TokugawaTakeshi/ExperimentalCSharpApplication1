using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components;
using UtilsIncubator;


namespace FrontEndFramework.Components.Badge;


public partial class Badge : ComponentBase
{

  [Parameter]
  public string? key { get; set; }
  
  [Parameter]
  public required string value { get; set; }
  
  [Parameter]
  public bool mustForceSingleLine { get; set; }
  
  
  public enum StandardThemes
  {
    regular
  }

  [Parameter] 
  public string theme { get; set; } = Badge.StandardThemes.regular.ToString();

  internal static bool mustConsiderThemesAsExternal = false;

  public void ConsiderThemesAsExternal()
  {
    Badge.mustConsiderThemesAsExternal = true;
  }

  [Parameter]
  public bool areThemesExternal { get; set; } = Badge.mustConsiderThemesAsExternal;
  
  
  public enum StandardGeometricVariations
  {
    regular
  }

  [Parameter]
  public string geometry { get; set; } = Badge.StandardThemes.regular.ToString();
  
  public enum GeometricModifiers
  {
    pillShape
  }

  [Parameter]
  public Badge.GeometricModifiers[] geometricModifiers { get; set; } = Array.Empty<Badge.GeometricModifiers>(); 
  
  
  public enum StandardDecorativeVariations
  {
    veryCatchyBright,
    catchyBright,
    modestlyCatchyBright,
    neutralBright,
    modestlyCalmingBright,
    calmingBright,
    achromaticBright,
    veryCatchyPastel,
    catchyPastel,
    modestlyCatchyPastel,
    neutralPastel,
    modestlyCalmingPastel,
    calmingPastel,
    achromaticPastel
  }

  private static object? CustomDecorativeVariations;

  public static void defineNewDecorativeVariations<TCustomDecorativeVariations>(
    TCustomDecorativeVariations[] newDecorativeVariations
  ) where TCustomDecorativeVariations : struct
  {
    
    if (!typeof(TCustomDecorativeVariations).IsEnum)
    {
      throw new System.Exception("Enumが待機された");
    }
    
    
    Badge.CustomDecorativeVariations = newDecorativeVariations;
    
  }

  protected string _decoration;

  [Parameter]
  public required object decoration
  {
    get => _decoration;
    set
    {

      if (value is Badge.StandardDecorativeVariations standardDecorativeVariation)
      {
        this._decoration = standardDecorativeVariation.ToString();
        return;
      }

      
      throw new Exception($"Invalid decorative variation");
      

      // if (
      //   Badge.CustomDecorativeVariations is not null &&
      //   value is Badge.CustomDecorativeVariations
      // )
      // {
      //   throw new Exception(
      //   $"The decorative variation must be either one of \"StandardDecorativeVariations\" or custom one "
      //   );
      // }
      //
      //
      // if (
      //   !value is StandardDecorativeVariations &&
      //   !value is CustomDecorativeVariations
      // )
      // {
      //   throw new Exception($"修飾的変形{value}は不明");
      // }

    }
  }
  
  public enum DecorativeModifiers
  {
    bordersDisguising
  }
  
  [Parameter]
  public Badge.DecorativeModifiers[] decorativeModifiers { get; set; } = Array.Empty<Badge.DecorativeModifiers>();


  [Parameter]
  public string? spaceSeparatedAdditionalCSS_Classes { get; set; }


  private string rootElementModifierCSS_Classes
  {
    get
    {

      return new List<string>().
          AddElementToEndIf("Badge--YDF__SingleLineMode", _ => this.mustForceSingleLine).
          AddElementToEndIf(
            $"Badge--YDF__${ this.theme.ToUpperCamelCase() }Theme",
            _ => Enum.GetNames(typeof(Badge.StandardThemes)).Length > 1 && !this.areThemesExternal
          ).
          AddElementToEndIf(
            $"Badge--YDF__${ this.geometry.ToUpperCamelCase() }Geometry",
            _ => Enum.GetNames(typeof(Badge.StandardGeometricVariations)).Length > 1
          ).
          AddElementToEndIf(
            "Badge--YDF__PllShapeGeometricModifier", 
            _ => this.geometricModifiers.Contains(Badge.GeometricModifiers.pillShape)
          ).
          AddElementToEndIf(
            $"Badge--YDF__{ this._decoration.ToUpperCamelCase() }Decoration",
            _ => Enum.GetNames(typeof(Badge.StandardDecorativeVariations)).Length > 1
          ).
          AddElementToEndIf(
            "Badge--YDF__BordersDisguisingDecorativeModifier", 
            _ => this.decorativeModifiers.Contains(Badge.DecorativeModifiers.bordersDisguising)
          ).
          StringifyEachElementAndJoin("");

    }
  }
  
}