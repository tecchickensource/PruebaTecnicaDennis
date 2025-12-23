# GestorClientes

Aplicacion ASP.NET MVC (VB.NET) para gestionar clientes con login y bitacora de acciones.

## Requisitos
- Visual Studio 2019/2022
- .NET Framework 4.7.2
- EntityFrameWork 6.0
- SQL Server + SSMS

## Instalacion
1) Ejecuta el script de base de datos:
   - Abre `script.sql` en SSMS y ejecutalo para crear la base y tablas.

2) Configura la cadena de conexion:
   - Edita `Web.config` y actualiza la cadena `clientesDbEntities` con tu servidor, usuario y clave.

3) Restaura paquetes NuGet:
   - En Visual Studio: clic derecho en la solucion/proyecto -> Restore NuGet Packages.

4) Compila y ejecuta:
   - Abre `GestorClientes.vbproj` en Visual Studio.
   - Ejecuta con IIS Express.

## Uso basico
- En la pantalla de login, crea un usuario con "Crear usuario".
- Inicia sesion con ese usuario.
- En la pantalla de clientes puedes crear, editar y eliminar.
- Cada accion se registra en la tabla Bitacora.

## Notas
- Las contrasenas se almacenan con hashing PBKDF2.
- Si tienes usuarios antiguos en texto plano, se migran a hash al iniciar sesion.
