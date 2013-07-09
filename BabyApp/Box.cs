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
        public Box(string description, string imageSourceSmall, string imageSourceLarge, string soundSource)
        {
            Description = description;
            ImageSourceSmall = imageSourceSmall;
            ImageSourceLarge = imageSourceLarge;
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

        private string _imageSourceSmall;
        public string ImageSourceSmall
        {
            get { return _imageSourceSmall; }
            set { _imageSourceSmall = value; NotifyPropertyChanged("ImageSourceSmall"); }
        }

        private string _imageSourceLarge;
        public string ImageSourceLarge
        {
            get { return _imageSourceLarge; }
            set { _imageSourceLarge = value; NotifyPropertyChanged("ImageSourceLarge"); }
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
