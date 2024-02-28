using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GanaSoft.Models
{
    public class TipoRaza
    {
        [Key]
        public int TipoRazaId { get; set; }

        public string? Descripcion { get; set; }

        public bool Activo { get; set; }

    }
}