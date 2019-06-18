using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasaDann.Utils;

namespace BasaDann.Classes
{
    public class Ticket : BaseShell
    {

        private int _id;                                     // ID заявки
        public int ID
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }

        private string _name;                               // Наименование заявки
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _zayavCategory_id;                    // Категория заявки
        public string ZayavCategory_id
        {
            get { return _zayavCategory_id; }
            set
            {
                _zayavCategory_id = value;
                OnPropertyChanged(nameof(ZayavCategory_id));
            }
        }

        private string _content;                            // Текст заявки с управляющими тегами
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        private string _closed;                             // Состояние заявки
        public string Closed
        {
            get { return _closed; }
            set
            {
                _closed = value;
                OnPropertyChanged(nameof(Closed));
            }
        }

        private string _date;                             // Дата создания заявки
        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private string _status;                             // Статус заявки
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private string _priority;                             // Приоритет заявки
        public string Priority
        {
            get { return _priority; }
            set
            {
                _priority = value;
                OnPropertyChanged(nameof(Priority));
            }
        }

        private string _fullName;                             // Имя создателя
        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        private string _mfullName;                             // Имя исполнителя
        public string MfullName
        {
            get { return _mfullName; }
            set
            {
                _mfullName = value;
                OnPropertyChanged(nameof(MfullName));
            }
        }

        private string _timestamp;                             // Time
        public string Timestamp
        {
            get { return _timestamp; }
            set
            {
                _timestamp = value;
                OnPropertyChanged(nameof(Timestamp));
            }
        }





    }
}
