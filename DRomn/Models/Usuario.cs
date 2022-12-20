using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRomn.Models
{
    public class Usuario
    {
        public string nombre_ape { get; set; }
        public string dni { get; set; }     
        public string sexo { get; set; }
        public string pais { get; set; }
        public string username { get; set; }
        public string contrasena { get; set; }
        public string email { get; set; }
        public string tipo_carnet { get; set; }

        public Usuario(string nombre_ape, string dni, string sexo, string pais, string username,
            string contrasena, string email, string tipo_carnet)
        {
            this.nombre_ape = nombre_ape;
            this.dni = dni;
            this.sexo = sexo;
            this.pais = pais;
            this.username = username;
            this.contrasena = contrasena;
            this.email = email;
            this.tipo_carnet = tipo_carnet;


        }
    }
}
