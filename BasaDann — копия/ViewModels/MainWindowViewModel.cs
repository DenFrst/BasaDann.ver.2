using BasaDann.Classes;
using BasaDann.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using BasaDann.Views;

namespace BasaDann.ViewModels
{
    public class MainWindowViewModel : BaseShell
    {

        // Properties
        private DataTable _dt;

        public DataTable DT
        {
            get { return _dt; }
            set
            {
                _dt = value;
                OnPropertyChanged("DT");
            }
        }

        private DataTable _dt_all;

        public DataTable DT_ALL
        {
            get { return _dt_all; }
            set
            {
                _dt_all = value;
                OnPropertyChanged(nameof(DT_ALL));
            }
        }


        private ObservableCollection<Ticket> _tickets { get; set; }

        public ObservableCollection<Ticket> Tickets
        {
            get { return _tickets; }
            set
            {
                _tickets = value;
                OnPropertyChanged(nameof(Tickets));
            }
        }

        private Ticket _tick;

        public Ticket Tick
        {
            get { return _tick; }
            set
            {
                _tick = value;
                OnPropertyChanged(nameof(Tick));
            }
        }

        private int _idM;

        public int IDM
        {
            get { return _idM; }
            set
            {
                _idM = value;
                OnPropertyChanged(nameof(IDM));
            }
        }

        private ObservableCollection<TryMenu> _menuItems;

        public ObservableCollection<TryMenu> MenuItems
        {
            get { return _menuItems; }
            set
            {
                _menuItems = value;
                OnPropertyChanged(nameof(MenuItems));
            }
        }

        private string _src_brw;

        public string Src_Brow
        {
            get { return _src_brw; }
            set
            {
                if (_src_brw != value)
                {
                    _src_brw = value;
                    OnPropertyChanged(nameof(Src_Brow));
                }
            }
        }

        private DataRow _ssr;

        public DataRow SSR
        {
            get { return _ssr; }
            set
            {
                if (_ssr != value)
                {
                    _ssr = value;
                    OnPropertyChanged(nameof(SSR));
                }
            }
        }

        private int _tabindex;

        public int TabIndex
        {
            get { return _tabindex; }
            set
            {
                if (_tabindex != value)
                {
                    _tabindex = value;
                    OnPropertyChanged(nameof(TabIndex));
                }
            }
        }


        // Commands
        private RelayCommand _btnClick;

        public RelayCommand btnClick
        {
            get
            {
                return _btnClick = _btnClick ??
                                   new RelayCommand(But_Click);
            }

        }


        private void But_Click()
        {
            //Get_Fields();
            Thread mth = new Thread(new ThreadStart(Get_Fields));
            mth.Start();
        }

        private string request_fields;
        private bool _check_files;

        private void Get_Fields()
        {

            if (clsDB.Scalar_SQL(Requests.Req_Count_Single, IDM) > 0)
            {

                if (clsDB.Scalar_SQL(Requests.Req_Count_Files, IDM) > 0) // есть ли файлы 
                {
                    request_fields = Requests.Req_Files + IDM; // есть
                    _check_files = true;
                }
                else
                {
                    request_fields = Requests.Req_Single + IDM; // нет
                    _check_files = false;
                }

                DT = clsDB.Get_DataTable(request_fields);
                Tickets = new ObservableCollection<Ticket>
                {
                    new Ticket
                    {
                        ID = IDM,
                        Name = DT.Rows[0]["Name"].ToString(),
                        ZayavCategory_id = DT.Rows[0]["ZayavCategory_id"].ToString(),
                        Date = DT.Rows[0]["Date"].ToString(),
                        Status = DT.Rows[0]["Status"].ToString(),
                        Priority = DT.Rows[0]["Priority"].ToString(),
                        FullName = DT.Rows[0]["company"].ToString(),
                        MfullName = DT.Rows[0]["mfullname"].ToString(),

                        //Content = Get_Cont(DT.Rows[0]["Content"].ToString())
                    },
                };

                if (DT.Rows[0] != null)
                {
                    Src_Brow = DT.Rows[0]["Content"] +
                               "<meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>";
                }
                else
                {
                    MessageBox.Show("Что-то пошло не так");
                }

                // Menu
                if (_check_files == true)
                {
                    {
                        MenuItems = new ObservableCollection<TryMenu>();
                        foreach (DataRow row in DT.Rows)
                        {
                            if (row["Name1"] != null)
                            {
                                MenuItems.Add(
                                    new TryMenu
                                    {
                                        Header = row["Name1"].ToString(),
                                        Url = row["file_name"].ToString()
                                    }
                                );
                            }
                            else break;
                        }
                    }
                }
                // Menu

                clsDB.Close_DB_Connection();
            }
            else
            {
                MessageBox.Show("Заявка не найдена");
            }
        }


