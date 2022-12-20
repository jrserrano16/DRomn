using DRomn.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml;

namespace DRomn
{
    /// <summary>
    /// Lógica de interacción para ImageWindow.xaml
    /// </summary>
    public partial class ImageWindow : Window
    {
        private List<Imagen> imagenes;
        private string username;
        private string url;
        private List<Dron> drones;
        private string tipoImagen;
        private ImageMenuWindow mw;

        public ImageWindow(string username, string tipoImagen)
        {
            this.tipoImagen = tipoImagen;
            this.username = username;
            InitializeComponent();
            mostrarImagen();
            cambiarImagen();
            lbl_User.Content = username;
            txt_Tipo.Text = this.tipoImagen;
            dt_Fecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void cargarContenidoXML()
        {
            lstbx_Imagenes.ItemsSource=null;
            mostrarImagen();
            cambiarImagen();
            lstbx_Imagenes.ItemsSource = imagenes;
        }


        private void btn_ampliar_Click(object sender, RoutedEventArgs e)
        {
            img_imagenAmpliada.Source = img_Foto.Source;
            img_imagenAmpliada.Visibility = Visibility.Visible;
        }

        private void img_imagenAmpliada_MouseDown(object sender, MouseButtonEventArgs e)
        {
            img_imagenAmpliada.Visibility = Visibility.Hidden;
        }

        private void lstbx_Imagenes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstbx_Imagenes.SelectedIndex >= 0)
            {
                cb_Drones.Visibility = Visibility.Hidden;
                txt_Drone.Visibility = Visibility.Visible;
                enable_disableText(true);

                string foto = imagenes[lstbx_Imagenes.SelectedIndex].foto;
                if (!foto.Contains("DRomn\\"))
                {
                    foto= AppDomain.CurrentDomain.BaseDirectory+foto;
                }
                img_Foto.Source = new BitmapImage(new Uri(foto));
            }
        }

