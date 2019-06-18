using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BasaDann.Utils;
using BasaDann.Classes;
using GalaSoft.MvvmLight.Messaging;
using Telerik.Windows.Controls.Data.PropertyGrid;

namespace BasaDann.ViewModels
{
    public class StatsViewModel : BaseShell
    {


        private string _selectedManager;
        public string SelectedManager
        {
            get { return _selectedManager; }
            set
            {
                _selectedManager = value;
                OnPropertyChanged(nameof(SelectedManager));
            }
        }

        private ObservableCollection<NamesLogins> _managers { get; set; }
        public ObservableCollection<NamesLogins> Managers
        {
            get { return _managers; }
            set
            {
                _managers = value;
                OnPropertyChanged(nameof(Managers));
            }
        }

        private DataTable _managerslist;
        public DataTable ManagersList
        {
            get { return _managerslist; }
            set
            {
                _managerslist = value;
                OnPropertyChanged(nameof(ManagersList));
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

        private DateTime _date_Value = DateTime.Now;
        public DateTime Date_Value
        {
            get { return _date_Value; }
            set
            {
                _date_Value = value;
                OnPropertyChanged(nameof(Date_Value));
            }
        }

        private string _selectedCompany;
        public string SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                _selectedCompany = value;
                OnPropertyChanged(nameof(SelectedCompany));
            }
        }

        private DataTable _companiewBox;
        public DataTable CompaniesBox
        {
            get { return _companiewBox; }
            set
            {
                _companiewBox = value;
                OnPropertyChanged(nameof(CompaniesBox));
            }
        }

