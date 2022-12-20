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
    /// Lógica de interacción para UserRegistration.xaml
    /// </summary>
    public partial class UserRegistration : Window
    {
        MainWindow mw = new MainWindow();
        private BitmapImage imagCheck = new BitmapImage(new Uri("/src/correct.png", UriKind.Relative));
        private BitmapImage imagCross = new BitmapImage(new Uri("/src/wrong.png", UriKind.Relative));
        XmlDocument doc = new XmlDocument();
        public List<Usuario> usuarios;



        public UserRegistration()
        {
            InitializeComponent();
            usuarios = AuxClass.CargarContenidoXML();
        }
        public bool comprobarControl(Control c, Image i)
        {

            if (c is TextBox)
            {
                if (((TextBox)c).Text == string.Empty)
                {
                    ((TextBox)c).BorderBrush = Brushes.Red;
                    i.Source = imagCross;
                    return false;
                }
                else
                {
                    ((TextBox)c).BorderBrush = Brushes.Green;
                    i.Source = imagCheck;
                    return true;
                }


            }
            else if (c is ComboBox)
            {
                if (((ComboBox)c).Text == string.Empty)
                {
                    ((ComboBox)c).BorderBrush = Brushes.Red;
                    i.Source = imagCross;
                    return false;
                }
                else
                {
                    ((ComboBox)c).BorderBrush = Brushes.Green;
                    i.Source = imagCheck;
                    return true;
                }
            }

            else if (c is DatePicker)
            {
                if (((DatePicker)c).Text == string.Empty)
                {
                    ((DatePicker)c).BorderBrush = Brushes.Red;
                    i.Source = imagCross;
                    return false;
                }
                else
                {
                    ((DatePicker)c).BorderBrush = Brushes.Green;
                    i.Source = imagCheck;
                    return true;
                }
            }
            return true;

        }
        private bool comprobarAllControls()
        {
            if (comprobarControl(txtNombre,checkName) & comprobarControl(txtDNI,checkDNI) & 
                comprobarControl(cb_genero,checkGenero) & comprobarControl(txtPais,checkPais) & 
                comprobarControl(txtUsuario,checkUser) & comprobarControl(txtPassw,checkPassw) & 
                comprobarControl(txtEmail,checkEmail) & comprobarControl(cb_Carnet, checkCarnet))
            {
                return true;
            }
            else
                return false;
        }


        private bool comprobarUser_Email(TextBox t, int mode, Image i)
        {
            if (mode == 0)
            {
                foreach (Usuario u in usuarios)
                {
                    if (t.Text.Equals(u.username))
                    {
                        i.Source = imagCross;
                        t.BorderBrush = Brushes.IndianRed;
                        return false;
                    }
                }
            }
            else
            {
                foreach (Usuario u in usuarios)
                {
                    if (t.Text.Equals(u.email))
                    {
                        i.Source = imagCross;
                        t.BorderBrush = Brushes.IndianRed;
                        return false;
                    }
                }
            }
            return true;
        }


        private void añadirUsuarios()
        {
            doc.Load("listaUsuario.xml");
            XmlNode root = doc.SelectSingleNode("Dromn");
            XmlNode users = root.SelectSingleNode("Users");
            XmlElement nodoUsuario = doc.CreateElement("Usuario");
            Usuario nuevo = new Usuario(txtNombre.Text, txtDNI.Text, cb_genero.Text, txtPais.Text, txtUsuario.Text,
            txtPassw.Text, txtEmail.Text, cb_Carnet.Text);
            XmlAttribute nombre = doc.CreateAttribute("Nombre");
            nombre.Value = nuevo.nombre_ape;
            nodoUsuario.Attributes.Append(nombre);
            XmlAttribute DNI = doc.CreateAttribute("DNI");
            DNI.Value = nuevo.dni;
            nodoUsuario.Attributes.Append(DNI);
            XmlAttribute sexo = doc.CreateAttribute("Sexo");
            sexo.Value = nuevo.sexo;
            nodoUsuario.Attributes.Append(sexo);
            XmlAttribute pais = doc.CreateAttribute("Pais");
            pais.Value = nuevo.pais;
            nodoUsuario.Attributes.Append(pais);
            XmlAttribute username = doc.CreateAttribute("Username");
            username.Value = nuevo.username;
            nodoUsuario.Attributes.Append(username);
            XmlAttribute password = doc.CreateAttribute("Password");
            password.Value = nuevo.contrasena;
            nodoUsuario.Attributes.Append(password);
            XmlAttribute email = doc.CreateAttribute("Email");
            email.Value = nuevo.email;
            nodoUsuario.Attributes.Append(email);
            XmlAttribute carnet = doc.CreateAttribute("Carnet");
            carnet.Value = nuevo.tipo_carnet;
            nodoUsuario.Attributes.Append(carnet);

            users.AppendChild(nodoUsuario);
            usuarios.Add(nuevo);

            doc.Save("listaUsuario.xml");

        }

        private void btnLogin_Copy_Click(object sender, RoutedEventArgs e)
        {
            usuarios = AuxClass.CargarContenidoXML();
            if (comprobarAllControls() & comprobarUser_Email(txtUsuario,0,checkUser) & 
                comprobarUser_Email(txtEmail, 1, checkEmail))
                {
                    añadirUsuarios();
                
                    
                    MessageBox.Show("Usuario añadido con exito - "+txtUsuario.Text);

                }
                else
                    MessageBox.Show("No se rellenó alguno de los campos obligatorios o el formato es incorrecto");

            }

        private void txtNombre_GotFocus(object sender, RoutedEventArgs e)
        {
            txtNombre.Text = string.Empty;
        }

        private void txtDNI_GotFocus(object sender, RoutedEventArgs e)
        {
            txtDNI.Text = string.Empty;
        }

        private void txtUsuario_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUsuario.Text = string.Empty;
        }

        private void txtPassw_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPassw.Text = string.Empty;
        }

        private void txtPais_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPais.Text = string.Empty;
        }

        private void BtnAtras_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            mw.Show();

        }

        private void cb_Carnet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txt_Descripcion.Text = getDescripcion();   
        }

        private string getDescripcion()
        { 
        // Cargar contenido de prueba
        XmlDocument docSol = new XmlDocument();
        docSol.Load("listaUsuario.xml");
            string desc = null;
            XmlNode root = docSol.SelectSingleNode("Dromn");
        XmlNode target = root.SelectSingleNode("Carnets");

            foreach (XmlNode node in target.ChildNodes)
            {
                if (node.Attributes["Tipo"].Value == cb_Carnet.SelectedIndex.ToString())
                {
                    desc = node.Attributes["Descripcion"].Value;
                    break;
                }
            }
            return desc;
        }

        private void txtEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            txtEmail.Text = String.Empty;
        }
    }
    }


