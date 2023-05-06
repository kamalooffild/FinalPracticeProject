using PracticeProject.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace PracticeProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServiceListPage.xaml
    /// </summary>
    public partial class ServiceListPage : Page
    {
        public ServiceListPage()
        {
            InitializeComponent();
            LVService.ItemsSource = App.db.Service.Where(x => x.isDelete == false).ToList();
            
        }
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selService = (sender as Button).DataContext as Service;
            NavigationService.Navigate(new AddEditServiceList(selService));
            
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var selService = (sender as Button).DataContext as Service;
            if (MessageBox.Show("Данная запись будет удалена. Вы уверены?",
                "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                selService.isDelete = true;
                App.db.SaveChanges();
            }
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            //Byte фото в двоичный
            //foreach (var item in App.db.Service.ToArray().Where(x => !string.IsNullOrWhiteSpace(x.MainImagePath)))
            //{
            //    var fullpath = item.MainImagePath;
            //    var byteimage = File.ReadAllBytes(fullpath);
            //    item.ImageByte = byteimage;
            //}
            NavigationService.Navigate(new AddEditServiceList(new Service()));
        }
        public void Refresh()
        {
            IEnumerable<Service> filterService = App.db.Service;
            if (SortCb.SelectedIndex > 0)
            {
                if (SortCb.SelectedIndex == 1)
                    filterService = filterService.OrderBy(x => x.CostDiscount);
                else if (SortCb.SelectedIndex == 2)
                    filterService = filterService.OrderByDescending(x => x.CostDiscount);
            }
            if (DiscountSortCb.SelectedIndex > 0)
            {
                if (DiscountSortCb.SelectedIndex == 1)
                    filterService = filterService.Where(x => x.Discount >= 0 && x.Discount < 0.05 || x.Discount == null).ToList();
                else if (DiscountSortCb.SelectedIndex == 2)
                    filterService = filterService.Where(x => x.Discount >= 0.05 && x.Discount < 0.15).ToList();
                else if (DiscountSortCb.SelectedIndex == 3)
                    filterService = filterService.Where(x => x.Discount >= 0.15 && x.Discount < 0.3).ToList();
                else if (DiscountSortCb.SelectedIndex == 4)
                    filterService = filterService.Where(x => x.Discount >= 0.3 && x.Discount < 0.7).ToList();
                else if (DiscountSortCb.SelectedIndex == 5)
                    filterService = filterService.Where(x => x.Discount >= 0.7 && x.Discount < 1).ToList();
            }
            if (TitleDiscriptionTb.Text.Length > 0)
            {
                filterService = filterService.Where(x => x.Title.ToLower().StartsWith(TitleDiscriptionTb.Text.ToLower()) || x.Description.ToLower().StartsWith(TitleDiscriptionTb.Text.ToLower()));
            }
            LVService.ItemsSource = filterService.ToList();
            FilterCount.Text = filterService.Count() + " из";
            GeneralCount.Text = App.db.Service.Count().ToString();
        }
        private void TitleDiscriptionTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }
        private void DiscountSortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }
        private void SortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void RecordBtn_Click(object sender, RoutedEventArgs e)
        {
            //var serService =  (sender as Button).DataContext as Service;
            //NavigationService.Navigate(new RecordingPage(serService));
            var addClientService = (sender as Button).DataContext as Service;
            if (addClientService == null) return;
            NavigationService.Navigate(new RecordingPage(addClientService));

        }

        private void EntriesBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NextEntriesPage());
        }
    }
}
