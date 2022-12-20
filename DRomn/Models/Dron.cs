using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRomn.Models
{
    public class Dron
    {
        public int dronID { get; set; }
        public string modelo { get; set; }
        public string marca { get; set; }
        public double autonomia { get; set; }
        public double peso { get; set; }
        public double distancia { get; set; }
        public double altura { get; set; }
        public string info { get; set; }
        public string foto { get; set; }
    }
}
