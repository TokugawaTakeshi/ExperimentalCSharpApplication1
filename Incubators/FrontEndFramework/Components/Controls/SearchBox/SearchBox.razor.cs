using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using Button = FrontEndFramework.Components.Controls.Buttons.Plain.Button;

using YamatoDaiwa.CSharpExtensions;
using Utils;


namespace FrontEndFramework.Components.Controls.SearchBox;


public partial class SearchBox : ComponentBase
{

  [Parameter] public required EventCallback<string?> onNewSearchRequest { get; set; }
  
  [Parameter] public byte minimalCharactersCountForSearchRequest { get; set; } = 1;
  
  [Parameter] public string? placeholder { get; set; }
  [Parameter] public string? accessibilityGuidance { get; set; }
  
  [Parameter] public bool disabled { get; set; } = false;
  
  [Parameter] public bool ableToSubmitRequestByEnterKey { get; set; } = false;
  [Parameter] public bool emptySearchRequestAsResetting { get; set; } = false;

  protected string? _mustSynchronizeDisplayingSearchRequestWith = null;
  
  [Parameter]
  public string? mustSynchronizeDisplayingSearchRequestWith
  {
    get => _mustSynchronizeDisplayingSearchRequestWith;
    set
    {
      this._mustSynchronizeDisplayingSearchRequestWith = value;
      this.inputtedSearchRequest = this._mustSynchronizeDisplayingSearchRequestWith ?? "";
    }
  }
  
  
  protected Button searchingRequestEmittingButton = null!;
  
  protected string inputtedSearchRequest = "";
  protected bool isTooltipDisplaying = false;
  
  
  protected static readonly uint INVALID_REQUEST_TOOLTIP_DISPLAYING_DURATION__SECONDS = 3;

  
  protected async System.Threading.Tasks.Task submitSearchRequest() {

    if (this.inputtedSearchRequest.Length == 0 && this.emptySearchRequestAsResetting)
    {
      await this.onNewSearchRequest.InvokeAsync(null);
      return;
    }


    if (this.inputtedSearchRequest.Length < this.minimalCharactersCountForSearchRequest) {
      
      this.isTooltipDisplaying = true;

      await System.Threading.Tasks.Task.Delay(
        TimeSpan.FromSeconds(SearchBox.INVALID_REQUEST_TOOLTIP_DISPLAYING_DURATION__SECONDS)
      );

      this.isTooltipDisplaying = false;
      
      return;
      
    }

    
    await this.onNewSearchRequest.InvokeAsync(this.inputtedSearchRequest);

  }

  protected void onInputToInputElement(ChangeEventArgs eventArguments)
  {
    this.inputtedSearchRequest = eventArguments.Value?.ToString() ?? "";
  }
  
  protected async System.Threading.Tasks.Task onPressKeyDuringInputElementFocusedIn(KeyboardEventArgs eventArguments) {

    if (eventArguments.Key != "Enter")
    {
      return;
    }
    

    if (this.ableToSubmitRequestByEnterKey) {
      await this.submitSearchRequest();
      return;
    }

    
    await this.searchingRequestEmittingButton.focus();
    
  }

  protected async System.Threading.Tasks.Task resetSearchRequest()
  {

    this.inputtedSearchRequest = "";
    await this.onNewSearchRequest.InvokeAsync(null);
  }
  
  
  /* ─── CSS classes ──────────────────────────────────────────────────────────────────────────────────────────────── */
  [Parameter] public string? spaceSeparatedAdditionalCSS_Classes { get; set; }
  
  private string rootElementModifierSpaceSeparatedClasses => new List<string>().
      AddElementToEndIf("SearchBox--YDF__DisabledState", _ => this.disabled).
      StringifyEachElementAndJoin(" ");
  
}