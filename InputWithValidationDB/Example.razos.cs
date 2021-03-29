<EditForm Model="model" OnSubmit="Validate">
<DataAnnotationsValidator></DataAnnotationsValidator>
    <div class="row">
        <div class="col-lg-6 col-sm-12">            
                    <InputWithValidation @bind-Value="model.userName" Label="Documento de Indentidad"
                    nameField="userName" For="@(()=> model.userName)" urlAPI="@urlUser" Status="isValid"  />
        </div>

        <div class="col-12">
                    <button type="submit" class="btn btn-success col-12" disabled="@(!status)"> Crear</button>
        </div>
    </div>

</EditForm>

@code {    
    [Parameter] public User model { get; set; }
    [Parameter] public EventCallback Send { get; set; }
    private string urlUser = "api/Users/existUser";
    private bool status  = false;

     private void isValid(bool _status)
    {
        status = _status;
    }

    private async Task Validate()
    {
        if(status)
        {
            await Send.InvokeAsync(model);
        }

    }
}