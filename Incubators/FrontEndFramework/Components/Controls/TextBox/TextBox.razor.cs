using System.Diagnostics;
using System.Text.RegularExpressions;
using FrontEndFramework.ValidatableControl;
using Microsoft.JSInterop;
using YamatoDaiwa.CSharpExtensions;
using YamatoDaiwa.Frontend.Components.Controls.CompoundControlShell;
using YamatoDaiwa.Frontend.Helpers;


namespace FrontEndFramework.Components.Controls.TextBox;


public partial class TextBox : InputtableControl, IValidatableControl
{

  /* ━━━ HTML type ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public enum HTML_Types
  {
    regular,
    email,
    number,
    password,
    phoneNumber,
    URI,
    hidden
  }
  
  [Microsoft.AspNetCore.Components.Parameter]
  public HTML_Types HTML_Type { get; set; } = HTML_Types.regular;
  
  [Microsoft.AspNetCore.Components.Parameter]  
  public bool multiline { get; set; } = false;
  
  
  /* ━━━ Payload / Validation ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  protected string rawValue = "";
  
  /* [ Initialization ] Will be initialized via required property "payload". */
  protected ValidatableControl.Payload _payload = default!;
  
  [Microsoft.AspNetCore.Components.Parameter]
  [Microsoft.AspNetCore.Components.EditorRequired]
  public required ValidatableControl.Payload payload
  {
    get => _payload;
    set
    {
      this._payload = value;
      this.synchronizeRawValueWithPayloadValue();
    }
  }
  
  
  public enum ValidityHighlightingActivationModes
  {
    immediate,
    onFirstInputtedCharacter,
    onFocusOut
  }
  
  [Microsoft.AspNetCore.Components.Parameter]
  [Microsoft.AspNetCore.Components.EditorRequired]
  public required TextBox.ValidityHighlightingActivationModes validityHighlightingActivationMode { get; set; }

  protected bool mustHighlightInvalidInputIfAnyValidationErrorsMessages = false;
  
  
  /* ━━━ Public methods ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  /* ─── Requirements ─────────────────────────────────────────────────────────────────────────────────────────────── */
  protected CompoundControlShell compoundControlShell = null!;
  
