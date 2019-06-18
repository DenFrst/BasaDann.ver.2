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
    public class TicketViewModel : BaseShell
    {
        //private Ticket _ticket;

        private List<string> statuses { get; set; }

        public List<string> Get_Status
        {
            get
            {
                return statuses = clsDB.Execute_Reader_SQL();
               
            }


        }
    }
}
