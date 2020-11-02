using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RaldyP2Apli.Dal;
using RaldyP2Apli.Entidades;

namespace SegundoParcial_JoseLuis.BLL
{
    public class TareasBLL
    {
        public static List<Tareas> GetTareas()
        {
            List<Tareas> tareas = new List<Tareas>();
            Contexto contexto = new Contexto();

            try
            {
                tareas = contexto.Tareas.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return tareas;
        }
    }
}