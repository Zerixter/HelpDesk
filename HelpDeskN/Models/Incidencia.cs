using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelpDeskN.Models
{
    public class Incidencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdIncidencia { get; set; }
        public DateTime DataAltaIncidencia { get; set; }
        public string DescripcioCurta { get; set; }

        public int IdTecnicQueObreLaIncidencia { get; set; }
        public virtual Tecnic TecnicQueObreLaIncidencia { get; set; }

        public int? IdTecnicQueTancaLaIncidencia { get; set; }
        public virtual Tecnic TecnicQueTancaLaIncidencia { get; set; }
    }
}