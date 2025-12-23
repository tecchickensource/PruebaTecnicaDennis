@ModelType GestorClientes.Cliente

@Code
    ViewBag.Title = "Eliminar cliente"
End Code

<div class="row justify-content-center">
    <div class="col-12 col-md-8 col-lg-6">
        <div class="card app-card">
            <div class="card-body">
                <div class="mb-3">
                    <h2 class="page-title mb-1">@ViewBag.Title</h2>
                    <p class="page-subtitle">Esta accion no se puede deshacer.</p>
                </div>

                <div class="alert alert-warning">
                    Vas a eliminar a <strong>@Model.NombreCliente @Model.ApellidoCliente</strong>.
                </div>

                @Using (Html.BeginForm("Delete", "Cliente", New With {.id = Model.IdCliente}, FormMethod.Post))
                    @Html.AntiForgeryToken()
                    @<div class="d-flex gap-2">
                        <button type="submit" class="btn btn-primary">Si, eliminar</button>
                        @Html.ActionLink("Cancelar", "Index", Nothing, New With {.class = "btn btn-outline-secondary"})
                    </div>
                End Using
            </div>
        </div>
    </div>
</div>
