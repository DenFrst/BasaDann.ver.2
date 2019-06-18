using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasaDann.Utils;

namespace BasaDann.Classes
{
    public class Phone
    {
 
        public Phone()
        {

        }
        public Phone(string status)
        {
            this.Status = status;
        }

        public string Status { get; set; }

        //public string Name  { get; set; }

       // public string Price { get; set; }


    }
}
