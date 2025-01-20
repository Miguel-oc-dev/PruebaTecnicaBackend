# Proyecto de Administración de Eventos

Este proyecto es una aplicación web básica que permite a los usuarios iniciar sesión y administrar eventos. La aplicación incluye un sistema simple de autenticación y funcionalidad CRUD para gestionar eventos.

## Tecnologías utilizadas
- ASP.NET MVC 5
- Entity Framework
- HTML, CSS, Bootstrap
- C#
- SQL Server

---

## Configuración del proyecto

### Requisitos previos
1.  Visual Studio.
2. .NET Framework versión 4.8.
3. SQL Server

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

# Claves para ingresar
    admin@ejemplo.com
    12345
 
  ![image](https://github.com/user-attachments/assets/f9955565-e6eb-4aa9-bb7e-56bea864d51b)


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

- **Listar eventos:**
  - Vista: `Index.cshtml`
  - Muestra una tabla con los eventos registrados.
![image](https://github.com/user-attachments/assets/e30dbb15-ba62-48e2-bac0-5e1e6a30603d)

- **Crear evento:**
  - Vista: `Create.cshtml`
  - Permite ingresar detalles como título, descripción, lugar y fecha.
 ![image](https://github.com/user-attachments/assets/663078e0-d24e-48b6-af3f-55d0649b9189)


- **Editar evento:**
  - Vista: `Edit.cshtml`
  - Usa un formulario similar a "Crear" pero con los datos precargados.
  ![image](https://github.com/user-attachments/assets/ebfb8bd2-3901-454c-9f26-43a93cfc7d0c)

- **Detalles eventos:**
  - Vista: `Details.cshtml`
  - Muestra una tabla con el detalle del evento seleccionado.
  ![image](https://github.com/user-attachments/assets/dd96027e-fb4d-4a46-a7c1-4a02f2cfe08f)

- **Eliminar evento:**
  - Vista: `Delete.cshtml`
  - Muestra información del evento a eliminar.
  ![image](https://github.com/user-attachments/assets/0b1bd44f-d246-41cf-9c87-e19721084474)



### Controladores

```csharp
namespace AdministracionEventos.Controllers
{
    public class EventoController : Controller
    {
        // GET: Evento
        public ActionResult Index()
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Evento.ToList());
            }
        }

        // GET: Evento/Details/5
        public ActionResult Details(int id)
        {
            using (DbModels context = new DbModels())
            {
                Evento evento = context.Evento.FirstOrDefault(x => x.ID == id);

                if (evento == null)
                {
                    return HttpNotFound();  
                }

                return View(evento);  
            }
        }

        // GET: Evento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Evento/Create
        [HttpPost]
        public ActionResult Create(Evento evento)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Evento.Add(evento);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Evento/Edit/5
        public ActionResult Edit(int id)
        {
            using (DbModels context = new DbModels())
            {
                var evento = context.Evento.FirstOrDefault(x => x.ID == id);

                if (evento == null)
                {
                    return HttpNotFound(); 
                }

                var usuarios = context.Usuario.ToList(); 
                ViewBag.Usuarios = new SelectList(usuarios, "ID", "Nombre", evento.UsuarioID);

                return View(evento);
            }
        }



        // POST: Evento/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Evento evento)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Entry(evento).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Evento/Delete/5
        public ActionResult Delete(int id)
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Evento.Where(x => x.ID == id).FirstOrDefault());
            }
        }

        // POST: Evento/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DbModels context = new DbModels())
                { 
                    Evento evento = context.Evento.Where(x=>x.ID == id).FirstOrDefault();
                    context.Evento.Remove(evento);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

```

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
