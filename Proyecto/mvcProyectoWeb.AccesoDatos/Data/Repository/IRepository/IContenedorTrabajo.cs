using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvcProyectoHotel.AccesoDatos.Data.Repository.IRepository
{
    public interface IContenedorTrabajo : IDisposable
    {
        IAlmacenRepository Almacen { get; }
        IUsuarioRepository Usuario { get; }
        ISliderRepository Slider { get; }
        IHabitacionRepository Habitacion { get; }

        void Save();
    }
}
