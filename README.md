# GestorClientes

Aplicacion ASP.NET MVC (VB.NET) para gestionar clientes con login y bitacora de acciones.

## Requisitos
- Visual Studio 2019/2022
- .NET Framework 4.7.2
- EntityFrameWork 6.0
- SQL Server + SSMS

## Instalacion
1) Ejecutar el script de base de datos:
   - Abrir el script en `GestorClientes/script.sql` en SSMS, luego darle a "Ejecutar" para crear la base y tablas.

<img width="327" height="104" alt="image" src="https://github.com/user-attachments/assets/39bd2632-6f66-4bdd-8e13-19343c3104f5" />


<img width="719" height="630" alt="image" src="https://github.com/user-attachments/assets/e175d006-e2c1-4ae7-b865-50639a1a1adb" />


<img width="1298" height="201" alt="image" src="https://github.com/user-attachments/assets/c43ca2d2-d932-4195-82dc-cf6826bda7e2" />


2) Configurar la cadena de conexion:
   - Editar en `GestorClientes/Web.config` y actualiza la cadena de `clientesDbEntities` con el servidor, usuario y clave.

<img width="351" height="581" alt="image" src="https://github.com/user-attachments/assets/50d92746-5158-47a5-8578-347e2e2b2fbc" />

<img width="730" height="68" alt="image" src="https://github.com/user-attachments/assets/95d671c6-587f-4658-8e17-68cd08065feb" />

3) Restaura paquetes NuGet:
   - En Visual Studio: clic derecho en la solucion/proyecto -> Restore NuGet Packages.

<img width="475" height="552" alt="image" src="https://github.com/user-attachments/assets/6c3047e8-a92b-42bd-a525-8bce68199cab" />

4) Compila y ejecuta:
   - Abre el proyecto `GestorClientes.vbproj` en Visual Studio.
   - Ejecutar con IIS Express.

## Uso basico
- En la pantalla de login, se muestra un formulario basico para iniciar session con un usuario o tambien puedes crear uno propio.

<img width="1302" height="544" alt="Captura de pantalla 2025-12-22 225037" src="https://github.com/user-attachments/assets/52e4eb5e-d907-4f27-bb75-d04d993f070e" />
  
- Luego de haber iniciado session aparecera la pantalla de clientes donde puedes crear, editar y eliminar.

<img width="1202" height="360" alt="Captura de pantalla 2025-12-22 225120" src="https://github.com/user-attachments/assets/40195ff7-45b1-4963-978a-e1e7bc38e5d9" />
  
- Cada accion que un usuario realizar se registra en la tabla Bitacora.

<img width="544" height="589" alt="Captura de pantalla 2025-12-22 225344" src="https://github.com/user-attachments/assets/7f7e3524-d10f-4164-8555-cf739b3a123e" />


## Notas
- Las contrasenas se almacenan con hashing PBKDF2.

<img width="569" height="463" alt="Captura de pantalla 2025-12-22 225412" src="https://github.com/user-attachments/assets/7b638c0a-59a7-4985-bd2f-0a3c1d4958cd" />

