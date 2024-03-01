using YamatoDaiwa.Frontend.Helpers;
using YamatoDaiwa.CSharpExtensions;


namespace FrontEndFramework.Components.Controls.RadioButton;


public partial class RadioButton : Microsoft.AspNetCore.Components.ComponentBase
// public partial class RadioButton : YDF_Component
{

  [Microsoft.AspNetCore.Components.Parameter]
  public string? label { get; set; }
  
  [Microsoft.AspNetCore.Components.Parameter]
  public string? HTML_Name { get; set; }
  
  [Microsoft.AspNetCore.Components.Parameter]
  public string? HTML_Value { get; set; }
  
  [Microsoft.AspNetCore.Components.Parameter]
  public string? inputElementHTML_ID { get; set; }

  [Microsoft.AspNetCore.Components.Parameter]
  public bool @checked { get; set; } = false;
  
  [Microsoft.AspNetCore.Components.Parameter]
  public bool disabled { get; set; } = false;
  
  [Microsoft.AspNetCore.Components.Parameter]
  public bool @readonly { get; set; } = false;
  
  
  /* ━━━ Theming ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public enum StandardThemes { regular }
  
  protected internal static Type? CustomThemes;

  public static void defineCustomThemes(Type CustomThemes)
  {
    YDF_ComponentsHelper.ValidateCustomTheme(CustomThemes);
    RadioButton.CustomThemes = CustomThemes;
  }

  protected string _theme = RadioButton.StandardThemes.regular.ToString();
  
  [Microsoft.AspNetCore.Components.Parameter]
  public object theme
  {
    get => this._theme;
    set => YDF_ComponentsHelper.AssignThemeIfItIsValid<RadioButton.StandardThemes>(
      value, RadioButton.CustomThemes, ref this._theme
    );
  }

  protected internal static bool mustConsiderThemesCSS_ClassesAsCommon = YDF_ComponentsHelper.areThemesCSS_ClassesCommon;

  public static void considerThemesAsCommon()
  {
    RadioButton.mustConsiderThemesCSS_ClassesAsCommon = true;
  }
  
  [Microsoft.AspNetCore.Components.Parameter]
  public bool areThemesCSS_ClassesCommon { get; set; } = 
      YDF_ComponentsHelper.areThemesCSS_ClassesCommon || RadioButton.mustConsiderThemesCSS_ClassesAsCommon;
  

  /* ─── Geometry ─────────────────────────────────────────────────────────────────────────────────────────────────── */
  public enum StandardGeometricVariations
  {
    regular, 
    small
  }

  protected internal static Type? CustomGeometricVariations;
  
  public static void defineCustomGeometricVariations(Type CustomGeometricVariations)
  {
    YDF_ComponentsHelper.ValidateCustomGeometricVariation(CustomGeometricVariations);
    RadioButton.CustomGeometricVariations = CustomGeometricVariations;
  }

  protected string _geometry = RadioButton.StandardGeometricVariations.regular.ToString();

  [Microsoft.AspNetCore.Components.Parameter]
  public object geometry
  {
    get => this._geometry;
    set => YDF_ComponentsHelper.AssignGeometricVariationIfItIsValid<RadioButton.StandardGeometricVariations>(
    value, RadioButton.CustomGeometricVariations, ref this._geometry
    );
  }
  
  public enum GeometricModifiers
  {
    pillShape,
    squareShape,
    singleLine,
    noLeftBorderAndRoundings,
    noRightBorderAndRoundings,
  }

  [Microsoft.AspNetCore.Components.Parameter] 
  public RadioButton.GeometricModifiers[] geometricModifiers { get; set; } = Array.Empty<RadioButton.GeometricModifiers>();
  
  
  /* ─── Decorative variations ────────────────────────────────────────────────────────────────────────────────────── */
  public enum StandardDecorativeVariations { regular }

  protected internal static Type? CustomDecorativeVariations;

  public static void defineCustomDecorativeVariations(Type CustomDecorativeVariations) {
    YDF_ComponentsHelper.ValidateCustomDecorativeVariation(CustomDecorativeVariations);
    RadioButton.CustomDecorativeVariations = CustomDecorativeVariations;
  }

  protected string _decoration = RadioButton.StandardDecorativeVariations.regular.ToString();

  [Microsoft.AspNetCore.Components.Parameter]
  public object decoration
  {
    get => _decoration;
    set => YDF_ComponentsHelper.AssignDecorativeVariationIfItIsValid<RadioButton.StandardDecorativeVariations>(
      value, RadioButton.CustomDecorativeVariations, ref this._decoration
    );
  }
  
  public enum DecorativeModifiers { bordersDisguising }
  
  [Microsoft.AspNetCore.Components.Parameter]
  public RadioButton.DecorativeModifiers[] decorativeModifiers { get; set; } = Array.Empty<RadioButton.DecorativeModifiers>();

  
  /* ━━━ CSS classes ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Components.Parameter] 
  public string? spaceSeparatedAdditionalCSS_Classes { get; set; }
  
  private string rootElementModifierSpaceSeparatedCSS_Classes => new List<string>().
    
      AddElementToEndIf("RadioButton--YDF__CheckedState", this.@checked).
      AddElementToEndIf("RadioButton--YDF__UncheckedState", !this.@checked).
      AddElementToEndIf("RadioButton--YDF__DisabledState", !this.disabled).
      
      AddElementToEndIf(
        $"RadioButton--YDF__{ this._theme.ToUpperCamelCase() }Theme",
        YDF_ComponentsHelper.MustApplyThemeCSS_Class(
          typeof(RadioButton.StandardThemes), RadioButton.CustomThemes, this.areThemesCSS_ClassesCommon
        )
      ).
      
      AddElementToEndIf(
        $"RadioButton--YDF__{ this._geometry.ToUpperCamelCase() }Geometry",
        YDF_ComponentsHelper.MustApplyGeometricVariationModifierCSS_Class(
          typeof(RadioButton.StandardGeometricVariations), RadioButton.CustomGeometricVariations
        )
      ).
      AddElementToEndIf(
        "RadioButton--YDF__PllShapeGeometricModifier", 
        this.geometricModifiers.Contains(RadioButton.GeometricModifiers.pillShape)
      ).
      AddElementToEndIf(
        "RadioButton--YDF__SquareShapeGeometricModifier", 
        this.geometricModifiers.Contains(RadioButton.GeometricModifiers.squareShape)
      ).
      AddElementToEndIf(
        "RadioButton--YDF__SingleLineGeometricModifier", 
        this.geometricModifiers.Contains(RadioButton.GeometricModifiers.singleLine)
      ).
      AddElementToEndIf(
        "RadioButton--YDF__NoLeftBorderAndRoundingsGeometricModifier", 
        this.geometricModifiers.Contains(RadioButton.GeometricModifiers.noLeftBorderAndRoundings)
      ).
      AddElementToEndIf(
        "RadioButton--YDF__NoRightBorderAndRoundingsGeometricModifier", 
        this.geometricModifiers.Contains(RadioButton.GeometricModifiers.noRightBorderAndRoundings)
      ).
      
      AddElementToEndIf(
        $"RadioButton--YDF__{ this._decoration.ToUpperCamelCase() }Decoration",
        YDF_ComponentsHelper.MustApplyDecorativeVariationModifierCSS_Class(
          typeof(RadioButton.StandardDecorativeVariations), RadioButton.CustomDecorativeVariations
        )
      ).
      AddElementToEndIf(
        "RadioButton--YDF__BordersDisguisingDecorativeModifier", 
        this.decorativeModifiers.Contains(RadioButton.DecorativeModifiers.bordersDisguising)
      ).
      
      StringifyEachElementAndJoin(" ");

  
  /* ━━━ HTML IDs ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  protected static uint counterForInputElementHTML_ID_Generating = 0;
  
  protected static string generateInputElementHTML_ID() {
    RadioButton.counterForInputElementHTML_ID_Generating++;
    return $"RADIO_BUTTON--YDF-${ RadioButton.counterForInputElementHTML_ID_Generating }";
  }

  protected readonly string INPUT_ELEMENT_HTML_ID;


  
  /* ━━━ Constructor ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public RadioButton()
  {
    
    this.INPUT_ELEMENT_HTML_ID = this.inputElementHTML_ID ?? RadioButton.generateInputElementHTML_ID();
    
  }
  
}