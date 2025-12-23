@ModelType GestorClientes.Usuario

@Code
    ViewBag.Title = "Crear usuario"
End Code

<div class="row justify-content-center">
    <div class="col-12 col-md-7 col-lg-5">
        <div class="card app-card mt-4">
            <div class="card-body">
                <div class="mb-4">
                    <p class="text-uppercase small text-muted mb-2">Prueba tecnica</p>
                    <h2 class="page-title mb-1">@ViewBag.Title</h2>
                    <p class="page-subtitle">Registra un usuario para acceder.</p>
                </div>

                @Using (Html.BeginForm())
                    @Html.AntiForgeryToken()
                    @Html.ValidationSummary(True, "", New With {.class = "text-danger small"})

                    @<div>
                        <div class="mb-3">
                            @Html.LabelFor(Function(m) m.NombreUsuario, New With {.class = "form-label"})
                            @Html.TextBoxFor(Function(m) m.NombreUsuario, New With {.class = "form-control", .autocomplete = "username"})
                            @Html.ValidationMessageFor(Function(m) m.NombreUsuario, "", New With {.class = "text-danger small"})
                        </div>

                        <div class="mb-3">
                            @Html.LabelFor(Function(m) m.Contrasena, New With {.class = "form-label"})
                            @Html.PasswordFor(Function(m) m.Contrasena, New With {.class = "form-control", .autocomplete = "new-password"})
                            <div class="helper-text">Minimo 6 caracteres.</div>
                            @Html.ValidationMessageFor(Function(m) m.Contrasena, "", New With {.class = "text-danger small"})
                        </div>

                        <div class="d-flex gap-2">
                            <button type="submit" class="btn btn-primary">Guardar</button>
                            @Html.ActionLink("Cancelar", "Index", Nothing, New With {.class = "btn btn-outline-secondary"})
                        </div>
                    </div>
                End Using

                <div class="mt-3 small text-center">
                    ¿Ya tienes cuenta?
                    @Html.ActionLink("Iniciar sesión", "Index", Nothing, New With {.class = "link-primary"})
                </div>
            </div>
        </div>
    </div>
</div>
