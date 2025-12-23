@ModelType GestorClientes.Cliente

@Code
    ViewBag.Title = "Editar cliente"
End Code

<div class="row justify-content-center">
    <div class="col-12 col-lg-8">
        <div class="card app-card">
            <div class="card-body">
                <div class="mb-4">
                    <h2 class="page-title mb-1">@ViewBag.Title</h2>
                    <p class="page-subtitle">Actualiza la informacion del cliente.</p>
                </div>

                @Using (Html.BeginForm())
                    @Html.AntiForgeryToken()
                    @Html.HiddenFor(Function(m) m.IdCliente)
                    @Html.ValidationSummary(True, "", New With {.class = "text-danger small"})

                    @<div>
                        <div class="mb-3">
                            @Html.LabelFor(Function(m) m.NombreCliente, New With {.class = "form-label"})
                            @Html.TextBoxFor(Function(m) m.NombreCliente, New With {.class = "form-control"})
                            @Html.ValidationMessageFor(Function(m) m.NombreCliente, "", New With {.class = "text-danger small"})
                        </div>

                        <div class="mb-3">
                            @Html.LabelFor(Function(m) m.ApellidoCliente, New With {.class = "form-label"})
                            @Html.TextBoxFor(Function(m) m.ApellidoCliente, New With {.class = "form-control"})
                            @Html.ValidationMessageFor(Function(m) m.ApellidoCliente, "", New With {.class = "text-danger small"})
                        </div>

                        <div class="mb-3">
                            @Html.LabelFor(Function(m) m.FechaNacimiento, New With {.class = "form-label"})
                            @Html.TextBoxFor(Function(m) m.FechaNacimiento, New With {.[type] = "date", .class = "form-control"})
                            @Html.ValidationMessageFor(Function(m) m.FechaNacimiento, "", New With {.class = "text-danger small"})
                        </div>

                        <div class="mb-3">
                            @Html.LabelFor(Function(m) m.Dui, New With {.class = "form-label"})
                            @Html.TextBoxFor(Function(m) m.Dui, New With {.class = "form-control", .placeholder = "00000000-0"})
                            <div class="helper-text">Formato esperado: 00000000-0</div>
                            @Html.ValidationMessageFor(Function(m) m.Dui, "", New With {.class = "text-danger small"})
                        </div>

                        <div class="d-flex gap-2">
                            <button type="submit" class="btn btn-primary">Guardar cambios</button>
                            @Html.ActionLink("Cancelar", "Index", Nothing, New With {.class = "btn btn-outline-secondary"})
                        </div>
                    </div>
                End Using
            </div>
        </div>
    </div>
</div>
