using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RaldyP2Apli.Entidades
{

    public class Proyectos
    {
        [Key]

        public int ProyectoId {get; set;}

        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public double TiempoTotal { get; set; }

        [ForeignKey("ProyectoId")]
        public virtual List<ProyectosDetalle> Detalle {get; set;} = new List<ProyectosDetalle>();
    }
}