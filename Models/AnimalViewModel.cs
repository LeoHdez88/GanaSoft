using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GanaSoft.Models
{
    public class AnimalViewModel
    {
        [Key]
        public int AnimalId { get; set; }

        [DisplayName("Codigo Animal")]
        public string? Identificacion { get; set; }

        [DisplayName("Color")]
        public string? Color { get; set; }


        public int TipoRazaId { get; set; }

        [DisplayName("Raza")]
        public string? NombreRaza { get; set; }


        public int EstadoId { get; set; }

        [DisplayName("Estado")]
        public string? NombreEstado { get; set; }


        public int HaciendaId { get; set; }

        [DisplayName("Hacienda")]
        public string? NombreHacienda { get; set; }

    }
}