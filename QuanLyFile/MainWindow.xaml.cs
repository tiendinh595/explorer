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
using System.IO;

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                foreach (string str in Directory.GetLogicalDrives())
                {
                    StackPanel stp = new StackPanel();
                    stp.Orientation = Orientation.Horizontal;

                    TreeViewItem item = new TreeViewItem();
                    TextBlock text = new TextBlock();
                    text.Text = str;
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri("Img/Drive.png", UriKind.Relative));
                    img.Width = 15;
                    img.Margin = new Thickness(0, 0, 10, 0);

                    stp.Children.Add(img);
                    stp.Children.Add(text);

                    item.Header = stp;
                    item.Items.Add("Loading...");

                    item.Expanded += new RoutedEventHandler(item_Expanded);
                    item.Tag = str;

                    lst_listFile.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void item_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            e.Handled = true;
            if (item.IsExpanded)
                LoadContent(item.Tag.ToString(), "Folder", item);
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

        private string[] LoadSubDir(string dirName)
        {
            try
            {
                return Directory.GetDirectories(dirName);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
        }

        private string[] LoadSubFiles(string dirName)
        {
            try
            {
                return Directory.GetFiles(dirName);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
        }


        private void LoadContent(string dirName, string type, TreeViewItem treiTmp)
        {
            string[] list;
            string tag;
            if (type == "File")
                list = LoadSubFiles(dirName);
            else
                list = LoadSubDir(dirName);
            string[] arrExt = { ".png", ".jpg", ".jpeg" };
            if (list != null)
            {
                treiTmp.Items.Clear();
                foreach (string fileName in list)
                {
                    StackPanel stp = new StackPanel();
                    stp.Orientation = Orientation.Horizontal;

                    Image img = new Image();
                    stp.Children.Add(img);

                    TextBlock text = new TextBlock();
                    if (type == "File")
                    {
                        FileInfo fi = new FileInfo(fileName);
                        text.Text = fi.Name;
                        tag = fi.FullName;
                        if (Array.IndexOf(arrExt, fi.Extension.ToLower()) >= 0)
                        {
                            img.Source = new BitmapImage(new Uri(fi.FullName, UriKind.Absolute));
                            img.Width = img.Height = 24;
                        }
                        else
                        {
                            img.Source = new BitmapImage(new Uri("Img/" + type + ".png", UriKind.Relative));
                            img.Width = img.Height = 16;
                        }
                    }
                    else
                    {
                        DirectoryInfo fo = new DirectoryInfo(fileName);
                        text.Text = fo.Name;
                        tag = fo.FullName;
                        img.Source = new BitmapImage(new Uri("Img/" + type + ".png", UriKind.Relative));
                        img.Width = img.Height = 16;
                    }

                    text.Margin = new Thickness(10, 0, 0, 0);

                    stp.Children.Add(text);

                    TreeViewItem trei = new TreeViewItem();
                    //trei.Expanded += new RoutedEventHandler(item_Expanded);
                    trei.Expanded += item_Expanded;
                    trei.Tag = tag;
                    trei.Header = stp;
                    trei.Items.Add("Loading...");
                    treiTmp.Items.Add(trei);
                    

                }
            }
        }

    }
}
