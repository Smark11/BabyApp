using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyApp
{
    public class Box : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        //Constructor
        public Box(string description, string imageSource, string soundSource)
        {
            Description = description;
            ImageSource = imageSource;
            SoundSource = soundSource;
        }


        #region "Properties"

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; NotifyPropertyChanged("Description"); }
        }

        private string _imageSource;
        public string ImageSource
        {
            get { return _imageSource; }
            set { _imageSource = value; NotifyPropertyChanged("ImageSource"); }
        }

        private string _soundSource;
        public string SoundSource
        {
            get { return _soundSource; }
            set { _soundSource = value; NotifyPropertyChanged("SoundSource"); }
        }
        #endregion "Properties"
    }
}
