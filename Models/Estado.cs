using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GanaSoft.Models
{
    public class Estado
    {
        [Key]
        public int EstadoId { get; set; }

        public string? Descripcion { get; set; }
    }
}
