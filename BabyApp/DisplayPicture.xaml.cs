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
using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using System.Windows.Threading;
using Windows.Phone.Speech.Synthesis;
using System.Threading;
using System.Diagnostics;
using BabyApp.Resources;



namespace BabyApp
{
    public partial class DisplayPicture : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        DispatcherTimer _timer;
        SpeechSynthesizer synthesizer;

        //  InstalledVoices installedVoices;
        public DisplayPicture()
        {
            try
            {
                InitializeComponent();

                this.DataContext = this;
                
                synthesizer = new SpeechSynthesizer();

                _timer = new DispatcherTimer();
                _timer.Tick += new EventHandler(Timer_Tick);
                _timer.Interval = new TimeSpan(0, 0, 1);
                _timer.Start();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #region "Properties"

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _imageSource;
        public string ImageSource
        {
            get { return App.gDisplayPictureLarge; }
            set { _imageSource = value; NotifyPropertyChanged("ImageSource"); }
        }

        private string _imageSound;
        public string ImageSound
        {
            get { return App.gDisplaySound; }
            set { _imageSound = value; NotifyPropertyChanged("ImageSound"); }
        }

        private string _description;
        public string Description
        {
            get { return TextDesription(); }
            set { _description = value; NotifyPropertyChanged("Description"); }
        }

        #endregion "Properties"

        #region "Events"

        private void Timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();

            PlayVoiceText();

            if (App.gPlaySoundSetting == "On")
            {
                PlaySound();
                Thread.Sleep(5000);
            }
        
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        #endregion "Events"

        #region "Methods"

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

                    // await synthesizer.SpeakTextAsync(App.gDisplayDescription);
                    synthesizer.SpeakTextAsync(GetTextTranslation(language, App.gDisplayDescription));
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
                var soundfile = App.gDisplaySound.Substring(1); //Note no slash before the Assets folder, and it's a WAV file!
                Stream stream = TitleContainer.OpenStream(soundfile);
                SoundEffect effect = SoundEffect.FromStream(stream);
                FrameworkDispatcher.Update();
                effect.Play();
            }
            catch (Exception ex)
            {
            }
        }
        
        private string TextDesription()
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
                            returnValue = GetTextTranslation(language, App.gDisplayDescription);
                        }
                        else
                        {
                            returnValue = returnValue + "   " + GetTextTranslation(language, App.gDisplayDescription);
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
                returnValue = App.gDisplayDescription;

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
                }
            }
            catch (Exception)
            {
                return returnValue;
            }
            return returnValue;
        }



        #endregion "Methods"
    }
}