        private void Get_Stats()
        {

            string a =

                "SELECT 'Всего заявок' as 'Статус', count(*) as 'Количество' FROM request WHERE Managers_id = '" +
                SelectedManager + "' AND pid = 0 UNION ALL SELECT Status, count(Status) FROM request WHERE Managers_id = '" +
                SelectedManager +
                "' and pid = 0 and Status is not null and Date like '%." +
                Date_Value.ToString("MM.yyyy") + "%' group by Status UNION ALL SELECT 'Заявок за месяц',count(*) as count FROM request WHERE Managers_id = '" +
                SelectedManager +
                "' and pid = 0 and Date like '%." +
                Date_Value.ToString("MM.yyyy") + "%' UNION ALL SELECT 'Среднее время (ч.)', TRUNCATE((TIME_TO_SEC(lead_time) / count(*) / 60), 2) FROM request WHERE Managers_id = '" +
                SelectedManager + "' AND pid = 0 and Date like '%." +
                Date_Value.ToString("MM.yyyy") + "%' ";




            //'%." + Date_Value.ToString("MM.yyyy")+ "%'

            DTS = clsDB.Get_DataTable(a);
            clsDB.Close_DB_Connection();
            long totalMemory = GC.GetTotalMemory(false);

            GC.Collect();
            GC.Collect(1, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();

        }

        private RelayCommand _monthStat;
        public RelayCommand MonthStat
        {
            get
            {
                return _monthStat = _monthStat ??
                                  new RelayCommand(Get_monthStat);
            }
        }

        private void Get_monthStat()
        {
            string req =
                "SELECT distinct status as 'Статус', company as 'Компания', count(status) as 'Кол-во заявок' FROM request WHERE  pid=0  GROUP BY company ORDER BY count(status) DESC";


            DTS = clsDB.Get_DataTable(req);
            //MessageBox.Show(Date_Value.ToString());
        }

        private RelayCommand _getXLS;
        public RelayCommand GetXLS
        {
            get
            {
                return _getXLS = _getXLS ??
                                    new RelayCommand(GetXLSfile);
            }
        }

        private void GetXLSfile()
        {
            Microsoft.Office.Interop.Excel.Application excel = null;
            Microsoft.Office.Interop.Excel.Workbook wb = null;

            object missing = Type.Missing;
            Microsoft.Office.Interop.Excel.Worksheet ws = null;
            Microsoft.Office.Interop.Excel.Range rng = null;

            try
            {
                excel = new Microsoft.Office.Interop.Excel.Application();
                wb = excel.Workbooks.Add();
                ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;

                for (int Idx = 0; Idx < DTS.Columns.Count; Idx++)
                {
                    ws.Range["A1"].Offset[0, Idx].Value = DTS.Columns[Idx].ColumnName;
                }

                for (int Idx = 0; Idx < DTS.Rows.Count; Idx++)
                {
                    ws.Range["A2"].Offset[Idx].Resize[1, DTS.Columns.Count].Value =
                        DTS.Rows[Idx].ItemArray;
                }

                excel.Visible = true;
                wb.Activate();
            }
            catch (COMException ex)
            {
                MessageBox.Show("Error accessing Excel: " + ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }


        private RelayCommand _try2;
        public RelayCommand Try2
        {
            get
            {
                return _try2 = _try2 ??
                                 new RelayCommand(MyCommand_Execute);
            }
        }

        private void MyCommand_Execute()
        {
            try
            {
                // MessageBox.Show(String.Format("Parameter: {0}", parameter));
                Get_Stats();
            }
            catch (Exception)
            {
                MessageBox.Show("StatsViewModel2");
            }
        }

        private RelayCommand _statCompany;
        public RelayCommand StatCompany
        {
            get
            {
                return _statCompany = _statCompany ??
                                    new RelayCommand(Get_StatCompany);
            }
        }

        private void Get_StatCompany()
        {

            string req = "select company as 'Статус', count(*) as 'Количество' from request where company = '" +
                         SelectedCompany + "' and pid = 0  and Date like '%." +
                         Date_Value.ToString("MM.yyyy") + "%' UNION ALL select Status, count(Status) from request where company = '" +
                         SelectedCompany + "' and pid = 0 and Date like '%." +
                         Date_Value.ToString("MM.yyyy") + "%'";
            DTS = clsDB.Get_DataTable(req);

        }

        private RelayCommand _getdopstat;
        public RelayCommand GetDopStat
        {
            get
            {
                return _getdopstat = _getdopstat ??
                                      new RelayCommand(Get_DopStat);
            }
        }

        private void Get_DopStat()
        {
            
            string req =
                "SELECT distinct count(*) as 'Количество', Substring(action, instr(action,'>') + 1, instr( action, '</') - instr(action,'>')+1-2) as 'Действие' FROM vsdesk.history where cusers_id = 'system' and action like '%статус%' and datetime like '%" +
                Date_Value.ToString("MM.yyyy") + "%' group by action";

            DTS = clsDB.Get_DataTable(req);
            clsDB.Close_DB_Connection();
            long totalMemory = GC.GetTotalMemory(false);

            GC.Collect();
            GC.Collect(1, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
        }

        //MAIN
        public StatsViewModel()
        {

            string companies = "SELECT name FROM companies";
            CompaniesBox = clsDB.Get_DataTable(companies);


            string req =
                "SELECT Username, fullname FROM vsdesk.CUsers where role ='vsdeskmanager' and company = 'ITAvia' ";
            ManagersList = clsDB.Get_DataTable(req);
            Managers = new ObservableCollection<NamesLogins>();

            foreach (DataRow row in ManagersList.Rows)
            {
                Managers.Add(new NamesLogins
                {
                    ManagerName = row["fullname"].ToString(),
                    ManagerLogin = row["Username"].ToString()
                });

            };
            clsDB.Close_DB_Connection();
            long totalMemory = GC.GetTotalMemory(false);

            GC.Collect();
            GC.Collect(1, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
        }

        private RelayCommand _statManagers;
        public RelayCommand StatManagers
        {
            get
            {
                return _statManagers = _statManagers ??
                                      new RelayCommand(Get_StatManagers);
            }
        }

        private void Get_StatManagers()
        {
            
            string req = "SELECT mfullname as 'Сотрудник', 'Всего заявок' as status, count(*) as count FROM request WHERE pid = 0 and managers_id is not null group by Managers_id " +
            "UNION ALL SELECT mfullname as 'Сотрудник', Status, count(Status) FROM request WHERE pid = 0 and managers_id is not null and Status is not null and Date like '%." + 
            Date_Value.ToString("MM.yyyy") + "%' group by Status " +
            "UNION ALL SELECT mfullname as 'Сотрудник', 'Заявок за месяц',count(*) as count FROM request WHERE pid = 0 and managers_id is not null and Date like '%." + 
            Date_Value.ToString("MM.yyyy") + "%' group by Managers_id " +
            "UNION ALL SELECT mfullname as 'Сотрудник', 'Среднее время (ч.)', TRUNCATE((TIME_TO_SEC(lead_time) / count(*) / 60), 2) FROM request WHERE pid = 0 and managers_id is not null and Date like '%." + 
            Date_Value.ToString("MM.yyyy") + "%' group by Managers_id";

            DTS = clsDB.Get_DataTable(req);
            clsDB.Close_DB_Connection();
            long totalMemory = GC.GetTotalMemory(false);

            GC.Collect();
            GC.Collect(1, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();

        }

        private RelayCommand _get_zpt;
        public RelayCommand Get_ZPT
        {
            get
            {
                return _get_zpt = _get_zpt ??
                                       new RelayCommand(Get_Stat_ZPT);
            }
        }

        private void Get_Stat_ZPT()
        {
            
            string req =
                "select id,name,status,date,managers_id,company from request where status like '%Приостановлена%' or status like '%запланиров%' or status like '%требует%' and pid = 0 order by status";
            DTS = clsDB.Get_DataTable(req);
            clsDB.Close_DB_Connection();
            long totalMemory = GC.GetTotalMemory(false);

            GC.Collect();
            GC.Collect(1, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
        }

        private RelayCommand _get_ptr;
        public RelayCommand Get_PTR
        {
            get
            {
                return _get_ptr = _get_ptr ??
                                  new RelayCommand(Get_Stat_PTR);
            }
        }

        private void Get_Stat_PTR()
        {
           
                string req = "select id,name,status,date,managers_id,company from request where status like '%передан%' and pid = 0 order by status";
                DTS = clsDB.Get_DataTable(req);
                clsDB.Close_DB_Connection();

                long totalMemory = GC.GetTotalMemory(false);

                GC.Collect();
                GC.Collect(1, GCCollectionMode.Forced);
                GC.WaitForPendingFinalizers();

        }

    }
}

//
