namespace GanaSoft.Models
{

    using Microsoft.AspNetCore.Mvc.Rendering;
    public class FiltroAnimalesViewModel
    {
        public List<AnimalViewModel>? Animales { get; set; }
        public SelectList? Haciendas { get; set; }
        public SelectList? Estados { get; set; }
        public SelectList? TiposRazas { get; set; }
        public string? AnimalHacienda { get; set; }
        public string? AnimalCodigo { get; set; }
        public string? AnimalRaza { get; set; }
        public string? AnimalEstado { get; set; }
    }
}