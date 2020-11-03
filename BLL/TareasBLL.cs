using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RaldyP2Apli.Dal;
using RaldyP2Apli.Entidades;

namespace RaldyP2Apli.BLL
{
    public class TareasBLL
    {
        public static List<Tareas> GetList(Expression<Func<Tareas, bool>> criterio)
        {

            List<Tareas> lista = new List<Tareas>();
            Contexto contexto = new Contexto();

            try
            {
                //obtener la lista y filtrarla segun el criterio recibido por parametro
                lista = contexto.Tareas.Where(criterio).ToList();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
    }
}