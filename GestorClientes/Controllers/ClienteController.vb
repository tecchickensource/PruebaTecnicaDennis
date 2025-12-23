Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Web.Mvc

Namespace Controllers
    Public Class ClienteController
        Inherits Controller

        Protected Overrides Sub OnActionExecuting(filterContext As ActionExecutingContext)
            If Session("UserId") Is Nothing Then
                filterContext.Result = RedirectToAction("Index", "Login")
                Return
            End If

            MyBase.OnActionExecuting(filterContext)
        End Sub

        Private Sub RegistrarBitacora(db As clientesDbEntities, accion As String, clienteId As Integer)
            Dim userId As Integer = CInt(Session("UserId"))
            Dim nombreUsuario As String = TryCast(Session("UserName"), String)
            If nombreUsuario Is Nothing Then nombreUsuario = String.Empty

            Dim b As New Bitacora() With {
                .IdUsuario = userId,
                .Accion = accion,
                .FechaModificacion = DateTime.Now,
                .IdCliente = clienteId,
                .NombreUsuario = nombreUsuario
            }

            db.Bitacora.Add(b)
        End Sub

        ' GET: Cliente
        Function Index() As ActionResult
            Using db As New clientesDbEntities()
                Dim lista = db.Cliente.ToList()
                Return View(lista)
            End Using
        End Function

        ' GET: Cliente/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Cliente/Create
        <HttpPost()>
        <ValidateAntiForgeryToken>
        Function Create(c As Cliente) As ActionResult
            Try
                ValidateCliente(c)
                If Not ModelState.IsValid Then Return View(c)

                Using db As New clientesDbEntities()
                    Using tx = db.Database.BeginTransaction()
                        db.Cliente.Add(c)
                        db.SaveChanges()
                        RegistrarBitacora(db, "Agregar", c.IdCliente)
                        db.SaveChanges()
                        tx.Commit()
                    End Using
                End Using
                Return RedirectToAction("Index")
            Catch
                Return RedirectToAction("Index")
            End Try
        End Function




        ' GET: Cliente/Edit/{id}
        Function Edit(ByVal id As Integer?) As ActionResult
            If Not id.HasValue Then Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            Using db As New clientesDbEntities()
                Dim c = db.Cliente.Find(id.Value)
                If c Is Nothing Then Return HttpNotFound()
                Return View(c)
            End Using
        End Function

        ' POST: Cliente/Edit
        <HttpPost()>
        <ValidateAntiForgeryToken>
        Function Edit(c As Cliente) As ActionResult
            Try
                ValidateCliente(c)
                If Not ModelState.IsValid Then Return View(c)

                Using db As New clientesDbEntities()
                    db.Entry(c).State = Entity.EntityState.Modified
                    RegistrarBitacora(db, "Editar", c.IdCliente)
                    db.SaveChanges()
                End Using

                Return RedirectToAction("Index")
            Catch
                Return RedirectToAction("Index")
            End Try
        End Function





        ' GET: Cliente/Delete/{id}
        Function Delete(ByVal id As Integer?) As ActionResult
            If Not id.HasValue Then Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            Using db As New clientesDbEntities()
                Dim c = db.Cliente.Find(id.Value)
                If c Is Nothing Then Return HttpNotFound()
                Return View(c)
            End Using
        End Function

        ' POST: Cliente/Delete
        <HttpPost()>
        <ValidateAntiForgeryToken>
        Function Delete(ByVal id As Integer) As ActionResult
            Try
                Using db As New clientesDbEntities()
                    Dim c = db.Cliente.Find(id)
                    If c Is Nothing Then Return HttpNotFound()

                    RegistrarBitacora(db, "Eliminar", c.IdCliente)
                    db.Cliente.Remove(c)
                    db.SaveChanges()
                End Using

                Return RedirectToAction("Index")
            Catch
                Return RedirectToAction("Index")
            End Try
        End Function

        Private Sub ValidateCliente(c As Cliente)
            If c Is Nothing Then
                ModelState.AddModelError("", "Datos inválidos.")
                Return
            End If

            If String.IsNullOrWhiteSpace(c.NombreCliente) Then
                ModelState.AddModelError("NombreCliente", "El nombre es obligatorio.")
            ElseIf c.NombreCliente.Length > 255 Then
                ModelState.AddModelError("NombreCliente", "El nombre no puede exceder 255 caracteres.")
            End If

            If String.IsNullOrWhiteSpace(c.ApellidoCliente) Then
                ModelState.AddModelError("ApellidoCliente", "El apellido es obligatorio.")
            ElseIf c.ApellidoCliente.Length > 255 Then
                ModelState.AddModelError("ApellidoCliente", "El apellido no puede exceder 255 caracteres.")
            End If

            If c.FechaNacimiento = DateTime.MinValue Then
                ModelState.AddModelError("FechaNacimiento", "La fecha de nacimiento es obligatoria.")
            ElseIf c.FechaNacimiento > DateTime.Today Then
                ModelState.AddModelError("FechaNacimiento", "La fecha de nacimiento no puede ser futura.")
            End If

            If String.IsNullOrWhiteSpace(c.Dui) Then
                ModelState.AddModelError("Dui", "El DUI es obligatorio.")
            ElseIf c.Dui.Length > 10 Then
                ModelState.AddModelError("Dui", "El DUI no puede exceder 10 caracteres.")
            ElseIf Not Regex.IsMatch(c.Dui, "^\d{8}-\d$|^\d{9}$") Then
                ModelState.AddModelError("Dui", "El DUI debe tener formato 00000000-0 o 000000000.")
            End If
        End Sub
    End Class
End Namespace
