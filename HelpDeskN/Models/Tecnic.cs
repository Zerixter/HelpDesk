using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelpDeskN.Models
{
    public class Tecnic
    {
        [Key]
        public int IdTecnic { get; set; }
        [StringLength(70)]
        [Display(Name = "Nom del tècnic")]
        [Required]
        public string NomTecnic { get; set; }
        [Display(Name = "És Actiu")]
        public bool EsActiu { get; set; } = true;

        [Display(Name = "Incidencies que ha obert el tècnic")]
        public virtual List<Incidencia> IncidenciesQueObreElTecnic { get; set; } = new List<Incidencia>();
        [Display(Name = "Incidencies que ha tancat el tècnic")]
        public virtual List<Incidencia> IncidenciesQueTancaElTecnic { get; set; } = new List<Incidencia>();
    }
}