﻿using mvcProyectoHotel.AccesoDatos.Data.Repository.IRepository;
using mvcProyectoHotel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvcProyectoHotel.AccesoDatos.Data.Repository
{
    public class ContenedorTrabajo: IContenedorTrabajo
    {
        private readonly ApplicationDbContext _context;
        public ContenedorTrabajo(ApplicationDbContext context)
        {
            _context = context;
            //se agregan cada uno de los repositorios para que queden encapsulados
            Almacen = new AlmacenRepository(_context);
            Usuario = new UsuarioRepository(_context);
            Slider = new SliderRepository(_context);
            Habitacion = new HabitacionRepository(_context);

        }

        public IAlmacenRepository Almacen { get; private set; }
        public IUsuarioRepository Usuario { get; private set; }

        public ISliderRepository Slider { get; private set; }
        public IHabitacionRepository Habitacion { get; private set; }


        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
