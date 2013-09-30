using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.ComponentModel;

namespace BabyApp
{
    public partial class Help : PhoneApplicationPage, INotifyPropertyChanged
    {
        public Help()
        {
            InitializeComponent();
            DataContext = this;
            PhoneWidth = Application.Current.RootVisual.RenderSize.Width;
            PhoneHeight = Application.Current.RootVisual.RenderSize.Height;
        }


        #region properties

        private double _phoneWidth;
        public double PhoneWidth
        {
            get { return _phoneWidth; }
            set { _phoneWidth = value; RaisePropertyChanged("PhoneWidth"); }
        }

        private double _phoneHeight;
        public double PhoneHeight
        {
            get { return _phoneHeight; }
            set { _phoneHeight = value; RaisePropertyChanged("PhoneHeight"); }
        }
        

        #endregion properties

        public void RaisePropertyChanged(string t)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(t));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}