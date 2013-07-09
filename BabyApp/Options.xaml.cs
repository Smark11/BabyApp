using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace BabyApp
{
    public partial class Options
    {
        Common commonCode = new Common();

        public Options()
        {
            string settingValue = string.Empty;

            InitializeComponent();

            App.gLanguages = new List<string>();

            LoadPlaySoundSetting();
            LoadPlayMusicSetting();
            LoadShowTextSetting();
            LoadLanguageSettings();
        }

        #region "Events"

        private void togglePlayMusic_Checked(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;
            commonCode.SaveSettings("BabyApp-PlayMusic", "On");
            togglePlayMusic.Content = "On";
            App.gPlayMusicSetting = "On";
        }

        private void togglePlayMusic_Unchecked(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            commonCode.SaveSettings("BabyApp-PlayMusic", "Off");
            togglePlayMusic.Content = "Off";
            App.gPlayMusicSetting = "Off";
        }

        private void togglePlaySounds_Checked(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;
            commonCode.SaveSettings("BabyApp-PlaySounds", "On");
            togglePlaySounds.Content = "On";
            App.gPlaySoundSetting = "On";
        }

        private void togglePlaySounds_Unchecked(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            commonCode.SaveSettings("BabyApp-PlaySounds", "Off");
            togglePlaySounds.Content = "Off";
            App.gPlaySoundSetting = "Off";
        }

        private void toggleShowText_Checked(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;
            commonCode.SaveSettings("BabyApp-ShowText", "On");
            toggleShowText.Content = "On";
            App.gShowTextSetting = "On";
        }

        private void toggleShowText_Unchecked(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            commonCode.SaveSettings("BabyApp-ShowText", "Off");
            toggleShowText.Content = "Off";
            App.gShowTextSetting = "Off";
        }

        private void chkEnglish_Checked_1(object sender, RoutedEventArgs e)
        {
            if (App.gLanguages.Count < 3)
            {
                App.gLanguages.Add("English");
                commonCode.SaveSettings("BabyApp-English", "Yes");
            }
            else
            {
                MessageBox.Show("Can only select up to 3 languages.", "", MessageBoxButton.OK);
                chkEnglish.IsChecked = false;
            }

        }

        private void chkEnglish_Unchecked_1(object sender, RoutedEventArgs e)
        {
            App.gLanguages.Remove("English");
            commonCode.SaveSettings("BabyApp-English", "No");
        }

        private void chkSpanish_Checked_1(object sender, RoutedEventArgs e)
        {
            if (App.gLanguages.Count < 3)
            {
                App.gLanguages.Add("Spanish");
                commonCode.SaveSettings("BabyApp-Spanish", "Yes");
            }
            else
            {
                MessageBox.Show("Can only select up to 3 languages.", "", MessageBoxButton.OK);
                chkSpanish.IsChecked = false;
            }
        }

        private void chkSpanish_Unchecked_1(object sender, RoutedEventArgs e)
        {
            App.gLanguages.Remove("Spanish");
            commonCode.SaveSettings("BabyApp-Spanish", "No");
        }

        private void chkItalian_Checked_1(object sender, RoutedEventArgs e)
        {
            if (App.gLanguages.Count < 3)
            {
                App.gLanguages.Add("Italian");
                commonCode.SaveSettings("BabyApp-Italian", "Yes");
            }
            else
            {
                MessageBox.Show("Can only select up to 3 languages.", "", MessageBoxButton.OK);
                chkItalian.IsChecked = false;
            }
        }

        private void chkItalian_Unchecked_1(object sender, RoutedEventArgs e)
        {
            App.gLanguages.Remove("Italian");
            commonCode.SaveSettings("BabyApp-Italian", "No");
        }

        private void chkGerman_Checked_1(object sender, RoutedEventArgs e)
        {
            if (App.gLanguages.Count < 3)
            {
                App.gLanguages.Add("German");
                commonCode.SaveSettings("BabyApp-German", "Yes");
            }
            else
            {
                MessageBox.Show("Can only select up to 3 languages.", "", MessageBoxButton.OK);
                chkGerman.IsChecked = false;
            }
        }

        private void chkGerman_Unchecked_1(object sender, RoutedEventArgs e)
        {
            App.gLanguages.Remove("German");
            commonCode.SaveSettings("BabyApp-German", "No");
        }

        private void chkChinese_Checked_1(object sender, RoutedEventArgs e)
        {
            if (App.gLanguages.Count < 3)
            {
                App.gLanguages.Add("Chinese");
                commonCode.SaveSettings("BabyApp-Chinese", "Yes");
            }
            else
            {
                MessageBox.Show("Can only select up to 3 languages.", "", MessageBoxButton.OK);
                chkChinese.IsChecked = false;
            }
        }

        private void chkChinese_Unchecked_1(object sender, RoutedEventArgs e)
        {
            App.gLanguages.Remove("Chinese");
            commonCode.SaveSettings("BabyApp-Chinese", "No");
        }

        private void chkFrench_Checked_1(object sender, RoutedEventArgs e)
        {
            if (App.gLanguages.Count < 3)
            {
                App.gLanguages.Add("French");
                commonCode.SaveSettings("BabyApp-French", "Yes");
            }
            else
            {
                MessageBox.Show("Can only select up to 3 languages.", "", MessageBoxButton.OK);
                chkFrench.IsChecked = false;
            }
        }

        private void chkFrench_Unchecked_1(object sender, RoutedEventArgs e)
        {
            App.gLanguages.Remove("French");
            commonCode.SaveSettings("BabyApp-French", "No");
       }

        private void chkPortuguese_Checked_1(object sender, RoutedEventArgs e)
        {
            if (App.gLanguages.Count < 3)
            {
                App.gLanguages.Add("Portuguese");
                commonCode.SaveSettings("BabyApp-Portuguese", "Yes");
            }
            else
            {
                MessageBox.Show("Can only select up to 3 languages.", "", MessageBoxButton.OK);
                chkPortuguese.IsChecked = false;
            }
        }

        private void chkPortuguese_Unchecked_1(object sender, RoutedEventArgs e)
        {
            App.gLanguages.Remove("Portuguese");
            commonCode.SaveSettings("BabyApp-Portuguese", "No");
        }

        private void chkIrish_Checked_1(object sender, RoutedEventArgs e)
        {
            if (App.gLanguages.Count < 3)
            {
                App.gLanguages.Add("Irish");
                commonCode.SaveSettings("BabyApp-Irish", "Yes");
            }
            else
            {
                MessageBox.Show("Can only select up to 3 languages.", "", MessageBoxButton.OK);
                chkIrish.IsChecked = false;
            }
        }
        
        private void chkIrish_Unchecked_1(object sender, RoutedEventArgs e)
        {
            App.gLanguages.Remove("Irish");
            commonCode.SaveSettings("BabyApp-Irish", "No");
       }

        private void chkJapanese_Checked_1(object sender, RoutedEventArgs e)
        {
            if (App.gLanguages.Count < 3)
            {
                App.gLanguages.Add("Japanese");
                commonCode.SaveSettings("BabyApp-Japanese", "Yes");
            }
            else
            {
                MessageBox.Show("Can only select up to 3 languages.", "", MessageBoxButton.OK);
                chkJapanese.IsChecked = false;
            }
        }

        private void chkJapanese_Unchecked_1(object sender, RoutedEventArgs e)
        {
            App.gLanguages.Remove("Japanese");
            commonCode.SaveSettings("BabyApp-Japanese", "No");
        }

        #endregion "Events"

        #region "Methods"

        private void LoadPlaySoundSetting()
        {
            string settingValue = string.Empty;

            if (commonCode.GetSetting("BabyApp-PlaySounds") == string.Empty)
            {
                togglePlaySounds.IsChecked = false;
                togglePlaySounds.Content = "Off";
            }
            else
            {
                settingValue = commonCode.GetSetting("BabyApp-PlaySounds");
                if (settingValue == "On")
                {
                    togglePlaySounds.IsChecked = true;
                    togglePlaySounds.Content = "On";
                }
                else
                {
                    togglePlaySounds.IsChecked = false;
                    togglePlaySounds.Content = "Off";
                }
            }
        }

        private void LoadPlayMusicSetting()
        {
            string settingValue = string.Empty;

            if (commonCode.GetSetting("BabyApp-PlayMusic") == string.Empty)
            {
                togglePlayMusic.IsChecked = false;
                togglePlayMusic.Content = "Off";
            }
            else
            {
                settingValue = commonCode.GetSetting("BabyApp-PlayMusic");
                if (settingValue == "On")
                {
                    togglePlayMusic.IsChecked = true;
                    togglePlayMusic.Content = "On";
                }
                else
                {
                    togglePlayMusic.IsChecked = false;
                    togglePlayMusic.Content = "Off";
                }
            }
        }

        private void LoadShowTextSetting()
        {
            string settingValue = string.Empty;

            if (commonCode.GetSetting("BabyApp-ShowText") == string.Empty)
            {
                toggleShowText.IsChecked = false;
                toggleShowText.Content = "Off";
            }
            else
            {
                settingValue = commonCode.GetSetting("BabyApp-ShowText");
                if (settingValue == "On")
                {
                    toggleShowText.IsChecked = true;
                    toggleShowText.Content = "On";
                }
                else
                {
                    toggleShowText.IsChecked = false;
                    toggleShowText.Content = "Off";
                }
            }
        }

        private void LoadLanguageSettings()
        {
            string settingValue = string.Empty;

            try
            {
                if (commonCode.GetSetting("BabyApp-English") == string.Empty)
                {
                    chkEnglish.IsChecked = false;
                }
                else
                {
                    settingValue = commonCode.GetSetting("BabyApp-English");
                    if (settingValue == "Yes")
                    {
                        chkEnglish.IsChecked = true;
                    }
                    else
                    {
                        chkEnglish.IsChecked = false;
                    }
                }

                if (commonCode.GetSetting("BabyApp-Spanish") == string.Empty)
                {
                    chkSpanish.IsChecked = false;
                }
                else
                {
                    settingValue = commonCode.GetSetting("BabyApp-Spanish");
                    if (settingValue == "Yes")
                    {
                        chkSpanish.IsChecked = true;
                    }
                    else
                    {
                        chkSpanish.IsChecked = false;
                    }
                }

                if (commonCode.GetSetting("BabyApp-Italian") == string.Empty)
                {
                    chkItalian.IsChecked = false;
                }
                else
                {
                    settingValue = commonCode.GetSetting("BabyApp-Italian");
                    if (settingValue == "Yes")
                    {
                        chkItalian.IsChecked = true;
                    }
                    else
                    {
                        chkItalian.IsChecked = false;
                    }
                }

                if (commonCode.GetSetting("BabyApp-French") == string.Empty)
                {
                    chkFrench.IsChecked = false;
                }
                else
                {
                    settingValue = commonCode.GetSetting("BabyApp-French");
                    if (settingValue == "Yes")
                    {
                        chkFrench.IsChecked = true;
                    }
                    else
                    {
                        chkFrench.IsChecked = false;
                    }
                }

                if (commonCode.GetSetting("BabyApp-Irish") == string.Empty)
                {
                    chkIrish.IsChecked = false;
                }
                else
                {
                    settingValue = commonCode.GetSetting("BabyApp-Irish");
                    if (settingValue == "Yes")
                    {
                        chkIrish.IsChecked = true;
                    }
                    else
                    {
                        chkIrish.IsChecked = false;
                    }
                }

                if (commonCode.GetSetting("BabyApp-German") == string.Empty)
                {
                    chkGerman.IsChecked = false;
                }
                else
                {
                    settingValue = commonCode.GetSetting("BabyApp-German");
                    if (settingValue == "Yes")
                    {
                        chkGerman.IsChecked = true;
                    }
                    else
                    {
                        chkGerman.IsChecked = false;
                    }
                }

                if (commonCode.GetSetting("BabyApp-Portuguese") == string.Empty)
                {
                    chkPortuguese.IsChecked = false;
                }
                else
                {
                    settingValue = commonCode.GetSetting("BabyApp-Portuguese");
                    if (settingValue == "Yes")
                    {
                        chkPortuguese.IsChecked = true;
                    }
                    else
                    {
                        chkPortuguese.IsChecked = false;
                    }
                }

                if (commonCode.GetSetting("BabyApp-Japanese") == string.Empty)
                {
                    chkJapanese.IsChecked = false;
                }
                else
                {
                    settingValue = commonCode.GetSetting("BabyApp-Japanese");
                    if (settingValue == "Yes")
                    {
                        chkJapanese.IsChecked = true;
                    }
                    else
                    {
                        chkJapanese.IsChecked = false;
                    }
                }

                if (commonCode.GetSetting("BabyApp-Chinese") == string.Empty)
                {
                    chkChinese.IsChecked = false;
                }
                else
                {
                    settingValue = commonCode.GetSetting("BabyApp-Chinese");
                    if (settingValue == "Yes")
                    {
                        chkChinese.IsChecked = true;
                    }
                    else
                    {
                        chkChinese.IsChecked = false;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion "Methods"
    }
}