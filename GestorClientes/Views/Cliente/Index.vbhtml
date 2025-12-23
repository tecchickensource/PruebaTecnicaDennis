@ModelType List(Of GestorClientes.Cliente)

@Code
    ViewBag.Title = "Clientes"
End Code

<div class="page-header">
    <div>
        <h2 class="page-title">@ViewBag.Title</h2>
        <p class="page-subtitle">Gestiona altas, ediciones y eliminaciones.</p>
    </div>
    <div>
        @Html.ActionLink("Nuevo cliente", "Create", Nothing, New With {.class = "btn btn-primary"})
    </div>
</div>

<div class="card app-card">
    <div class="table-responsive">
        <table class="table table-hover align-middle">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Nombre</th>
                    <th>Apellido</th>
                    <th>Fecha nacimiento</th>
                    <th>DUI</th>
                    <th class="actions">Acciones</th>
                </tr>
            </thead>
            <tbody>
            @If Model Is Nothing OrElse Model.Count = 0 Then
                @<tr>
                    <td colspan="6" class="text-center text-muted py-4">Sin registros por ahora.</td>
                </tr>
            Else
                @For Each c In Model
                    @<tr>
                        <td>@c.IdCliente</td>
                        <td>@c.NombreCliente</td>
                        <td>@c.ApellidoCliente</td>
                        <td>@c.FechaNacimiento.ToString("dd/MM/yyyy")</td>
                        <td>@c.Dui</td>
                        <td class="actions">
                            @Html.ActionLink("Editar", "Edit", New With {.id = c.IdCliente}, New With {.class = "btn btn-sm btn-outline-primary"})
                            @Html.ActionLink("Eliminar", "Delete", New With {.id = c.IdCliente}, New With {.class = "btn btn-sm btn-outline-danger ms-2"})
                        </td>
                    </tr>
                Next
            End If
            </tbody>
        </table>
    </div>
</div>
