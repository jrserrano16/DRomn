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

namespace DRomn
{
    /// <summary>
    /// Lógica de interacción para ImageMenuWindow.xaml
    /// </summary>
    public partial class ImageMenuWindow : Window
    {
        private MenuWindow mw;
        private ImageWindow iw;
        private string user;
        public ImageMenuWindow(string user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
             mw = new MenuWindow(user);
            mw.Show();
            this.Close();
        }

        private void btn_Modelos3D_Click(object sender, RoutedEventArgs e)
        {
            iw = new ImageWindow(user,"3D");
            iw.Show();
            this.Close();
        }

        private void btn_Ortomosaicos_Click(object sender, RoutedEventArgs e)
        {
            iw = new ImageWindow(user, "Ortomosaica");
            iw.Show();
            this.Close();
        }

        private void btn_Elevacion_Click(object sender, RoutedEventArgs e)
        {
            iw = new ImageWindow(user, "Elevacion");
            iw.Show();
            this.Close();
        }

        private void btn_Paisajes_Click(object sender, RoutedEventArgs e)
        {
            iw = new ImageWindow(user, "Paisaje");
            iw.Show();
            this.Close();
        }

        private void btn_Cultivos_Click(object sender, RoutedEventArgs e)
        {
            iw = new ImageWindow(user, "Cultivos");
            iw.Show();
            this.Close();
        }

        private void btn_Otros_Click(object sender, RoutedEventArgs e)
        {
            iw = new ImageWindow(user, "Otras");
            iw.Show();
            this.Close();
        }
    }
}
