using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyApp
{
    public class BoxGrid : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        #region "Properties"

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Box _picture1;
        public Box Picture1
        {
            get { return _picture1; }
            set { _picture1 = value; NotifyPropertyChanged("Picture1"); }
        }

        private Box _picture2;
        public Box Picture2
        {
            get { return _picture2; }
            set { _picture2 = value; NotifyPropertyChanged("Picture2"); }
        }

        private Box _picture3;
        public Box Picture3
        {
            get { return _picture3; }
            set { _picture3 = value; NotifyPropertyChanged("Picture3"); }
        }

        private Box _picture4;
        public Box Picture4
        {
            get { return _picture4; }
            set { _picture4 = value; NotifyPropertyChanged("Picture4"); }
        }

        private Box _picture5;
        public Box Picture5
        {
            get { return _picture5; }
            set { _picture5 = value; NotifyPropertyChanged("Picture5"); }
        }

        private Box _picture6;
        public Box Picture6
        {
            get { return _picture6; }
            set { _picture6 = value; NotifyPropertyChanged("Picture6"); }
        }

        private Box _picture7;
        public Box Picture7
        {
            get { return _picture7; }
            set { _picture7 = value; NotifyPropertyChanged("Picture7"); }
        }

        private Box _picture8;
        public Box Picture8
        {
            get { return _picture8; }
            set { _picture8 = value; NotifyPropertyChanged("Picture8"); }
        }

        private Box _picture9;
        public Box Picture9
        {
            get { return _picture9; }
            set { _picture9 = value; NotifyPropertyChanged("Picture9"); }
        }

        #endregion "Properties"

    }
}
