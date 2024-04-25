using mvcProyectoHotel.AccesoDatos.Data.Repository.IRepository;
using mvcProyectoHotel.Data;
using mvcProyectoHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvcProyectoHotel.AccesoDatos.Data.Repository
{
    public class HabitacionRepository : Repository<Habitacion>, IHabitacionRepository
    {
        private readonly ApplicationDbContext _db;

        public HabitacionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Habitacion habitacion)
        {
            var objDesdeDb = _db.Habitacion.FirstOrDefault(s => s.Id == habitacion.Id);
            objDesdeDb.Nombre = habitacion.Nombre;
            objDesdeDb.Descripcion = habitacion.Descripcion;
            objDesdeDb.Estado = habitacion.Estado;
            objDesdeDb.UrlImagen = habitacion.UrlImagen;
        }
    }
}
