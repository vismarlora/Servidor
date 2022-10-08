using Servidor.DAL;
using System;
using Servidor.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Servidor.BLL
{
    public class CitasBLL
    {
        public static Mesa Buscar(int IdMesa)
        {
            Contexto contexto = new Contexto();
            Mesa Mesa;

            try
            {
                Mesa = contexto.Mesa.Find(IdMesa);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Mesa;
        }
        public static List<Mesa> GetCita()
        {
            Contexto contexto = new Contexto();
            List<Mesa> lista = new List<Mesa>();
            try
            {
                lista = contexto.Mesa.ToList();
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
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                paso = contexto.Mesa.Any(e => e.IdMesa == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static bool Insertar(Mesa mesa)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Mesa.Add(mesa);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static bool Modificar(Mesa mesa)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(mesa).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;

        }
        public static bool Guardar(Mesa mesas)
        {
            if (!Existe(mesas.IdMesa))
                return Insertar(mesas);
            else
                return Modificar(mesas);
        }
    }   
}
