using mvcProyectoHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvcProyectoHotel.AccesoDatos.Data.Repository.IRepository
{
    public interface IHabitacionRepository : IRepository<Habitacion>
    {

        void Update(Habitacion habitacion);
    }
}
