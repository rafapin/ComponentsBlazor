@inherits InputText
@using System.Linq.Expressions
@inject IRepository repository
@typeparam TValue

<div class="form-group">
    <label>@Label</label>
    <input type="text" @ref="input" @bind-value="CurrentValue" @onkeyup="Validate" class="form-control" 
           @oninput="@((e) => { ValueField=(string)e.Value;})" @onfocusout="Refocus" />
    <ValidationMessage For="For" />
    @if(!isValid){<span style="color: red">@messageError</span>}
</div>

@code {
    [Parameter] public Expression<Func<TValue>> For { get; set; }
    [Parameter] public string urlAPI { get; set; }
    [Parameter]public string Label { get; set; }
    [Parameter] public string nameField { get; set; }
    [Parameter]public EventCallback<bool> Status { get; set; }

    private string ValueField = "";
    private ElementReference input;
    public string messageError;
    private bool isValid = true;

    protected override void OnInitialized()
    {
        messageError = $"El {Label} ya se encuentra registrado.";
    }


    private async Task Validate()
    {
        //You can user direct HttpClient, I user my repository with my methods
        var exist = await repository.valueExist(urlAPI,ValueField);

        isValid = !exist;
        await Status.InvokeAsync(isValid);
    }

    private async Task Refocus()
    {
        if(!isValid) await input.FocusAsync();
    }

}
