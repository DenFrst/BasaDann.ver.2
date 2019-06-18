using BasaDann.Classes;
using BasaDann.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
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

        private string _src_brw_old;
        public string Src_Brow_old
        {
            get { return _src_brw_old; }
            set
            {
                if (_src_brw_old != value)
                {
                    _src_brw_old = value;
                    OnPropertyChanged(nameof(Src_Brow_old));
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

        private bool check_com = false;
        public bool Check_Com
        {
            get { return check_com; }
            set
            {
                if (check_com != value)
                {
                    check_com = value;
                    OnPropertyChanged(nameof(Check_Com));
                }
            }
        }

        private DateTime _fromdate = DateTime.Now;
        public DateTime FromDate
        {
            get { return _fromdate; }
            set
            {
                if (_fromdate != value)
                {
                    _fromdate = value;
                    OnPropertyChanged(nameof(FromDate));
                }
            }
        }

        private DateTime _todate = DateTime.Now;
        public DateTime ToDate
        {
            get { return _todate; }
            set
            {
                if (_todate != value)
                {
                    _todate = value;
                    OnPropertyChanged(nameof(ToDate));
                }
            }
        }

        private string request_fields;
        private bool _check_files;

        private bool check_back = false;
        public bool Check_Back
        {
            get { return check_back; }
            set
            {
                if (check_back != value)
                {
                    check_back = value;
                    OnPropertyChanged(nameof(Check_Back));
                }
            }
        }

        private string _get_content;
        public string Get_Content
        {
            get { return _get_content; }
            set
            {
                if (_get_content != value)
                {
                    _get_content = value;
                    OnPropertyChanged(nameof(Get_Content));
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
            Thread mth = new Thread(new ThreadStart(Get_Fields));
            mth.Start();
        }

        private void Get_Fields()
        {

            if (clsDB.Scalar_SQL(Requests.Req_Count_Single, IDM) > 0)
            {
                Check_Com = clsDB.Scalar_SQL(Requests.Req_Comments, IDM) > 0;

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
                    Src_Brow = DT.Rows[0]["Content"] + Requests.Meta;
                    Src_Brow_old = Src_Brow;
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
                else
                {
                    if (MenuItems != null)
                    {
                        App.Current.Dispatcher.Invoke((Action) delegate { MenuItems.Clear(); });
                    }
                };
                // Menu

                clsDB.Close_DB_Connection();

                /////
                long totalMemory = GC.GetTotalMemory(false);

                GC.Collect();
                GC.Collect(1, GCCollectionMode.Forced);
                GC.WaitForPendingFinalizers();

                /////
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
            if (FromDate != null && ToDate != null)
            {

            }
            var xmlEntityReplacements = new Dictionary<string, string> {
                { "&apos;", "'" },
                { "&lt;", "<" },
                { "&gt;", ">" },
                { "&quot;", "\"" },
            };

            string req = "SELECT * FROM request ORDER BY id DESC";
            DT_ALL = clsDB.Get_DataTable(req);

            foreach (DataRow row in DT_ALL.Rows)
            {
                string a = row["content"].ToString();

                a = Regex.Replace(a, @"<(.|\n)*?>", String.Empty);
                a = Regex.Replace(a, String.Join("|",
                    xmlEntityReplacements.Keys.Select(k => k.ToString()).ToArray()), m => xmlEntityReplacements[m.Value]);
            }

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
            TabIndex = 0;
            IDM = Convert.ToInt32(SSR["id"].ToString());
            if (clsDB.Scalar_SQL(Requests.Req_Count_Single, IDM) > 0)
            {
                Get_Fields();
                //Get_Menu(SSR); --как сделать images из всех
                clsDB.Close_DB_Connection();
            }
            else
            {
                MessageBox.Show("SelectedFromAll");
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
            if (clsDB.Scalar_SQL(Requests.Req_Single, IDM) > 0)
            {
                string pathreq = "https://helpdesk.soft.aero/request/" + IDM;
                System.Diagnostics.Process.Start(pathreq);
            }
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
            Window win = new Window();
            win.Content = new StatsView();
            win.Height = 500;
            win.Width = 800;
            win.Show();

        }

        private RelayCommand _openHelp;
        public RelayCommand OpenHelp
        {
            get
            {
                return _openHelp = _openHelp ??
                                    new RelayCommand(OpenHelpWin);
            }
        }

        private void OpenHelpWin()
        {
            Window nmw = new Window();
            nmw.Content = new HelpView();
            nmw.Height = 600;
            nmw.Width = 950;
            //nmw.SizeToContent = SizeToContent.WidthAndHeight;
            nmw.Show();

            //

        }

        private RelayCommand _gethistory;
        public RelayCommand GetHistory
        {
            get
            {
                return _gethistory = _gethistory ??
                                 new RelayCommand(Get_History);
            }
        }

        private void Get_History()
        {
            if (clsDB.Scalar_SQL(Requests.Req_Count_Single, IDM) > 0)
            {
                string req = Requests.Req_History + IDM;
                DataTable dat = clsDB.Get_DataTable(req);
                string al = "";
                foreach (DataRow row in dat.Rows)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        al += row[i].ToString() + "<p></p>";
                    }
                }

                Src_Brow = al + Requests.Meta;
                Check_Back = true;
            }
            else
            {
                MessageBox.Show("Заявки не найдено");
            }

        }

        private RelayCommand _goback;
        public RelayCommand GoBack
        {
            get
            {
                return _goback = _goback ??
                                     new RelayCommand(Go_Back_Brow);
            }
        }

        private void Go_Back_Brow()
        {
            Src_Brow = Src_Brow_old;
            Check_Back = false;
        }

        private RelayCommand _getcomments;
        public RelayCommand GetComments
        {
            get
            {
                return _getcomments = _getcomments ??
                                 new RelayCommand(Get_Comments);
            }
        }

        private void Get_Comments()
        {
            if (clsDB.Scalar_SQL(Requests.Req_Count_Single, IDM) > 0)
            {
                //если есть строка в таблице коммент
                if (clsDB.Scalar_SQL(Requests.Req_Comments, IDM) > 0)
                {


                    string req = Requests.Req_Comments + IDM;
                    DataTable dat = clsDB.Get_DataTable(req);
                    string al = "";
                    foreach (DataRow row in dat.Rows)
                    {
                        if (Convert.ToInt32(row["show"]) == 0)
                        {
                            al += "<span style = \"background-color:#00FFFF\">" + " Отправлено" + "</span> " + row["timestamp"].ToString() + "<p></p>";
                        }
                        else
                        {
                            al += "<span style = \"background-color:#FFFF00\">" + " Скрыто" + "</span> " + row["timestamp"].ToString() + "<p></p>";
                        }
                        al += row["author"].ToString() + "<p></p>";
                        al += row["comment"].ToString() + "<p></p>";
                    }

                    Src_Brow = al + Requests.Meta;
                    Check_Back = true;
                }
                else
                {
                    Check_Com = false;
                }
            }
            else
            {
                MessageBox.Show("Get_Comments");
            }
        }

        private RelayCommand _tryDate;
        public RelayCommand TryDate
        {
            get
            {
                return _tryDate = _tryDate ??
                                      new RelayCommand(Get_Date_betw);
            }
        }

        private void Get_Date_betw()
        {
            Thread mth1 = new Thread(new ThreadStart(Get_tryDate));
            mth1.Start();
        }

        private void Get_tryDate()
        {
            var xmlEntityReplacements = new Dictionary<string, string> {
                { "&apos;", "'" },
                { "&lt;", "<" },
                { "&gt;", ">" },
                { "&quot;", "\"" },
            };

            string req =
                "SELECT * from request where STR_TO_DATE(date, \"%d.%m.%Y %k:%i\") between STR_TO_DATE('" + 
                FromDate.ToString("dd.MM.yyyy") + " 00:00', \"%d.%m.%Y %k:%i\") and STR_TO_DATE('" + 
                ToDate.ToString("dd.MM.yyyy") + " 23:59:59', \"%d.%m.%Y %k:%i:%s\"); ";

            DT_ALL = clsDB.Get_DataTable(req);

            foreach (DataRow row in DT_ALL.Rows)
            {

                //Get_Content = Convert.ToString(row["content"]);

                row["content"] = Regex.Replace(row["content"].ToString(), @"<(.|\n)*?>", String.Empty);
                row["content"] = Regex.Replace(row["content"].ToString(), String.Join("|",
                    xmlEntityReplacements.Keys.Select(k => k.ToString()).ToArray()), m => xmlEntityReplacements[m.Value]);
            }
            
            

            clsDB.Close_DB_Connection();

            /////
            long totalMemory = GC.GetTotalMemory(false);

            GC.Collect();
            GC.Collect(1, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();

            /////

        }

        


        // Main
        public MainWindowViewModel()
        {

            #region MyRegion

            /*

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
        // /Main


    }

    //Statistic


}
