using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasaDann.Utils;

namespace BasaDann.Classes
{
    public class NamesLogins : BaseShell
    {
        private string managername;

        public string ManagerName
        {
            get { return managername; }
            set
            {
                managername = value;
                OnPropertyChanged(nameof(ManagerName));
            }
        }

        private string managerlogin;

        public string ManagerLogin
        {
            get { return managerlogin; }
            set
            {
                managerlogin = value;
                OnPropertyChanged(nameof(ManagerLogin));
            }
        }

        public NamesLogins()
        {
           
        }

        //  } //  SELECT Username, fullname FROM vsdesk.CUsers where role ='vsdeskmanager' and company = 'ITAvia' 

        /////////// SELECT * FROM vsdesk.CUsers where role ='vsdeskmanager' and company = 'ITAvia'

    }
}
