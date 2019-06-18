using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using BasaDann.Utils;
using BasaDann.ViewModels;

namespace BasaDann.Classes
{
    public class TryMenu
    {
        MainWindowViewModel mv=new MainWindowViewModel();

        private readonly ICommand _command;

        public TryMenu()
        {
            _command = new CommandViewModel(Execute);
        }

        //private string _header;
        public string Header { get; set; }
        public string Url { get; set; }
        public Uri PathUrl { get; set; }

        public ObservableCollection<TryMenu> MenuItems { get; set; }

        public ICommand Command
        {
            get
            {
                return _command;
            }
        }


        
        private void Execute()
        {

            PathUrl = new Uri("https://helpdesk.soft.aero/uploads/" + Url);
           
            Window nmw = new Window();
            nmw.Show();
            nmw.Width = 500;
            nmw.Height = 500;
            WebBrowser nwb = new WebBrowser();
            nmw.Title = Header;
            nwb.Source = PathUrl;
            nmw.Content = nwb;
            // nmw.NavigationService("https://helpdesk.soft.aero/uploads/" + Url);


            //   "/var/www/vsdesk/uploads/5b8018c29ad25.jpg"


        }
    }
    public class CommandViewModel : ICommand
    {
        private readonly Action _action;

        public CommandViewModel(Action action)
        {
            _action = action;
        }

        public void Execute(object o)
        {
            _action();
        }

        public bool CanExecute(object o)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }
    }
}
