using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRomn.Models
{
    public class Imagen
    {
        public string name { get; set; }
        public string tipo { get; set; }
        public string fecha { get; set; }
        public int dronID { get; set; }
        public string dronName { get; set; }
        public int userID {  get; set;  }
        public double altura { get; set; }
        public string loc { get; set; }
        public string info { get; set; }
        public string foto { get; set; }
    }
}
