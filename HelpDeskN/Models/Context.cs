using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
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
        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            var result = new DbEntityValidationResult(entityEntry, new List<DbValidationError>());

            if (entityEntry.Entity is Incidencia &&
                (entityEntry.State == EntityState.Added ||
                entityEntry.State == EntityState.Modified)
                )
            {
                Incidencia incidencia = entityEntry.Entity as Incidencia;

                Tecnic tecnic = incidencia.TecnicQueObreLaIncidencia;

                bool ja_te_3_incidencies = this.
                                Incidencies.
                                Where(x => x.IdTecnicQueObreLaIncidencia == incidencia.IdTecnicQueObreLaIncidencia)
                                .Where(x => x.IdTecnicQueTancaLaIncidencia == null)
                                .Count() >= 3;

                if (ja_te_3_incidencies)
                {
                    result.ValidationErrors.Add(
                        new System.Data.Entity.Validation.DbValidationError("IdTecnicQueTancaLaIncidencia",
                        "Amb compte! Aquest tècnic ja té 3 incidències obertes"));
                }
            }

            return base.ValidateEntity(entityEntry, items);
        }
    }
}