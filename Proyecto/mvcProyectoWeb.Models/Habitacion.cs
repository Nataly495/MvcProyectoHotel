using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvcProyectoHotel.Models
{
    public class Habitacion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre para la habitacion")]
        [Display(Name = "Nombre de la habitacion")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese una descripcion para la habitacion")]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Required]
        public bool Estado { get; set; }    

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string UrlImagen { get; set; }
    }
}
