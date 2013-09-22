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
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;
using Windows.Phone.Speech.Synthesis;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;
using BabyApp.Resources;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using Common.Utilities;
using System.Collections.ObjectModel;
using Common.Licencing;
using Common.IsolatedStoreage;


namespace BabyApp
{
    public partial class MainPage : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //    DispatcherTimer _timer;
        SpeechSynthesizer synthesizer;

        private bool _slideShowInProgress = false;
        private bool _stopSlideShow = false;

        ApplicationBarIconButton appBarButton1 = new ApplicationBarIconButton(new Uri("/Assets/transport.play.png", UriKind.Relative));
        MarketplaceDetailTask _marketPlaceDetailTask = new MarketplaceDetailTask();
        private const string NUMBEROFTIMESOPENED = "NUMBEROFTIMESOPENED";      

        private int _numberOfTimesOpened = 0;

        private bool _rated;

        public enum Screen
        {
            MainGrid,
            SlideShow
        }

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            if (IS.GetSetting(NUMBEROFTIMESOPENED) == null)
            {
                IS.SaveSetting(NUMBEROFTIMESOPENED, 0);
            }
            else
            {
                IS.SaveSetting(NUMBEROFTIMESOPENED, (int)IS.GetSetting(NUMBEROFTIMESOPENED) + 1);
                _numberOfTimesOpened = (int)IS.GetSetting(NUMBEROFTIMESOPENED);
            }

            //if the user has rated using the 5 day, or
            //if the user has rated to extend the trial, mark the app as rated.
            if (Rate.HasAppBeenRated() == "Yes" )
            {
                _rated = true;
            }
            
            PivotSlides = new ObservableCollection<PivotSlide>();

            synthesizer = new SpeechSynthesizer();
            this.DataContext = this;

