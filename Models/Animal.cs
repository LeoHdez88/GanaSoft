using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GanaSoft.Models
{
    public class Animal
    {
        [Key]
        public int AnimalId { get; set; }

        [DisplayName("Codigo Animal")]
        public string? Identificacion { get; set; }

        [DisplayName("Color")]
        public string? Color { get; set; }

        [DisplayName("Raza")]
        public int TipoRazaId { get; set; }

        [DisplayName("Estado")]
        public int EstadoId { get; set; }

        [DisplayName("Hacienda")]
        public int HaciendaId { get; set; }

    }
}