using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GanaSoft.Models
{
    public class Hacienda
    {
        [Key]
        public int HaciendaId { get; set; }

        public string? nombre { get; set; }

        public string? Propietario { get; set; }

        public string? Direccion { get; set; }

        public bool Activo { get; set; }
    }
}