  [Microsoft.AspNetCore.Components.Inject]
  protected Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; } = null!;

  
  /* ─── Interface implementation ─────────────────────────────────────────────────────────────────────────────────── */
  public new IValidatableControl HighlightInvalidInput()
  {
    base.HighlightInvalidInput();
    return this;
  }
  
  public ValueTask<IValidatableControl.RootElementOffsetCoordinates> GetRootElementOffsetCoordinates()
  {
    return this.JSRuntime.InvokeAsync<IValidatableControl.RootElementOffsetCoordinates>(
          "getDOM_ElementOffsetCoordinates", this.compoundControlShell
        );
  }
  
  public IValidatableControl Focus()
  {
    _ = this.JSRuntime.InvokeVoidAsync("putFocusOnElement", this.nativeInputAcceptingElement);
    return this;
  }

  
  /* ━━━ Constructor ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public TextBox()
  {
    this.INPUT_OR_TEXT_AREA_ELEMENT_HTML_ID = base.coreElementHTML_ID ?? TextBox.generateInputOrTextAreaElementHTML_ID();
  }
  
  
  /* ━━━ Lifecycle hooks ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  protected override void OnInitialized()
  {
    
    base.OnInitialized();

    if (
      String.IsNullOrEmpty(this.label) &&
      String.IsNullOrEmpty(this.externalLabelHTML_ID) &&
      String.IsNullOrEmpty(this.accessibilityGuidance)
    )
    {
      throw new Exception(
        "For the basic accessibility, either \"label\" or \"externalLabelHTML_ID\" or \"accessibilityGuidance\"" +
            "must be specific with non-empty string for the \"TextBox\" component."
      );
    }
    
    this.synchronizeRawValueWithPayloadValue();
    this.mustHighlightInvalidInputIfAnyValidationErrorsMessages =
          this.validityHighlightingActivationMode == TextBox.ValidityHighlightingActivationModes.immediate;

  }

  
  /* ━━━ Preventing of inputting of invalid value ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Components.Parameter]  
  public ulong? minimalCharactersCount { get; set; }
  
  [Microsoft.AspNetCore.Components.Parameter]  
  public ulong? maximalCharactersCount { get; set; }
  
  
  [Microsoft.AspNetCore.Components.Parameter]  
  public ulong? minimalNumericValue { get; set; }
  
  [Microsoft.AspNetCore.Components.Parameter]  
  public ulong? maximalNumericValue { get; set; }
  
  
  [Microsoft.AspNetCore.Components.Parameter]  
  public bool valueMustBeTheNonNegativeIntegerOfRegularNotation { get; set; } = false;
   
  [Microsoft.AspNetCore.Components.Parameter]  
  public bool valueMustBeTheDigitsSequence { get; set; } = false;

  
  /* ━━━ Raw value transforments ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Components.Parameter] 
  public bool mustConvertEmptyStringToNull { get; set; }
  
  [Microsoft.AspNetCore.Components.Parameter] 
  public bool mustConvertEmptyStringToZero { get; set; }
  
  
  /* ━━━ Events handling ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  private void onKeyDown(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs keyboardEventArguments)
  {
    if (
      (this.valueMustBeTheNonNegativeIntegerOfRegularNotation || this.valueMustBeTheDigitsSequence) && 
      new Regex("^[+\\-e.]$").IsMatch(keyboardEventArguments.Key))
    {
    }
  }
  
  private void onInputEventHandler(Microsoft.AspNetCore.Components.ChangeEventArgs inputtingEvent)
  {

    string stringifiedValue = inputtingEvent.Value?.ToString() ?? "";
    
    if (stringifiedValue.Length == 0)
    {

      if (this.mustConvertEmptyStringToZero)
      {
        this.rawValue = "0";
        this.payload.SetValue(0, asynchronousValidationDelay__seconds: 1);
        return;
      }
    
      
      if (this.mustConvertEmptyStringToNull)
      {
        this.payload.SetValue(null, asynchronousValidationDelay__seconds: 1);
      }
      
    }

    
    
    this._payload.SetValue(inputtingEvent.Value?.ToString() ?? "");

    if (
      this.validityHighlightingActivationMode == TextBox.ValidityHighlightingActivationModes.onFirstInputtedCharacter &&
      !this.mustHighlightInvalidInputIfAnyValidationErrorsMessages
    )
    {
      this.mustHighlightInvalidInputIfAnyValidationErrorsMessages = true;
    }
    
  }
  
  }

  
  /* ━━━ Textings ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Components.Parameter]
  public string? placeholder { get; set; }

  
  
  
  
  /* ━━━ Raw value transformations ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  
  
  /* ━━━ HTML IDs ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  /* ─── Input or text area ──────────────────────────────────────────────────────────────────────────────────────── */
  protected static uint counterForInputOrTextAreaElementHTML_ID_Generating = 0;
  
  protected static string generateInputOrTextAreaElementHTML_ID() {
    TextBox.counterForInputOrTextAreaElementHTML_ID_Generating++;
    return $"TEXT_BOX--YDF-INPUT_OR_TEXT_AREA_ELEMENT-{ TextBox.counterForInputOrTextAreaElementHTML_ID_Generating }";
  }

  protected readonly string INPUT_OR_TEXT_AREA_ELEMENT_HTML_ID;
  
  /* ─── Label ───────────────────────────────────────────────────────────────────────────────────────────────────── */
  [Microsoft.AspNetCore.Components.Parameter] 
  public string? labelElementHTML_ID { get; set; }
  
  
  /* ━━━ Children components/elements ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  protected Microsoft.AspNetCore.Components.ElementReference nativeInputAcceptingElement;

  
  
  

  /* ━━━ Theming ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public enum StandardThemes { regular }
  
  protected internal static Type? CustomThemes;

  public static void defineCustomThemes(Type CustomThemes)
  {
    YDF_ComponentsHelper.ValidateCustomTheme(CustomThemes);
    TextBox.CustomThemes = CustomThemes;
  }

  protected string _theme = TextBox.StandardThemes.regular.ToString();
  
  [Microsoft.AspNetCore.Components.Parameter]
  public object theme
  {
    get => this._theme;
    set => YDF_ComponentsHelper.AssignThemeIfItIsValid<TextBox.StandardThemes>(
      value, TextBox.CustomThemes, ref this._theme
    );
  }

  protected internal static bool mustConsiderThemesCSS_ClassesAsCommon = YDF_ComponentsHelper.areThemesCSS_ClassesCommon;

  public static void considerThemesAsCommon()
  {
    TextBox.mustConsiderThemesCSS_ClassesAsCommon = true;
  }
  
  [Microsoft.AspNetCore.Components.Parameter]
  public bool areThemesCSS_ClassesCommon { get; set; } = 
      YDF_ComponentsHelper.areThemesCSS_ClassesCommon || TextBox.mustConsiderThemesCSS_ClassesAsCommon;
  
  
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
    TextBox.CustomGeometricVariations = CustomGeometricVariations;
  }

  protected string _geometry = TextBox.StandardGeometricVariations.regular.ToString();

  [Microsoft.AspNetCore.Components.Parameter]
  public object geometry
  {
    get => this._geometry;
    set => YDF_ComponentsHelper.AssignGeometricVariationIfItIsValid<TextBox.StandardGeometricVariations>(
    value, TextBox.CustomGeometricVariations, ref this._geometry
    );
  }
  
  
  /* ─── Decoration ───────────────────────────────────────────────────────────────────────────────────────────────── */
  public enum StandardDecorativeVariations { regular }

  protected internal static Type? CustomDecorativeVariations;

  public static void defineCustomDecorativeVariations(Type CustomDecorativeVariations) {
    YDF_ComponentsHelper.ValidateCustomDecorativeVariation(CustomDecorativeVariations);
    TextBox.CustomDecorativeVariations = CustomDecorativeVariations;
  }

  protected string _decoration = TextBox.StandardDecorativeVariations.regular.ToString();

  [Microsoft.AspNetCore.Components.Parameter]
  public required object decoration
  {
    get => _decoration;
    set => YDF_ComponentsHelper.AssignDecorativeVariationIfItIsValid<TextBox.StandardDecorativeVariations>(
      value, TextBox.CustomDecorativeVariations, ref this._decoration
    );
  }
  
  
  /* ━━━ CSS classes ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Components.Parameter]
  public string? spaceSeparatedAdditionalCSS_Classes { get; set; }
  
  private string rootElementModifierCSS_Classes => new List<string>().
      
      AddElementToEndIf("TextBox--YDF__Multiline", this.multiline).
      AddElementToEndIf("TextBox--YDF__DisabledState", this.disabled).
      AddElementToEndIf("TextBox--YDF__ReadonlyState", this.@readonly).
    
      AddElementToEndIf(
        $"TextBox--YDF__{ this._theme.ToUpperCamelCase() }Theme",
        YDF_ComponentsHelper.MustApplyThemeCSS_Class(
          typeof(TextBox.StandardThemes), TextBox.CustomThemes, this.areThemesCSS_ClassesCommon
        )
      ).
      
      AddElementToEndIf(
        $"Badge--YDF__{ this._geometry.ToUpperCamelCase() }Geometry",
        YDF_ComponentsHelper.MustApplyGeometricVariationModifierCSS_Class(
          typeof(TextBox.StandardGeometricVariations), TextBox.CustomGeometricVariations
        )
      ).
      
      AddElementToEndIf(
        $"Badge--YDF__{ this._decoration.ToUpperCamelCase() }Decoration",
        YDF_ComponentsHelper.MustApplyDecorativeVariationModifierCSS_Class(
          typeof(TextBox.StandardDecorativeVariations), TextBox.CustomDecorativeVariations
        )
      ).
      
      StringifyEachElementAndJoin(" ");
   
  
  
  
}