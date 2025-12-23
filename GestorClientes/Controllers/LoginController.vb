Imports System.Web.Mvc

Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Security.Cryptography

Namespace Controllers
    Public Class LoginController
        Inherits Controller

        ' GET: Login
        Function Index() As ActionResult
            If Session("UserId") IsNot Nothing Then
                Return RedirectToAction("Index", "Cliente")
            End If

            Return View(New LoginViewModel())
        End Function

        ' POST: Login
        <HttpPost()>
        <ValidateAntiForgeryToken>
        Function Index(model As LoginViewModel) As ActionResult
            If Not ModelState.IsValid Then Return View(model)

            Using db As New clientesDbEntities()
                Dim user = db.Usuario.FirstOrDefault(Function(u) u.NombreUsuario = model.NombreUsuario)
                If user Is Nothing OrElse Not VerifyPassword(model.Contrasena, user.Contrasena) Then
                    ModelState.AddModelError("", "Usuario o contraseña incorrectos.")
                    Return View(model)
                End If

                If Not IsHashed(user.Contrasena) Then
                    user.Contrasena = HashPassword(model.Contrasena)
                    db.SaveChanges()
                End If

                Session("UserId") = user.IdUsuario
                Session("UserName") = user.NombreUsuario
            End Using

            Return RedirectToAction("Index", "Cliente")
        End Function


        ' GET: Login/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Login/Create
        <HttpPost()>
        <ValidateAntiForgeryToken>
        Function Create(u As Usuario) As ActionResult
            Try
                ValidateUsuario(u)
                If Not ModelState.IsValid Then Return View(u)

                Using db As New clientesDbEntities()
                    Dim existing = db.Usuario.Any(Function(x) x.NombreUsuario = u.NombreUsuario)
                    If existing Then
                        ModelState.AddModelError("NombreUsuario", "El usuario ya existe.")
                        Return View(u)
                    End If

                    u.Contrasena = HashPassword(u.Contrasena)
                    db.Usuario.Add(u)
                    db.SaveChanges()
                End Using
                Return RedirectToAction("Index")
            Catch
                Return RedirectToAction("Index")
            End Try
        End Function

        ' GET: Login/Logout
        Function Logout() As ActionResult
            Session.Clear()
            Return RedirectToAction("Index")
        End Function

        Private Sub ValidateUsuario(u As Usuario)
            If u Is Nothing Then
                ModelState.AddModelError("", "Datos inválidos.")
                Return
            End If

            If String.IsNullOrWhiteSpace(u.NombreUsuario) Then
                ModelState.AddModelError("NombreUsuario", "El usuario es obligatorio.")
            ElseIf u.NombreUsuario.Length > 255 Then
                ModelState.AddModelError("NombreUsuario", "El usuario no puede exceder 255 caracteres.")
            End If

            If String.IsNullOrWhiteSpace(u.Contrasena) Then
                ModelState.AddModelError("Contrasena", "La contraseña es obligatoria.")
            ElseIf u.Contrasena.Length < 6 Then
                ModelState.AddModelError("Contrasena", "La contraseña debe tener al menos 6 caracteres.")
            ElseIf u.Contrasena.Length > 255 Then
                ModelState.AddModelError("Contrasena", "La contraseña no puede exceder 255 caracteres.")
            End If
        End Sub

        Private Shared Function IsHashed(stored As String) As Boolean
            Return Not String.IsNullOrWhiteSpace(stored) AndAlso stored.StartsWith("pbkdf2:", StringComparison.OrdinalIgnoreCase)
        End Function

        Private Shared Function HashPassword(plain As String) As String
            Dim salt(15) As Byte
            Using rng As New RNGCryptoServiceProvider()
                rng.GetBytes(salt)
            End Using

            Const iterations As Integer = 100000
            Using pbkdf2 As New Rfc2898DeriveBytes(plain, salt, iterations, HashAlgorithmName.SHA256)
                Dim hash = pbkdf2.GetBytes(32)
                Return String.Format("pbkdf2:{0}:{1}:{2}", iterations, Convert.ToBase64String(salt), Convert.ToBase64String(hash))
            End Using
        End Function

        Private Shared Function VerifyPassword(plain As String, stored As String) As Boolean
            If String.IsNullOrWhiteSpace(stored) Then Return False

            If stored.StartsWith("pbkdf2:", StringComparison.OrdinalIgnoreCase) Then
                Dim parts = stored.Split(":"c)
                If parts.Length <> 4 Then Return False

                Dim iterations As Integer
                If Not Integer.TryParse(parts(1), iterations) Then Return False

                Dim salt As Byte()
                Dim hash As Byte()
                Try
                    salt = Convert.FromBase64String(parts(2))
                    hash = Convert.FromBase64String(parts(3))
                Catch
                    Return False
                End Try

                Using pbkdf2 As New Rfc2898DeriveBytes(plain, salt, iterations, HashAlgorithmName.SHA256)
                    Dim computed = pbkdf2.GetBytes(hash.Length)
                    Return AreEqualSlow(hash, computed)
                End Using
            End If

            Return stored = plain
        End Function

        Private Shared Function AreEqualSlow(a As Byte(), b As Byte()) As Boolean
            If a Is Nothing OrElse b Is Nothing OrElse a.Length <> b.Length Then Return False

            Dim diff As Integer = 0
            For i = 0 To a.Length - 1
                diff = diff Or (a(i) Xor b(i))
            Next

            Return diff = 0
        End Function

    End Class
End Namespace

Namespace Controllers
    Public Class LoginViewModel
        <Required(ErrorMessage:="El usuario es obligatorio.")>
        Public Property NombreUsuario As String

        <Required(ErrorMessage:="La contraseña es obligatoria.")>
        <DataType(DataType.Password)>
        Public Property Contrasena As String
    End Class
End Namespace
