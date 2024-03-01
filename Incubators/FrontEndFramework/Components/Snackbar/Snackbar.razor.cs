using FrontEndFramework.Components.Controls.RadioButton;
using YamatoDaiwa.Frontend.Helpers;
using YamatoDaiwa.CSharpExtensions;


namespace FrontEndFramework.Components.Snackbar;


public partial class Snackbar : Microsoft.AspNetCore.Components.ComponentBase 
{

  /* ━━━ Livecycle hooks ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  protected override void OnInitialized()
  {
    SnackbarService.onStateChanged += base.StateHasChanged;
  }

  
  /* ━━━ Theming ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public enum StandardThemes { regular }
  
  protected internal static Type? CustomThemes;

  public static void defineCustomThemes(Type CustomThemes)
  {
    YDF_ComponentsHelper.ValidateCustomTheme(CustomThemes);
    Snackbar.CustomThemes = CustomThemes;
  }

  protected string _theme = Snackbar.StandardThemes.regular.ToString();
  
  [Microsoft.AspNetCore.Components.Parameter]
  public object theme
  {
    get => this._theme;
    set => YDF_ComponentsHelper.AssignThemeIfItIsValid<Snackbar.StandardThemes>(
      value, Snackbar.CustomThemes, ref this._theme
    );
  }

  protected internal static bool mustConsiderThemesCSS_ClassesAsCommon = YDF_ComponentsHelper.areThemesCSS_ClassesCommon;

  public static void considerThemesAsCommon()
  {
    Snackbar.mustConsiderThemesCSS_ClassesAsCommon = true;
  }
  
  [Microsoft.AspNetCore.Components.Parameter]
  public bool areThemesCSS_ClassesCommon { get; set; } = 
      YDF_ComponentsHelper.areThemesCSS_ClassesCommon || Snackbar.mustConsiderThemesCSS_ClassesAsCommon;
  
  
  /* ─── Geometry ─────────────────────────────────────────────────────────────────────────────────────────────────── */
  public enum StandardGeometricVariations { regular }

  protected internal static Type? CustomGeometricVariations;
  
  public static void defineCustomGeometricVariations(Type CustomGeometricVariations)
  {
    YDF_ComponentsHelper.ValidateCustomGeometricVariation(CustomGeometricVariations);
    Snackbar.CustomGeometricVariations = CustomGeometricVariations;
  }

  protected string _geometry = Snackbar.StandardGeometricVariations.regular.ToString();

  [Microsoft.AspNetCore.Components.Parameter]
  public object geometry
  {
    get => this._geometry;
    set => YDF_ComponentsHelper.AssignGeometricVariationIfItIsValid<Snackbar.StandardGeometricVariations>(
    value, Snackbar.CustomGeometricVariations, ref this._geometry
    );
  }
  
  /* ─── Decorative variations ────────────────────────────────────────────────────────────────────────────────────── */
  public enum StandardDecorativeVariations
  {
    error,
    warning,
    info,
    success
  }

  protected internal static Type? CustomDecorativeVariations;

  public static void defineCustomDecorativeVariations(Type CustomDecorativeVariations) {
    YDF_ComponentsHelper.ValidateCustomDecorativeVariation(CustomDecorativeVariations);
    RadioButton.CustomDecorativeVariations = CustomDecorativeVariations;
  }

  
  /* ━━━ CSS classes ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Components.Parameter] 
  public string? rootElementModifierCSS_Class { get; set; }
  
  private string rootElementModifierCSS_Classes => new List<string>().
    
      AddElementToEndIf(
        $"Snackbar--YDF__{ this._theme.ToUpperCamelCase() }Theme",
        YDF_ComponentsHelper.MustApplyThemeCSS_Class(
          typeof(Snackbar.StandardThemes), Snackbar.CustomThemes, this.areThemesCSS_ClassesCommon
        )
      ).
      
      AddElementToEndIf(
        $"Snackbar--YDF__{ this._geometry.ToUpperCamelCase() }Geometry",
        YDF_ComponentsHelper.MustApplyGeometricVariationModifierCSS_Class(
          typeof(Snackbar.StandardGeometricVariations), Snackbar.CustomGeometricVariations
        )
      ).

      AddElementToEndIf(
        $"Snackbar--YDF__{ SnackbarService.decoration.ToUpperCamelCase() }Decoration",
        YDF_ComponentsHelper.MustApplyDecorativeVariationModifierCSS_Class(
          typeof(Snackbar.StandardDecorativeVariations), Snackbar.CustomDecorativeVariations
        )
      ).
      
      AddElementToEndIf(
        this.rootElementModifierCSS_Class ?? "", !String.IsNullOrEmpty(this.rootElementModifierCSS_Class)
      ).

      StringifyEachElementAndJoin(" ");
  
}
