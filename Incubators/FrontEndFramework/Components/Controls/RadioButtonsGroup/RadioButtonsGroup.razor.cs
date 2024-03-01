using FrontEndFramework.ValidatableControl;
using YamatoDaiwa.Frontend.Helpers;
using YamatoDaiwa.CSharpExtensions;


namespace FrontEndFramework.Components.Controls.RadioButtonsGroup;


public partial class RadioButtonsGroup : InputtableControl, IValidatableControl
{

  public struct SelectingOption
  {
    public required string label { get; init; }
    public required string key { get; init; }
  }
    
  [Microsoft.AspNetCore.Components.Parameter]
  public required SelectingOption[] selectingOptions { get; set; }

  
  /* ━━━ Payload ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  protected SelectingOption? currentlySelectedOption = null;
  protected ValidatableControl.Payload _payload;
  
  [Microsoft.AspNetCore.Components.Parameter]
  public required ValidatableControl.Payload payload
  {
    get => _payload;
    set
    {
      this._payload = value;
      this.synchronizeRawValueWithPayloadValue();
    }
  }
    
  
  /* ━━━ Public methods ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Components.Inject] 
  protected Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; } = null!;
  
  
  public new IValidatableControl HighlightInvalidInput()
  {
    base.HighlightInvalidInput();
    return this;
  }
  
  public ValueTask<IValidatableControl.RootElementOffsetCoordinates> GetRootElementOffsetCoordinates()
  {
    throw new NotImplementedException();
  }
  
  public IValidatableControl Focus()
  {
    // TODO 非同期呼び出し始末
    // TODO 偽造フォカス
    return this;
  }
  
  
  /* ━━━ HTML IDs ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  protected static uint counterForID_Generating = 0;
  
  protected static string generateInputOrTextAreaElementHTML_ID() {
    RadioButtonsGroup.counterForID_Generating++;
    return $"RADIO_BUTTONS_GROUP--YDF-${ RadioButtonsGroup.counterForID_Generating }";
  }

  protected string BASIC_ID = RadioButtonsGroup.generateInputOrTextAreaElementHTML_ID();
  
  
  /* ─── Label ───────────────────────────────────────────────────────────────────────────────────────────────────── */
  [Microsoft.AspNetCore.Components.Parameter] 
  public string? labelElementHTML_ID { get; set; }
  
  [Microsoft.AspNetCore.Components.Parameter]
  public string radioButtonsHTML_Name { get; set; }
  
  
  /* ━━━ Theming ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public enum StandardThemes { regular }
  
  protected internal static Type? CustomThemes;

  public static void defineCustomThemes(Type CustomThemes)
  {
    YDF_ComponentsHelper.ValidateCustomTheme(CustomThemes);
    RadioButtonsGroup.CustomThemes = CustomThemes;
  }

  protected string _theme = RadioButtonsGroup.StandardThemes.regular.ToString();
  
  [Microsoft.AspNetCore.Components.Parameter]
  public object theme
  {
    get => this._theme;
    set => YDF_ComponentsHelper.AssignThemeIfItIsValid<RadioButtonsGroup.StandardThemes>(
      value, RadioButtonsGroup.CustomThemes, ref this._theme
    );
  }

  protected internal static bool mustConsiderThemesCSS_ClassesAsCommon = YDF_ComponentsHelper.areThemesCSS_ClassesCommon;

  public static void considerThemesAsCommon()
  {
    RadioButtonsGroup.mustConsiderThemesCSS_ClassesAsCommon = true;
  }
  
  [Microsoft.AspNetCore.Components.Parameter]
  public bool areThemesCSS_ClassesCommon { get; set; } = 
      YDF_ComponentsHelper.areThemesCSS_ClassesCommon || RadioButtonsGroup.mustConsiderThemesCSS_ClassesAsCommon;
  
  
  /* ─── Geometry ─────────────────────────────────────────────────────────────────────────────────────────────────── */
  public enum StandardGeometricVariations { regular }

