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
    /// Lógica de interacción para MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        MainWindow mw = new MainWindow();
        ImageMenuWindow imw;
        DroneWindow dw;
        private string user;
        public MenuWindow(string user)
        {
            this.user = user;
            InitializeComponent();
            lbl_Usuario.Content = user;
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            mw.Show();
        }

        private void btn_Imagen_Click(object sender, RoutedEventArgs e)
        {
            imw = new ImageMenuWindow(user);
            this.Close();
            imw.Show();
        }

        private void btn_Dron_Click(object sender, RoutedEventArgs e)
        {
            dw = new DroneWindow(user);

            this.Close();
            dw.Show();
        }
    }
}
