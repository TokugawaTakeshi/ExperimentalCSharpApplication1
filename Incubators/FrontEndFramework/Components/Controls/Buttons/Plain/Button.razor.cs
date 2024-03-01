using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using YamatoDaiwa.CSharpExtensions;
using Utils;


namespace FrontEndFramework.Components.Controls.Buttons.Plain;


public partial class Button : ComponentBase
{

  public enum HTML_Types
  {
    regular,
    submit,
    inputButton,
    inputSubmit,
    inputReset
  }

  [Parameter] public HTML_Types HTML_Type { get; set; } = HTML_Types.regular;
  
  
  [Parameter] public string? label { get; set; }
  
  [Parameter] public string? accessibilityGuidance { get; set; }
  
  [Parameter] public string? internalURN { get; set; }

  [Parameter] public string? externalURI { get; set; }

  [Parameter] public bool disabled { get; set; } = false;

  [Inject] protected IJSRuntime JSRuntime { get; set; } = null!;

  private ElementReference rootElement;
  
  
  /* === Public methods ============================================================================================= */
  public async System.Threading.Tasks.Task focus()
  {
    await JSRuntime.InvokeVoidAsync("putFocusOnElement", this.rootElement);
  }
  
  
  /* --- Theme ------------------------------------------------------------------------------------------------------ */
  public enum StandardThemes { regular }

  protected static object? CustomThemes;
  
  public static void defineCustomThemes(Type CustomThemes) {

    if (!CustomThemes.IsEnum)
    {
      throw new System.ArgumentException("The custom themes must the enumeration.");
    }


    Button.CustomThemes = CustomThemes;

  }
  
  protected string _theme = Button.StandardThemes.regular.ToString();
  
  [Parameter] public object theme
  {
    get => this._theme;
    set
    {

      if (value is Button.StandardThemes standardTheme)
      {
        this._theme = standardTheme.ToString();
        return;
      }
      
      
      // TODO CustomThemes確認 https://github.com/TokugawaTakeshi/ExperimentalCSharpApplication1/issues/34#issuecomment-1500788874
      
      this._theme = value.ToString();

    }
  }
  
  internal static bool mustConsiderThemesAsExternal = false;

  public static void ConsiderThemesAsExternal()
  {
    Button.mustConsiderThemesAsExternal = true;
  }
  
  [Parameter] public bool areThemesExternal { get; set; } = Button.mustConsiderThemesAsExternal;

  
  /* --- Geometry --------------------------------------------------------------------------------------------------- */
  public enum StandardGeometricVariations
  {
    regular,
    small,
    linkLike
  }

  protected static object? CustomGeometricVariations;
  
  public static void defineCustomGeometricVariations(Type CustomGeometricVariations)
  {

    if (!CustomGeometricVariations.IsEnum)
    {
      throw new System.ArgumentException("The custom geometric variations must the enumeration.");
    }


    Button.CustomGeometricVariations = CustomGeometricVariations;

  }
  
  protected string _geometry = Button.StandardGeometricVariations.regular.ToString();
  
  [Parameter] public object geometry
  {
    get => this._geometry;
    set
    {

      if (value is Button.StandardGeometricVariations standardGeometricVariation)
      {
        this._geometry = standardGeometricVariation.ToString();
        return;
      }
      
      
      // TODO CustomGeometricVariations https://github.com/TokugawaTakeshi/ExperimentalCSharpApplication1/issues/34#issuecomment-1500788874

      this._geometry = value.ToString();

    }
  }
  
  
  /* --- Decorative variations -------------------------------------------------------------------------------------- */
  public enum StandardDecorativeVariations
  {
    regular,
    accented,
    linkLike
  }
  
  protected static object? CustomDecorativeVariations;
  
  public static void defineNewDecorativeVariations(Type CustomDecorativeVariations) {
    
    if (!CustomDecorativeVariations.IsEnum)
    {
      throw new System.Exception("The custom decorative variations must the enumeration.");
    }
      
        
    Button.CustomDecorativeVariations = CustomDecorativeVariations;
      
  }  
  
  protected string _decoration = Button.StandardDecorativeVariations.regular.ToString();

  [Parameter] public required object decoration
  {
    get => _decoration;
    set
    {

      if (value is Button.StandardDecorativeVariations standardDecorativeVariation)
      {
        this._decoration = standardDecorativeVariation.ToString();
        return;
      }

      
      // TODO CustomDecorativeVariations確認 https://github.com/TokugawaTakeshi/ExperimentalCSharpApplication1/issues/34#issuecomment-1500788874
      
      this._decoration = value.ToString();

    }
  }
  
  
  /* --- Computing of tag name of root element ---------------------------------------------------------------------- */
  private bool isButtonTheTagNameOfRootElement =>
      String.IsNullOrEmpty(this.internalURN) &&
      String.IsNullOrEmpty(this.externalURI) &&
      HTML_Type is HTML_Types.regular or HTML_Types.submit;

  private bool isInputTheTagNameOfRootElement => 
      HTML_Type is HTML_Types.inputButton or HTML_Types.inputSubmit or HTML_Types.inputReset;
  
  private bool isAnchorTheTagNameOfRootElement => !String.IsNullOrEmpty(this.externalURI);
  
  private bool isNavLinkTheRootElement => !String.IsNullOrEmpty(this.internalURN);
  
  
  /* --- Computing of the attributes -------------------------------------------------------------------------------- */
  private string? typeAttributeValueOfInputOrButtonElement {

    get
    {
      
      if (!this.isButtonTheTagNameOfRootElement && !this.isInputTheTagNameOfRootElement)
      {
        return null;
      }
      

      return this.HTML_Type switch
      {
        HTML_Types.regular => "button",
        HTML_Types.submit => "submit",
        HTML_Types.inputButton => "button",
        HTML_Types.inputSubmit => "submit",
        HTML_Types.inputReset => "reset",
        _ => null
      };
    }

  }
  
  
  /* --- CSS classes ------------------------------------------------------------------------------------------------ */
  [Parameter] public string? spaceSeparatedAdditionalCSS_Classes { get; set; }
  
  private string rootElementSpaceSeparatedClasses => new List<string>().
      AddElementsToEnd("Button--YDF").
      AddElementToEndIf(
        "Button--YDF__DisabledState",
        _ => (this.isAnchorTheTagNameOfRootElement || this.isNavLinkTheRootElement) && this.disabled
      ).
      AddElementToEndIf(
        $"Button--YDF__{ this._theme.ToUpperCamelCase() }Theme",
        _ => Enum.GetNames(typeof(Button.StandardThemes)).Length > 1 && !this.areThemesExternal
      ).
      AddElementToEndIf(
        $"Button--YDF__{ this._geometry.ToUpperCamelCase() }Geometry",
        _ => Enum.GetNames(typeof(Button.StandardGeometricVariations)).Length > 1
      ).
      AddElementToEndIf(
        $"Button--YDF__{ this._decoration.ToUpperCamelCase() }Decoration",
        _ => Enum.GetNames(typeof(Button.StandardDecorativeVariations)).Length > 1
      ).
      StringifyEachElementAndJoin(" ");

  
  /* --- Events handling -------------------------------------------------------------------------------------------- */
  [Parameter]
  public EventCallback<MouseEventArgs> onClick { get; set; }
  

  /* --- Children elements ------------------------------------------------------------------------------------------ */
  [Parameter] public RenderFragment? PrependedSVG_Icon { get; set; }
  [Parameter] public RenderFragment? AppendedSVG_Icon { get; set; }
  [Parameter] public RenderFragment? LoneSVG_Icon { get; set; }
  
}