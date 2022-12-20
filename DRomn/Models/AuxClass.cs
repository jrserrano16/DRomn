using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml;

namespace DRomn.Models
{
    public class AuxClass
    {
        private static List<Dron> drones;

        public static List<Usuario> CargarContenidoXML()
        {
            List<Usuario> listadoUsuario = new List<Usuario>();
            // Cargar contenido de prueba
            XmlDocument docSol = new XmlDocument();
            docSol.Load("listaUsuario.xml");

            XmlNode root = docSol.SelectSingleNode("Dromn");
            XmlNode target = root.SelectSingleNode("Users");

            foreach (XmlNode node in target.ChildNodes)
            {
                var nuevoUser = new Usuario("", "", "", "", "", "", "", "");
                nuevoUser.nombre_ape = node.Attributes["Nombre"].Value;
                nuevoUser.dni = node.Attributes["DNI"].Value;
                nuevoUser.sexo = node.Attributes["Sexo"].Value;
                nuevoUser.pais = node.Attributes["Pais"].Value;
                nuevoUser.username = node.Attributes["Username"].Value;
                nuevoUser.contrasena = node.Attributes["Password"].Value;
                nuevoUser.email = node.Attributes["Email"].Value;
                nuevoUser.tipo_carnet = node.Attributes["Carnet"].Value;

                listadoUsuario.Add(nuevoUser);
            }
            return listadoUsuario;
        }

        public static List<Dron> mostrarDrones(string username)
        {

            drones = new List<Dron>();
            XmlDocument doc = new XmlDocument();
            doc.Load("listaUsuario.xml");
            XmlNode root = doc.SelectSingleNode("Dromn");
            XmlNode users = root.SelectSingleNode("Users");

            foreach (XmlNode user in users.ChildNodes)
            {
                if (user.Attributes["Username"].Value.Equals(username))
                {
                    if (user.SelectSingleNode("Drones") != null)
                    {

                        XmlNode dron = user.SelectSingleNode("Drones");
                        if (dron.HasChildNodes)
                        {
                            foreach (XmlNode dr in dron.ChildNodes)
                            {
                                Dron d = new Dron();
                                d.dronID = Convert.ToInt32(dr.Attributes["DroneID"].Value);
                                d.autonomia = Convert.ToDouble(dr.Attributes["Autonomia"].Value);
                                d.marca =  dr.Attributes["Marca"].Value;
                                d.modelo =  dr.Attributes["Modelo"].Value;
                                d.peso = Convert.ToDouble(dr.Attributes["Peso"].Value);
                                d.distancia = Convert.ToDouble(dr.Attributes["Distancia"].Value);
                                d.altura = Convert.ToDouble(dr.Attributes["Altura"].Value);
                                d.info = dr.Attributes["Info"].Value;
                                d.foto = dr.Attributes["Foto"].Value;
                                drones.Add(d);
                            }
                        }
                    }

                }

            }
            return drones;


        }





        public static OpenFileDialog abrirImagen(string carpeta)
        {
            OpenFileDialog abrirDialog = new OpenFileDialog();
            abrirDialog.Title = "Seleccionar foto";
            abrirDialog.Filter = "Images|*.jpg;*.bmp;*.png;*.jpeg";
            abrirDialog.InitialDirectory = Environment.SpecialFolder.UserProfile + @"\Downloads";

            if (abrirDialog.ShowDialog() == true)
            {
                try
                {
                    string targetPath = AppDomain.CurrentDomain.BaseDirectory+ "dron_images\\"+carpeta+"\\";
                    string target = targetPath+abrirDialog.SafeFileName;

                    string source = abrirDialog.FileName;

                    if (!File.Exists(target))
                    {

                        File.Copy(source, target, true);
                    }
                    else
                    {
                        int dia = System.DateTime.Now.Day;
                        int mes = System.DateTime.Now.Month;
                        int anio = System.DateTime.Now.Year;
                        int min = System.DateTime.Now.Minute;
                        int sec = System.DateTime.Now.Second;
                        int hor = System.DateTime.Now.Hour;
                        target = targetPath+anio+mes+dia+hor+min+dia+"_"+abrirDialog.SafeFileName;
                        File.Copy(source, target, true);


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen " + ex.Message);
                }

            }

            return abrirDialog;
        }
    }
}
