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
    public class UsuarioRepository: Repository<ApplicationUser>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;
        public UsuarioRepository(ApplicationDbContext db): base(db) 
        {
            _db = db;
        }

        public void BloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeBd = _db.ApplicationUser.FirstOrDefault(u => u.Id == IdUsuario);
            usuarioDesdeBd.LockoutEnd = DateTime.Now.AddYears(1000);
            _db.SaveChanges();
        }

        public void Desbloquearusuario(string IdUsuario)
        {
            var usuarioDesdeBd = _db.ApplicationUser.FirstOrDefault(u => u.Id == IdUsuario);
            usuarioDesdeBd.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
        }
    }
}
