using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HelpDeskN.Models
{
    public class Context: DbContext
    {
        public DbSet<Tecnic> Tecnics { get; set; }
        public DbSet<Incidencia> Incidencies { get; set; }
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Incidencia>()
                .HasRequired(t => t.TecnicQueObreLaIncidencia)
                .WithMany(m => m.IncidenciesQueObreElTecnic)
                .HasForeignKey(k => k.IdTecnicQueObreLaIncidencia)
                .WillCascadeOnDelete(false);

            builder.Entity<Incidencia>()
                .HasOptional(t => t.TecnicQueTancaLaIncidencia)
                .WithMany(m => m.IncidenciesQueTancaElTecnic)
                .HasForeignKey(k => k.IdTecnicQueTancaLaIncidencia)
                .WillCascadeOnDelete(false);
        }
    }
}