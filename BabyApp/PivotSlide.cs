using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyApp
{
    public class PivotSlide : INotifyPropertyChanged
    {
        public PivotSlide(string header, Box box1, Box box2, Box box3, Box box4, Box box5, Box box6, Box box7, Box box8, Box box9)
        {
            Box1 = box1;
            Box2 = box2;
            Box3 = box3;
            Box4 = box4;
            Box5 = box5;
            Box6 = box6;
            Box7 = box7;
            Box8 = box8;
            Box9 = box9;
            Header = header;
        }

        #region properties

        private string _header;
        public string Header
        {
            get { return _header; }
            set { _header = value; RaisePropertyChanged("Header"); }
        }
        

        private Box _box1;
        public Box Box1
        {
            get { return _box1; }
            set { _box1 = value; RaisePropertyChanged("Box1"); }
        }

        private Box _box2;
        public Box Box2
        {
            get { return _box2; }
            set { _box2 = value; RaisePropertyChanged("Box2"); }
        }

        private Box _box3;
        public Box Box3
        {
            get { return _box3; }
            set { _box3 = value; RaisePropertyChanged("Box3"); }
        }

        private Box _box4;
        public Box Box4
        {
            get { return _box4; }
            set { _box4 = value; RaisePropertyChanged("Box4"); }
        }

        private Box _box5;
        public Box Box5
        {
            get { return _box5; }
            set { _box5 = value; RaisePropertyChanged("Box5"); }
        }

        private Box _box6;
        public Box Box6
        {
            get { return _box6; }
            set { _box6 = value; RaisePropertyChanged("Box6"); }
        }

        private Box _box7;
        public Box Box7
        {
            get { return _box7; }
            set { _box7 = value; RaisePropertyChanged("Box7"); }
        }

        private Box _box8;
        public Box Box8
        {
            get { return _box8; }
            set { _box8 = value; RaisePropertyChanged("Box8"); }
        }

        private Box _box9;
        public Box Box9
        {
            get { return _box9; }
            set { _box9 = value; RaisePropertyChanged("Box9"); }
        }
        

        #endregion properties

        #region propchanged

        public void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion propchanged
    }
}