            NavigateToScreen(Screen.MainGrid);
            LoadPicsIntoCollection();
            SetPivots(App.gCategory);
            BuildLocalizedApplicationBar();
            Mode = Screen.MainGrid;
        }

        private void PaidAppInitialization()
        {
            if ((Application.Current as App).IsTrial)
            {
                if (Trial.IsTrialExpired())
                {
                    MessageBox.Show("Your trial has expired, please purchase the application!");
                    _marketPlaceDetailTask.Show();
                }
                else
                {
                    //App has already been rated
                    if (_rated || _numberOfTimesOpened < 2)
                    {
                        MessageBox.Show("You have " + Trial.GetDaysLeftInTrial() + " days left in your trial.");
                    }
                    //app not rated, rate to add 10 days to trial
                    else if (!_rated && _numberOfTimesOpened >= 2)
                    {
                        MessageBoxResult result = MessageBox.Show("Extend Trial to 10 days by rating the application?", "Extend Trial?", MessageBoxButton.OKCancel);

                        if (result == MessageBoxResult.OK)
                        {
                            Trial.Add10DaysToTrial();
                           
                            IS.SaveSetting("AppRated", "Yes");
                            _rated = true;
                            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
                            marketplaceReviewTask.Show();
                        }
                    }
                }
            }
            else
            {
                if (!_rated)
                {
                    //5th, 10th, 15th time prompt, 20th time ok only to rate, never prompt them again after they rate.
                    Rate.RateTheApp(AppResources.RateTheAppQuestion, AppResources.RateTheAppPrompt, AppResources.RateAppHeader);
                }
            }
        }

        #region "Methods"

        private void LoadPicsIntoCollection()
        {
            Animals = new List<Box>();
            Animals.Add(new Box("Alligator", "/Assets/Pics/Animals/alligator80x100.png", "/Assets/Pics/Animals/alligator480x800.png", "/Assets/Sounds/Animals/alligator.wav"));
            Animals.Add(new Box("Anteater", "/Assets/Pics/Animals/anteater80x100.png", "/Assets/Pics/Animals/anteater480x800.png", "/Assets/Sounds/Animals/anteater.wav"));
            Animals.Add(new Box("Badger", "/Assets/Pics/Animals/badger80x100.png", "/Assets/Pics/Animals/badger480x800.png", "/Assets/Sounds/Animals/badger.wav"));
            Animals.Add(new Box("Bear", "/Assets/Pics/Animals/bear80x100.png", "/Assets/Pics/Animals/bear480x800.png", "/Assets/Sounds/Animals/bear.wav"));
            Animals.Add(new Box("Camel", "/Assets/Pics/Animals/camel80x100.png", "/Assets/Pics/Animals/camel480x800.png", "/Assets/Sounds/Animals/camel.wav"));
            Animals.Add(new Box("Cheetah", "/Assets/Pics/Animals/cheetah80x100.png", "/Assets/Pics/Animals/cheetah480x800.png", "/Assets/Sounds/Animals/cheetah.wav"));
            Animals.Add(new Box("Crocodile", "/Assets/Pics/Animals/crocodile80x100.png", "/Assets/Pics/Animals/crocodile480x800.png", "/Assets/Sounds/Animals/crocodile.wav"));
            Animals.Add(new Box("Deer", "/Assets/Pics/Animals/deer80x100.png", "/Assets/Pics/Animals/deer480x800.png", "/Assets/Sounds/Animals/deer.wav"));
            Animals.Add(new Box("Dog", "/Assets/Pics/Animals/dog80x100.png", "/Assets/Pics/Animals/dog480x800.png", "/Assets/Sounds/Animals/dog.wav"));
            Animals.Add(new Box("Dolphin", "/Assets/Pics/Animals/dolphin80x100.png", "/Assets/Pics/Animals/dolphin480x800.png", "/Assets/Sounds/Animals/dolphin.wav"));
            Animals.Add(new Box("Donkey", "/Assets/Pics/Animals/donkey80x100.png", "/Assets/Pics/Animals/donkey480x800.png", "/Assets/Sounds/Animals/donkey.wav"));
            Animals.Add(new Box("Duck", "/Assets/Pics/Animals/duck80x100.png", "/Assets/Pics/Animals/duck480x800.png", "/Assets/Sounds/Animals/duck.wav"));
            Animals.Add(new Box("Eagle", "/Assets/Pics/Animals/eagle80x100.png", "/Assets/Pics/Animals/eagle480x800.png", "/Assets/Sounds/Animals/eagle.wav"));
            Animals.Add(new Box("Elephant", "/Assets/Pics/Animals/elephant80x100.png", "/Assets/Pics/Animals/elephant480x800.png", "/Assets/Sounds/Animals/elephant.wav"));
            Animals.Add(new Box("Elk", "/Assets/Pics/Animals/elk80x100.png", "/Assets/Pics/Animals/elk480x800.png", "/Assets/Sounds/Animals/elk.wav"));
            Animals.Add(new Box("Frog", "/Assets/Pics/Animals/frog80x100.png", "/Assets/Pics/Animals/frog480x800.png", "/Assets/Sounds/Animals/frog.wav"));
            Animals.Add(new Box("Hippopotamus", "/Assets/Pics/Animals/hippopotamus80x100.png", "/Assets/Pics/Animals/hippopotamus480x800.png", "/Assets/Sounds/Animals/hippopotamus.wav"));
            Animals.Add(new Box("Horse", "/Assets/Pics/Animals/horse80x100.png", "/Assets/Pics/Animals/horse480x800.png", "/Assets/Sounds/Animals/horse.wav"));
            Animals.Add(new Box("Jaguar", "/Assets/Pics/Animals/jaguar80x100.png", "/Assets/Pics/Animals/jaguar480x800.png", "/Assets/Sounds/Animals/jaguar.wav"));
            Animals.Add(new Box("Kitten", "/Assets/Pics/Animals/kitten80x100.png", "/Assets/Pics/Animals/kitten480x800.png", "/Assets/Sounds/Animals/kitten.wav"));
            Animals.Add(new Box("Koala", "/Assets/Pics/Animals/koala80x100.png", "/Assets/Pics/Animals/koala480x800.png", "/Assets/Sounds/Animals/koala.wav"));
            Animals.Add(new Box("Lemur", "/Assets/Pics/Animals/lemur80x100.png", "/Assets/Pics/Animals/lemur480x800.png", "/Assets/Sounds/Animals/lemur.wav"));
            Animals.Add(new Box("Leopard", "/Assets/Pics/Animals/leopard80x100.png", "/Assets/Pics/Animals/leopard480x800.png", "/Assets/Sounds/Animals/leopard.wav"));
            Animals.Add(new Box("Llama", "/Assets/Pics/Animals/llama80x100.png", "/Assets/Pics/Animals/llama480x800.png", "/Assets/Sounds/Animals/llama.wav"));
            Animals.Add(new Box("Lion", "/Assets/Pics/Animals/lion80x100.png", "/Assets/Pics/Animals/lion480x800.png", "/Assets/Sounds/Animals/lion.wav"));
            Animals.Add(new Box("Macaw", "/Assets/Pics/Animals/macaw80x100.png", "/Assets/Pics/Animals/macaw480x800.png", "/Assets/Sounds/Animals/macaw.wav"));
            Animals.Add(new Box("Monkey", "/Assets/Pics/Animals/monkey80x100.png", "/Assets/Pics/Animals/monkey480x800.png", "/Assets/Sounds/Animals/monkey.wav"));
            Animals.Add(new Box("Orca", "/Assets/Pics/Animals/orca80x100.png", "/Assets/Pics/Animals/orca480x800.png", "/Assets/Sounds/Animals/orca.wav"));
            Animals.Add(new Box("Owl", "/Assets/Pics/Animals/owl80x100.png", "/Assets/Pics/Animals/owl480x800.png", "/Assets/Sounds/Animals/owl.wav"));
            Animals.Add(new Box("Panda", "/Assets/Pics/Animals/panda80x100.png", "/Assets/Pics/Animals/panda480x800.png", "/Assets/Sounds/Animals/panda.wav"));
            Animals.Add(new Box("Penguin", "/Assets/Pics/Animals/penguin80x100.png", "/Assets/Pics/Animals/penguin480x800.png", "/Assets/Sounds/Animals/penguin.wav"));
            Animals.Add(new Box("Pig", "/Assets/Pics/Animals/pig80x100.png", "/Assets/Pics/Animals/pig480x800.png", "/Assets/Sounds/Animals/pig.wav"));
            Animals.Add(new Box("Polarbear", "/Assets/Pics/Animals/polarbear80x100.png", "/Assets/Pics/Animals/polarbear480x800.png", "/Assets/Sounds/Animals/polarbear.wav"));
            Animals.Add(new Box("Racoon", "/Assets/Pics/Animals/racoon80x100.png", "/Assets/Pics/Animals/racoon480x800.png", "/Assets/Sounds/Animals/racoon.wav"));
            Animals.Add(new Box("Redfox", "/Assets/Pics/Animals/redfox80x100.png", "/Assets/Pics/Animals/redfox480x800.png", "/Assets/Sounds/Animals/redfox.wav"));
            Animals.Add(new Box("Rhino", "/Assets/Pics/Animals/rhino80x100.png", "/Assets/Pics/Animals/rhino480x800.png", "/Assets/Sounds/Animals/Rhinoceros.wav"));
            Animals.Add(new Box("Rooster", "/Assets/Pics/Animals/rooster80x100.png", "/Assets/Pics/Animals/rooster480x800.png", "/Assets/Sounds/Animals/rooster.wav"));
            Animals.Add(new Box("Seagull", "/Assets/Pics/Animals/seagull80x100.png", "/Assets/Pics/Animals/seagull480x800.png", "/Assets/Sounds/Animals/seagulls.wav"));
            Animals.Add(new Box("Sheep", "/Assets/Pics/Animals/sheep80x100.png", "/Assets/Pics/Animals/sheep480x800.png", "/Assets/Sounds/Animals/sheep.wav"));
            Animals.Add(new Box("Snake", "/Assets/Pics/Animals/snake80x100.png", "/Assets/Pics/Animals/snake480x800.png", "/Assets/Sounds/Animals/snake.wav"));
            Animals.Add(new Box("Squirrel", "/Assets/Pics/Animals/squirrel80x100.png", "/Assets/Pics/Animals/squirrel480x800.png", "/Assets/Sounds/Animals/squirrel.wav"));
            Animals.Add(new Box("Tiger", "/Assets/Pics/Animals/tiger80x100.png", "/Assets/Pics/Animals/tiger480x800.png", "/Assets/Sounds/Animals/tiger.wav"));
            Animals.Add(new Box("Turkey", "/Assets/Pics/Animals/turkey80x100.png", "/Assets/Pics/Animals/turkey480x800.png", "/Assets/Sounds/Animals/turkey.wav"));
            Animals.Add(new Box("Walrus", "/Assets/Pics/Animals/walrus80x100.png", "/Assets/Pics/Animals/walrus480x800.png", "/Assets/Sounds/Animals/walrus.wav"));
            Animals.Add(new Box("Wolf", "/Assets/Pics/Animals/wolf80x100.png", "/Assets/Pics/Animals/wolf480x800.png", "/Assets/Sounds/Animals/wolf.wav"));
            Animals.Add(new Box("Zebra", "/Assets/Pics/Animals/zebra80x100.png", "/Assets/Pics/Animals/zebra480x800.png", "/Assets/Sounds/Animals/zebra.wav"));


            Animals.Add(new Box("", "", "", ""));
            Animals.Add(new Box("", "", "", ""));
            Animals.Add(new Box("", "", "", ""));
            Animals.Add(new Box("", "", "", ""));
            Animals.Add(new Box("", "", "", ""));
            Animals.Add(new Box("", "", "", ""));
            Animals.Add(new Box("", "", "", ""));
            Animals.Add(new Box("", "", "", ""));

            //TJY LOOK HERE:
            //I created a new "PivotSlide" data object, which has 9 "Box" Objects in it.
            //The Pivot control bindes to "PivotSlides" and uses a data template to bind to "Box1.ImageSourceSmall" etc.
            //The Pivot Data Template needs to be modified, and we need to make sure we don't run out of memory when loading the PivotSlides (I did run out of memory before).

            CartoonAnimals = new List<Box>();

            CartoonAnimals.Add(new Box("Alligator", "/Assets/Pics/BabyAnimals/alligator80x100.png", "/Assets/Pics/BabyAnimals/alligator480x800.png", "/Assets/Sounds/Animals/alligator.wav"));
            CartoonAnimals.Add(new Box("Anteater", "/Assets/Pics/BabyAnimals/anteater80x100.png", "/Assets/Pics/BabyAnimals/anteater480x800.png", "/Assets/Sounds/Animals/anteater.wav"));
            CartoonAnimals.Add(new Box("Badger", "/Assets/Pics/BabyAnimals/badger80x100.png", "/Assets/Pics/BabyAnimals/badger480x800.png", "/Assets/Sounds/Animals/badger.wav"));
            CartoonAnimals.Add(new Box("Bear", "/Assets/Pics/BabyAnimals/bear80x100.png", "/Assets/Pics/BabyAnimals/bear480x800.png", "/Assets/Sounds/Animals/bear.wav"));
            CartoonAnimals.Add(new Box("Camel", "/Assets/Pics/BabyAnimals/camel80x100.png", "/Assets/Pics/BabyAnimals/camel480x800.png", "/Assets/Sounds/Animals/camel.wav"));
            CartoonAnimals.Add(new Box("Cheetah", "/Assets/Pics/BabyAnimals/cheetah80x100.png", "/Assets/Pics/BabyAnimals/cheetah480x800.png", "/Assets/Sounds/Animals/cheetah.wav"));
            CartoonAnimals.Add(new Box("Crocodile", "/Assets/Pics/BabyAnimals/crocodile80x100.png", "/Assets/Pics/BabyAnimals/crocodile480x800.png", "/Assets/Sounds/Animals/crocodile.wav"));
            CartoonAnimals.Add(new Box("Deer", "/Assets/Pics/BabyAnimals/deer80x100.png", "/Assets/Pics/BabyAnimals/deer480x800.png", "/Assets/Sounds/Animals/deer.wav"));
            CartoonAnimals.Add(new Box("Dog", "/Assets/Pics/BabyAnimals/dog80x100.png", "/Assets/Pics/BabyAnimals/dog480x800.png", "/Assets/Sounds/Animals/dog.wav"));
            CartoonAnimals.Add(new Box("Dolphin", "/Assets/Pics/BabyAnimals/dolphin80x100.png", "/Assets/Pics/BabyAnimals/dolphin480x800.png", "/Assets/Sounds/Animals/dolphin.wav"));
            CartoonAnimals.Add(new Box("Donkey", "/Assets/Pics/BabyAnimals/donkey80x100.png", "/Assets/Pics/BabyAnimals/donkey480x800.png", "/Assets/Sounds/Animals/donkey.wav"));
            CartoonAnimals.Add(new Box("Duck", "/Assets/Pics/BabyAnimals/duck80x100.png", "/Assets/Pics/BabyAnimals/duck480x800.png", "/Assets/Sounds/Animals/duck.wav"));
            CartoonAnimals.Add(new Box("Eagle", "/Assets/Pics/BabyAnimals/eagle80x100.png", "/Assets/Pics/BabyAnimals/eagle480x800.png", "/Assets/Sounds/Animals/eagle.wav"));
            CartoonAnimals.Add(new Box("Elephant", "/Assets/Pics/BabyAnimals/elephant80x100.png", "/Assets/Pics/BabyAnimals/elephant480x800.png", "/Assets/Sounds/Animals/elephant.wav"));
            CartoonAnimals.Add(new Box("Elk", "/Assets/Pics/BabyAnimals/elk80x100.png", "/Assets/Pics/BabyAnimals/elk480x800.png", "/Assets/Sounds/Animals/elk.wav"));
            CartoonAnimals.Add(new Box("Frog", "/Assets/Pics/BabyAnimals/frog80x100.png", "/Assets/Pics/BabyAnimals/frog480x800.png", "/Assets/Sounds/Animals/frog.wav"));
            CartoonAnimals.Add(new Box("Hippopotamus", "/Assets/Pics/BabyAnimals/hippopotamus80x100.png", "/Assets/Pics/BabyAnimals/hippopotamus480x800.png", "/Assets/Sounds/Animals/hippopotamus.wav"));
            CartoonAnimals.Add(new Box("Horse", "/Assets/Pics/BabyAnimals/horse80x100.png", "/Assets/Pics/BabyAnimals/horse480x800.png", "/Assets/Sounds/Animals/horse.wav"));
            CartoonAnimals.Add(new Box("Jaguar", "/Assets/Pics/BabyAnimals/jaguar80x100.png", "/Assets/Pics/BabyAnimals/jaguar480x800.png", "/Assets/Sounds/Animals/jaguar.wav"));
            CartoonAnimals.Add(new Box("Kitten", "/Assets/Pics/BabyAnimals/kitten80x100.png", "/Assets/Pics/BabyAnimals/kitten480x800.png", "/Assets/Sounds/Animals/kitten.wav"));
            CartoonAnimals.Add(new Box("Koala", "/Assets/Pics/BabyAnimals/koala80x100.png", "/Assets/Pics/BabyAnimals/koala480x800.png", "/Assets/Sounds/Animals/koala.wav"));
            CartoonAnimals.Add(new Box("Lemur", "/Assets/Pics/BabyAnimals/lemur80x100.png", "/Assets/Pics/BabyAnimals/lemur480x800.png", "/Assets/Sounds/Animals/lemur.wav"));
            CartoonAnimals.Add(new Box("Leopard", "/Assets/Pics/BabyAnimals/leopard80x100.png", "/Assets/Pics/BabyAnimals/leopard480x800.png", "/Assets/Sounds/Animals/leopard.wav"));
            CartoonAnimals.Add(new Box("Llama", "/Assets/Pics/BabyAnimals/llama80x100.png", "/Assets/Pics/BabyAnimals/llama480x800.png", "/Assets/Sounds/Animals/llama.wav"));
            CartoonAnimals.Add(new Box("Lion", "/Assets/Pics/BabyAnimals/lion80x100.png", "/Assets/Pics/BabyAnimals/lion480x800.png", "/Assets/Sounds/Animals/lion.wav"));
            CartoonAnimals.Add(new Box("Macaw", "/Assets/Pics/BabyAnimals/macaw80x100.png", "/Assets/Pics/BabyAnimals/macaw480x800.png", "/Assets/Sounds/Animals/macaw.wav"));
            CartoonAnimals.Add(new Box("Monkey", "/Assets/Pics/BabyAnimals/monkey80x100.png", "/Assets/Pics/BabyAnimals/monkey480x800.png", "/Assets/Sounds/Animals/monkey.wav"));
            CartoonAnimals.Add(new Box("Orca", "/Assets/Pics/BabyAnimals/orca80x100.png", "/Assets/Pics/BabyAnimals/orca480x800.png", "/Assets/Sounds/Animals/orca.wav"));
            CartoonAnimals.Add(new Box("Owl", "/Assets/Pics/BabyAnimals/owl80x100.png", "/Assets/Pics/BabyAnimals/owl480x800.png", "/Assets/Sounds/Animals/owl.wav"));
            CartoonAnimals.Add(new Box("Panda", "/Assets/Pics/BabyAnimals/panda80x100.png", "/Assets/Pics/BabyAnimals/panda480x800.png", "/Assets/Sounds/Animals/panda.wav"));
            CartoonAnimals.Add(new Box("Penguin", "/Assets/Pics/BabyAnimals/penguin80x100.png", "/Assets/Pics/BabyAnimals/penguin480x800.png", "/Assets/Sounds/Animals/penguin.wav"));
            CartoonAnimals.Add(new Box("Pig", "/Assets/Pics/BabyAnimals/pig80x100.png", "/Assets/Pics/BabyAnimals/pig480x800.png", "/Assets/Sounds/Animals/pig.wav"));
            CartoonAnimals.Add(new Box("Polarbear", "/Assets/Pics/BabyAnimals/polarbear80x100.png", "/Assets/Pics/BabyAnimals/polarbear480x800.png", "/Assets/Sounds/Animals/polarbear.wav"));
            CartoonAnimals.Add(new Box("Racoon", "/Assets/Pics/BabyAnimals/racoon80x100.png", "/Assets/Pics/BabyAnimals/racoon480x800.png", "/Assets/Sounds/Animals/racoon.wav"));
            CartoonAnimals.Add(new Box("Redfox", "/Assets/Pics/BabyAnimals/redfox80x100.png", "/Assets/Pics/BabyAnimals/redfox480x800.png", "/Assets/Sounds/Animals/redfox.wav"));
            CartoonAnimals.Add(new Box("Rhino", "/Assets/Pics/BabyAnimals/rhino80x100.png", "/Assets/Pics/BabyAnimals/rhino480x800.png", "/Assets/Sounds/Animals/Rhinoceros.wav"));
            CartoonAnimals.Add(new Box("Rooster", "/Assets/Pics/BabyAnimals/rooster80x100.png", "/Assets/Pics/BabyAnimals/rooster480x800.png", "/Assets/Sounds/Animals/rooster.wav"));
            CartoonAnimals.Add(new Box("Seagull", "/Assets/Pics/BabyAnimals/seagull80x100.png", "/Assets/Pics/BabyAnimals/seagull480x800.png", "/Assets/Sounds/Animals/seagulls.wav"));
            CartoonAnimals.Add(new Box("Sheep", "/Assets/Pics/BabyAnimals/sheep80x100.png", "/Assets/Pics/BabyAnimals/sheep480x800.png", "/Assets/Sounds/Animals/sheep.wav"));
            CartoonAnimals.Add(new Box("Snake", "/Assets/Pics/BabyAnimals/snake80x100.png", "/Assets/Pics/BabyAnimals/snake480x800.png", "/Assets/Sounds/Animals/snake.wav"));
            CartoonAnimals.Add(new Box("Squirrel", "/Assets/Pics/BabyAnimals/squirrel80x100.png", "/Assets/Pics/BabyAnimals/squirrel480x800.png", "/Assets/Sounds/Animals/squirrel.wav"));
            CartoonAnimals.Add(new Box("Tiger", "/Assets/Pics/BabyAnimals/tiger80x100.png", "/Assets/Pics/BabyAnimals/tiger480x800.png", "/Assets/Sounds/Animals/tiger.wav"));
            CartoonAnimals.Add(new Box("Turkey", "/Assets/Pics/BabyAnimals/turkey80x100.png", "/Assets/Pics/BabyAnimals/turkey480x800.png", "/Assets/Sounds/Animals/turkey.wav"));
            CartoonAnimals.Add(new Box("Walrus", "/Assets/Pics/BabyAnimals/walrus80x100.png", "/Assets/Pics/BabyAnimals/walrus480x800.png", "/Assets/Sounds/Animals/walrus.wav"));
            CartoonAnimals.Add(new Box("Wolf", "/Assets/Pics/BabyAnimals/wolf80x100.png", "/Assets/Pics/BabyAnimals/wolf480x800.png", "/Assets/Sounds/Animals/wolf.wav"));
            CartoonAnimals.Add(new Box("Zebra", "/Assets/Pics/BabyAnimals/zebra80x100.png", "/Assets/Pics/BabyAnimals/zebra480x800.png", "/Assets/Sounds/Animals/zebra.wav"));
        }

        private void SetPivots(string category)
        {
            Box blankBox = new Box("", "", "", "");

            switch (category)
            {
                case "CartoonAnimals":
                    PivotSlides.Clear();
                    PivotSlides.Add(new PivotSlide("Slide1", CartoonAnimals[0], CartoonAnimals[1], CartoonAnimals[2], CartoonAnimals[3], CartoonAnimals[4], CartoonAnimals[5], CartoonAnimals[6], CartoonAnimals[7], CartoonAnimals[8]));
                    PivotSlides.Add(new PivotSlide("Slide2", CartoonAnimals[9], CartoonAnimals[10], CartoonAnimals[11], CartoonAnimals[12], CartoonAnimals[13], CartoonAnimals[14], CartoonAnimals[15], CartoonAnimals[16], CartoonAnimals[17]));
                    PivotSlides.Add(new PivotSlide("Slide3", CartoonAnimals[18], CartoonAnimals[19], CartoonAnimals[20], CartoonAnimals[21], CartoonAnimals[22], CartoonAnimals[23], CartoonAnimals[24], CartoonAnimals[25], CartoonAnimals[26]));
                    PivotSlides.Add(new PivotSlide("Slide4", CartoonAnimals[27], CartoonAnimals[28], CartoonAnimals[29], CartoonAnimals[30], CartoonAnimals[31], CartoonAnimals[32], CartoonAnimals[33], CartoonAnimals[34], CartoonAnimals[35]));
                    PivotSlides.Add(new PivotSlide("Slide5", CartoonAnimals[36], CartoonAnimals[37], CartoonAnimals[38], CartoonAnimals[39], CartoonAnimals[40], CartoonAnimals[41], CartoonAnimals[42], CartoonAnimals[43], CartoonAnimals[44]));
                    //          PivotSlides.Add(new PivotSlide("Slide6", CartoonAnimals[45], CartoonAnimals[46], CartoonAnimals[47], CartoonAnimals[48], CartoonAnimals[49], CartoonAnimals[50], CartoonAnimals[51], CartoonAnimals[52], CartoonAnimals[53]));
                    //          PivotSlides.Add(new PivotSlide("Slide7", CartoonAnimals[54], CartoonAnimals[55], CartoonAnimals[56], CartoonAnimals[57], CartoonAnimals[58], CartoonAnimals[59], CartoonAnimals[60], CartoonAnimals[61], CartoonAnimals[62]));

                    break;
                case "Animals":
                    PivotSlides.Clear();
                    PivotSlides.Add(new PivotSlide("Slide1", Animals[0], Animals[1], Animals[2], Animals[3], Animals[4], Animals[5], Animals[6], Animals[7], Animals[8]));
                    PivotSlides.Add(new PivotSlide("Slide2", Animals[9], Animals[10], Animals[11], Animals[12], Animals[13], Animals[14], Animals[15], Animals[16], Animals[17]));
                    PivotSlides.Add(new PivotSlide("Slide3", Animals[18], Animals[19], Animals[20], Animals[21], Animals[22], Animals[23], Animals[24], Animals[25], Animals[26]));
                    PivotSlides.Add(new PivotSlide("Slide4", Animals[27], Animals[28], Animals[29], Animals[30], Animals[31], Animals[32], Animals[33], Animals[34], Animals[35]));
                    PivotSlides.Add(new PivotSlide("Slide5", Animals[36], Animals[37], Animals[38], Animals[39], Animals[40], Animals[41], Animals[42], Animals[43], Animals[44]));
                    break;
            }
        }

        private void OnePicSlideShowAsync(Box selectedPic, int tokenNumber)
        {
            if (!_cancellationTokens[tokenNumber].IsCancellationRequested)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    this.PictureGrid.Visibility = Visibility.Collapsed;
                    this.SlideShow.Visibility = Visibility.Visible;
                    this.TitlePanel.Visibility = Visibility.Collapsed;
                });
            }

            if (!_cancellationTokens[tokenNumber].IsCancellationRequested)
            {
                Task.Factory.StartNew(() => SetOnePicGrid(selectedPic, tokenNumber)).Wait();
            }

            if (!_cancellationTokens[tokenNumber].IsCancellationRequested)
            {
                Dispatcher.BeginInvoke(() =>
                {

                    this.PictureGrid.Visibility = Visibility.Visible;
                    this.SlideShow.Visibility = Visibility.Collapsed;
                    this.TitlePanel.Visibility = Visibility.Visible;
                    pivotControl.Focus();
                });
            }
        }

        private void SetOnePicGrid(Box selectedPic, int tokenNumber)
        {
            if (!_cancellationTokens[tokenNumber].IsCancellationRequested)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    ImageSourceSmall = selectedPic.ImageSourceSmall;
                    ImageSourceLarge = selectedPic.ImageSourceLarge;
                    Description = selectedPic.Description;
                    DisplayDescription = GetTextDesription(selectedPic.Description);
                    ImageSound = selectedPic.SoundSource;
                });
            }

            if (!_cancellationTokens[tokenNumber].IsCancellationRequested)
            {
                //TJY I needed to add a small sleep value here because the above properites being in an  ASYNC block were not being set by
                //the time PlayVoiceTextAndSound was being executed.
                Thread.Sleep(250);
            }

            if (!_cancellationTokens[tokenNumber].IsCancellationRequested)
            {
                PlayVoiceTextAndSound(tokenNumber);
            }
        }

        private void NavigateToScreen(Screen screenToGoTo)
        {
            switch (screenToGoTo)
            {
                case Screen.MainGrid:
                    this.PictureGrid.Visibility = Visibility.Visible;
                    this.SlideShow.Visibility = Visibility.Collapsed;
                    this.TitlePanel.Visibility = Visibility.Visible;
                    break;
                case Screen.SlideShow:
                    this.PictureGrid.Visibility = Visibility.Collapsed;
                    this.SlideShow.Visibility = Visibility.Visible;
                    this.TitlePanel.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private Screen GetDisplayedScreen()
        {
            Screen returnValue = Screen.MainGrid;

            if (this.PictureGrid.Visibility == System.Windows.Visibility.Visible)
            {
                returnValue = Screen.MainGrid;
            }
            if (this.SlideShow.Visibility == System.Windows.Visibility.Visible)
            {
                returnValue = Screen.SlideShow;
            }

            return returnValue;
        }

        private void PlaySlideShowAsync(int tokenNumber)
        {
            List<Box> continuousPlayList = new List<Box>();

            switch (App.gCategory)
            {
                case "CartoonAnimals":
                    continuousPlayList = CartoonAnimals;
                    break;
                case "Animals":
                    continuousPlayList = Animals;
                    break;
            }

            for (int i = 0; i < continuousPlayList.Count - 1; i++)
            {
                if (!_cancellationTokens[tokenNumber].IsCancellationRequested)
                {
                    Dispatcher.BeginInvoke(() =>
                    {

                        ImageSourceSmall = continuousPlayList[i].ImageSourceSmall;
                        ImageSourceLarge = continuousPlayList[i].ImageSourceLarge;
                        Description = continuousPlayList[i].Description;
                        DisplayDescription = GetTextDesription(continuousPlayList[i].Description);
                        ImageSound = continuousPlayList[i].SoundSource;
                    });
                }

                if (!_cancellationTokens[tokenNumber].IsCancellationRequested)
                {
                    Thread.Sleep(250);
                }

                PlayVoiceTextAndSound(tokenNumber);

                if (_cancellationTokens[tokenNumber].IsCancellationRequested)
                {
                    break;
                }
            }
            //set so user can re-start slide-show
            _stopSlideShow = false;
        }

        private Box GetBoxFromPivotAndTag(string tagName, PivotSlide slide)
        {
            Box returnValue = null;

            switch (tagName.ToUpper())
            {
                case "BOX1":
                    returnValue = slide.Box1;
                    break;
                case "BOX2":
                    returnValue = slide.Box2;
                    break;
                case "BOX3":
                    returnValue = slide.Box3;
                    break;
                case "BOX4":
                    returnValue = slide.Box4;
                    break;
                case "BOX5":
                    returnValue = slide.Box5;
                    break;
                case "BOX6":
                    returnValue = slide.Box6;
                    break;
                case "BOX7":
                    returnValue = slide.Box7;
                    break;
                case "BOX8":
                    returnValue = slide.Box8;
                    break;
                case "BOX9":
                    returnValue = slide.Box9;
                    break;
            }

            return returnValue;
        }

        #endregion "Methods"

        #region "VoiceSound Methods"

        //await casues execution to be suspended until the SpeakTextAsynch
        //private async void PlayText()
        private void PlayVoiceText(int tokenNumber)
        {
            string voiceLanguage = "en-US";

            try
            {
                foreach (string language in App.gLanguages)
                {
                    if (_cancellationTokens[tokenNumber].IsCancellationRequested)
                    {
                        break;
                    }

                    switch (language)
                    {
                        case "English":
                            voiceLanguage = "en-US";
                            //   IEnumerable<VoiceInformation> englishVoices = from voice in InstalledVoices.All where voice.Language == "en-US" && voice.Gender == VoiceGender.Female select voice;
                            //    synthesizer.SetVoice(englishVoices.ElementAt(0));
                            break;
                        case "Spanish":
                            voiceLanguage = "es-ES";
                            //  IEnumerable<VoiceInformation> spanishVoices = from voice in InstalledVoices.All where voice.Language == "es-ES" && voice.Gender == VoiceGender.Female select voice;
                            //   synthesizer.SetVoice(spanishVoices.ElementAt(0));
                            break;
                        case "French":
                            voiceLanguage = "fr-FR";
                            //  IEnumerable<VoiceInformation> frenchVoices = from voice in InstalledVoices.All where voice.Language == "fr-FR" && voice.Gender == VoiceGender.Female select voice;
                            //  synthesizer.SetVoice(frenchVoices.ElementAt(0));
                            break;
                        case "Chinese":
                            voiceLanguage = "zh-HK";
                            //   IEnumerable<VoiceInformation> chineseVoices = from voice in InstalledVoices.All where voice.Language == "zh-HK" && voice.Gender == VoiceGender.Female select voice;
                            //   synthesizer.SetVoice(chineseVoices.ElementAt(0));
                            break;
                        case "Italian":
                            voiceLanguage = "it-IT";
                            // IEnumerable<VoiceInformation> italianVoices = from voice in InstalledVoices.All where voice.Language == "it-IT" && voice.Gender == VoiceGender.Female select voice;
                            // synthesizer.SetVoice(italianVoices.ElementAt(0));
                            break;
                        case "German":
                            voiceLanguage = "de-DE";
                            // IEnumerable<VoiceInformation> germanVoices = from voice in InstalledVoices.All where voice.Language == "de-DE" && voice.Gender == VoiceGender.Female select voice;
                            //  synthesizer.SetVoice(germanVoices.ElementAt(0));
                            break;
                        case "Portuguese":
                            voiceLanguage = "pt-BR";
                            //  IEnumerable<VoiceInformation> portugueseVoices = from voice in InstalledVoices.All where voice.Language == "pt-BR" && voice.Gender == VoiceGender.Female select voice;
                            //  synthesizer.SetVoice(portugueseVoices.ElementAt(0));
                            break;
                        case "Japanese":
                            voiceLanguage = "ja-JP";
                            // IEnumerable<VoiceInformation> JapaneseVoices = from voice in InstalledVoices.All where voice.Language == "ja-JP" && voice.Gender == VoiceGender.Female select voice;
                            //   synthesizer.SetVoice(JapaneseVoices.ElementAt(0));
                            break;
                        case "Polish":
                            voiceLanguage = "pl-PL";
                            break;
                    }

                    if (Description != null)
                    {
                        // await synthesizer.SpeakTextAsync(App.gDisplayDescription);
                        //synthesizer.SpeakTextAsync(GetTextTranslation(language, Description));
                        synthesizer.SpeakSsmlAsync(VoiceOptions.GetText(GetTextTranslation(language, Description), Pitch.Default, Speed.Slow, SpeakerVolume.ExtraLoud, voiceLanguage));
                    }
                    if (!_cancellationTokens[tokenNumber].IsCancellationRequested)
                    {
                        Thread.Sleep(2000);
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        //media element allows for pausing sound, Soundeffect does not allow for pause/stop so NOT good for background music
        private void PlaySoundViaMediaElement(int tokenNumber)
        {
            try
            {
                if (!_cancellationTokens[tokenNumber].IsCancellationRequested)
                {
                    if (myBoundSound.CurrentState == System.Windows.Media.MediaElementState.Playing)
                        myBoundSound.Pause();
                    else
                        myBoundSound.Play();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("PlaySoundViaMediaElement - Error playing sound = " + ImageSound.Substring(1) + ". Error = " + ex.Message);

            }
        }

        //media element allows for pausing sound, Soundeffect does not allow for pause/stop so NOT good for background music
        private void PlaySoundViaSoundEffect(int tokenNumber)
        {
            try
            {
                if (!_cancellationTokens[tokenNumber].IsCancellationRequested)
                {
                    var soundfile = ImageSound.Substring(1); //Note no slash before the Assets folder, and it's a WAV file!
                    Stream stream = TitleContainer.OpenStream(soundfile);
                    SoundEffect effect = SoundEffect.FromStream(stream);
                    FrameworkDispatcher.Update();
                    effect.Play();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("PlaySoundViaSoundEffect - Error playing sound = " + ImageSound.Substring(1) + ". Error = " + ex.Message);
            }
        }

        private string GetTextDesription(string textDescription)
        {
            string returnValue = "No languages selected";
            try
            {
                if (App.gShowTextSetting == "On")
                {
                    foreach (string language in App.gLanguages)
                    {
                        if (returnValue == "No languages selected")
                        {
                            returnValue = GetTextTranslation(language, textDescription);
                        }
                        else
                        {
                            returnValue = returnValue + "   " + GetTextTranslation(language, textDescription);
                        }
                    }
                }
                else //for now show 'Text Display Off' just so we know we correctly identified 'Show Text' was off, when publishing simply show nothing.
                {
                    returnValue = "Text Display Off";
                }

            }
            catch (Exception)
            {
                returnValue = textDescription;

            }

            return returnValue;
        }

        private string GetTextTranslation(string language, string textToTranslate)
        {
            string returnValue = string.Empty;
            try
            {

                switch (language)
                {
                    case "English":
                        AppResources.Culture = new System.Globalization.CultureInfo("en");
                        break;
                    case "Spanish":
                        AppResources.Culture = new System.Globalization.CultureInfo("es");
                        break;
                    case "French":
                        AppResources.Culture = new System.Globalization.CultureInfo("fr");
                        break;
                    case "Polish":
                        AppResources.Culture = new System.Globalization.CultureInfo("pl-PL");
                        break;
                    case "Chinese":
                        AppResources.Culture = new System.Globalization.CultureInfo("zh-Hant");
                        break;
                    case "Italian":
                        AppResources.Culture = new System.Globalization.CultureInfo("it");
                        break;
                    case "German":
                        AppResources.Culture = new System.Globalization.CultureInfo("de");
                        break;
                    case "Portuguese":
                        AppResources.Culture = new System.Globalization.CultureInfo("pt");
                        break;
                    case "Japanese":
                        AppResources.Culture = new System.Globalization.CultureInfo("ja-JP");
                        break;
                }
                returnValue = GetText(textToTranslate);
            }
            catch (Exception)
            {
                AppResources.Culture = new System.Globalization.CultureInfo("en");
                return textToTranslate;
            }

            AppResources.Culture = new System.Globalization.CultureInfo("en");
            return returnValue;
        }

        //TJY TO DO, for EVERY pic we need an entry here....
        private string GetText(string textToTranslate)
        {
            string returnValue = string.Empty;
            try
            {
                switch (textToTranslate)
                {
                    case "Alligator":
                        returnValue = AppResources.Alligator;
                        break;
                    case "Anteater":
                        returnValue = AppResources.Anteater;
                        break;
                    case "Antelope":
                        returnValue = AppResources.Antelope;
                        break;
                    case "Badger":
                        returnValue = AppResources.Badger;
                        break;
                    case "Bear":
                        returnValue = AppResources.Bear;
                        break;
                    case "Bee":
                        returnValue = AppResources.Bee;
                        break;
                    case "Butterfly":
                        returnValue = AppResources.Butterfly;
                        break;
                    case "Camel":
                        returnValue = AppResources.Camel;
                        break;
                    case "Capuchinmonkey":
                        returnValue = AppResources.Capuchinmonkey;
                        break;
                    case "Caribou":
                        returnValue = AppResources.Caribou;
                        break;
                    case "Chameleon":
                        returnValue = AppResources.Chameleon;
                        break;
                    case "Cheetah":
                        returnValue = AppResources.Cheetah;
                        break;
                    case "Chimpanzee":
                        returnValue = AppResources.Chimpanzee;
                        break;
                    case "Cougar":
                        returnValue = AppResources.Cougar;
                        break;
                    case "Cow":
                        returnValue = AppResources.Cow;
                        break;
                    case "Crab":
                        returnValue = AppResources.Crab;
                        break;
                    case "Crocodile":
                        returnValue = AppResources.Crocodile;
                        break;
                    case "Deer":
                        returnValue = AppResources.Deer;
                        break;
                    case "Dog":
                        returnValue = AppResources.Dog;
                        break;
                    case "Dolphin":
                        returnValue = AppResources.Dolphin;
                        break;
                    case "Donkey":
                        returnValue = AppResources.Donkey;
                        break;
                    case "DragonFly":
                        returnValue = AppResources.DragonFly;
                        break;
                    case "Duck":
                        returnValue = AppResources.Duck;
                        break;
                    case "Eagle":
                        returnValue = AppResources.Eagle;
                        break;
                    case "Elephant":
                        returnValue = AppResources.Elephant;
                        break;
                    case "Elk":
                        returnValue = AppResources.Elk;
                        break;
                    case "Fish":
                        returnValue = AppResources.Fish;
                        break;
                    case "Fox":
                        returnValue = AppResources.Fox;
                        break;
                    case "Frog":
                        returnValue = AppResources.Frog;
                        break;
                    case "Giraffe":
                        returnValue = AppResources.Giraffe;
                        break;
                    case "Goat":
                        returnValue = AppResources.Goat;
                        break;
                    case "Gorilla":
                        returnValue = AppResources.Gorilla;
                        break;
                    case "Guineapig":
                        returnValue = AppResources.Guineapig;
                        break;
                    case "Horse":
                        returnValue = AppResources.Horse;
                        break;
                    case "Hippopotamus":
                        returnValue = AppResources.Hippopotamus;
                        break;
                    case "Hawk":
                        returnValue = AppResources.Hawk;
                        break;
                    case "Iguana":
                        returnValue = AppResources.Iguana;
                        break;
                    case "Jaguar":
                        returnValue = AppResources.Jaguar;
                        break;
                    case "Kangaroo":
                        returnValue = AppResources.Kangaroo;
                        break;
                    case "Kitten":
                        returnValue = AppResources.Kitten;
                        break;
                    case "Koala":
                        returnValue = AppResources.Koala;
                        break;
                    case "Lemur":
                        returnValue = AppResources.Lemur;
                        break;
                    case "Leopard":
                        returnValue = AppResources.Leopard;
                        break;
                    case "Lion":
                        returnValue = AppResources.Lion;
                        break;
                    case "Llama":
                        returnValue = AppResources.Llama;
                        break;
                    case "Macaw":
                        returnValue = AppResources.Macaw;
                        break;
                    case "Marmot":
                        returnValue = AppResources.Marmot;
                        break;
                    case "Monkey":
                        returnValue = AppResources.Monkey;
                        break;
                    case "Mosquito":
                        returnValue = AppResources.Mosquito;
                        break;
                    case "Ocelot":
                        returnValue = AppResources.Ocelot;
                        break;
                    case "Octopus":
                        returnValue = AppResources.Octopus;
                        break;
                    case "Orca":
                        returnValue = AppResources.Orca;
                        break;
                    case "Otter":
                        returnValue = AppResources.Otter;
                        break;
                    case "Owl":
                        returnValue = AppResources.Owl;
                        break;
                    case "Panda":
                        returnValue = AppResources.Panda;
                        break;
                    case "Penguin":
                        returnValue = AppResources.Penguin;
                        break;
                    case "Pig":
                        returnValue = AppResources.Pig;
                        break;
                    case "Polarbear":
                        returnValue = AppResources.Polarbear;
                        break;
                    case "Prayingmantis":
                        returnValue = AppResources.Prayingmantis;
                        break;
                    case "Puma":
                        returnValue = AppResources.Puma;
                        break;
                    case "Rabbit":
                        returnValue = AppResources.Rabbit;
                        break;
                    case "Raccoon":
                        returnValue = AppResources.Raccoon;
                        break;
                    case "Redfox":
                        returnValue = AppResources.Redfox;
                        break;
                    case "Redpanda":
                        returnValue = AppResources.Redpanda;
                        break;
                    case "Rhino":
                        returnValue = AppResources.Rhino;
                        break;
                    case "Rooster":
                        returnValue = AppResources.Rooster;
                        break;
                    case "Seagull":
                        returnValue = AppResources.Seagull;
                        break;
                    case "Shark":
                        returnValue = AppResources.Shark;
                        break;
                    case "Sheep":
                        returnValue = AppResources.Sheep;
                        break;
                    case "Skink":
                        returnValue = AppResources.Skink;
                        break;
                    case "Snake":
                        returnValue = AppResources.Snake;
                        break;
                    case "Snowleopard":
                        returnValue = AppResources.Snowleopard;
                        break;
                    case "Squid":
                        returnValue = AppResources.Squid;
                        break;
                    case "Squirrel":
                        returnValue = AppResources.Squirrel;
                        break;
                    case "Starfish":
                        returnValue = AppResources.Starfish;
                        break;
                    case "Sumatrantiger":
                        returnValue = AppResources.Sumatrantiger;
                        break;
                    case "Tayra":
                        returnValue = AppResources.Tayra;
                        break;
                    case "Tiger":
                        returnValue = AppResources.Tiger;
                        break;
                    case "Tortoise":
                        returnValue = AppResources.Tortoise;
                        break;
                    case "Turkey":
                        returnValue = AppResources.Turkey;
                        break;
                    case "Walrus":
                        returnValue = AppResources.Walrus;
                        break;
                    case "Wolf":
                        returnValue = AppResources.Wolf;
                        break;
                    case "Zebra":
                        returnValue = AppResources.Zebra;
                        break;
                }
            }
            catch (Exception)
            {
                return returnValue;
            }
            return returnValue;
        }

        private void PlayVoiceTextAndSound(int tokenNumber)
        {
            if (!_cancellationTokens[tokenNumber].IsCancellationRequested)
            {
                PlayVoiceText(tokenNumber);
            }

            if (!_cancellationTokens[tokenNumber].IsCancellationRequested)
            {
                if (App.gPlaySoundSetting == "On")
                {
                    Dispatcher.BeginInvoke(() =>
                    {
                        // PlaySoundViaSoundEffect(tokenNumber);
                        PlaySoundViaMediaElement(tokenNumber);
                    });

                    Thread.Sleep(5000);

                    Dispatcher.BeginInvoke(() =>
                    {
                        myBoundSound.Pause();
                    });
                }
            }
        }

        #endregion "VoiceSound Methods"

        #region "Properties"

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<PivotSlide> _pivotSlides;
        public ObservableCollection<PivotSlide> PivotSlides
        {
            get { return _pivotSlides; }
            set { _pivotSlides = value; NotifyPropertyChanged("PivotSlides"); }
        }

        private List<Box> _cartoonAnimals;
        public List<Box> CartoonAnimals
        {
            get { return _cartoonAnimals; }
            set { _cartoonAnimals = value; NotifyPropertyChanged("CartoonAnimals"); }
        }

        private List<Box> _babyMisc;
        public List<Box> BabyMisc
        {
            get { return _babyMisc; }
            set { _babyMisc = value; NotifyPropertyChanged("BabyMisc"); }
        }

        private List<Box> _animals;
        public List<Box> Animals
        {
            get { return _animals; }
            set { _animals = value; NotifyPropertyChanged("Animals"); }
        }

        private List<Box> _misc;
        public List<Box> Misc
        {
            get { return _misc; }
            set { _misc = value; NotifyPropertyChanged("Misc"); }
        }

        private string _box1Description;
        public string Box1Description
        {
            get { return _box1Description; }
            set { _box1Description = value; NotifyPropertyChanged("Box1Description"); }
        }

        private string _box1ImageSourceSmall;
        public string Box1ImageSourceSmall
        {
            get { return _box1ImageSourceSmall; }
            set { _box1ImageSourceSmall = value; NotifyPropertyChanged("Box1ImageSourceSmall"); }
        }

        private string _box1ImageSourceLarge;
        public string Box1ImageSourceLarge
        {
            get { return _box1ImageSourceLarge; }
            set { _box1ImageSourceLarge = value; NotifyPropertyChanged("Box1ImageSourceLarge"); }
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

        private string _box2ImageSourceSmall;
        public string Box2ImageSourceSmall
        {
            get { return _box2ImageSourceSmall; }
            set { _box2ImageSourceSmall = value; NotifyPropertyChanged("Box2ImageSourceSmall"); }
        }

        private string _box2ImageSourceLarge;
        public string Box2ImageSourceLarge
        {
            get { return _box2ImageSourceLarge; }
            set { _box2ImageSourceLarge = value; NotifyPropertyChanged("Box2ImageSourceLarge"); }
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

        private string _box3ImageSourceSmall;
        public string Box3ImageSourceSmall
        {
            get { return _box3ImageSourceSmall; }
            set { _box3ImageSourceSmall = value; NotifyPropertyChanged("Box3ImageSourceSmall"); }
        }

        private string _box3ImageSourceLarge;
        public string Box3ImageSourceLarge
        {
            get { return _box3ImageSourceLarge; }
            set { _box3ImageSourceLarge = value; NotifyPropertyChanged("Box3ImageSourceLarge"); }
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

        private string _box4ImageSourceSmall;
        public string Box4ImageSourceSmall
        {
            get { return _box4ImageSourceSmall; }
            set { _box4ImageSourceSmall = value; NotifyPropertyChanged("Box4ImageSourceSmall"); }
        }

        private string _box4ImageSourceLarge;
        public string Box4ImageSourceLarge
        {
            get { return _box4ImageSourceLarge; }
            set { _box4ImageSourceLarge = value; NotifyPropertyChanged("Box4ImageSourceLarge"); }
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

        private string _box5ImageSourceSmall;
        public string Box5ImageSourceSmall
        {
            get { return _box5ImageSourceSmall; }
            set { _box5ImageSourceSmall = value; NotifyPropertyChanged("Box5ImageSourceSmall"); }
        }

        private string _box5ImageSourceLarge;
        public string Box5ImageSourceLarge
        {
            get { return _box5ImageSourceLarge; }
            set { _box5ImageSourceLarge = value; NotifyPropertyChanged("Box5ImageSourceLarge"); }
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

        private string _box6ImageSourceSmall;
        public string Box6ImageSourceSmall
        {
            get { return _box6ImageSourceSmall; }
            set { _box6ImageSourceSmall = value; NotifyPropertyChanged("Box6ImageSourceSmall"); }
        }

        private string _box6ImageSourceLarge;
        public string Box6ImageSourceLarge
        {
            get { return _box6ImageSourceLarge; }
            set { _box6ImageSourceLarge = value; NotifyPropertyChanged("Box6ImageSourceLarge"); }
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

        private string _box7ImageSourceSmall;
        public string Box7ImageSourceSmall
        {
            get { return _box7ImageSourceSmall; }
            set { _box7ImageSourceSmall = value; NotifyPropertyChanged("Box7ImageSourceSmall"); }
        }

        private string _box7ImageSourceLarge;
        public string Box7ImageSourceLarge
        {
            get { return _box7ImageSourceLarge; }
            set { _box7ImageSourceLarge = value; NotifyPropertyChanged("Box7ImageSourceLarge"); }
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

        private string _box8ImageSourceSmall;
        public string Box8ImageSourceSmall
        {
            get { return _box8ImageSourceSmall; }
            set { _box8ImageSourceSmall = value; NotifyPropertyChanged("Box8ImageSourceSmall"); }
        }

        private string _box8ImageSourceLarge;
        public string Box8ImageSourceLarge
        {
            get { return _box8ImageSourceLarge; }
            set { _box8ImageSourceLarge = value; NotifyPropertyChanged("Box8ImageSourceLarge"); }
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

        private string _box9ImageSourceSmall;
        public string Box9ImageSourceSmall
        {
            get { return _box9ImageSourceSmall; }
            set { _box9ImageSourceSmall = value; NotifyPropertyChanged("Box9ImageSourceSmall"); }
        }

        private string _box9ImageSourceLarge;
        public string Box9ImageSourceLarge
        {
            get { return _box9ImageSourceLarge; }
            set { _box9ImageSourceLarge = value; NotifyPropertyChanged("Box9ImageSourceLarge"); }
        }

        private string _box9SoundSource;
        public string Box9SoundSource
        {
            get { return _box9SoundSource; }
            set { _box9SoundSource = value; NotifyPropertyChanged("Box9SoundSource"); }
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

        private string _imageSound;
        public string ImageSound
        {
            get { return _imageSound; }
            set { _imageSound = value; NotifyPropertyChanged("ImageSound"); }
        }

        private string _displayDescription;
        public string DisplayDescription
        {
            get { return _displayDescription; }
            set { _displayDescription = value; NotifyPropertyChanged("DisplayDescription"); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; NotifyPropertyChanged("Description"); }
        }

        private Screen _mode;
        public Screen Mode
        {
            get { return _mode; }
            set { _mode = value; NotifyPropertyChanged("Mode"); }
        }

        #endregion "Properties"

        #region "Events"


        Dictionary<int, CancellationToken> _cancellationTokens = new Dictionary<int, CancellationToken>();
        Dictionary<int, CancellationTokenSource> _cancellationTokenSources = new Dictionary<int, CancellationTokenSource>();

        CancellationToken _cancellationToken = new CancellationToken();
        CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private void Box_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //ImageSound = "/Assets/Sounds/Animals/badger.wav";
                //if (myBoundSound.CurrentState == System.Windows.Media.MediaElementState.Playing)
                //    myBoundSound.Pause();
                //else
                //    myBoundSound.Play();

                //myBoundSound.Pause();

                foreach (var row in _cancellationTokenSources.Keys)
                {
                    _cancellationTokenSources[row].Cancel();
                }

                CancellationToken token;
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                token = tokenSource.Token;

                _cancellationTokens.Add(_cancellationTokens.Keys.Count(), token);
                _cancellationTokenSources.Add(_cancellationTokens.Keys.Count(), tokenSource);

                _backButtonPressedStopSounds = false;
                Button btn = sender as Button;
                string imageTag = btn.Tag.ToString();
                PivotSlide selectedPivot = (PivotSlide)btn.DataContext;

                Task.Factory.StartNew(() => OnePicSlideShowAsync(GetBoxFromPivotAndTag(imageTag, selectedPivot), _cancellationTokens.Keys.Count() - 1), token);
            }
            catch (Exception ex)
            {

            }
        }


        //Button btn = sender as Button;
        //string imageTag = btn.Tag.ToString();
        private void ContiniousPlay_Click(object sender, EventArgs e)
        {
            switch (Mode)
            {
                case Screen.MainGrid:
                    Mode = Screen.SlideShow;
                //    appBarButton1.IconUri = new Uri("/Assets/transport.pause.png", UriKind.Relative);
                    appBarButton1.IsEnabled = false;
                    NavigateToScreen(Screen.SlideShow);

                    _slideShowInProgress = true;
                    //Start the slide-show
                    CancellationToken token;
                    CancellationTokenSource tokenSource = new CancellationTokenSource();

                    token = tokenSource.Token;

                    _cancellationTokens.Add(_cancellationTokens.Keys.Count(), token);
                    _cancellationTokenSources.Add(_cancellationTokens.Keys.Count(), tokenSource);

                    Task.Factory.StartNew(() => PlaySlideShowAsync(_cancellationTokens.Keys.Count() - 1), token);

                    break;
                case Screen.SlideShow:
                    Mode = Screen.MainGrid;
                   // appBarButton1.IconUri = new Uri("/Assets/transport.play.png", UriKind.Relative);
                    appBarButton1.IsEnabled = true;
                    //stop the slideshow
                    _stopSlideShow = true;
                    _slideShowInProgress = false;
                    NavigateToScreen(Screen.MainGrid);
                    break;
            }
        }

        private void CartoonAnimals_Click(object sender, EventArgs e)
        {
            SetPivots("CartoonAnimals");
        }

        private void BabyMisc_Click(object sender, EventArgs e)
        {
            SetPivots("BabyMisc");
        }

        private void Animals_Click(object sender, EventArgs e)
        {
            SetPivots("Animals");
        }

        private void Misc_Click(object sender, EventArgs e)
        {
            SetPivots("Misc");
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

                //TJY Had to make appBarButton1 global because we are enabling and disabling it when user clicks it...
                //     ApplicationBarIconButton appBarButton1 = new ApplicationBarIconButton(new Uri("/Assets/transport.play.png", UriKind.Relative));
          
                appBarButton1.Text = AppResources.AppBarMenuPlayButton;
                ApplicationBar.Buttons.Add(appBarButton1);
                appBarButton1.Click += new EventHandler(ContiniousPlay_Click);
                appBarButton1.IsEnabled = true;

                ApplicationBarIconButton appBarButton2 = new ApplicationBarIconButton(new Uri("/Assets/Pics/BabyAnimals/Elephant80x100.png", UriKind.Relative));
                appBarButton2.Text = AppResources.Cartoon;
                ApplicationBar.Buttons.Add(appBarButton2);
                appBarButton2.Click += new EventHandler(CartoonAnimals_Click);

                ApplicationBarIconButton appBarButton3 = new ApplicationBarIconButton(new Uri("/Assets/Pics/BabyAnimals/Dog80x100.png", UriKind.Relative));
                appBarButton3.Text = AppResources.Real;
                ApplicationBar.Buttons.Add(appBarButton3);
                appBarButton3.Click += new EventHandler(Animals_Click);

                // Create a new menu item with the localized string from AppResources.
                ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppMenuItemOptions);
                ApplicationBar.MenuItems.Add(appBarMenuItem);
                appBarMenuItem.Click += new EventHandler(Options_Click);

                ApplicationBarMenuItem appBarMenuItem2 = new ApplicationBarMenuItem(AppResources.AppMenuItemAbout);
                ApplicationBar.MenuItems.Add(appBarMenuItem2);
                appBarMenuItem2.Click += new EventHandler(About_Click);

                //Only add the "rate" button if the app has not been rated yet.
                if (!_rated)
                {
                    ApplicationBarMenuItem appBarMenuItem3 = new ApplicationBarMenuItem(AppResources.Review);
                    ApplicationBar.MenuItems.Add(appBarMenuItem3);
                    appBarMenuItem3.Click += new EventHandler(Review_Click);
                }

                ApplicationBarMenuItem appBarMenuItem4 = new ApplicationBarMenuItem(AppResources.AppMenuItemMoreApps);
                ApplicationBar.MenuItems.Add(appBarMenuItem4);
                appBarMenuItem4.Click += new EventHandler(MoreApps_Click);
            }
            catch (Exception ex)
            {
            }
        }

        #endregion "Common Routines"

        private bool _backButtonPressedStopSounds = false;
        private void BackButtonClicked(object sender, CancelEventArgs e)
        {
            if (GetDisplayedScreen() == Screen.SlideShow)
            {
                NavigateToScreen(Screen.MainGrid);
                Mode = Screen.MainGrid;

                foreach (var row in _cancellationTokenSources.Keys)
                {
                    _cancellationTokenSources[row].Cancel();
                }
              //  appBarButton1.IconUri = new Uri("/Assets/transport.play.png", UriKind.Relative);
                appBarButton1.IsEnabled = true;
                e.Cancel = true;
            }
        }

    }
}