using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using BasaDann.Utils;

namespace BasaDann.Classes
{
    public class MenuItems : BaseShell
    {

        private string _name1;
        public string Name1
        {
            get { return _name1; }
            set
            {
                _name1 = value;
                OnPropertyChanged(nameof(Name1));
            }
        }

        private string _url;
        public string Url
        {
            get { return _url; }
            set
            {
                _url = value;
                OnPropertyChanged(nameof(Url));
            }
        }

        

    }
}
