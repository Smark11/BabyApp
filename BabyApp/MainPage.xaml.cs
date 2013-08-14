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


namespace BabyApp
{
    public partial class MainPage : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //    DispatcherTimer _timer;
        SpeechSynthesizer synthesizer;

        private bool _slideShowInProgress = false;
        private bool _stopSlideShow = false;


        public enum Screen
        {
            MainGrid,
            SlideShow
        }

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            PivotSlides = new ObservableCollection<PivotSlide>();

            synthesizer = new SpeechSynthesizer();
            this.DataContext = this;

            NavigateToScreen(Screen.MainGrid);
            LoadPicsIntoCollection();
            SetPivots(App.gCategory);
            BuildLocalizedApplicationBar();
            ButtonDisplay = "/Assets/transport.play.png";
            Mode = Screen.MainGrid;
        }

        #region "Methods"
       
        private void LoadPicsIntoCollection()
        {
            Animals = new List<Box>();

            Animals.Add(new Box("Anteater", "/Assets/Pics/Animals/anteater80x100.png", "/Assets/Pics/Animals/anteater480x800.png", "/Assets/Sounds/Animals/anteater.wav"));
            Animals.Add(new Box("Antelope", "/Assets/Pics/Animals/antelope80x100.png", "/Assets/Pics/Animals/antelope480x800.png", "/Assets/Sounds/Animals/antelope.wav"));
            Animals.Add(new Box("Badger", "/Assets/Pics/Animals/badger80x100.png", "/Assets/Pics/Animals/badger480x800.png", "/Assets/Sounds/Animals/badger.wav"));
            Animals.Add(new Box("Bear", "/Assets/Pics/Animals/bear80x100.png", "/Assets/Pics/Animals/bear480x800.png", "/Assets/Sounds/Animals/bear.wav"));
            Animals.Add(new Box("Butterfly", "/Assets/Pics/Animals/butterfly80x100.png", "/Assets/Pics/Animals/butterfly480x800.png", "/Assets/Sounds/Animals/butterfly.wav"));
            Animals.Add(new Box("Capuchinmonkey", "/Assets/Pics/Animals/capuchinmonkey80x100.png", "/Assets/Pics/Animals/capuchinmonkey480x800.png", "/Assets/Sounds/Animals/capuchinmonkey.wav"));
            Animals.Add(new Box("Caribou", "/Assets/Pics/Animals/caribou80x100.png", "/Assets/Pics/Animals/caribou480x800.png", "/Assets/Sounds/Animals/caribou.wav"));
            Animals.Add(new Box("Chameleon", "/Assets/Pics/Animals/chameleon80x100.png", "/Assets/Pics/Animals/chameleon480x800.png", "/Assets/Sounds/Animals/chameleon.wav"));
            Animals.Add(new Box("Cheetah", "/Assets/Pics/Animals/cheetah80x100.png", "/Assets/Pics/Animals/cheetah480x800.png", "/Assets/Sounds/Animals/cheetah.wav"));
            Animals.Add(new Box("Crab", "/Assets/Pics/Animals/crab80x100.png", "/Assets/Pics/Animals/crab480x800.png", "/Assets/Sounds/Animals/crab.wav"));
            Animals.Add(new Box("Dog", "/Assets/Pics/Animals/dog80x100.png", "/Assets/Pics/Animals/dog480x800.png", "/Assets/Sounds/Animals/dog.wav"));
            Animals.Add(new Box("Dolphin", "/Assets/Pics/Animals/dolphin80x100.png", "/Assets/Pics/Animals/dolphin480x800.png", "/Assets/Sounds/Animals/dolphin.wav"));
            Animals.Add(new Box("Dragonfly", "/Assets/Pics/Animals/dragonfly80x100.png", "/Assets/Pics/Animals/dragonfly480x800.png", "/Assets/Sounds/Animals/dragonfly.wav"));
            Animals.Add(new Box("Duck", "/Assets/Pics/Animals/duck80x100.png", "/Assets/Pics/Animals/duck480x800.png", "/Assets/Sounds/Animals/duck.wav"));
            Animals.Add(new Box("Eagle", "/Assets/Pics/Animals/eagle80x100.png", "/Assets/Pics/Animals/eagle480x800.png", "/Assets/Sounds/Animals/eagle.wav"));
            Animals.Add(new Box("Elephant", "/Assets/Pics/Animals/elephant80x100.png", "/Assets/Pics/Animals/elephant480x800.png", "/Assets/Sounds/Animals/elephant.wav"));
            Animals.Add(new Box("Elk", "/Assets/Pics/Animals/elk80x100.png", "/Assets/Pics/Animals/elk480x800.png", "/Assets/Sounds/Animals/elk.wav"));
            Animals.Add(new Box("Fish", "/Assets/Pics/Animals/fish80x100.png", "/Assets/Pics/Animals/fish480x800.png", "/Assets/Sounds/Animals/fish.wav"));
            Animals.Add(new Box("Fox", "/Assets/Pics/Animals/fox80x100.png", "/Assets/Pics/Animals/fox480x800.png", "/Assets/Sounds/Animals/fox.wav"));
            Animals.Add(new Box("Giraffe", "/Assets/Pics/Animals/giraffe80x100.png", "/Assets/Pics/Animals/giraffe480x800.png", "/Assets/Sounds/Animals/giraffe.wav"));
            Animals.Add(new Box("Guineapig", "/Assets/Pics/Animals/guineapig80x100.png", "/Assets/Pics/Animals/guineapig480x800.png", "/Assets/Sounds/Animals/guineapig.wav"));
            Animals.Add(new Box("Hippopotamus", "/Assets/Pics/Animals/hippopotamus80x100.png", "/Assets/Pics/Animals/hippopotamus480x800.png", "/Assets/Sounds/Animals/hippopotamus.wav"));
            Animals.Add(new Box("Horse", "/Assets/Pics/Animals/horse80x100.png", "/Assets/Pics/Animals/horse480x800.png", "/Assets/Sounds/Animals/horse.wav"));
            Animals.Add(new Box("Iguana", "/Assets/Pics/Animals/iguana80x100.png", "/Assets/Pics/Animals/iguana480x800.png", "/Assets/Sounds/Animals/iguana.wav"));
            Animals.Add(new Box("Jaguar", "/Assets/Pics/Animals/jaguar80x100.png", "/Assets/Pics/Animals/jaguar480x800.png", "/Assets/Sounds/Animals/jaguar.wav"));
            Animals.Add(new Box("Kangaroo", "/Assets/Pics/Animals/kangaroo80x100.png", "/Assets/Pics/Animals/kangaroo480x800.png", "/Assets/Sounds/Animals/kangaroo.wav"));
            Animals.Add(new Box("Kitten", "/Assets/Pics/Animals/kitten80x100.png", "/Assets/Pics/Animals/kitten480x800.png", "/Assets/Sounds/Animals/kitten.wav"));
            Animals.Add(new Box("Koala", "/Assets/Pics/Animals/koala80x100.png", "/Assets/Pics/Animals/koala480x800.png", "/Assets/Sounds/Animals/koala.wav"));
            Animals.Add(new Box("Lemur", "/Assets/Pics/Animals/lemur80x100.png", "/Assets/Pics/Animals/lemur480x800.png", "/Assets/Sounds/Animals/lemur.wav"));
            Animals.Add(new Box("Lion", "/Assets/Pics/Animals/lion80x100.png", "/Assets/Pics/Animals/lion480x800.png", "/Assets/Sounds/Animals/lion.wav"));
            Animals.Add(new Box("Macaw", "/Assets/Pics/Animals/macaw80x100.png", "/Assets/Pics/Animals/macaw480x800.png", "/Assets/Sounds/Animals/macaw.wav"));
            Animals.Add(new Box("Marmot", "/Assets/Pics/Animals/marmot80x100.png", "/Assets/Pics/Animals/marmot480x800.png", "/Assets/Sounds/Animals/marmot.wav"));
            Animals.Add(new Box("Monkey", "/Assets/Pics/Animals/monkey80x100.png", "/Assets/Pics/Animals/monkey480x800.png", "/Assets/Sounds/Animals/monkey.wav"));
            Animals.Add(new Box("Ocelot", "/Assets/Pics/Animals/ocelot80x100.png", "/Assets/Pics/Animals/ocelot480x800.png", "/Assets/Sounds/Animals/ocelot.wav"));
            Animals.Add(new Box("Octopus", "/Assets/Pics/Animals/octopus80x100.png", "/Assets/Pics/Animals/octopus480x800.png", "/Assets/Sounds/Animals/octopus.wav"));
            Animals.Add(new Box("Orca", "/Assets/Pics/Animals/orca80x100.png", "/Assets/Pics/Animals/orca480x800.png", "/Assets/Sounds/Animals/orca.wav"));
            Animals.Add(new Box("Otter", "/Assets/Pics/Animals/otter80x100.png", "/Assets/Pics/Animals/otter480x800.png", "/Assets/Sounds/Animals/otter.wav"));
            Animals.Add(new Box("Owl", "/Assets/Pics/Animals/owl80x100.png", "/Assets/Pics/Animals/owl480x800.png", "/Assets/Sounds/Animals/owl.wav"));
            Animals.Add(new Box("Panda", "/Assets/Pics/Animals/panda80x100.png", "/Assets/Pics/Animals/panda480x800.png", "/Assets/Sounds/Animals/panda.wav"));
            Animals.Add(new Box("Penguin", "/Assets/Pics/Animals/penguin80x100.png", "/Assets/Pics/Animals/penguin480x800.png", "/Assets/Sounds/Animals/penguin.wav"));
            Animals.Add(new Box("Pig", "/Assets/Pics/Animals/pig80x100.png", "/Assets/Pics/Animals/pig480x800.png", "/Assets/Sounds/Animals/pig.wav"));
            Animals.Add(new Box("Prayingmantis", "/Assets/Pics/Animals/prayingmantis80x100.png", "/Assets/Pics/Animals/prayingmantis480x800.png", "/Assets/Sounds/Animals/prayingmantis.wav"));
            Animals.Add(new Box("Rabbit", "/Assets/Pics/Animals/rabbit80x100.png", "/Assets/Pics/Animals/rabbit480x800.png", "/Assets/Sounds/Animals/rabbit.wav"));
            Animals.Add(new Box("Raccoon", "/Assets/Pics/Animals/raccoon80x100.png", "/Assets/Pics/Animals/raccoon480x800.png", "/Assets/Sounds/Animals/raccoon.wav"));
            Animals.Add(new Box("Redfox", "/Assets/Pics/Animals/redfox80x100.png", "/Assets/Pics/Animals/redfox480x800.png", "/Assets/Sounds/Animals/redfox.wav"));
            Animals.Add(new Box("Redpanda", "/Assets/Pics/Animals/redpanda80x100.png", "/Assets/Pics/Animals/redpanda480x800.png", "/Assets/Sounds/Animals/redpanda.wav"));
            Animals.Add(new Box("Rhino", "/Assets/Pics/Animals/rhino80x100.png", "/Assets/Pics/Animals/rhino480x800.png", "/Assets/Sounds/Animals/rhino.wav"));
            Animals.Add(new Box("Shark", "/Assets/Pics/Animals/shark80x100.png", "/Assets/Pics/Animals/shark480x800.png", "/Assets/Sounds/Animals/shark.wav"));
            Animals.Add(new Box("Sheep", "/Assets/Pics/Animals/sheep80x100.png", "/Assets/Pics/Animals/sheep480x800.png", "/Assets/Sounds/Animals/sheep.wav"));
            Animals.Add(new Box("Skink", "/Assets/Pics/Animals/skink80x100.png", "/Assets/Pics/Animals/skink480x800.png", "/Assets/Sounds/Animals/skink.wav"));
            Animals.Add(new Box("Snake", "/Assets/Pics/Animals/snake80x100.png", "/Assets/Pics/Animals/snake480x800.png", "/Assets/Sounds/Animals/snake.wav"));
            Animals.Add(new Box("Snowleopard", "/Assets/Pics/Animals/snowleopard80x100.png", "/Assets/Pics/Animals/snowleopard480x800.png", "/Assets/Sounds/Animals/snowleopard.wav"));
            Animals.Add(new Box("Squid", "/Assets/Pics/Animals/squid80x100.png", "/Assets/Pics/Animals/squid480x800.png", "/Assets/Sounds/Animals/squid.wav"));
            Animals.Add(new Box("Squirrel", "/Assets/Pics/Animals/squirrel80x100.png", "/Assets/Pics/Animals/squirrel480x800.png", "/Assets/Sounds/Animals/squirrel.wav"));
            Animals.Add(new Box("Starfish", "/Assets/Pics/Animals/starfish80x100.png", "/Assets/Pics/Animals/starfish480x800.png", "/Assets/Sounds/Animals/starfish.wav"));
            Animals.Add(new Box("Sumatrantiger", "/Assets/Pics/Animals/sumatrantiger80x100.png", "/Assets/Pics/Animals/sumatrantiger480x800.png", "/Assets/Sounds/Animals/sumatrantiger.wav"));
            Animals.Add(new Box("Tayra", "/Assets/Pics/Animals/tayra80x100.png", "/Assets/Pics/Animals/tayra480x800.png", "/Assets/Sounds/Animals/tayra.wav"));
            Animals.Add(new Box("Tiger", "/Assets/Pics/Animals/tiger80x100.png", "/Assets/Pics/Animals/tiger480x800.png", "/Assets/Sounds/Animals/tiger.wav"));
            Animals.Add(new Box("Tortoise", "/Assets/Pics/Animals/tortoise80x100.png", "/Assets/Pics/Animals/tortoise480x800.png", "/Assets/Sounds/Animals/tortoise.wav"));
            Animals.Add(new Box("Walrus", "/Assets/Pics/Animals/walrus80x100.png", "/Assets/Pics/Animals/walrus480x800.png", "/Assets/Sounds/Animals/walrus.wav"));
            Animals.Add(new Box("Wolf", "/Assets/Pics/Animals/wolf80x100.png", "/Assets/Pics/Animals/wolf480x800.png", "/Assets/Sounds/Animals/wolf.wav"));
            Animals.Add(new Box("", "", "", ""));
            Animals.Add(new Box("", "", "", ""));

            //TJY LOOK HERE:
            //I created a new "PivotSlide" data object, which has 9 "Box" Objects in it.
            //The Pivot control bindes to "PivotSlides" and uses a data template to bind to "Box1.ImageSourceSmall" etc.
            //The Pivot Data Template needs to be modified, and we need to make sure we don't run out of memory when loading the PivotSlides (I did run out of memory before).

            CartoonAnimals = new List<Box>();

            CartoonAnimals.Add(new Box("Dog", "/Assets/Pics/BabyAnimals/dog80x100.png", "/Assets/Pics/Animals/dog480x800.png", "/Assets/Sounds/Animals/dog.wav"));
            CartoonAnimals.Add(new Box("Dolphin", "/Assets/Pics/BabyAnimals/dolphin80x100.png", "/Assets/Pics/Animals/dolphin480x800.png", "/Assets/Sounds/Animals/dolphin.wav"));
            CartoonAnimals.Add(new Box("Elephant", "/Assets/Pics/BabyAnimals/elephant80x100.png", "/Assets/Pics/Animals/elephant480x800.png", "/Assets/Sounds/Animals/elephant.wav"));
            CartoonAnimals.Add(new Box("Frog", "/Assets/Pics/BabyAnimals/frog80x100.png", "/Assets/Pics/Animals/frog480x800.png", "/Assets/Sounds/Animals/frog.wav"));
            CartoonAnimals.Add(new Box("Owl", "/Assets/Pics/BabyAnimals/owl80x100.png", "/Assets/Pics/Animals/owl480x800.png", "/Assets/Sounds/Animals/owl.wav"));
            CartoonAnimals.Add(new Box("Pig", "/Assets/Pics/BabyAnimals/pig80x100.png", "/Assets/Pics/Animals/pig480x800.png", "/Assets/Sounds/Animals/pig.wav"));
            CartoonAnimals.Add(new Box("", "", "", ""));
            CartoonAnimals.Add(new Box("", "", "", ""));
            CartoonAnimals.Add(new Box("", "", "", ""));
        }

        private void SetPivots(string category)
        {
            Box blankBox = new Box("", "", "", "");

            switch (category)
            {
                case "CartoonAnimals":
                    PivotSlides.Clear();
                    PivotSlides.Add(new PivotSlide("Slide1", CartoonAnimals[0], CartoonAnimals[1], CartoonAnimals[2], CartoonAnimals[3], CartoonAnimals[4], CartoonAnimals[5], CartoonAnimals[6], CartoonAnimals[7], CartoonAnimals[8]));
                    break;
                case "Animals":
                    PivotSlides.Clear();
                    PivotSlides.Add(new PivotSlide("Slide1", Animals[0], Animals[1], Animals[2], Animals[3], Animals[4], Animals[5], Animals[6], Animals[7], Animals[8]));
                    PivotSlides.Add(new PivotSlide("Slide2", Animals[9], Animals[10], Animals[11], Animals[12], Animals[13], Animals[14], Animals[15], Animals[16], Animals[17]));
                    PivotSlides.Add(new PivotSlide("Slide3", Animals[18], Animals[19], Animals[20], Animals[21], Animals[22], Animals[23], Animals[24], Animals[25], Animals[26]));
                    PivotSlides.Add(new PivotSlide("Slide4", Animals[27], Animals[28], Animals[29], Animals[30], Animals[31], Animals[32], Animals[33], Animals[34], Animals[35]));
                    PivotSlides.Add(new PivotSlide("Slide5", Animals[36], Animals[37], Animals[38], Animals[39], Animals[40], Animals[41], Animals[42], Animals[43], Animals[44]));
                    PivotSlides.Add(new PivotSlide("Slide6", Animals[45], Animals[46], Animals[47], Animals[48], Animals[49], Animals[50], Animals[51], Animals[52], Animals[53]));
                    PivotSlides.Add(new PivotSlide("Slide7", Animals[54], Animals[55], Animals[56], Animals[57], Animals[58], Animals[59], Animals[60], Animals[61], Animals[62]));

                    break;
            }
        }

        private void OnePicSlideShowAsync(Box selectedPic)
        {
            Dispatcher.BeginInvoke(() =>
            {
                this.PictureGrid.Visibility = Visibility.Collapsed;
                this.SlideShow.Visibility = Visibility.Visible;
            });

            SetOnePicGrid(selectedPic);

            Dispatcher.BeginInvoke(() =>
            {

                this.PictureGrid.Visibility = Visibility.Visible;
                this.SlideShow.Visibility = Visibility.Collapsed;
            });
        }

        private void SetOnePicGrid(Box selectedPic)
        {
            Dispatcher.BeginInvoke(() =>
            {
                ImageSourceSmall = selectedPic.ImageSourceSmall;
                ImageSourceLarge = selectedPic.ImageSourceLarge;
                Description = selectedPic.Description;
                DisplayDescription = GetTextDesription(selectedPic.Description);
                ImageSound = selectedPic.SoundSource;
            });

            //TJY I needed to add a small sleep value here because the above properites being in an  ASYNC block were not being set by
            //the time PlayVoiceTextAndSound was being executed.
            Thread.Sleep(250);

            PlayVoiceTextAndSound();
        }

        private void NavigateToScreen(Screen screenToGoTo)
        {
            switch (screenToGoTo)
            {
                case Screen.MainGrid:
                    this.PictureGrid.Visibility = Visibility.Visible;
                    this.SlideShow.Visibility = Visibility.Collapsed;
                    break;
                case Screen.SlideShow:
                    this.PictureGrid.Visibility = Visibility.Collapsed;
                    this.SlideShow.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void PlaySlideShowAsync()
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
                Dispatcher.BeginInvoke(() =>
                {
                   
                    ImageSourceSmall = continuousPlayList[i].ImageSourceSmall;
                    ImageSourceLarge = continuousPlayList[i].ImageSourceLarge;
                    Description = continuousPlayList[i].Description;
                    DisplayDescription = GetTextDesription(continuousPlayList[i].Description);
                    ImageSound = continuousPlayList[i].SoundSource;                                  
                });

                Thread.Sleep(250);

                PlayVoiceTextAndSound();

                if (_stopSlideShow)
                {
                    //stop the slide show, user said so
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
        private void PlayVoiceText()
        {
            string voiceLanguage = "en-US";

            try
            {
                foreach (string language in App.gLanguages)
                {

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
                    Thread.Sleep(2000);

                }
            }
            catch (Exception ex)
            {


            }
        }

        //media element allows for pausing sound, Soundeffect does not allow for pause/stop so NOT good for background music
        private void PlayMusic()
        {
            try
            {
                if (myBoundSound.CurrentState == System.Windows.Media.MediaElementState.Playing)
                    myBoundSound.Pause();
                else
                    myBoundSound.Play();
            }
            catch (Exception ex)
            {
            }
        }

        //media element allows for pausing sound, Soundeffect does not allow for pause/stop so NOT good for background music
        private void PlaySound()
        {
            try
            {
                var soundfile = ImageSound.Substring(1); //Note no slash before the Assets folder, and it's a WAV file!
                Stream stream = TitleContainer.OpenStream(soundfile);
                SoundEffect effect = SoundEffect.FromStream(stream);
                FrameworkDispatcher.Update();
                effect.Play();
            }
            catch (Exception ex)
            {
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
                        AppResources.Culture = new System.Globalization.CultureInfo("pl");
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
                        AppResources.Culture = new System.Globalization.CultureInfo("ja");
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
                    case "Butterfly":
                        returnValue = AppResources.Butterfly;
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
                    case "Crab":
                        returnValue = AppResources.Crab;
                        break;
                    case "Dog":
                        returnValue = AppResources.Dog;
                        break;
                    case "Dolphin":
                        returnValue = AppResources.Dolphin;
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
                    case "Giraffe":
                        returnValue = AppResources.Giraffe;
                        break;
                    case "Guineapig":
                        returnValue = AppResources.Guineapig;
                        break;
                    case "Hippopotamus":
                        returnValue = AppResources.Hippopotamus;
                        break;
                    case "Horse":
                        returnValue = AppResources.Horse;
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
                    case "Lion":
                        returnValue = AppResources.Lion;
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
                    case "Prayingmantis":
                        returnValue = AppResources.Prayingmantis;
                        break;
                    case "Rabbit":
                        returnValue = AppResources.Rabbit;
                        break;
                    case "Racoon":
                        returnValue = AppResources.Racoon;
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
                    case "Walrus":
                        returnValue = AppResources.Walrus;
                        break;
                    case "Wolf":
                        returnValue = AppResources.Wolf;
                        break;
                }
            }
            catch (Exception)
            {
                return returnValue;
            }
            return returnValue;
        }

        private void PlayVoiceTextAndSound()
        {
            PlayVoiceText();

            if (App.gPlaySoundSetting == "On")
            {
                PlaySound();
                Thread.Sleep(5000);
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

        private string _buttonDisplay;
        public string ButtonDisplay
        {
            get { return _buttonDisplay; }
            set { _buttonDisplay = value; NotifyPropertyChanged("ButtonDisplay"); }
        }

        private Screen _mode;
        public Screen Mode
        {
            get { return _mode; }
            set { _mode = value; NotifyPropertyChanged("Mode"); }
        }

        #endregion "Properties"

        #region "Events"

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
           // base.OnBackKeyPress(e);

           // NavigationService.Navigate(new Uri("/Options.xaml", UriKind.Relative));
        }  


        private void Box_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                string imageTag = btn.Tag.ToString();
                PivotSlide selectedPivot = (PivotSlide)btn.DataContext;

                Task.Factory.StartNew(() => OnePicSlideShowAsync(GetBoxFromPivotAndTag(imageTag, selectedPivot)));
            }
            catch (Exception ex)
            {

            }
        }

        private void ContiniousPlay_Click(object sender, EventArgs e)
        {
            switch (Mode)
            {
                case Screen.MainGrid:
                    Mode = Screen.SlideShow;
                    ButtonDisplay = "/Assets/transport.pause.png";
                    NavigateToScreen(Screen.SlideShow);

                    _slideShowInProgress = true;
                    //Start the slide-show
                    Task.Factory.StartNew(PlaySlideShowAsync);

                    break;
                case Screen.SlideShow:
                    Mode = Screen.MainGrid;
                    ButtonDisplay = "/Assets/transport.play.png";
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
                ApplicationBarIconButton appBarButton1 = new ApplicationBarIconButton(new Uri("/Assets/Pics/BabyAnimals/Elephant80x100.png", UriKind.Relative));
                appBarButton1.Text = "Cartoon";
                ApplicationBar.Buttons.Add(appBarButton1);
                appBarButton1.Click += new EventHandler(CartoonAnimals_Click);

                ApplicationBarIconButton appBarButton2 = new ApplicationBarIconButton(new Uri("/Assets/Pics/BabyAnimals/Dog80x100.png", UriKind.Relative));
                appBarButton2.Text = "Real";
                ApplicationBar.Buttons.Add(appBarButton2);
                appBarButton2.Click += new EventHandler(Animals_Click);

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