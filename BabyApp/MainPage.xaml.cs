﻿using System;
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
            LoadPicsOnScreen(App.gCategory);
            BuildLocalizedApplicationBar();
            ButtonDisplay = "/Assets/transport.play.png";
            Mode = Screen.MainGrid;
        }

        #region "Methods"

        private void LoadPicsIntoCollection()
        {
            BabyAnimals = new List<Box>();

            BabyAnimals.Add(new Box("Elephant", "/Assets/Pics/BabyAnimals/Elephant80x100.png", "/Assets/Pics/BabyAnimals/Elephant480x800.png", "/Assets/Sounds/Animals/Elephant.wav"));
            BabyAnimals.Add(new Box("Pig", "/Assets/Pics/BabyAnimals/Pig80x100.png", "/Assets/Pics/BabyAnimals/Pig480x800.png", "/Assets/Sounds/Animals/Pig.wav"));


            BabyAnimals.Add(new Box("Bear", "/Assets/Pics/BabyAnimals/Bear.png", "/Assets/Pics/BabyAnimals/Bear.png", "/Assets/Sounds/Animals/Bear.wav"));
            BabyAnimals.Add(new Box("Bison", "/Assets/Pics/BabyAnimals/Bison.png", "/Assets/Pics/BabyAnimals/Bison.png", "/Assets/Sounds/Animals/Bison.wav"));
            BabyAnimals.Add(new Box("Cat", "/Assets/Pics/BabyAnimals/Cat.png", "/Assets/Pics/BabyAnimals/Cat.png", "/Assets/Sounds/Animals/Cat.wav"));
            BabyAnimals.Add(new Box("Chick", "/Assets/Pics/BabyAnimals/Chick.png", "/Assets/Pics/BabyAnimals/Chick.png", "/Assets/Sounds/Animals/Chick.wav"));
            BabyAnimals.Add(new Box("Cow", "/Assets/Pics/BabyAnimals/Cow.png", "/Assets/Pics/BabyAnimals/Cow.png", "/Assets/Sounds/Animals/Cow.wav"));
            BabyAnimals.Add(new Box("Deer", "/Assets/Pics/BabyAnimals/Deer.png", "/Assets/Pics/BabyAnimals/Deer.png", "/Assets/Sounds/Animals/Deer.wav"));
            BabyAnimals.Add(new Box("Dolphin", "/Assets/Pics/BabyAnimals/Dolphin.png", "/Assets/Pics/BabyAnimals/Dolphin.png", "/Assets/Sounds/Animals/Dolphin.wav"));
            BabyAnimals.Add(new Box("Duck", "/Assets/Pics/BabyAnimals/Duck.png", "/Assets/Pics/BabyAnimals/Duck.png", "/Assets/Sounds/Animals/Duck.wav"));
            BabyAnimals.Add(new Box("Fish", "/Assets/Pics/BabyAnimals/Fish.png", "/Assets/Pics/BabyAnimals/Fish.png", "/Assets/Sounds/Animals/Fish.wav"));
            BabyAnimals.Add(new Box("Frog", "/Assets/Pics/BabyAnimals/Frog.png", "/Assets/Pics/BabyAnimals/Frog.png", "/Assets/Sounds/Animals/Frog.wav"));
            BabyAnimals.Add(new Box("Giraffe", "/Assets/Pics/BabyAnimals/Giraffe.png", "/Assets/Pics/BabyAnimals/Giraffe.png", "/Assets/Sounds/Animals/Giraffe.wav"));
            BabyAnimals.Add(new Box("Goat", "/Assets/Pics/BabyAnimals/Goat.png", "/Assets/Pics/BabyAnimals/Goat.png", "/Assets/Sounds/Animals/Goat.wav"));
            BabyAnimals.Add(new Box("Hippo", "/Assets/Pics/BabyAnimals/Hippo.png", "/Assets/Pics/BabyAnimals/Hippo.png", "/Assets/Sounds/Animals/Hippo.wav"));
            BabyAnimals.Add(new Box("Monkey", "/Assets/Pics/BabyAnimals/Monkey.png", "/Assets/Pics/BabyAnimals/Monkey.png", "/Assets/Sounds/Animals/Monkey.wav"));
            BabyAnimals.Add(new Box("Mouse", "/Assets/Pics/BabyAnimals/Mouse.png", "/Assets/Pics/BabyAnimals/Mouse.png", "/Assets/Sounds/Animals/Mouse.wav"));
            BabyAnimals.Add(new Box("Owl", "/Assets/Pics/BabyAnimals/Owl.png", "/Assets/Pics/BabyAnimals/Owl.png", "/Assets/Sounds/Animals/Owl.wav"));
            BabyAnimals.Add(new Box("Panda", "/Assets/Pics/BabyAnimals/Panda.png", "/Assets/Pics/BabyAnimals/Panda.png", "/Assets/Sounds/Animals/Panda.wav"));
            BabyAnimals.Add(new Box("Penguin", "/Assets/Pics/BabyAnimals/Penquin.png", "/Assets/Pics/BabyAnimals/Penquin.png", "/Assets/Sounds/Animals/Penquin.wav"));
            BabyAnimals.Add(new Box("Pig", "/Assets/Pics/BabyAnimals/Pig.png", "/Assets/Pics/BabyAnimals/Pig.png", "/Assets/Sounds/Animals/Pig.wav"));
            BabyAnimals.Add(new Box("Rabbit", "/Assets/Pics/BabyAnimals/Rabbit.png", "/Assets/Pics/BabyAnimals/Rabbit.png", "/Assets/Sounds/Animals/Rabbit.wav"));
            BabyAnimals.Add(new Box("Seal", "/Assets/Pics/BabyAnimals/Seal.png", "/Assets/Pics/BabyAnimals/Seal.png", "/Assets/Sounds/Animals/Seal.wav"));
            BabyAnimals.Add(new Box("Sheep", "/Assets/Pics/BabyAnimals/Sheep.png", "/Assets/Pics/BabyAnimals/Sheep.png", "/Assets/Sounds/Animals/Sheep.wav"));
            BabyAnimals.Add(new Box("Tiger", "/Assets/Pics/BabyAnimals/Tiger.png", "/Assets/Pics/BabyAnimals/Tiger.png", "/Assets/Sounds/Animals/Tiger.wav"));
            BabyAnimals.Add(new Box("Turkey", "/Assets/Pics/BabyAnimals/Turkey.png", "/Assets/Pics/BabyAnimals/Turkey.png", "/Assets/Sounds/Animals/Turkey.wav"));
            BabyAnimals.Add(new Box("Zebra", "/Assets/Pics/BabyAnimals/Zebra.png", "/Assets/Pics/BabyAnimals/Zebra.png", "/Assets/Sounds/Animals/Zebra.wav"));

            //TJY LOOK HERE:
            //I created a new "PivotSlide" data object, which has 9 "Box" Objects in it.
            //The Pivot control bindes to "PivotSlides" and uses a data template to bind to "Box1.ImageSourceSmall" etc.
            //The Pivot Data Template needs to be modified, and we need to make sure we don't run out of memory when loading the PivotSlides (I did run out of memory before).
            PivotSlides.Add(new PivotSlide("Slide1", BabyAnimals[0], BabyAnimals[1], BabyAnimals[2], BabyAnimals[3], BabyAnimals[4], BabyAnimals[5], BabyAnimals[6], BabyAnimals[7], BabyAnimals[8]));
            PivotSlides.Add(new PivotSlide("Slide2", BabyAnimals[9], BabyAnimals[10], BabyAnimals[11], BabyAnimals[12], BabyAnimals[13], BabyAnimals[14], BabyAnimals[15], BabyAnimals[16], BabyAnimals[17]));
            PivotSlides.Add(new PivotSlide("Slide3", BabyAnimals[18], BabyAnimals[19], BabyAnimals[20], BabyAnimals[21], BabyAnimals[22], BabyAnimals[23], BabyAnimals[24], BabyAnimals[25], BabyAnimals[26]));


            BabyMisc = new List<Box>();

            BabyMisc.Add(new Box("Airpane", "/Assets/Pics/BabyMisc/Airplane.png", "/Assets/Pics/BabyMisc/Airplane.png", "/Assets/Sounds/Misc/Airplane.wav"));
            BabyMisc.Add(new Box("Ambulance", "/Assets/Pics/BabyMisc/Ambulance.png", "/Assets/Pics/BabyMisc/Ambulance.png", "/Assets/Sounds/Misc/Ambulance.wav"));
            BabyMisc.Add(new Box("Bike", "/Assets/Pics/BabyMisc/Bike.png", "/Assets/Pics/BabyMisc/Bike.png", "/Assets/Sounds/Misc/Bike.wav"));
            BabyMisc.Add(new Box("Camera", "/Assets/Pics/BabyMisc/Camera.png", "/Assets/Pics/BabyMisc/Camera.png", "/Assets/Sounds/Misc/Camera.wav"));
            BabyMisc.Add(new Box("Car", "/Assets/Pics/BabyMisc/Car.png", "/Assets/Pics/BabyMisc/Car.png", "/Assets/Sounds/Misc/Car.wav"));
            BabyMisc.Add(new Box("Clock", "/Assets/Pics/BabyMisc/Clock.png", "/Assets/Pics/BabyMisc/clock.png", "/Assets/Sounds/Misc/Clock.wav"));
            BabyMisc.Add(new Box("Firetruck", "/Assets/Pics/BabyMisc/Firetruck.png", "/Assets/Pics/BabyMisc/Firetruck.png", "/Assets/Sounds/Misc/Firetruck.wav"));
            BabyMisc.Add(new Box("Iron", "/Assets/Pics/BabyMisc/Iron.png", "/Assets/Pics/BabyMisc/Iron.png", "/Assets/Sounds/Misc/Iron.wav"));
            BabyMisc.Add(new Box("Key", "/Assets/Pics/BabyMisc/Key.png", "/Assets/Pics/BabyMisc/Key.png", "/Assets/Sounds/Misc/Key.wav"));
            BabyMisc.Add(new Box("Lock", "/Assets/Pics/BabyMisc/Lock.png", "/Assets/Pics/BabyMisc/Lock.png", "/Assets/Sounds/BabyMisc/Lock.wav"));
            BabyMisc.Add(new Box("Pencil", "/Assets/Pics/BabyMisc/Pencil.png", "/Assets/Pics/BabyMisc/Pencil.png", "/Assets/Sounds/Misc/Pencil.wav"));
            BabyMisc.Add(new Box("Phone", "/Assets/Pics/BabyMisc/Phone.png", "/Assets/Pics/BabyMisc/Phone.png", "/Assets/Sounds/Misc/Phone.wav"));
            BabyMisc.Add(new Box("Scissors", "/Assets/Pics/BabyMisc/Scissors.png", "/Assets/Pics/BabyMisc/Scissors.png", "/Assets/Sounds/Misc/Scissors.wav"));
            BabyMisc.Add(new Box("Stapler", "/Assets/Pics/BabyMisc/Stapler.png", "/Assets/Pics/BabyMisc/Stapler.png", "/Assets/Sounds/Misc/Stapler.wav"));
            BabyMisc.Add(new Box("Toothbrush", "/Assets/Pics/BabyMisc/Toothbrush.png", "/Assets/Pics/BabyMisc/Tooothbrush.png", "/Assets/Sounds/Misc/Toothbrush.wav"));
            BabyMisc.Add(new Box("Tractor", "/Assets/Pics/BabyMisc/Tractor.png", "/Assets/Pics/BabyMisc/Tractor.png", "/Assets/Sounds/Misc/Tractor.wav"));
            BabyMisc.Add(new Box("TV", "/Assets/Pics/BabyMisc/TV.png", "/Assets/Pics/BabyMisc/TV.png", "/Assets/Sounds/Misc/TV.wav"));
            BabyMisc.Add(new Box("Vacuum", "/Assets/Pics/BabyMisc/Vacuum.png", "/Assets/Pics/BabyMisc/Vacuum.png", "/Assets/Sounds/Misc/Vacuum.wav"));

            Animals = new List<Box>();

            Animals.Add(new Box("Elephant", "/Assets/Pics/Animals/Elephant80x100.png", "/Assets/Pics/Animals/Elephant480x800.png", "/Assets/Sounds/Animals/Elephant.wav"));
            Animals.Add(new Box("Pig", "/Assets/Pics/Animals/Pig80x100.png", "/Assets/Pics/Animals/Pig480x800.png", "/Assets/Sounds/Animals/Pig.wav"));


        }

        private void LoadPicsOnScreen(string category)
        {
            try
            {
                App.gCategory = category;

                switch (category)
                {
                    case "BabyAnimals":
                        Box1ImageSourceSmall = BabyAnimals[0].ImageSourceSmall;
                        Box1ImageSourceLarge = BabyAnimals[0].ImageSourceLarge;
                        Box1Description = BabyAnimals[0].Description;
                        Box1SoundSource = BabyAnimals[0].SoundSource;

                        Box2ImageSourceSmall = BabyAnimals[1].ImageSourceSmall;
                        Box2ImageSourceLarge = BabyAnimals[1].ImageSourceLarge;
                        Box2Description = BabyAnimals[1].Description;
                        Box2SoundSource = BabyAnimals[1].SoundSource;

                        Box3ImageSourceSmall = BabyAnimals[2].ImageSourceSmall;
                        Box3ImageSourceLarge = BabyAnimals[2].ImageSourceLarge;
                        Box3Description = BabyAnimals[2].Description;
                        Box3SoundSource = BabyAnimals[2].SoundSource;

                        Box4ImageSourceSmall = BabyAnimals[3].ImageSourceSmall;
                        Box4ImageSourceLarge = BabyAnimals[3].ImageSourceLarge;
                        Box4Description = BabyAnimals[3].Description;
                        Box4SoundSource = BabyAnimals[3].SoundSource;

                        Box5ImageSourceSmall = BabyAnimals[4].ImageSourceSmall;
                        Box5ImageSourceLarge = BabyAnimals[4].ImageSourceLarge;
                        Box5Description = BabyAnimals[4].Description;
                        Box5SoundSource = BabyAnimals[4].SoundSource;

                        Box6ImageSourceSmall = BabyAnimals[5].ImageSourceSmall;
                        Box6ImageSourceLarge = BabyAnimals[5].ImageSourceLarge;
                        Box6Description = BabyAnimals[5].Description;
                        Box6SoundSource = BabyAnimals[5].SoundSource;

                        Box7ImageSourceSmall = BabyAnimals[6].ImageSourceSmall;
                        Box7ImageSourceLarge = BabyAnimals[6].ImageSourceLarge;
                        Box7Description = BabyAnimals[6].Description;
                        Box7SoundSource = BabyAnimals[6].SoundSource;

                        Box8ImageSourceSmall = BabyAnimals[7].ImageSourceSmall;
                        Box8ImageSourceLarge = BabyAnimals[7].ImageSourceLarge;
                        Box8Description = BabyAnimals[7].Description;
                        Box8SoundSource = BabyAnimals[7].SoundSource;

                        Box9ImageSourceSmall = BabyAnimals[8].ImageSourceSmall;
                        Box9ImageSourceLarge = BabyAnimals[8].ImageSourceLarge;
                        Box9Description = BabyAnimals[8].Description;
                        Box9SoundSource = BabyAnimals[8].SoundSource;
                        break;
                    case "BabyMisc":
                        Box1ImageSourceSmall = BabyMisc[0].ImageSourceSmall;
                        Box1ImageSourceLarge = BabyMisc[0].ImageSourceLarge;
                        Box1Description = BabyMisc[0].Description;
                        Box1SoundSource = BabyMisc[0].SoundSource;

                        Box2ImageSourceSmall = BabyMisc[1].ImageSourceSmall;
                        Box2ImageSourceLarge = BabyMisc[1].ImageSourceLarge;
                        Box2Description = BabyMisc[1].Description;
                        Box2SoundSource = BabyMisc[1].SoundSource;

                        Box3ImageSourceSmall = BabyMisc[2].ImageSourceSmall;
                        Box3ImageSourceLarge = BabyMisc[2].ImageSourceLarge;
                        Box3Description = BabyMisc[2].Description;
                        Box3SoundSource = BabyMisc[2].SoundSource;

                        Box4ImageSourceSmall = BabyMisc[3].ImageSourceSmall;
                        Box4ImageSourceLarge = BabyMisc[3].ImageSourceLarge;
                        Box4Description = BabyMisc[3].Description;
                        Box4SoundSource = BabyMisc[3].SoundSource;

                        Box5ImageSourceSmall = BabyMisc[4].ImageSourceSmall;
                        Box5ImageSourceLarge = BabyMisc[4].ImageSourceLarge;
                        Box5Description = BabyMisc[4].Description;
                        Box5SoundSource = BabyMisc[4].SoundSource;

                        Box6ImageSourceSmall = BabyMisc[5].ImageSourceSmall;
                        Box6ImageSourceLarge = BabyMisc[5].ImageSourceLarge;
                        Box6Description = BabyMisc[5].Description;
                        Box6SoundSource = BabyMisc[5].SoundSource;

                        Box7ImageSourceSmall = BabyMisc[6].ImageSourceSmall;
                        Box7ImageSourceLarge = BabyMisc[6].ImageSourceLarge;
                        Box7Description = BabyMisc[6].Description;
                        Box7SoundSource = BabyMisc[6].SoundSource;

                        Box8ImageSourceSmall = BabyMisc[7].ImageSourceSmall;
                        Box8ImageSourceLarge = BabyMisc[7].ImageSourceLarge;
                        Box8Description = BabyMisc[7].Description;
                        Box8SoundSource = BabyMisc[7].SoundSource;

                        Box9ImageSourceSmall = BabyMisc[8].ImageSourceSmall;
                        Box9ImageSourceLarge = BabyMisc[8].ImageSourceLarge;
                        Box9Description = BabyMisc[8].Description;
                        Box9SoundSource = BabyMisc[8].SoundSource;
                        break;
                    case "Animals":
                        Box1ImageSourceSmall = Animals[0].ImageSourceSmall;
                        Box1ImageSourceLarge = Animals[0].ImageSourceLarge;
                        Box1Description = Animals[0].Description;
                        Box1SoundSource = Animals[0].SoundSource;

                        Box2ImageSourceSmall = Animals[1].ImageSourceSmall;
                        Box2ImageSourceLarge = Animals[1].ImageSourceLarge;
                        Box2Description = Animals[1].Description;
                        Box2SoundSource = Animals[1].SoundSource;

                        Box3ImageSourceSmall = Animals[2].ImageSourceSmall;
                        Box3ImageSourceLarge = Animals[2].ImageSourceLarge;
                        Box3Description = Animals[2].Description;
                        Box3SoundSource = Animals[2].SoundSource;

                        Box4ImageSourceSmall = Animals[3].ImageSourceSmall;
                        Box4ImageSourceLarge = Animals[3].ImageSourceLarge;
                        Box4Description = Animals[3].Description;
                        Box4SoundSource = Animals[3].SoundSource;

                        Box5ImageSourceSmall = Animals[4].ImageSourceSmall;
                        Box5ImageSourceLarge = Animals[4].ImageSourceLarge;
                        Box5Description = Animals[4].Description;
                        Box5SoundSource = Animals[4].SoundSource;

                        Box6ImageSourceSmall = Animals[5].ImageSourceSmall;
                        Box6ImageSourceLarge = Animals[5].ImageSourceLarge;
                        Box6Description = Animals[5].Description;
                        Box6SoundSource = Animals[5].SoundSource;

                        Box7ImageSourceSmall = Animals[6].ImageSourceSmall;
                        Box7ImageSourceLarge = Animals[6].ImageSourceLarge;
                        Box7Description = Animals[6].Description;
                        Box7SoundSource = Animals[6].SoundSource;

                        Box8ImageSourceSmall = Animals[7].ImageSourceSmall;
                        Box8ImageSourceLarge = Animals[7].ImageSourceLarge;
                        Box8Description = Animals[7].Description;
                        Box8SoundSource = Animals[7].SoundSource;

                        Box9ImageSourceSmall = Animals[8].ImageSourceSmall;
                        Box9ImageSourceLarge = Animals[8].ImageSourceLarge;
                        Box9Description = Animals[8].Description;
                        Box9SoundSource = Animals[8].SoundSource;
                        break;
                }
            }
            catch (Exception ex)
            {


            }
        }

        //await casues execution to be suspended until the SpeakTextAsynch
        //private async void PlayText()
        private void PlayVoiceText()
        {
            try
            {
                foreach (string language in App.gLanguages)
                {

                    switch (language)
                    {
                        case "English":
                            IEnumerable<VoiceInformation> englishVoices = from voice in InstalledVoices.All where voice.Language == "en-US" && voice.Gender == VoiceGender.Female select voice;
                            synthesizer.SetVoice(englishVoices.ElementAt(0));
                            break;
                        case "Spanish":
                            IEnumerable<VoiceInformation> spanishVoices = from voice in InstalledVoices.All where voice.Language == "es-ES" && voice.Gender == VoiceGender.Female select voice;
                            synthesizer.SetVoice(spanishVoices.ElementAt(0));
                            break;
                        case "French":
                            IEnumerable<VoiceInformation> frenchVoices = from voice in InstalledVoices.All where voice.Language == "fr-FR" && voice.Gender == VoiceGender.Female select voice;
                            synthesizer.SetVoice(frenchVoices.ElementAt(0));
                            break;
                        case "Chinese":
                            IEnumerable<VoiceInformation> chineseVoices = from voice in InstalledVoices.All where voice.Language == "zh-HK" && voice.Gender == VoiceGender.Female select voice;
                            synthesizer.SetVoice(chineseVoices.ElementAt(0));
                            break;
                        case "Italian":
                            IEnumerable<VoiceInformation> italianVoices = from voice in InstalledVoices.All where voice.Language == "it-IT" && voice.Gender == VoiceGender.Female select voice;
                            synthesizer.SetVoice(italianVoices.ElementAt(0));
                            break;
                        case "German":
                            IEnumerable<VoiceInformation> germanVoices = from voice in InstalledVoices.All where voice.Language == "de-DE" && voice.Gender == VoiceGender.Female select voice;
                            synthesizer.SetVoice(germanVoices.ElementAt(0));
                            break;
                        case "Portuguese":
                            IEnumerable<VoiceInformation> portugueseVoices = from voice in InstalledVoices.All where voice.Language == "pt-BR" && voice.Gender == VoiceGender.Female select voice;
                            synthesizer.SetVoice(portugueseVoices.ElementAt(0));
                            break;
                        case "Japanese":
                            IEnumerable<VoiceInformation> JapaneseVoices = from voice in InstalledVoices.All where voice.Language == "ja-JP" && voice.Gender == VoiceGender.Female select voice;
                            synthesizer.SetVoice(JapaneseVoices.ElementAt(0));
                            break;
                    }

                    if (Description != null)
                    {
                        // await synthesizer.SpeakTextAsync(App.gDisplayDescription);
                        //synthesizer.SpeakTextAsync(GetTextTranslation(language, Description));
                        synthesizer.SpeakSsmlAsync(VoiceOptions.GetText(Description, Pitch.Default, Speed.Slow, SpeakerVolume.ExtraLoud, "en-US"));
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
                    case "Irish":
                        AppResources.Culture = new System.Globalization.CultureInfo("ga");
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

        private string GetText(string textToTranslate)
        {
            string returnValue = string.Empty;
            try
            {
                switch (textToTranslate)
                {
                    case "Cow":
                        returnValue = AppResources.Cow;
                        break;
                    case "Lion":
                        returnValue = AppResources.Lion;
                        break;
                    case "Duck":
                        returnValue = AppResources.Duck;
                        break;
                    case "Elephant":
                        returnValue = AppResources.Elephant;
                        break;
                    case "Pig":
                        returnValue = AppResources.Pig;
                        break;
                }
            }
            catch (Exception)
            {
                return returnValue;
            }
            return returnValue;
        }

        private void OnePicSlideShowAsync(string tag)
        {
            Dispatcher.BeginInvoke(() =>
            {
                //Want to show the SlideShow grid for this ONE IMAGE
                this.PictureGrid.Visibility = Visibility.Collapsed;
                this.SlideShow.Visibility = Visibility.Visible;
            });

            //Task.Factory.StartNew(() => SetOnePicGrid(tag));
            SetOnePicGrid(tag);

           // Thread.Sleep(1000);

            Dispatcher.BeginInvoke(() =>
            {

                this.PictureGrid.Visibility = Visibility.Visible;
                this.SlideShow.Visibility = Visibility.Collapsed;
            }
            );
        }

        private void SetOnePicGrid(string tag)
        {
            Dispatcher.BeginInvoke(() =>
            {
                switch (tag)
                {
                    case "Box1":
                        ImageSourceSmall = Box1ImageSourceSmall;
                        ImageSourceLarge = Box1ImageSourceLarge;
                        Description = Box1Description;
                        ImageSound = Box1SoundSource;
                        break;
                    case "Box2":
                        ImageSourceSmall = Box2ImageSourceSmall;
                        ImageSourceLarge = Box2ImageSourceLarge;
                        Description = Box2Description;
                        ImageSound = Box2SoundSource;
                        break;
                    case "Box3":
                        ImageSourceSmall = Box3ImageSourceSmall;
                        ImageSourceLarge = Box3ImageSourceLarge;
                        Description = Box3Description;
                        ImageSound = Box3SoundSource;
                        break;
                    case "Box4":
                        ImageSourceSmall = Box4ImageSourceSmall;
                        ImageSourceLarge = Box4ImageSourceLarge;
                        Description = Box4Description;
                        ImageSound = Box4SoundSource;
                        break;
                    case "Box5":
                        ImageSourceSmall = Box5ImageSourceSmall;
                        ImageSourceLarge = Box5ImageSourceLarge;
                        Description = Box5Description;
                        ImageSound = Box5SoundSource;
                        break;
                    case "Box6":
                        ImageSourceSmall = Box6ImageSourceSmall;
                        ImageSourceLarge = Box6ImageSourceLarge;
                        Description = Box6Description;
                        ImageSound = Box6SoundSource;
                        break;
                    case "Box7":
                        ImageSourceSmall = Box7ImageSourceSmall;
                        ImageSourceLarge = Box7ImageSourceLarge;
                        Description = Box7Description;
                        ImageSound = Box7SoundSource;
                        break;
                    case "Box8":
                        ImageSourceSmall = Box8ImageSourceSmall;
                        ImageSourceLarge = Box8ImageSourceLarge;
                        Description = Box8Description;
                        ImageSound = Box8SoundSource;
                        break;
                    case "Box9":
                        ImageSourceSmall = Box9ImageSourceSmall;
                        ImageSourceLarge = Box9ImageSourceLarge;
                        Description = Box9Description;
                        ImageSound = Box9SoundSource;
                        break;
                }

                
            });

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
                case "BabyAnimals":
                    continuousPlayList = BabyAnimals;
                    break;
                case "BabyMisc":
                    continuousPlayList = BabyMisc;
                    break;
            }

            for (int i = 0; i < continuousPlayList.Count - 1; i++)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    Description = GetTextDesription(continuousPlayList[i].Description);
                    ImageSound = continuousPlayList[i].SoundSource;
                    ImageSourceLarge = continuousPlayList[i].ImageSourceLarge;
                    ImageSourceSmall = continuousPlayList[i].ImageSourceSmall;
                });

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

        private void PlayVoiceTextAndSound()
        {
            PlayVoiceText();

            if (App.gPlaySoundSetting == "On")
            {
                PlaySound();
                Thread.Sleep(5000);
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

        private ObservableCollection<PivotSlide> _pivotSlides;
        public ObservableCollection<PivotSlide> PivotSlides
        {
            get { return _pivotSlides; }
            set { _pivotSlides = value; NotifyPropertyChanged("PivotSlides"); }
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
     
        private void Box_Click(object sender, EventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();
            Task.Factory.StartNew(() => OnePicSlideShowAsync(tag));
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
                appBarButton1.Text = AppResources.AppBarBabyMiscButton;
                ApplicationBar.Buttons.Add(appBarButton1);
                appBarButton1.Click += new EventHandler(BabyMisc_Click);

                ApplicationBarIconButton appBarButton2 = new ApplicationBarIconButton(new Uri("/Assets/Pics/BabyAnimals/Elephant80x100.png", UriKind.Relative));
                appBarButton2.Text = "Baby Animals";
                ApplicationBar.Buttons.Add(appBarButton2);
                appBarButton2.Click += new EventHandler(BabyAnimals_Click);

                ApplicationBarIconButton appBarButton3 = new ApplicationBarIconButton(new Uri("/Assets/feature.email.png", UriKind.Relative));
                appBarButton3.Text = "Misc";
                ApplicationBar.Buttons.Add(appBarButton3);
                appBarButton3.Click += new EventHandler(Misc_Click);

                ApplicationBarIconButton appBarButton4 = new ApplicationBarIconButton(new Uri("/Assets/Pics/Animals/Elephant80x100.png", UriKind.Relative));
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