using DRomn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace DRomn
{
    /// <summary>
    /// Lógica de interacción para DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        private string username;
        private List<Dron> drones;
        private string url;
        private MenuWindow mw;

        public DroneWindow(string username)
        {
            this.username = username;
            InitializeComponent();
            lbl_User.Content = username;
            cargarContenidoXML();
        }

        private void BtnAtras_Click(object sender, RoutedEventArgs e)
        {
            mw = new MenuWindow(username);
            mw.Show();
            this.Close();
        }


        public void crearXMLDron()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("listaUsuario.xml");
            XmlNode root = doc.SelectSingleNode("Dromn");
            XmlNode users = root.SelectSingleNode("Users");
            XmlNode drone = null;
            int iddron = 0;
            foreach (XmlNode usuario in users.ChildNodes)
            {
                if (usuario.Attributes["Username"].Value.Equals(username))
                {
                    if (usuario.SelectSingleNode("Drones") == null)
                    {
                        drone = doc.CreateElement("Drones");
                        usuario.AppendChild(drone);
                    }
                    else
                    {
                        drone = usuario.SelectSingleNode("Drones");
                        
                        foreach(XmlNode n in drone)
                        {
                            iddron = Convert.ToInt32(n.Attributes["DroneID"].Value) + 1;
                        }
                    }
                    XmlElement dr = doc.CreateElement("Dron");

                    XmlAttribute id = doc.CreateAttribute("DroneID");
                    id.Value = iddron.ToString();
                    dr.Attributes.Append(id);

                    XmlAttribute model = doc.CreateAttribute("Modelo");
                    model.Value = txt_Modelo.Text;
                    dr.Attributes.Append(model);

                    XmlAttribute marca = doc.CreateAttribute("Marca");
                    marca.Value = txt_Marca.Text;
                    dr.Attributes.Append(marca);

                    XmlAttribute autonomia = doc.CreateAttribute("Autonomia");
                    autonomia.Value = txt_Autonomia.Text;
                    dr.Attributes.Append(autonomia);

                    XmlAttribute peso = doc.CreateAttribute("Peso");
                    peso.Value = txt_Peso.Text;
                    dr.Attributes.Append(peso);

                    XmlAttribute dist = doc.CreateAttribute("Distancia");
                    dist.Value = txt_Distancia.Text;
                    dr.Attributes.Append(dist);

                    XmlAttribute alt = doc.CreateAttribute("Altura");
                    alt.Value = txt_Altura.Text;
                    dr.Attributes.Append(alt);

                    XmlAttribute info = doc.CreateAttribute("Info");
                    info.Value = txt_Info.Text;
                    dr.Attributes.Append(info);

                    XmlAttribute foto = doc.CreateAttribute("Foto");
                    foto.Value = "dron_images\\dron\\"+url;
                    dr.Attributes.Append(foto);


                    drone.AppendChild(dr);
                    doc.Save("listaUsuario.xml");
                }

            }
        }

        private void cargarContenidoXML()
        {
            lstbx_Drones.ItemsSource=null;
            drones = AuxClass.mostrarDrones(username);
            lstbx_Drones.ItemsSource = drones;
        }


      
        private void btn_Imagen_Click(object sender, RoutedEventArgs e)
        {
            var ruta= AuxClass.abrirImagen("dron");
            if (!ruta.FileName.Equals(String.Empty))
            {
                url = ruta.SafeFileName;
                img_Drone.Source =  new BitmapImage(new Uri(ruta.FileName));
            }
            else
            {
                MessageBox.Show("Error al seleccionar Foto.", "Aviso");
            }
           
           
        }

        private void lstbx_Drones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstbx_Drones.SelectedIndex >= 0) 
            {
              
                btn_Añadir.IsEnabled=false;
                string foto = AppDomain.CurrentDomain.BaseDirectory+drones[lstbx_Drones.SelectedIndex].foto;
                img_Drone.Source = new BitmapImage(new Uri(foto));
                enable_disableText(true);
            }

        }

        private void enable_disableText(bool valor)
        {
            txt_Altura.IsReadOnly = valor;
            txt_Autonomia.IsReadOnly = valor;
            txt_Distancia.IsReadOnly = valor;
            txt_Info.IsReadOnly = valor;
            txt_Marca.IsReadOnly = valor;
            txt_Modelo.IsReadOnly = valor;
            txt_Peso.IsReadOnly = valor;
        }

        private void btn_Añadir_Click(object sender, RoutedEventArgs e)
        {
            if (checkData())
            {
                crearXMLDron();
                cargarContenidoXML();
                MessageBox.Show("Dron añadido con éxito - "+txt_Modelo.Text, "Aviso");
                btn_Limpiar_Click(sender, e);

            }
            else
            {
                MessageBox.Show("Algunos campos no fueron rellandos, tienen formato incorrecto o no se seleccionó ninguna imagen..", "Aviso");
            }

        }
       
            
        

        private bool checkData()
        {
            double alt;
            bool altisDouble = Double.TryParse(txt_Altura.Text, out alt);

            double aut;
            bool autisDouble = Double.TryParse(txt_Autonomia.Text, out aut);

            double dis;
            bool disisDouble = Double.TryParse(txt_Distancia.Text, out dis);

            double pes;
            bool pesisDouble = Double.TryParse(txt_Peso.Text, out pes);

            if (!txt_Altura.Text.Equals(String.Empty) && !txt_Marca.Text.Equals(String.Empty)
                && !txt_Info.Text.Equals(String.Empty) && !txt_Modelo.Text.Equals(String.Empty)
                && !txt_Autonomia.Text.Equals(String.Empty) && !txt_Distancia.Text.Equals(String.Empty)
                && !txt_Peso.Text.Equals(String.Empty) && !img_Drone.Source.ToString().Contains("not_selected.jpg")
                && altisDouble && autisDouble && disisDouble && pesisDouble)
            {
                return true;
            }
            return false;
        }

        private void btn_ampliar_Click(object sender, RoutedEventArgs e)
        {
            img_imagenAmpliada.Source = img_Drone.Source;
            img_imagenAmpliada.Visibility = Visibility.Visible;
            fondo_ampliada.Visibility = Visibility.Visible;
        }

        private void img_imagenAmpliada_MouseDown(object sender, MouseButtonEventArgs e)
        {
            img_imagenAmpliada.Visibility = Visibility.Hidden;
            fondo_ampliada.Visibility = Visibility.Hidden;
        }

        private void btn_Limpiar_Click(object sender, RoutedEventArgs e)
        {
            txt_Altura.Text = String.Empty;
            txt_Autonomia.Text = String.Empty;
            txt_Distancia.Text = String.Empty;
            txt_Info.Text = String.Empty;
            txt_Marca.Text = String.Empty;
            txt_Modelo.Text = String.Empty;
            txt_Peso.Text = String.Empty;
            string foto = AppDomain.CurrentDomain.BaseDirectory+"dron_images\\dron\\not_selected.jpg";
            img_Drone.Source = new BitmapImage(new Uri(foto));
            btn_Añadir.IsEnabled = true;
            enable_disableText(false);
            txt_Marca.Focus();
            cargarContenidoXML();

        }


      
    }
}
