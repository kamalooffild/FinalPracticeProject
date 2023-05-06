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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PracticeProject.Pages;
using PracticeProject.Model;
using System.IO;

namespace PracticeProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach (var item in App.db.Service.ToArray().Where(x => !string.IsNullOrWhiteSpace(x.MainImagePath)))
            {
                var fullpath = item.MainImagePath;
                var byteimage = File.ReadAllBytes(fullpath);
                item.ImageByte = byteimage;
            }


            MainFrame.Navigate(new ServiceListPage());
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            { MainFrame.GoBack(); }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
