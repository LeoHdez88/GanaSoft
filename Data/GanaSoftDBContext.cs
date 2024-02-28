using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GanaSoft.Models;

namespace GanaSoft.Data
{
    public class GanaSoftDBContext : DbContext
    {
        public GanaSoftDBContext(DbContextOptions<GanaSoftDBContext> options)
            : base(options)
        {
        }

        public DbSet<GanaSoft.Models.Estado> Estado { get; set; } = default!;

        public DbSet<GanaSoft.Models.TipoRaza> TipoRaza { get; set; } = default!;

        public DbSet<GanaSoft.Models.Hacienda> Hacienda { get; set; } = default!;

        public DbSet<GanaSoft.Models.Animal> Animal { get; set; } = default!;
    }
}
