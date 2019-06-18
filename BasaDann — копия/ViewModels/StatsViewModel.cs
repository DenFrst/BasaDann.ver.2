using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BasaDann.Utils;
using BasaDann.Classes;
using GalaSoft.MvvmLight.Messaging;

namespace BasaDann.ViewModels
{
    public class StatsViewModel : BaseShell
    {
        public StatsViewModel()
        {

        }

        private Stats _selectedPersone;
        public Stats SelectedPersone
        {
            get { return _selectedPersone; }
            set
            {
                _selectedPersone = value;
                OnPropertyChanged(nameof(SelectedPersone));
            }
        }

        private ObservableCollection<Stats> _stats { get; set; }
        public ObservableCollection<Stats> StatsCollection
        {
            get { return _stats; }
            set
            {
                _stats = value;
                OnPropertyChanged(nameof(StatsCollection));
            }
        }

        private DataTable _dts;
        public DataTable DTS
        {
            get { return _dts; }
            set
            {
                _dts = value;
                OnPropertyChanged(nameof(DTS));
            }
        }

        private RelayCommand _stattry;
        public RelayCommand StatTry
        {
            get
            {
                return _stattry = _stattry ??
                                  new RelayCommand(Stats);
            }
        }

        private void Stats()
        {
            //Thread mth = new Thread(new ThreadStart(Get_Stats));
            // mth.Start();
        }

        private void Get_Stats(object parameter)
        {
            string a =
                "SELECT null as status, count(*) as count, TIME_TO_SEC(lead_time) as time FROM request WHERE Managers_id = '" +
                parameter +
                "' AND pid = 0 UNION ALL SELECT Status,count(Status), TIME_TO_SEC(lead_time) as time FROM request WHERE Managers_id = '" +
                parameter + "' and pid = 0 and Status is not null group by Status";
            DTS = clsDB.Get_DataTable(a);

            if (DTS.Rows.Count > 1)
            {
                StatsCollection = new ObservableCollection<Stats>
                {
                    new Stats
                    {
                        z_All = Convert.ToInt32(DTS.Rows[0][1]),
                        z_Closed = Convert.ToInt32(DTS.AsEnumerable()
                            .SingleOrDefault(r => r.Field<string>("status") == "Завершена")["count"]),
                        z_Time_Sp = Convert.ToInt32(DTS.AsEnumerable()
                                        .SingleOrDefault(w => w.Field<string>("status") == "Завершена")["time"]) / 60,


                    }
                };
            }
            else
            {
                MessageBox.Show("StatsViewModel");
            }
        }


        private ICommand _try2;
        public ICommand Try2
        {
            get
            {
                if (_try2 == null)
                { _try2 = new RelayCommand<object>(this.MyCommand_Execute); }
                return _try2;
            }
        }


        private void MyCommand_Execute(object parameter)
        {
            try
            {
                // MessageBox.Show(String.Format("Parameter: {0}", parameter));
                Get_Stats(parameter);
            }
            catch (Exception)
            {
                MessageBox.Show("StatsViewModel2");
            }
        }


    }
}
