using System;
using System.Collections.Generic;
using System.Text;

namespace VerificadorPrecios.Models
{
    public class Productos
    {
        public int id { get; set; }
        public string code { get; set; }//
        public string name { get; set; }//
        public string reference { get; set; }//
        public string detail { get; set; }//
        public int vendorID { get; set; }//
        public int marca { get; set; }
        public int catID { get; set; }//
        public decimal cost { get; set; }//
        public decimal price1 { get; set; }//
        public int lists { get; set; }//
        public int inventory { get; set; }//
        public string remarks { get; set; }//
        public string active { get; set; }//
        public string image { get; set; }//
        public int listas { get; set; }//

    }
}
