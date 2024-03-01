using System.Diagnostics;
using FrontEndFramework.Components.Controls.Buttons.Plain;
using FrontEndFramework.ValidatableControl;
using Microsoft.JSInterop;
using Utils;
using YamatoDaiwa.Frontend.Helpers;
using YamatoDaiwa.CSharpExtensions;

namespace FrontEndFramework.Components.Controls.FilesUploader;


public partial class FilesUploader : InputtableControl, IValidatableControl
{

  /* ━━━ Payload ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  protected ValidatableControl.Payload _payload;
  
  [Microsoft.AspNetCore.Components.Parameter]
  public required ValidatableControl.Payload payload
  {
    get => _payload;
    set => this._payload = value;
  }
  
  private void onInputEventHandler(Microsoft.AspNetCore.Components.ChangeEventArgs inputtingEvent)
  {
    this._payload.SetValue(inputtingEvent.Value?.ToString() ?? "");
  }
  
  
  /* ━━━ Preventing of inputting of invalid value ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Components.Parameter]  
  public ulong? minimalFilesCount { get; set; }

  protected ulong? _maximalFilesCount;
  
  [Microsoft.AspNetCore.Components.Parameter]
  public ulong? maximalFilesCount
  {
    get => this._maximalFilesCount;
    set
    {
      
      if (this.minimalFilesCount is not null && value > this.minimalFilesCount)
      {
        throw new ArgumentOutOfRangeException(
          nameof(this.maximalFilesCount),
          "\"maximalFilesCount\" could not be less than \"minimalFilesCount\"."
        );
      }


      this._maximalFilesCount = value;

    }
  }

  [Microsoft.AspNetCore.Components.Parameter]
  public string[] supportedFilesNamesExtensions { get; set; } = Array.Empty<string>();
  
  
  /* ━━━ HTML IDs ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  /* ─── Input element ───────────────────────────────────────────────────────────────────────────────────────────── */
  protected static uint counterForInputElementHTML_ID_Generating = 0;
  
  protected static string generateInputElementHTML_ID() {
    FilesUploader.counterForInputElementHTML_ID_Generating++;
    return $"FILES_UPLOADER--YDE-INPUT_ELEMENT-${ FilesUploader.counterForInputElementHTML_ID_Generating }";
  }

  protected readonly string INPUT_ELEMENT_HTML_ID;
  
  /* ─── Label ───────────────────────────────────────────────────────────────────────────────────────────────────── */
  [Microsoft.AspNetCore.Components.Parameter] 
  public string? labelElementHTML_ID { get; set; }

  
  /* ━━━ Optional elements ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Components.Parameter]
  public bool noButton { get; set; } = false;
  
  [Microsoft.AspNetCore.Components.Parameter]
  public bool noDragAndDropArea { get; set; } = false;
  
  /* ━━━ Other flags ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Components.Parameter]
  public bool @readonly { get; set; } = false;
  
  
  /* ━━━ Children components/elements ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  protected Microsoft.AspNetCore.Components.ElementReference nativeInputElement;
  protected Button? filesPickingButton = null;
  
  /* ━━━ Public methods ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
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
    JSRuntime.InvokeVoidAsync("putFocusOnElement", this.nativeInputElement);
    return this;
  }
  
  
  /* ━━━ Actions handling ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  protected void onClickFilesPickingButton()
  {
    JSRuntime.InvokeVoidAsync("triggerLeftClickEvent", this.nativeInputElement);
  }

  protected void onSelectNewImagesByFilesExplorerSession()
  {
    
  }

  protected void onClickDeleteFileButton()
  {
    
  }
  
  
  /* ━━━ Theming ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public enum StandardThemes { regular }
  
  protected internal static Type? CustomThemes;

  public static void defineCustomThemes(Type CustomThemes)
  {
    YDF_ComponentsHelper.ValidateCustomTheme(CustomThemes);
    FilesUploader.CustomThemes = CustomThemes;
  }

  protected string _theme = FilesUploader.StandardThemes.regular.ToString();
  
  [Microsoft.AspNetCore.Components.Parameter]
  public object theme
  {
    get => this._theme;
    set => YDF_ComponentsHelper.AssignThemeIfItIsValid<FilesUploader.StandardThemes>(
      value, FilesUploader.CustomThemes, ref this._theme
    );
  }

  protected internal static bool mustConsiderThemesCSS_ClassesAsCommon = YDF_ComponentsHelper.areThemesCSS_ClassesCommon;

  public static void considerThemesAsCommon()
  {
    FilesUploader.mustConsiderThemesCSS_ClassesAsCommon = true;
  }
  
  [Microsoft.AspNetCore.Components.Parameter]
  public bool areThemesCSS_ClassesCommon { get; set; } = 
      YDF_ComponentsHelper.areThemesCSS_ClassesCommon || FilesUploader.mustConsiderThemesCSS_ClassesAsCommon;
  
  
  /* ─── Geometry ─────────────────────────────────────────────────────────────────────────────────────────────────── */
  public enum StandardGeometricVariations { regular }

