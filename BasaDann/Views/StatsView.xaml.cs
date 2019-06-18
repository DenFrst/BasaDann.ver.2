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
using BasaDann.ViewModels;

namespace BasaDann.Views
{
    /// <summary>
    /// Логика взаимодействия для StatsView.xaml
    /// </summary>
    public partial class StatsView : UserControl
    {
        public StatsView()
        {
            InitializeComponent();
            this.DataContext = new StatsViewModel();
        }
    }
}