        private void btn_Imagen_Click(object sender, RoutedEventArgs e)
        {
            var ruta = AuxClass.abrirImagen("image");
            if  (!ruta.FileName.Equals(String.Empty))
                {
                url = ruta.SafeFileName;
                img_Foto.Source =  new BitmapImage(new Uri(ruta.FileName));
            }
            else
            {
                MessageBox.Show("Error al seleccionar Foto.", "Aviso");
            }
            

        }
        private void cambiarImagen()
        {
            foreach (Imagen i in imagenes)
            {
                i.foto = AppDomain.CurrentDomain.BaseDirectory+i.foto.ToString();
            }
            cb_Drones.ItemsSource=null;
            drones = AuxClass.mostrarDrones(username);
            foreach (Dron d in drones)
            {
                d.foto = AppDomain.CurrentDomain.BaseDirectory+d.foto.ToString();
            }
            cb_Drones.ItemsSource = drones;
        }
        public void crearXMLImagen()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("listaUsuario.xml");
            XmlNode root = doc.SelectSingleNode("Dromn");
            XmlNode users = root.SelectSingleNode("Users");
            XmlNode image;
            foreach (XmlNode user in users.ChildNodes)
            {
                if (user.Attributes["Username"].Value.Equals(username))
                {
                    if (user.SelectSingleNode("Imagenes") == null)
                    {
                        image = doc.CreateElement("Imagenes");
                        user.AppendChild(image);
                    }
                    image = user.SelectSingleNode("Imagenes");
                    XmlElement imag = doc.CreateElement("Imagen");


                    XmlAttribute nom = doc.CreateAttribute("Nombre");
                    nom.Value = txt_Nombre.Text;
                    imag.Attributes.Append(nom);

                    XmlAttribute type = doc.CreateAttribute("Tipo");
                    type.Value = tipoImagen;
                    imag.Attributes.Append(type);

                    XmlAttribute dronname = doc.CreateAttribute("DroneName");
                    dronname.Value = drones[cb_Drones.SelectedIndex].modelo;
                    imag.Attributes.Append(dronname);

                    XmlAttribute droneid = doc.CreateAttribute("DroneID");
                    droneid.Value = drones[cb_Drones.SelectedIndex].dronID.ToString();
                    imag.Attributes.Append(droneid);

                    XmlAttribute fecha = doc.CreateAttribute("Fecha");
                    fecha.Value = dt_Fecha.Text;
                    imag.Attributes.Append(fecha);

                    XmlAttribute loc = doc.CreateAttribute("Localizacion");
                    loc.Value = txt_Localizacion.Text;
                    imag.Attributes.Append(loc);

                    XmlAttribute alt = doc.CreateAttribute("Altura");
                    alt.Value = txt_Altura.Text;
                    imag.Attributes.Append(alt);


                    XmlAttribute info = doc.CreateAttribute("Info");
                    info.Value = txt_Info.Text;
                    imag.Attributes.Append(info);

                    XmlAttribute foto = doc.CreateAttribute("Foto");
                    foto.Value = "dron_images\\image\\"+url;
                    imag.Attributes.Append(foto);


                    image.AppendChild(imag);
                    doc.Save("listaUsuario.xml");
                }
            }



        }

        private void mostrarImagen()
        {
            lstbx_Imagenes.ItemsSource=null;
            imagenes = new List<Imagen>();
            XmlDocument doc = new XmlDocument();
            doc.Load("listaUsuario.xml");
            XmlNode root = doc.SelectSingleNode("Dromn");
            XmlNode users = root.SelectSingleNode("Users");
            XmlNode imagenesxml = null;

            foreach (XmlNode usuario in users.ChildNodes)
            {
                if (usuario.Attributes["Username"].Value.Equals(username))
                {
                    if (usuario.SelectSingleNode("Imagenes") != null)
                    {

                        imagenesxml = usuario.SelectSingleNode("Imagenes");
                        foreach (XmlNode img in imagenesxml)
                        {
                            if (img.Attributes["Tipo"].Value.Equals(tipoImagen))
                            {
                                Imagen i = new Imagen();
                                i.name = img.Attributes["Nombre"].Value;
                                i.tipo = img.Attributes["Tipo"].Value;
                                i.dronName = img.Attributes["DroneName"].Value;
                                i.dronID =Convert.ToInt32(img.Attributes["DroneID"].Value);
                                i.fecha =  img.Attributes["Fecha"].Value;
                                i.altura=  Convert.ToDouble(img.Attributes["Altura"].Value);
                                i.loc = img.Attributes["Localizacion"].Value;
                                i.info = img.Attributes["Info"].Value;
                                i.foto = img.Attributes["Foto"].Value;
                                imagenes.Add(i);
                            }
                        }
                    }
                }
            }
            lstbx_Imagenes.ItemsSource = imagenes;


        }
        private void btn_Limpiar_Click(object sender, RoutedEventArgs e)
        {

            txt_Altura.Text = String.Empty;
            txt_Nombre.Text = String.Empty;
            txt_Info.Text = String.Empty;
            txt_Localizacion.Text = String.Empty;
            dt_Fecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            string foto = AppDomain.CurrentDomain.BaseDirectory+"dron_images\\image\\not_image_selected.jpg";
            img_Foto.Source = new BitmapImage(new Uri(foto));
            btn_Añadir.IsEnabled = true;
            enable_disableText(false);
            txt_Nombre.Focus();
            cb_Drones.Visibility = Visibility.Visible;
            txt_Drone.Visibility = Visibility.Hidden;
        }

        private void enable_disableText(bool valor)
        {
            txt_Altura.IsReadOnly = valor;
            txt_Nombre.IsReadOnly = valor;
            txt_Localizacion.IsReadOnly = valor;
            txt_Info.IsReadOnly = valor;
           
        }

        private bool checkData()
        {
            double alt;
            bool altisDouble = Double.TryParse(txt_Altura.Text, out alt);


            if (!txt_Altura.Text.Equals(String.Empty) && !txt_Nombre.Text.Equals(String.Empty)
                && !txt_Info.Text.Equals(String.Empty) && !txt_Localizacion.Text.Equals(String.Empty)
                && cb_Drones.SelectedIndex >=0 && !img_Foto.Source.ToString().Contains("not_image_selected.jpg")
                && altisDouble)
            {
                return true;
            }
            return false;
        }
        private void btn_Añadir_Click(object sender, RoutedEventArgs e)
        {
            if (checkData())
            {
                crearXMLImagen();
                cargarContenidoXML();
                MessageBox.Show("Imagen añadida con éxito - "+txt_Nombre.Text, "Aviso");
                btn_Limpiar_Click(sender, e);

            }
            else
            {
                MessageBox.Show("Algunos campos no fueron rellandos, tienen formato incorrecto o no se seleccionó ninguna imagen..", "Aviso");
            }

        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {

            mw = new ImageMenuWindow(username);
            mw.Show();
            this.Close();

        }
    }
}