  protected internal static Type? CustomGeometricVariations;
  
  public static void defineCustomGeometricVariations(Type CustomGeometricVariations)
  {
    YDF_ComponentsHelper.ValidateCustomGeometricVariation(CustomGeometricVariations);
    RadioButtonsGroup.CustomGeometricVariations = CustomGeometricVariations;
  }

  protected string _geometry = RadioButtonsGroup.StandardGeometricVariations.regular.ToString();

  [Microsoft.AspNetCore.Components.Parameter]
  public object geometry
  {
    get => this._geometry;
    set => YDF_ComponentsHelper.AssignGeometricVariationIfItIsValid<RadioButtonsGroup.StandardGeometricVariations>(
    value, RadioButtonsGroup.CustomGeometricVariations, ref this._geometry
    );
  }
  
  
  /* ─── Decorative variations ────────────────────────────────────────────────────────────────────────────────────── */
  public enum StandardDecorativeVariations { regular }

  protected internal static Type? CustomDecorativeVariations;

  public static void defineCustomDecorativeVariations(Type CustomDecorativeVariations) {
    YDF_ComponentsHelper.ValidateCustomDecorativeVariation(CustomDecorativeVariations);
    RadioButtonsGroup.CustomDecorativeVariations = CustomDecorativeVariations;
  }

  protected string _decoration = RadioButtonsGroup.StandardDecorativeVariations.regular.ToString();

  [Microsoft.AspNetCore.Components.Parameter]
  public required object decoration
  {
    get => _decoration;
    set => YDF_ComponentsHelper.AssignDecorativeVariationIfItIsValid<RadioButtonsGroup.StandardDecorativeVariations>(
      value, RadioButtonsGroup.CustomDecorativeVariations, ref this._decoration
    );
  }
  
  /* ━━━ Other flags ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Components.Parameter]  
  public bool @readonly { get; set; } = false;

  
  /* ━━━ CSS classes ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Components.Parameter]
  public string? spaceSeparatedAdditionalCSS_Classes { get; set; }
  
  private string rootElementModifierCSS_Classes => new List<string>().
      
      AddElementToEndIf("RadioButtonsGroup--YDF__DisabledState", this.disabled).
      AddElementToEndIf("RadioButtonsGroup--YDF__ReadonlyState", this.@readonly).
    
      AddElementToEndIf(
        $"RadioButtonsGroup--YDF__{ this._theme.ToUpperCamelCase() }Theme",
        YDF_ComponentsHelper.MustApplyThemeCSS_Class(
          typeof(RadioButtonsGroup.StandardThemes), RadioButtonsGroup.CustomThemes, this.areThemesCSS_ClassesCommon
        )
      ).
      
      AddElementToEndIf(
        $"Badge--YDF__{ this._geometry.ToUpperCamelCase() }Geometry",
        YDF_ComponentsHelper.MustApplyGeometricVariationModifierCSS_Class(
          typeof(RadioButtonsGroup.StandardGeometricVariations), RadioButtonsGroup.CustomGeometricVariations
        )
      ).
      
      AddElementToEndIf(
        $"Badge--YDF__{ this._decoration.ToUpperCamelCase() }Decoration",
        YDF_ComponentsHelper.MustApplyDecorativeVariationModifierCSS_Class(
          typeof(RadioButtonsGroup.StandardDecorativeVariations), RadioButtonsGroup.CustomDecorativeVariations
        )
      ).
      
      StringifyEachElementAndJoin(" ");
  
  
  /* ━━━  Constructor ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public RadioButtonsGroup()
  {
    this.radioButtonsHTML_Name ??= this.BASIC_ID;
  }
  
  
  /* ━━━ Other ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  protected bool mustHighlightInvalidInputIfAnyValidationErrorsMessages = true;
  
  protected void synchronizeRawValueWithPayloadValue()
  {
    this.currentlySelectedOption = (SelectingOption)this.payload.Value;
  }
  
}