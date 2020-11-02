using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RaldyP2Apli.Entidades
{
    public class Tareas
    {
        [Key]
        public int TareaId { get; set; }
        public string TipoTarea { get; set; }
    }
}