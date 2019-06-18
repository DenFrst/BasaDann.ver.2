using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BasaDann.Classes;
using BasaDann.Utils;
using BasaDann.Logic;
using MySql.Data.MySqlClient;
using Telerik.Windows.Controls;

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
        public int IDM {
            get { return _idM; }
            set
            {
                _idM = value;
                OnPropertyChanged(nameof(IDM));
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

            if (clsDB.Scalar_SQL("request",IDM) > 0)
            {
                string re1q = "SELECT * FROM request WHERE id = " + IDM;
                DT = clsDB.Get_DataTable(re1q);
                
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

                        Content = Get_Cont()
                    },
                };


            }
            else { MessageBox.Show("Заявки " + IDM + " нет"); }

           
        }

        public string Get_Cont()
        {
            var xmlEntityReplacements = new Dictionary<string, string> {
                { "&apos;", "'" },
                { "&lt;", "<" },
                { "&gt;", ">" },
                { "&quot;", "\"" },
            };

            string mycontent = DT.Rows[0]["Content"].ToString();

            mycontent = Regex.Replace(mycontent, @"<(.|\n)*?>", String.Empty); // <[^>]*>
            mycontent = Regex.Replace(mycontent, String.Join("|",
                xmlEntityReplacements.Keys.Select(k => k.ToString()).ToArray()), m => xmlEntityReplacements[m.Value]);


            return mycontent;
        }


        // Main
        public MainWindowViewModel()
        {
           

            #region MyRegion

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


            #endregion

        }

       

    }
}
