using DRomn.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace DRomn
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MenuWindow menu;
        private BitmapImage imagCheck = new BitmapImage(new Uri("/src/correct.png", UriKind.Relative));
        private BitmapImage imagCross = new BitmapImage(new Uri("/src/wrong.png", UriKind.Relative));
        private UserRegistration ur;
        public List<Usuario> usuarios;
        public MainWindow()
        {
            InitializeComponent();
            usuarios = null;
            usuarios = AuxClass.CargarContenidoXML();


        }

       
        private void btn_Recuperar_Click(object sender, RoutedEventArgs e)
        {
           
        }



        private void txtUsuario_KeyUp(object sender, KeyEventArgs e)
        {
            usuarios = AuxClass.CargarContenidoXML();
            passContrasena.IsEnabled = false;
            txtUsuario.Background = Brushes.White;
            if (e.Key == Key.Enter)
            {
                if (ComprobarEntrada(txtUsuario.Text, usuarios, txtUsuario, checkUser, true))
                {
                    txtUsuario.IsEnabled = false;
                    passContrasena.IsEnabled = true;
                    passContrasena.Focus();
                }

            }

        }
        private void passContrasena_KeyUp(object sender, KeyEventArgs e)
        {
            btnLogin.IsEnabled = false;
            passContrasena.Background = Brushes.White;

            {
                if (e.Key == Key.Enter)
                {
                    if (ComprobarEntrada(passContrasena.Password, usuarios, passContrasena, checkPassword, false))
                    {
                        btnLogin.IsEnabled = true;
                        passContrasena.IsEnabled = false;
                        btnLogin.Focus();
                    }
                }

            }
        }
        private Boolean ComprobarEntrada(string valorIntroducido, List<Usuario> usuarios,
           Control componenteEntrada, Image imagenFeedBack, Boolean dato)
        {
            Boolean valido = false;
            String valorReal;
            Usuario aux;

            foreach (Usuario usuario in usuarios)
            {
                if (dato)
                {
                    valorReal = usuario.username;
                }
                else
                {
                    if (usuario.username.Equals(txtUsuario.Text))
                    {
                        aux = usuario;
                        valorReal = aux.contrasena;
                    }
                    else { valorReal = ""; }
                }
                if (valorReal == valorIntroducido)
                {
                    // borde y background en verde
                    componenteEntrada.BorderBrush = Brushes.Green;
                    componenteEntrada.Background = Brushes.LightGreen;
                    // imagen al lado de la entrada de usuario --> check
                    imagenFeedBack.Source = imagCheck;
                    imagenFeedBack.ToolTip = "Credencial Correcta";
                    valido = true;
                    break;
                }
                else
                {
                    // marcamos borde en rojo
                    componenteEntrada.BorderBrush = Brushes.Red;
                    componenteEntrada.Background = Brushes.IndianRed;
                    // imagen al lado de la entrada de usuario --> cross
                    imagenFeedBack.Source = imagCross;  


                    imagenFeedBack.ToolTip = "Credencial No encontrada";
                    valido = false;
                }
            }
            return valido;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            menu = new MenuWindow(txtUsuario.Text);
            menu.Show();
            this.Close();

        }

        private void btnLogin_Copy_Click(object sender, RoutedEventArgs e)
        {
            ur = new UserRegistration();
            ur.Show();
            this.Close();
        }

        private void txtUsuario_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void txtUsuario_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUsuario.Text = string.Empty;
        }

        private void passContrasena_GotFocus(object sender, RoutedEventArgs e)
        {
            passContrasena.Password = string.Empty;
        }
    }
}
