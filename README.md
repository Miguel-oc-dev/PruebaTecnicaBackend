# Proyecto de Administración de Eventos

Este proyecto es una aplicación web básica que permite a los usuarios iniciar sesión y administrar eventos. La aplicación incluye un sistema simple de autenticación y funcionalidad CRUD para gestionar eventos.

## Tecnologías utilizadas
- ASP.NET MVC 5
- Entity Framework
- HTML, CSS, Bootstrap
- C#

---

## Configuración del proyecto

### Requisitos previos
1. Tener instalado Visual Studio.
2. .NET Framework (versión 4.5 o superior).
3. SQL Server o una base de datos compatible (opcional si se simula una base de datos).

### Estructura principal del proyecto
- **Controladores:** Manejan la lógica del negocio.
  - `AccountController`: Maneja la autenticación de usuarios.
  - `EventoController`: Proporciona las funcionalidades CRUD para eventos.
- **Modelos:** Definen las entidades.
  - `Usuario`: Representa un usuario del sistema.
  - `Evento`: Representa un evento en el sistema.
- **Vistas:** Contienen el HTML y lógica de presentación.
  - `Views/Account/Login.cshtml`: Vista de inicio de sesión.
  - `Views/Evento/Index.cshtml`: Lista de eventos.
  - `Views/Evento/Create.cshtml`: Formulario para crear eventos.
  - `Views/Evento/Edit.cshtml`: Formulario para editar eventos.

---

## Instrucciones para ejecutar el proyecto

1. **Clonar el repositorio:**
   ```bash
   git clone <URL_DEL_REPOSITORIO>
   ```

2. **Abrir en Visual Studio:**
   - Abre el archivo `.sln` en Visual Studio.

3. **Restaurar paquetes NuGet:**
   - En Visual Studio, ve a `Tools` > `NuGet Package Manager` > `Manage NuGet Packages for Solution` y restaura los paquetes necesarios.

4. **Configurar la base de datos (opcional):**
   - Si deseas conectar una base de datos real, actualiza el archivo `Web.config` con la cadena de conexión correspondiente.

5. **Ejecutar la aplicación:**
   - Presiona `F5` o haz clic en el botón de "Iniciar depuración".

---

## Explicación del código principal

### Inicio de sesión
- **Vista:** `Login.cshtml`
  - Contiene un formulario estilizado para que los usuarios ingresen su correo y contraseña.
  - Usa `Html.BeginForm` para enviar los datos al servidor.

```html
@using (Html.BeginForm()) {
    @Html.AntiForgeryToken()
    <div class="form-group">
        @Html.LabelFor(model => model.Correo, new { @class = "control-label" })
        @Html.TextBoxFor(model => model.Correo, new { @class = "form-control" })
    </div>
    <div class="form-group">
        @Html.LabelFor(model => model.Contraseña, new { @class = "control-label" })
        @Html.PasswordFor(model => model.Contraseña, new { @class = "form-control" })
    </div>
    <button type="submit" class="btn btn-primary">Iniciar sesión</button>
}
```

- **Controlador:** `AccountController`
  - Maneja la lógica para validar el correo y la contraseña.
  - Si la autenticación es exitosa, redirige al usuario al índice de eventos.

```csharp
public ActionResult Login(Usuario usuario) {
    if (ModelState.IsValid) {
        var user = usuarios.FirstOrDefault(u => u.Correo == usuario.Correo && u.Contraseña == usuario.Contraseña);
        if (user != null) {
            Session["Usuario"] = user.Correo;
            return RedirectToAction("Index", "Evento");
        }
        ModelState.AddModelError("", "Correo o contraseña incorrectos.");
    }
    return View(usuario);
}
```

### CRUD de eventos
- **Crear evento:**
  - Vista: `Create.cshtml`
  - Permite ingresar detalles como título, descripción, lugar y fecha.

- **Editar evento:**
  - Vista: `Edit.cshtml`
  - Usa un formulario similar a "Crear" pero con los datos precargados.

- **Listar eventos:**
  - Vista: `Index.cshtml`
  - Muestra una tabla con los eventos registrados.

```csharp
public ActionResult Index() {
    var eventos = db.Eventos.ToList();
    return View(eventos);
}
```

### Formato de fecha
La propiedad `Fecha` en el modelo se configura con un formato de visualización:

```csharp
[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
public DateTime Fecha { get; set; }
```

Esto asegura que las fechas se muestren y editen en el formato `YYYY/MM/DD`.

---

## Puntos destacados
1. **Diseño responsivo:** Se utiliza Bootstrap para garantizar que la aplicación se vea bien en dispositivos móviles y de escritorio.
2. **Autenticación sencilla:** Un sistema básico basado en correo y contraseña.
3. **Gestor de eventos:** CRUD funcional para manejar eventos.
