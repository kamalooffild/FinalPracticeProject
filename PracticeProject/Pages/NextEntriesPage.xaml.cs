using PracticeProject.Model;
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
using System.Windows.Threading;

namespace PracticeProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для NextEntriesPage.xaml
    /// </summary>
    public partial class NextEntriesPage : Page
    {
        public NextEntriesPage()
        {
            InitializeComponent();
            Refresh();

        }

        private void Refresh()
        {
            DateTime week = DateTime.Today.AddDays(7).AddHours(DateTime.Now.Hour);
            IEnumerable<ClientService> buffer = App.db.ClientService.Where(
                x => x.StartTime >= DateTime.Now
                && x.StartTime <= week
                ).ToList().OrderBy(x => x.StartTime);

            if (buffer.Count() == 0)
                LVEntries.Visibility = Visibility.Hidden;
            else
                LVEntries.Visibility = Visibility.Visible;

            LVEntries.ItemsSource = buffer.ToList();
        }

        private void TimerRefresh_Tick(object sender, EventArgs e)
        {
            Refresh();        
        }
    }
}
