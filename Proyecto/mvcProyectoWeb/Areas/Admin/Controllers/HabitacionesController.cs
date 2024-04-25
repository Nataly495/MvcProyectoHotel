using Microsoft.AspNetCore.Mvc;
using mvcProyectoHotel.AccesoDatos.Data.Repository.IRepository;
using mvcProyectoHotel.Models;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

namespace mvcProyectoHotel.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Administrador")]
    [Area("Admin")]
    public class HabitacionesController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public HabitacionesController(IContenedorTrabajo contenedorTrabajo,
            IWebHostEnvironment hostingEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var habitaciones = _contenedorTrabajo.Habitacion.GetAll();
            return View(habitaciones);
        }

        [HttpGet]
        public IActionResult Create() { return View(); }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if (archivos.Count() > 0)
                {
                    //Nuevo habitacion
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\Habitaciones");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }
                    habitacion.UrlImagen = @"\imagenes\Habitaciones\" + nombreArchivo + extension;
                    _contenedorTrabajo.Habitacion.Add(habitacion);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Imagen", "Debes seleccionar una imagen");
                }

            }
            return View(habitacion);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                var habitacion = _contenedorTrabajo.Habitacion.Get(id.GetValueOrDefault());
                return View(habitacion);
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var habitacionDdesdeBd = _contenedorTrabajo.Habitacion.Get(habitacion.Id);

                if (archivos.Count() > 0)
                {
                    //Nuevo imagen habitacion
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\Habitaciones");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    //var nuevaExtension = Path.GetExtension(archivos[0].FileName);
                    var rutaImagen = Path.Combine(rutaPrincipal, habitacionDdesdeBd.UrlImagen.TrimStart('\\'));
                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }
                    //Nuevamente subimos el archivo
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }
                    habitacion.UrlImagen = @"\imagenes\Habitaciones\" + nombreArchivo + extension;
                    _contenedorTrabajo.Habitacion.Update(habitacion);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //Aquí sería cuando la imagen ya existe y se conserva
                    habitacion.UrlImagen = habitacionDdesdeBd.UrlImagen;
                }
                _contenedorTrabajo.Habitacion.Update(habitacion);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(habitacion);
        }
        #region Llamadas a la API
        [HttpGet]
        public IActionResult GetAll()
        { return Json(new { data = _contenedorTrabajo.Habitacion.GetAll() }); }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
          int  id =Convert.ToInt32(Id);
            var habitacionDesdeBd = _contenedorTrabajo.Habitacion.Get(id);
            string rutaDirectorioPrincipal = _hostingEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, habitacionDesdeBd.UrlImagen.TrimStart('\\'));
            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }
            if (habitacionDesdeBd == null)
            {
                return Json(new { success = false, message = "Error borrando habitacion" });
            }
            _contenedorTrabajo.Habitacion.Remove(habitacionDesdeBd);
            _contenedorTrabajo.Save();
            return RedirectToAction(nameof(Index));
            //return Json(new { success = true, message = "Habitacion Borrado Correctamente" });
        }
        #endregion
    }

}
