using System;
using System.Collections.Generic;
using System.Text;

namespace VerificadorPrecios.Models
{
    public class Proveedores
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string address { get; set; }
        public string remarks { get; set; }
    }
}