  protected internal static Type? CustomGeometricVariations;
  
  public static void defineCustomGeometricVariations(Type CustomGeometricVariations)
  {
    YDF_ComponentsHelper.ValidateCustomGeometricVariation(CustomGeometricVariations);
    FilesUploader.CustomGeometricVariations = CustomGeometricVariations;
  }

  protected string _geometry = FilesUploader.StandardGeometricVariations.regular.ToString();

  [Microsoft.AspNetCore.Components.Parameter]
  public object geometry
  {
    get => this._geometry;
    set => YDF_ComponentsHelper.AssignGeometricVariationIfItIsValid<FilesUploader.StandardGeometricVariations>(
    value, FilesUploader.CustomGeometricVariations, ref this._geometry
    );
  }
  
  /* ─── Decorative variations ────────────────────────────────────────────────────────────────────────────────────── */
  public enum StandardDecorativeVariations { regular }

  protected internal static Type? CustomDecorativeVariations;

  public static void defineCustomDecorativeVariations(Type CustomDecorativeVariations) {
    YDF_ComponentsHelper.ValidateCustomDecorativeVariation(CustomDecorativeVariations);
    FilesUploader.CustomDecorativeVariations = CustomDecorativeVariations;
  }

  protected string _decoration = FilesUploader.StandardDecorativeVariations.regular.ToString();

  [Microsoft.AspNetCore.Components.Parameter]
  public required object decoration
  {
    get => _decoration;
    set => YDF_ComponentsHelper.AssignDecorativeVariationIfItIsValid<FilesUploader.StandardDecorativeVariations>(
      value, FilesUploader.CustomDecorativeVariations, ref this._decoration
    );
  }
  
  
  /* ━━━ CSS classes ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  [Microsoft.AspNetCore.Components.Parameter] 
  public string? spaceSeparatedAdditionalCSS_Classes { get; set; }
  
  private string rootElementModifierCSS_Classes => new List<string>().
      
      AddElementToEndIf("TextBox--YDF__DisabledState", this.disabled).
    
      AddElementToEndIf(
        $"TextBox--YDF__{ this._theme.ToUpperCamelCase() }Theme",
        YDF_ComponentsHelper.MustApplyThemeCSS_Class(
          typeof(FilesUploader.StandardThemes), FilesUploader.CustomThemes, this.areThemesCSS_ClassesCommon
        )
      ).
      
      AddElementToEndIf(
        $"Badge--YDF__{ this._geometry.ToUpperCamelCase() }Geometry",
        YDF_ComponentsHelper.MustApplyGeometricVariationModifierCSS_Class(
          typeof(FilesUploader.StandardGeometricVariations), FilesUploader.CustomGeometricVariations
        )
      ).
      
      AddElementToEndIf(
        $"Badge--YDF__{ this._decoration.ToUpperCamelCase() }Decoration",
        YDF_ComponentsHelper.MustApplyDecorativeVariationModifierCSS_Class(
          typeof(FilesUploader.StandardDecorativeVariations), FilesUploader.CustomDecorativeVariations
        )
      ).
      
      StringifyEachElementAndJoin(" ");

  
  /* ━━━  Constructor ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public FilesUploader()
  {
    this.INPUT_ELEMENT_HTML_ID = base.coreElementHTML_ID ?? FilesUploader.generateInputElementHTML_ID();
  }
  
  
  /* ━━━ Other ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  protected bool mustHighlightInvalidInputIfAnyValidationErrorsMessages = true;
  
  [Microsoft.AspNetCore.Components.Inject] 
  protected IJSRuntime JSRuntime { get; set; } = null!;
  
}