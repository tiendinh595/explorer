using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void mn_address_Click(object sender, RoutedEventArgs e)
        {
            if (mn_address.IsChecked)
                grd_address.Visibility = System.Windows.Visibility.Visible;
            else
                grd_address.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void mn_standar_Click(object sender, RoutedEventArgs e)
        {
            if (mn_standar.IsChecked)
                toolbar.Visibility = System.Windows.Visibility.Visible;
            else
                toolbar.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
