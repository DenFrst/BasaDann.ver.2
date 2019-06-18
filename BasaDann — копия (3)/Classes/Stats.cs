using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasaDann.Utils;

namespace BasaDann.Classes
{
    public class Stats : BaseShell
    {
        private int _z_all;                                     // Всего заявок
        public int z_All
        {
            get { return _z_all; }
            set
            {
                _z_all = value;
                OnPropertyChanged(nameof(z_All));
            }
        }

        private int _z_closed;                                     // Завершено заявок
        public int z_Closed
        {
            get { return _z_closed; }
            set
            {
                _z_closed = value;
                OnPropertyChanged(nameof(z_Closed));
            }
        }

        private double _z_time_sp;                                     // Среднее затраченное время
        public double z_Time_Sp
        {
            get { return _z_time_sp/_z_closed; }
            set
            {
                _z_time_sp = value;
                OnPropertyChanged(nameof(z_Time_Sp));
            }
        }

        private int _z_last;                                     // Последняя заявка
        public int z_Last
        {
            get { return _z_last; }
            set
            {
                _z_last = value;
                OnPropertyChanged(nameof(z_Last));
            }
        }

        private int _z_expired;                                     // Просрочено исполнение
        public int z_Expired
        {
            get { return _z_expired; }
            set
            {
                _z_expired = value;
                OnPropertyChanged(nameof(z_Expired));
            }
        }

        private int _z_open_raz;
        public int z_Open_Raz
        {
            get { return _z_all-z_Closed; }
            set
            {
                _z_open_raz = value;
                OnPropertyChanged(nameof(z_Open_Raz));
            }
        }
    }
}
