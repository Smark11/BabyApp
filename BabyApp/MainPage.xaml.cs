using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BabyApp.Resources;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;

namespace BabyApp
{
    public partial class MainPage : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = this;

            LoadPicsIntoCollection();
            LoadPicsOnScreen("BabyAnimals");
            BuildLocalizedApplicationBar();
        }

        #region "Methods"

        private void LoadPicsIntoCollection()
        {
            BabyAnimals = new List<Box>();

            BabyAnimals.Add(new Box("Bear", "/Assets/Pics/BabyAnimals/Bear.png", "/Assets/Sounds/BabyAnimals/Bear.wav"));
            BabyAnimals.Add(new Box("Bison", "/Assets/Pics/BabyAnimals/Bison.png", "/Assets/Sounds/BabyAnimals/Bison.wav"));
            BabyAnimals.Add(new Box("Cat", "/Assets/Pics/BabyAnimals/Cat.png", "/Assets/Sounds/BabyAnimals/Cat.wav"));
            BabyAnimals.Add(new Box("Chick", "/Assets/Pics/BabyAnimals/Chick.png", "/Assets/Sounds/BabyAnimals/Chick.wav"));
            BabyAnimals.Add(new Box("Cow", "/Assets/Pics/BabyAnimals/Cow.png", "/Assets/Sounds/BabyAnimals/Cow.wav"));
            BabyAnimals.Add(new Box("Deer", "/Assets/Pics/BabyAnimals/Deer.png", "/Assets/Sounds/BabyAnimals/Deer.wav"));
            BabyAnimals.Add(new Box("Dolphin", "/Assets/Pics/BabyAnimals/Dolphin.png", "/Assets/Sounds/BabyAnimals/Dolphin.wav"));
            BabyAnimals.Add(new Box("Duck", "/Assets/Pics/BabyAnimals/Duck.png", "/Assets/Sounds/BabyAnimals/Duck.wav"));
            BabyAnimals.Add(new Box("Elephant", "/Assets/Pics/BabyAnimals/Elephant.png", "/Assets/Sounds/BabyAnimals/Elephnat.wav"));
            BabyAnimals.Add(new Box("Fish", "/Assets/Pics/BabyAnimals/Fish.png", "/Assets/Sounds/BabyAnimals/Fish.wav"));
            BabyAnimals.Add(new Box("Frog", "/Assets/Pics/BabyAnimals/Frog.png", "/Assets/Sounds/BabyAnimals/Frog.wav"));
            BabyAnimals.Add(new Box("Giraffe", "/Assets/Pics/BabyAnimals/Giraffe.png", "/Assets/Sounds/BabyAnimals/Giraffe.wav"));
            BabyAnimals.Add(new Box("Goat", "/Assets/Pics/BabyAnimals/Goat.png", "/Assets/Sounds/BabyAnimals/Goat.wav"));
            BabyAnimals.Add(new Box("Hippo", "/Assets/Pics/BabyAnimals/Hippo.png", "/Assets/Sounds/BabyAnimals/Hippo.wav"));
            BabyAnimals.Add(new Box("Monkey", "/Assets/Pics/BabyAnimals/Monkey.png", "/Assets/Sounds/BabyAnimals/Monkey.wav"));
            BabyAnimals.Add(new Box("Mouse", "/Assets/Pics/BabyAnimals/Mouse.png", "/Assets/Sounds/BabyAnimals/Mouse.wav"));
            BabyAnimals.Add(new Box("Owl", "/Assets/Pics/BabyAnimals/Owl.png", "/Assets/Sounds/BabyAnimals/Owl.wav"));
            BabyAnimals.Add(new Box("Panda", "/Assets/Pics/BabyAnimals/Panda.png", "/Assets/Sounds/BabyAnimals/Panda.wav"));
            BabyAnimals.Add(new Box("Penguin", "/Assets/Pics/BabyAnimals/Penquin.png", "/Assets/Sounds/BabyAnimals/Penquin.wav"));
            BabyAnimals.Add(new Box("Pig", "/Assets/Pics/BabyAnimals/Pig.png", "/Assets/Sounds/BabyAnimals/Pig.wav"));
            BabyAnimals.Add(new Box("Rabbit", "/Assets/Pics/BabyAnimals/Rabbit.png", "/Assets/Sounds/BabyAnimals/Rabbit.wav"));
            BabyAnimals.Add(new Box("Seal", "/Assets/Pics/BabyAnimals/Seal.png", "/Assets/Sounds/BabyAnimals/Seal.wav"));
            BabyAnimals.Add(new Box("Sheep", "/Assets/Pics/BabyAnimals/Sheep.png", "/Assets/Sounds/BabyAnimals/Sheep.wav"));
            BabyAnimals.Add(new Box("Tiger", "/Assets/Pics/BabyAnimals/Tiger.png", "/Assets/Sounds/BabyAnimals/Tiger.wav"));
            BabyAnimals.Add(new Box("Turkey", "/Assets/Pics/BabyAnimals/Turkey.png", "/Assets/Sounds/BabyAnimals/Turkey.wav"));
            BabyAnimals.Add(new Box("Zebra", "/Assets/Pics/BabyAnimals/Zebra.png", "/Assets/Sounds/BabyAnimals/Zebra.wav"));

            BabyMisc = new List<Box>();

            BabyMisc.Add(new Box("Airpane", "/Assets/Pics/BabyMisc/Airplane.png", "/Assets/Sounds/BabyMisc/Airplane.wav"));
            BabyMisc.Add(new Box("Ambulance", "/Assets/Pics/BabyMisc/Ambulance.png", "/Assets/Sounds/BabyMisc/Ambulance.wav"));
            BabyMisc.Add(new Box("Bike", "/Assets/Pics/BabyMisc/Bike.png", "/Assets/Sounds/BabyMisc/Bike.wav"));
            BabyMisc.Add(new Box("Camera", "/Assets/Pics/BabyMisc/Camera.png", "/Assets/Sounds/BabyMisc/Camera.wav"));
            BabyMisc.Add(new Box("Car", "/Assets/Pics/BabyMisc/Car.png", "/Assets/Sounds/BabyMisc/Car.wav"));
            BabyMisc.Add(new Box("Clock", "/Assets/Pics/BabyMisc/Clock.png", "/Assets/Sounds/BabyMisc/Clock.wav"));
            BabyMisc.Add(new Box("Firetruck", "/Assets/Pics/BabyMisc/Firetruck.png", "/Assets/Sounds/BabyMisc/Firetruck.wav"));
            BabyMisc.Add(new Box("Iron", "/Assets/Pics/BabyMisc/Iron.png", "/Assets/Sounds/BabyMisc/Iron.wav"));
            BabyMisc.Add(new Box("Key", "/Assets/Pics/BabyMisc/Key.png", "/Assets/Sounds/BabyMisc/Key.wav"));
            BabyMisc.Add(new Box("Lock", "/Assets/Pics/BabyMisc/Lock.png", "/Assets/Sounds/BabyMisc/Lock.wav"));
            BabyMisc.Add(new Box("Pencil", "/Assets/Pics/BabyMisc/Pencil.png", "/Assets/Sounds/BabyMisc/Pencil.wav"));
            BabyMisc.Add(new Box("Phone", "/Assets/Pics/BabyMisc/Phone.png", "/Assets/Sounds/BabyMisc/Phone.wav"));
            BabyMisc.Add(new Box("Scissors", "/Assets/Pics/BabyMisc/Scissors.png", "/Assets/Sounds/BabyMisc/Scissors.wav"));
            BabyMisc.Add(new Box("Stapler", "/Assets/Pics/BabyMisc/Stapler.png", "/Assets/Sounds/BabyMisc/Stapler.wav"));
            BabyMisc.Add(new Box("Toothbrush", "/Assets/Pics/BabyMisc/Toothbrush.png", "/Assets/Sounds/BabyMisc/Toothbrush.wav"));
            BabyMisc.Add(new Box("Tractor", "/Assets/Pics/BabyMisc/Tractor.png", "/Assets/Sounds/BabyMisc/Tractor.wav"));
            BabyMisc.Add(new Box("TV", "/Assets/Pics/BabyMisc/TV.png", "/Assets/Sounds/BabyMisc/TV.wav"));
            BabyMisc.Add(new Box("Vacuum", "/Assets/Pics/BabyMisc/Vacuum.png", "/Assets/Sounds/BabyMisc/Vacuum.wav"));
        }

        private void LoadPicsOnScreen(string category)
        {
            switch (category)
            {
                case "BabyAnimals":
                    Box1ImageSource = BabyAnimals[0].ImageSource;
                    Box1Description = BabyAnimals[0].Description;
                    Box1SoundSource = BabyAnimals[0].SoundSource;

                    Box2ImageSource = BabyAnimals[1].ImageSource;
                    Box2Description = BabyAnimals[1].Description;
                    Box2SoundSource = BabyAnimals[1].SoundSource;

                    Box3ImageSource = BabyAnimals[2].ImageSource;
                    Box3Description = BabyAnimals[2].Description;
                    Box3SoundSource = BabyAnimals[2].SoundSource;

                    Box4ImageSource = BabyAnimals[3].ImageSource;
                    Box4Description = BabyAnimals[3].Description;
                    Box4SoundSource = BabyAnimals[3].SoundSource;

                    Box5ImageSource = BabyAnimals[4].ImageSource;
                    Box5Description = BabyAnimals[4].Description;
                    Box5SoundSource = BabyAnimals[4].SoundSource;

                    Box6ImageSource = BabyAnimals[5].ImageSource;
                    Box6Description = BabyAnimals[5].Description;
                    Box6SoundSource = BabyAnimals[5].SoundSource;

                    Box7ImageSource = BabyAnimals[6].ImageSource;
                    Box7Description = BabyAnimals[6].Description;
                    Box7SoundSource = BabyAnimals[6].SoundSource;

                    Box8ImageSource = BabyAnimals[7].ImageSource;
                    Box8Description = BabyAnimals[7].Description;
                    Box8SoundSource = BabyAnimals[7].SoundSource;

                    Box9ImageSource = BabyAnimals[8].ImageSource;
                    Box9Description = BabyAnimals[8].Description;
                    Box9SoundSource = BabyAnimals[8].SoundSource;
                    break;
                case "BabyMisc":
                    Box1ImageSource = BabyMisc[0].ImageSource;
                    Box1Description = BabyMisc[0].Description;
                    Box1SoundSource = BabyMisc[0].SoundSource;

                    Box2ImageSource = BabyMisc[1].ImageSource;
                    Box2Description = BabyMisc[1].Description;
                    Box2SoundSource = BabyMisc[1].SoundSource;

                    Box3ImageSource = BabyMisc[2].ImageSource;
                    Box3Description = BabyMisc[2].Description;
                    Box3SoundSource = BabyMisc[2].SoundSource;

                    Box4ImageSource = BabyMisc[3].ImageSource;
                    Box4Description = BabyMisc[3].Description;
                    Box4SoundSource = BabyMisc[3].SoundSource;

                    Box5ImageSource = BabyMisc[4].ImageSource;
                    Box5Description = BabyMisc[4].Description;
                    Box5SoundSource = BabyMisc[4].SoundSource;

                    Box6ImageSource = BabyMisc[5].ImageSource;
                    Box6Description = BabyMisc[5].Description;
                    Box6SoundSource = BabyMisc[5].SoundSource;

                    Box7ImageSource = BabyMisc[6].ImageSource;
                    Box7Description = BabyMisc[6].Description;
                    Box7SoundSource = BabyMisc[6].SoundSource;

                    Box8ImageSource = BabyMisc[7].ImageSource;
                    Box8Description = BabyMisc[7].Description;
                    Box8SoundSource = BabyMisc[7].SoundSource;

                    Box9ImageSource = BabyMisc[8].ImageSource;
                    Box9Description = BabyMisc[8].Description;
                    Box9SoundSource = BabyMisc[8].SoundSource;
                    break;
            }
        }

        //TO DO - This is the guts of how the continious play should work
        //Not sure where this needs to live and how it gets kicked off (i.e. do we need a completely new page, do we call into DisplayPicture.xaml with a parameter
        //indicating it is continious play

        //Also need to decide how on the app we are going to let user turn on and off continious play (do not think it should be from the Options page, not accessible enough)
        private void ContiniousPlay(string category)
        {         
            List<Box> continuousPlayList=new List<Box>();

            switch (category)
            {
                case "BabyAnimals":
                    continuousPlayList = BabyAnimals;
                    break;
                case "BabyMisc":
                    continuousPlayList = BabyMisc;
                    break;
            }

             for(int i=0; i<continuousPlayList.Count-1; i++) 
             {
                 App.gDisplayPicture = continuousPlayList[i].ImageSource;
                 App.gDisplayDescription = continuousPlayList[i].Description;
                 App.gDisplaySound = continuousPlayList[i].SoundSource;
                 NavigationService.Navigate(new Uri("/DisplayPicture.xaml", UriKind.Relative));
             }                           
        }

        #endregion "Methods"

        #region "Properties"

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private List<Box> _babyAnimals;
        public List<Box> BabyAnimals
        {
            get { return _babyAnimals; }
            set { _babyAnimals = value; NotifyPropertyChanged("BabyAnimals"); }
        }

        private List<Box> _babyMisc;
        public List<Box> BabyMisc
        {
            get { return _babyMisc; }
            set { _babyMisc = value; NotifyPropertyChanged("BabyMisc"); }
        }

        private string _box1Description;
        public string Box1Description
        {
            get { return _box1Description; }
            set { _box1Description = value; NotifyPropertyChanged("Box1Description"); }
        }

        private string _box1ImageSource;
        public string Box1ImageSource
        {
            get { return _box1ImageSource; }
            set { _box1ImageSource = value; NotifyPropertyChanged("Box1ImageSource"); }
        }

        private string _box1SoundSource;
        public string Box1SoundSource
        {
            get { return _box1SoundSource; }
            set { _box1SoundSource = value; NotifyPropertyChanged("Box1SoundSource"); }
        }

        private string _box2Description;
        public string Box2Description
        {
            get { return _box2Description; }
            set { _box2Description = value; NotifyPropertyChanged("Box2Description"); }
        }

        private string _box2ImageSource;
        public string Box2ImageSource
        {
            get { return _box2ImageSource; }
            set { _box2ImageSource = value; NotifyPropertyChanged("Box2ImageSource"); }
        }

        private string _box2SoundSource;
        public string Box2SoundSource
        {
            get { return _box2SoundSource; }
            set { _box2SoundSource = value; NotifyPropertyChanged("Box2SoundSource"); }
        }

        private string _box3Description;
        public string Box3Description
        {
            get { return _box3Description; }
            set { _box3Description = value; NotifyPropertyChanged("Box3Description"); }
        }

        private string _box3ImageSource;
        public string Box3ImageSource
        {
            get { return _box3ImageSource; }
            set { _box3ImageSource = value; NotifyPropertyChanged("Box3ImageSource"); }
        }

        private string _box3SoundSource;
        public string Box3SoundSource
        {
            get { return _box3SoundSource; }
            set { _box3SoundSource = value; NotifyPropertyChanged("Box3SoundSource"); }
        }

        private string _box4Description;
        public string Box4Description
        {
            get { return _box4Description; }
            set { _box4Description = value; NotifyPropertyChanged("Box4Description"); }
        }

        private string _box4ImageSource;
        public string Box4ImageSource
        {
            get { return _box4ImageSource; }
            set { _box4ImageSource = value; NotifyPropertyChanged("Box4ImageSource"); }
        }

        private string _box4SoundSource;
        public string Box4SoundSource
        {
            get { return _box4SoundSource; }
            set { _box4SoundSource = value; NotifyPropertyChanged("Box4SoundSource"); }
        }

        private string _box5Description;
        public string Box5Description
        {
            get { return _box5Description; }
            set { _box5Description = value; NotifyPropertyChanged("Box5Description"); }
        }

        private string _box5ImageSource;
        public string Box5ImageSource
        {
            get { return _box5ImageSource; }
            set { _box5ImageSource = value; NotifyPropertyChanged("Box5ImageSource"); }
        }

        private string _box5SoundSource;
        public string Box5SoundSource
        {
            get { return _box5SoundSource; }
            set { _box5SoundSource = value; NotifyPropertyChanged("Box5SoundSource"); }
        }

        private string _box6Description;
        public string Box6Description
        {
            get { return _box6Description; }
            set { _box6Description = value; NotifyPropertyChanged("Box6Description"); }
        }

        private string _box6ImageSource;
        public string Box6ImageSource
        {
            get { return _box6ImageSource; }
            set { _box6ImageSource = value; NotifyPropertyChanged("Box6ImageSource"); }
        }

        private string _box6SoundSource;
        public string Box6SoundSource
        {
            get { return _box6SoundSource; }
            set { _box6SoundSource = value; NotifyPropertyChanged("Box6SoundSource"); }
        }

        private string _box7Description;
        public string Box7Description
        {
            get { return _box7Description; }
            set { _box7Description = value; NotifyPropertyChanged("Box7Description"); }
        }

        private string _box7ImageSource;
        public string Box7ImageSource
        {
            get { return _box7ImageSource; }
            set { _box7ImageSource = value; NotifyPropertyChanged("Box7ImageSource"); }
        }

        private string _box7SoundSource;
        public string Box7SoundSource
        {
            get { return _box7SoundSource; }
            set { _box7SoundSource = value; NotifyPropertyChanged("Box7SoundSource"); }
        }

        private string _box8Description;
        public string Box8Description
        {
            get { return _box8Description; }
            set { _box8Description = value; NotifyPropertyChanged("Box8Description"); }
        }

        private string _box8ImageSource;
        public string Box8ImageSource
        {
            get { return _box8ImageSource; }
            set { _box8ImageSource = value; NotifyPropertyChanged("Box8ImageSource"); }
        }

        private string _box8SoundSource;
        public string Box8SoundSource
        {
            get { return _box8SoundSource; }
            set { _box8SoundSource = value; NotifyPropertyChanged("Box8SoundSource"); }
        }

        private string _box9Description;
        public string Box9Description
        {
            get { return _box9Description; }
            set { _box9Description = value; NotifyPropertyChanged("Box9Description"); }
        }

        private string _box9ImageSource;
        public string Box9ImageSource
        {
            get { return _box9ImageSource; }
            set { _box9ImageSource = value; NotifyPropertyChanged("Box9ImageSource"); }
        }

        private string _box9SoundSource;
        public string Box9SoundSource
        {
            get { return _box9SoundSource; }
            set { _box9SoundSource = value; NotifyPropertyChanged("Box9SoundSource"); }
        }

        #endregion "Properties"

        #region "Events"

        private void Box_Click(object sender, EventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();
            string imageSource = string.Empty;
            string description = string.Empty;
            string soundSource = string.Empty;

            switch (tag)
            {
                case "Box1":
                    imageSource = Box1ImageSource;
                    description = Box1Description;
                    soundSource = Box1SoundSource;
                    break;
                case "Box2":
                    imageSource = Box2ImageSource;
                    description = Box2Description;
                    soundSource = Box2SoundSource;
                    break;
                case "Box3":
                    imageSource = Box3ImageSource;
                    description = Box3Description;
                    soundSource = Box3SoundSource;
                    break;
                case "Box4":
                    imageSource = Box4ImageSource;
                    description = Box4Description;
                    soundSource = Box4SoundSource;
                    break;
                case "Box5":
                    imageSource = Box5ImageSource;
                    description = Box5Description;
                    soundSource = Box5SoundSource;
                    break;
                case "Box6":
                    imageSource = Box6ImageSource;
                    description = Box6Description;
                    soundSource = Box6SoundSource;
                    break;
                case "Box7":
                    imageSource = Box7ImageSource;
                    description = Box7Description;
                    soundSource = Box7SoundSource;
                    break;
                case "Box8":
                    imageSource = Box8ImageSource;
                    description = Box8Description;
                    soundSource = Box8SoundSource;
                    break;
                case "Box9":
                    imageSource = Box9ImageSource;
                    description = Box9Description;
                    soundSource = Box9SoundSource;
                    break;
            }

            App.gDisplayPicture = imageSource;
            App.gDisplayDescription = description;
            App.gDisplaySound = soundSource;
            NavigationService.Navigate(new Uri("/DisplayPicture.xaml", UriKind.Relative));
        }

        private void Play_Click(object sender, EventArgs e)
        {

        }

        private void BabyAnimals_Click(object sender, EventArgs e)
        {
            LoadPicsOnScreen("BabyAnimals");
        }

        private void BabyMisc_Click(object sender, EventArgs e)
        {
            LoadPicsOnScreen("BabyMisc");
        }

        private void Animals_Click(object sender, EventArgs e)
        {
            LoadPicsOnScreen("Animals");
        }

        private void Misc_Click(object sender, EventArgs e)
        {
            LoadPicsOnScreen("Misc");
        }

        private void About_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }

        private void Options_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Options.xaml", UriKind.Relative));
        }

        private void Review_Click(object sender, EventArgs e)
        {

            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }

        private void MoreApps_Click(object sender, EventArgs e)
        {
            MarketplaceSearchTask marketplaceSearchTask = new MarketplaceSearchTask();

            marketplaceSearchTask.SearchTerms = "KLBCreations";
            marketplaceSearchTask.Show();
        }

        #endregion "Events"

        #region "Common Routines"

        private void BuildLocalizedApplicationBar()
        {
            try
            {
                // Set the page's ApplicationBar to a new instance of ApplicationBar.
                ApplicationBar = new ApplicationBar();

                ApplicationBar.Mode = ApplicationBarMode.Default;
                ApplicationBar.Opacity = 1.0;
                ApplicationBar.IsVisible = true;
                ApplicationBar.IsMenuEnabled = true;

                // Create a new button and set the text value to the localized string from AppResources.
                ApplicationBarIconButton appBarButton1 = new ApplicationBarIconButton(new Uri("/Assets/feature.email.png", UriKind.Relative));
                appBarButton1.Text = "Baby Misc";
                ApplicationBar.Buttons.Add(appBarButton1);
                appBarButton1.Click += new EventHandler(BabyMisc_Click);

                ApplicationBarIconButton appBarButton2 = new ApplicationBarIconButton(new Uri("/Assets/cancel.png", UriKind.Relative));
                appBarButton2.Text = "Baby Animals";
                ApplicationBar.Buttons.Add(appBarButton2);
                appBarButton2.Click += new EventHandler(BabyAnimals_Click);

                ApplicationBarIconButton appBarButton3 = new ApplicationBarIconButton(new Uri("/Assets/feature.email.png", UriKind.Relative));
                appBarButton3.Text = "Misc";
                ApplicationBar.Buttons.Add(appBarButton3);
                appBarButton3.Click += new EventHandler(Misc_Click);

                ApplicationBarIconButton appBarButton4 = new ApplicationBarIconButton(new Uri("/Assets/cancel.png", UriKind.Relative));
                appBarButton4.Text = "Animals";
                ApplicationBar.Buttons.Add(appBarButton4);
                appBarButton4.Click += new EventHandler(Animals_Click);

                // Create a new menu item with the localized string from AppResources.
                ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem("Options");
                ApplicationBar.MenuItems.Add(appBarMenuItem);
                appBarMenuItem.Click += new EventHandler(Options_Click);

                ApplicationBarMenuItem appBarMenuItem2 = new ApplicationBarMenuItem("About");
                ApplicationBar.MenuItems.Add(appBarMenuItem2);
                appBarMenuItem2.Click += new EventHandler(About_Click);

                ApplicationBarMenuItem appBarMenuItem3 = new ApplicationBarMenuItem("Review");
                ApplicationBar.MenuItems.Add(appBarMenuItem3);
                appBarMenuItem3.Click += new EventHandler(Review_Click);

                ApplicationBarMenuItem appBarMenuItem4 = new ApplicationBarMenuItem("More Apps");
                ApplicationBar.MenuItems.Add(appBarMenuItem4);
                appBarMenuItem4.Click += new EventHandler(MoreApps_Click);
            }
            catch (Exception ex)
            {
            }
        }

        #endregion "Common Routines"


    }
}