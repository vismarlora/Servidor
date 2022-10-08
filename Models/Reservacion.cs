using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Servidor.Models
{
    public class Reservacion
    {
        [Key]
        public int idReservacion { get; set; }
        public int IdMesa { get; set; }
        public int idPersona { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Ubicacion { get; set; }

        [ForeignKey("IdMesa")]
        public Mesa Mesa { get; set; }

        [ForeignKey("idPersona")]

        public Persona Persona { get; set; }

    }
}
