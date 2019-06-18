using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasaDann.Classes;
using BasaDann.Utils;

namespace BasaDann.ViewModels
{

    public class ShellViewModel : BaseShell
    {
        private string _activeItem;
        public string ActiveItem
        {
            get { return _activeItem; }
            set
            {
                _activeItem = value;
                OnPropertyChanged(nameof(ActiveItem));
            }
        }

        private readonly string[] glyphStrings = new string[] { "&#xe908;", "&#xe905;", "&#xe13d;" };

        public ObservableCollection<NavigationItemModel> Items { get; set; }


        public ShellViewModel()
        {
            this.Items = new ObservableCollection<NavigationItemModel>();


            for (int i = 1; i <= 3; i++)
            {
                var glyphString = this.glyphStrings[i - 1];
                this.Items.Add(new NavigationItemModel() { Title = "Item " + i, IconGlyph = glyphString });
            }

            
        }
    }


}
