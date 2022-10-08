using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.Models
{
    public class Mesa
    {
        [Key]
        public int IdMesa { get; set; }
        public int Capacidad { get; set; }
        public string Forma { get; set; }
        public float Precio { get; set; }
        public bool Disponibilidad { get; set; }

    }
}
