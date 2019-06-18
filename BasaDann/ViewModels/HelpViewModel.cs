using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasaDann.Utils;

namespace BasaDann.ViewModels
{
    public class HelpViewModel : BaseShell
    {
        
        private string curr_im;
        public string Curr_Im
        {
            get { return curr_im; }
            set
            {
                curr_im = value;
                OnPropertyChanged(nameof(Curr_Im));
            }
        }
        
        private RelayCommand _image;
        public RelayCommand Image
        {
            get
            {
                return _image = _image ??
                                   new RelayCommand(ImageChange);
            }
        }
        private int i = 2;
        private void ImageChange()
        {
            Curr_Im = "../images/help_" + i + ".png";
                i++;
                if (i > 3)
                {
                    i = 1;
                }
            
        }

        public HelpViewModel()
        {
            Curr_Im = "../images/help_1.png";
        }
    }
}
