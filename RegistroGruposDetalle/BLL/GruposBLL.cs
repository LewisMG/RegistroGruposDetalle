using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using RegistroGruposDetalle.DAL;
using RegistroGruposDetalle.Entidades;

namespace RegistroGruposDetalle.BLL
{
    public class GruposBLL
    {
        public static bool Guardar(Grupos grupo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                if (contexto.grupos.Add(grupo) != null)
                {
                    contexto.SaveChanges();
                    paso = true;
                }

                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        public static bool Modificar(Grupos grupo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                //buscar las entidades que no estan para removerlas
                //recorrer el detalle
                foreach (var item in grupo.Detalle)
                {
                    //Muy importante indicar que pasara con la entidad del detalle
                    var estado = item.Id > 0 ? EntityState.Modified : EntityState.Added;
                    contexto.Entry(item).State = estado;
                }
                //Idicar que se esta modificando el encabezado
                contexto.Entry(grupo).State = EntityState.Modified;

                if (contexto.SaveChanges() > 0)
                {
                    paso = true;
                }
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                Grupos grupo = contexto.grupos.Find(id);
                contexto.grupos.Remove(grupo);
                if (contexto.SaveChanges() > 0)
                {
                    paso = true;
                }
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        public static Grupos Buscar(int id)
        {
            Grupos grupo = new Grupos();
            Contexto contexto = new Contexto();
            try
            {
                grupo = contexto.grupos.Find(id);
                //Cargar la lista en este punto porque
                //luego de hacer Dispose() el Contexto 
                //no sera posible leer la lista
                grupo.Detalle.Count();
                //Cargar los nombres de las personas
                foreach (var item in grupo.Detalle)
                {
                    //forzando la persona a cargarse
                    string s = item.Persona.Nombres;
                }
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return grupo;
        }

        public static List<Grupos> GetList(Expression<Func<Grupos, bool>> expression)
        {
            List<Grupos> grupo = new List<Grupos>();
            Contexto contexto = new Contexto();
            try
            {
                grupo = contexto.grupos.Where(expression).ToList();
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return grupo;
        }
    }
}
