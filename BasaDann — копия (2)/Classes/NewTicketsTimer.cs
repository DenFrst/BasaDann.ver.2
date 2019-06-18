using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using BasaDann.Utils;
using BasaDann.ViewModels;
using WPFNotification.Model;
using WPFNotification.Services;
using Timer = System.Timers.Timer;

namespace BasaDann.Classes
{
    public class NewTicketsTimer
    {
        private MainWindowViewModel mv = new MainWindowViewModel();

        private static Timer aTimer;

        public static void TicketsTimer()
        {
            Set_Timer();

            //aTimer.Stop();
            //aTimer.Dispose();
            //MessageBox.Show("1");
        }

        private static void Set_Timer()
        {
            // Create a timer with a two second interval.
            aTimer = new Timer(60000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

        }


        private static DataTable mdt;
        private static int topid;
        private static int _topid;


        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            mdt = clsDB.Get_Top_ID();
            _topid = Convert.ToInt32(mdt.Rows[0]["id"]);

            if (topid < _topid)
            {
                //MessageBox.Show("Новый тикет № " + _topid); // notify
                DefNot();
                topid = _topid;
              

            }
            else
            {
               //DefNot();
               // MessageBox.Show(topid+_topid.ToString());
            }
        }

        private static void DefNot()
        {
           System.Windows.Application.Current.Dispatcher.Invoke(
               (Action) (() =>
                {
                    Notification req = new Notification()
                    { 
                        
                        Message = "Новая заявка № " + _topid + Environment.NewLine + Environment.NewLine
                                  + mdt.Rows[0]["Name"] + Environment.NewLine
                                  + mdt.Rows[0]["company"],
                        Title = "Получена новая заявка" ,
                        
                                
                    };
                    INotificationDialogService dia = new NotificationDialogService();
                    dia.ShowNotificationWindow(req);
                }));

           //var p = MainWindowViewModel.Values4Not();

        }

       
    }
}
