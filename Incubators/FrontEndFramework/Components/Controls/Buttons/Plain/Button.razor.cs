using Microsoft.AspNetCore.Components;
using UtilsIncubator;


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

  // --- 一時的 ---------------------------------------------------------------------------------------------------------
  [Parameter] 
  public HTML_Types HTML_Type { get; set; } = HTML_Types.regular;
  
  /*
  private HTML_Types _HTML_Type = Button.HTML_Types.regular;
  
  [Parameter]
  public HTML_Types HTML_Type
  {
    get => this._HTML_Type;
    set
    {

      if (value is not Enum.GetNames<Button.HTML_Types>())
      {
        throw new Exception($"HTML_Type { value }は不明");  
      }
      
      
      this._HTML_Type = value;

    }
  }
  */
  
  // -------------------------------------------------------------------------------------------------------------------
  
  [Parameter] 
  public string? label { get; set; }
  
  [Parameter] 
  public string? accessibilityGuidance { get; set; }
  
  [Parameter]
  public string? internalURN { get; set; }

  [Parameter]
  public string? externalURI { get; set; }
  
  [Parameter] 
  public bool disabled { get; set; }
  
  
  // < === TODO テーマ当たり始末方法が分かり次第着手 https://github.com/TokugawaTakeshi/ExperimentalCSharpApplication1/issues/34
  public enum StandardThemes
  {
    regular
  }

  [Parameter] 
  public string theme { get; set; } = Button.StandardThemes.regular.ToString(); // TODO ① 正しく文字列と始末する

  protected static bool mustConsiderThemesAsExternal = false;
  
  public void ConsiderThemesAsExternal()
  {
    Button.mustConsiderThemesAsExternal = true;
  }
  
  [Parameter]
  public bool areThemesExternal { get; set; }
  
  
  public enum StandardGeometricVariations
  {
    regular,
    small,
    linkLike
  }

  [Parameter] 
  public string geometry { get; set; } = Button.StandardGeometricVariations.regular.ToString(); // TODO ① 正しく文字列と始末する

  
  public enum StandardDecorativeVariations
  {
    regular,
    accented,
    linkLike
  }

  [Parameter] 
  public string decoration { get; set; } = Button.StandardDecorativeVariations.regular.ToString(); // TODO ① 正しく文字列と始末する
  // > =================================================================================================================
  
  [Parameter]
  public string? spaceSeparatedAdditionalCSS_Classes { get; set; }
  
  
  /* === Computing of tag name of root element ====================================================================== */
  private bool isButtonTheTagNameOfRootElement =>
      String.IsNullOrEmpty(this.internalURN) &&
      String.IsNullOrEmpty(this.externalURI) &&
      HTML_Type is HTML_Types.regular or HTML_Types.submit;

  private bool isInputTheTagNameOfRootElement => 
      HTML_Type is HTML_Types.inputButton or HTML_Types.inputSubmit or HTML_Types.inputReset;
  
  private bool isAnchorTheTagNameOfRootElement => !String.IsNullOrEmpty(this.externalURI);
  
  private bool isNavLinkTheRootElement => !String.IsNullOrEmpty(this.internalURN);

  
  /* === Computing of the attributes ================================================================================ */
  private string? typeAttributeValueOfInputOrButtonElement {

    get
    {
      if (!this.isAnchorTheTagNameOfRootElement && !this.isNavLinkTheRootElement)
      {
        return null;
      }
      

      return HTML_Type switch
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

  private void onClick()
  {
    
  }
  
  private string rootElementSpaceSeparatedClasses => new List<string>().
      AddElementsToEnd("Button--YDF").
      AddElementToEndIf(
        "Button--YDF__DisabledState",
        _ => (isAnchorTheTagNameOfRootElement || isNavLinkTheRootElement) && this.disabled
      ).
      AddElementToEndIf(
        $"Button--YDF__${ this.theme.ToLowerCamelCase() }Theme",
        _ => Enum.GetNames(typeof(StandardThemes)).Length > 1 && !this.areThemesExternal
      ).
      AddElementToEndIf(
        $"Button--YDF__${ this.geometry.ToLowerCamelCase() }Geometry",
        _ => Enum.GetNames(typeof(StandardGeometricVariations)).Length > 1
      ).
      AddElementToEndIf(
        $"Button--YDF__${ this.decoration.ToLowerCamelCase() }Decoration",
        _ => Enum.GetNames(typeof(StandardDecorativeVariations)).Length > 1
      ).
      StringifyEachElementAndJoin(" ");

}