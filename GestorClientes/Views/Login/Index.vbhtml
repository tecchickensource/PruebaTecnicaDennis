@ModelType GestorClientes.Controllers.LoginViewModel

@Code
    ViewBag.Title = "Iniciar sesión"
End Code

<div class="row justify-content-center">
    <div class="col-12 col-md-6 col-lg-4">
        <div class="card app-card mt-4">
            <div class="card-body">
                <div class="mb-4">
                    <p class="text-uppercase small text-muted mb-2">Prueba tecnica</p>
                    <h2 class="page-title mb-1">@ViewBag.Title</h2>
                    <p class="page-subtitle">Accede para gestionar clientes.</p>
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
                            @Html.PasswordFor(Function(m) m.Contrasena, New With {.class = "form-control", .autocomplete = "current-password"})
                            @Html.ValidationMessageFor(Function(m) m.Contrasena, "", New With {.class = "text-danger small"})
                        </div>

                        <button type="submit" class="btn btn-primary w-100">Entrar</button>
                    </div>
                End Using

                <div class="mt-3 text-center small">
                    ¿No tienes cuenta?
                    @Html.ActionLink("Crear usuario", "Create", Nothing, New With {.class = "link-primary"})
                </div>
            </div>
        </div>
    </div>
</div>
