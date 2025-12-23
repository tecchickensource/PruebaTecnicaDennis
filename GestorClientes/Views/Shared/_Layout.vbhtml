<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>@ViewBag.Title</title>
    @Styles.Render("~/Content/css")
</head>
<body>
    <div class="app-shell">
        <header class="app-header">
            <div class="container app-header-inner">
                <div class="app-brand">GestorClientes</div>
                @If Session("UserId") IsNot Nothing Then
                    @<div class="app-user">
                        <span class="app-user-name">Hola, @Session("UserName")</span>
                        @Html.ActionLink("Cerrar sesión", "Logout", "Login", Nothing, New With {.class = "btn btn-outline-secondary btn-sm"})
                    </div>
                Else
                    @<div class="app-user">
                        <span class="app-user-name text-muted">Acceso</span>
                    </div>
                End If
            </div>
        </header>
        <main class="app-main">
            <div class="container py-4">
                @RenderBody()
            </div>
        </main>
    </div>

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("scripts", required:=False)
</body>
</html>