        private RelayCommand _newTicket;

        public RelayCommand NewTicket
        {
            get
            {
                return _newTicket = _newTicket ??
                                    new RelayCommand(Create_New_Ticket);
            }

        }

        private void Create_New_Ticket()
        {
            System.Diagnostics.Process.Start("https://helpdesk.soft.aero/request/create");
        }

        private RelayCommand _alltickets;

        public RelayCommand AllTickets
        {
            get
            {
                return _alltickets = _alltickets ??
                                     new RelayCommand(Get_All_Tickets);
            }
        }


        private void Get_All_Tickets()
        {
            Thread mth1 = new Thread(new ThreadStart(GetAllTick));
            mth1.Start();

        }

        private void GetAllTick()
        {
            // busy Indicator
            string req = "SELECT * FROM request ORDER BY id DESC";
            DT_ALL = clsDB.Get_DataTable(req);
            clsDB.Close_DB_Connection();
        }

        private RelayCommand _selected_from_All;

        public RelayCommand Selected_From_All
        {
            get
            {
                return _selected_from_All = _selected_from_All ??
                                            new RelayCommand(Get_Selected_From_All);
            }
        }


        private void Get_Selected_From_All() // DblClick
        {
            if (clsDB.Scalar_SQL(Requests.Req_Count_Files, IDM) > 0) //Tuuuuut
            {
                TabIndex = 0;
                IDM = Convert.ToInt32(SSR["id"].ToString());
                Get_Fields();
                //Get_Menu(SSR); --как сделать images из всех
                clsDB.Close_DB_Connection();
            }
        }


        private RelayCommand _opsiti;

        public RelayCommand Opsiti
        {
            get
            {
                return _opsiti = _opsiti ??
                                 new RelayCommand(open_single_ticket);
            }
        }

        private void open_single_ticket()
        {
            if (clsDB.Scalar_SQL(Requests.Req_Single, IDM) > 0) //tuuuuuuuuuuut
            {
                string pathreq = "https://helpdesk.soft.aero/request/" + IDM;
                System.Diagnostics.Process.Start(pathreq);
            }
        }

        // Main
        public MainWindowViewModel()
        {


            #region MyRegion

            /*
             
             // string cn_String = Properties.Settings.Default.connection_String;
             //  SqlConnection cn = new SqlConnection(cn_String);
 
             //   Phoness = new ObservableCollection<Phone>
             //   {
             //      new Phone{ID = 10,Name = "Nameraz",Price = 300,Title = "asd"},
             //       new Phone{ID = 11,Name = "Namera1z",Price = 3100,Title = "a1sd"}
             //   };
 
             //  string sql = "SELECT TOP 1 * FROM tbl_Details";
             //  
             //  db_Update_Add_Record("qwe","qew");
 
 
 
             // Команда Insert.
             //  string sql = "Insert into request (channel, channel_icon, Name, ZayavCategory_id, Date, ) "
             //              + " values (Manual, iicon-pencil-line, @name_p, @zayav_p, @date_p,) ";
 
             // Создать объект Parameter.
             SqlParameter gradeParam = new SqlParameter("@grade", SqlDbType.Int);
             gradeParam.Value = 3;
             cmd.Parameters.Add(gradeParam);
 
             // Добавить параметр @highSalary (Написать короче).
             SqlParameter highSalaryParam = cmd.Parameters.Add("@highSalary", SqlDbType.Float);
             highSalaryParam.Value = 20000;
 
             // Добавить параметр @lowSalary (Написать короче).
             cmd.Parameters.Add("@lowSalary", SqlDbType.Float).Value = 10000;
 
             // Выполнить Command (Используется для delete, insert, update).
             int rowCount = cmd.ExecuteNonQuery(); 
             
              */

            #endregion

        }

        private int a;

        public int A
        {
            get { return a; }
        }

        public void Values4Not()
        {
            a = _tabindex;


        }

        private RelayCommand _statistic;
        public RelayCommand Statistic
        {
            get
            {
                return _statistic = _statistic ??
                                 new RelayCommand(Statisticqq);
            }
        }

        private void Statisticqq()
        {
            StatsView nmw = new StatsView();
           
            StatsViewModel contex = new StatsViewModel();
            nmw.DataContext = contex;
            nmw.Width = 500;
            nmw.Height = 500;

            IDialogService dialogCheckIn = new DialogService();
            dialogCheckIn.ShowTitleDialog(contex, nmw, "Title");





        }






    }

    //Statistic


}
