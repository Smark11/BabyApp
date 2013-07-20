using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Common.IsolatedStoreage;

namespace BabyApp
{
    public partial class Options
    {
        
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
            IS.SaveSetting("BabyApp-PlayMusic", "On");
            togglePlayMusic.Content = "On";
            App.gPlayMusicSetting = "On";
        }

        private void togglePlayMusic_Unchecked(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            IS.SaveSetting("BabyApp-PlayMusic", "Off");
            togglePlayMusic.Content = "Off";
            App.gPlayMusicSetting = "Off";
        }

        private void togglePlaySounds_Checked(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;
            IS.SaveSetting("BabyApp-PlaySounds", "On");
            togglePlaySounds.Content = "On";
            App.gPlaySoundSetting = "On";
        }

        private void togglePlaySounds_Unchecked(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            IS.SaveSetting("BabyApp-PlaySounds", "Off");
            togglePlaySounds.Content = "Off";
            App.gPlaySoundSetting = "Off";
        }

        private void toggleShowText_Checked(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;
            IS.SaveSetting("BabyApp-ShowText", "On");
            toggleShowText.Content = "On";
            App.gShowTextSetting = "On";
        }

        private void toggleShowText_Unchecked(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            IS.SaveSetting("BabyApp-ShowText", "Off");
            toggleShowText.Content = "Off";
            App.gShowTextSetting = "Off";
        }

        private void chkEnglish_Checked_1(object sender, RoutedEventArgs e)
        {
            if (App.gLanguages.Count < 3)
            {
                App.gLanguages.Add("English");
                IS.SaveSetting("BabyApp-English", "Yes");
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
            IS.SaveSetting("BabyApp-English", "No");
        }

        private void chkSpanish_Checked_1(object sender, RoutedEventArgs e)
        {
            if (App.gLanguages.Count < 3)
            {
                App.gLanguages.Add("Spanish");
                IS.SaveSetting("BabyApp-Spanish", "Yes");
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
            IS.SaveSetting("BabyApp-Spanish", "No");
        }

        private void chkItalian_Checked_1(object sender, RoutedEventArgs e)
        {
            if (App.gLanguages.Count < 3)
            {
                App.gLanguages.Add("Italian");
                IS.SaveSetting("BabyApp-Italian", "Yes");
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
            IS.SaveSetting("BabyApp-Italian", "No");
        }

        private void chkGerman_Checked_1(object sender, RoutedEventArgs e)
        {
            if (App.gLanguages.Count < 3)
            {
                App.gLanguages.Add("German");
                IS.SaveSetting("BabyApp-German", "Yes");
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
            IS.SaveSetting("BabyApp-German", "No");
        }

        private void chkChinese_Checked_1(object sender, RoutedEventArgs e)
        {
            if (App.gLanguages.Count < 3)
            {
                App.gLanguages.Add("Chinese");
                IS.SaveSetting("BabyApp-Chinese", "Yes");
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
            IS.SaveSetting("BabyApp-Chinese", "No");
        }

        private void chkFrench_Checked_1(object sender, RoutedEventArgs e)
        {
            if (App.gLanguages.Count < 3)
            {
                App.gLanguages.Add("French");
                IS.SaveSetting("BabyApp-French", "Yes");
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
            IS.SaveSetting("BabyApp-French", "No");
       }

        private void chkPortuguese_Checked_1(object sender, RoutedEventArgs e)
        {
            if (App.gLanguages.Count < 3)
            {
                App.gLanguages.Add("Portuguese");
                IS.SaveSetting("BabyApp-Portuguese", "Yes");
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
            IS.SaveSetting("BabyApp-Portuguese", "No");
        }

        private void chkIrish_Checked_1(object sender, RoutedEventArgs e)
        {
            if (App.gLanguages.Count < 3)
            {
                App.gLanguages.Add("Irish");
                IS.SaveSetting("BabyApp-Irish", "Yes");
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
            IS.SaveSetting("BabyApp-Irish", "No");
       }

        private void chkJapanese_Checked_1(object sender, RoutedEventArgs e)
        {
            if (App.gLanguages.Count < 3)
            {
                App.gLanguages.Add("Japanese");
                IS.SaveSetting("BabyApp-Japanese", "Yes");
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
            IS.SaveSetting("BabyApp-Japanese", "No");
        }

        #endregion "Events"

        #region "Methods"

        private void LoadPlaySoundSetting()
        {
            string settingValue = string.Empty;

            if (IS.GetSettingStringValue("BabyApp-PlaySounds").ToString() == string.Empty)
            {
                togglePlaySounds.IsChecked = false;
                togglePlaySounds.Content = "Off";
            }
            else
            {
                settingValue = IS.GetSettingStringValue("BabyApp-PlaySounds").ToString();
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

            if (IS.GetSettingStringValue("BabyApp-PlayMusic").ToString() == string.Empty)
            {
                togglePlayMusic.IsChecked = false;
                togglePlayMusic.Content = "Off";
            }
            else
            {
                settingValue = IS.GetSettingStringValue("BabyApp-PlayMusic").ToString();
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

            if (IS.GetSettingStringValue("BabyApp-ShowText").ToString() == string.Empty)
            {
                toggleShowText.IsChecked = false;
                toggleShowText.Content = "Off";
            }
            else
            {
                settingValue = IS.GetSettingStringValue("BabyApp-ShowText").ToString();
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
                if (IS.GetSettingStringValue("BabyApp-English").ToString() == string.Empty)
                {
                    chkEnglish.IsChecked = false;
                }
                else
                {
                    settingValue = IS.GetSettingStringValue("BabyApp-English").ToString();
                    if (settingValue == "Yes")
                    {
                        chkEnglish.IsChecked = true;
                    }
                    else
                    {
                        chkEnglish.IsChecked = false;
                    }
                }

                if (IS.GetSettingStringValue("BabyApp-Spanish").ToString() == string.Empty)
                {
                    chkSpanish.IsChecked = false;
                }
                else
                {
                    settingValue = IS.GetSettingStringValue("BabyApp-Spanish").ToString();
                    if (settingValue == "Yes")
                    {
                        chkSpanish.IsChecked = true;
                    }
                    else
                    {
                        chkSpanish.IsChecked = false;
                    }
                }

                if (IS.GetSettingStringValue("BabyApp-Italian") == string.Empty)
                {
                    chkItalian.IsChecked = false;
                }
                else
                {
                    settingValue = IS.GetSettingStringValue("BabyApp-Italian").ToString();
                    if (settingValue == "Yes")
                    {
                        chkItalian.IsChecked = true;
                    }
                    else
                    {
                        chkItalian.IsChecked = false;
                    }
                }

                if (IS.GetSettingStringValue("BabyApp-French") == string.Empty)
                {
                    chkFrench.IsChecked = false;
                }
                else
                {
                    settingValue = IS.GetSettingStringValue("BabyApp-French").ToString();
                    if (settingValue == "Yes")
                    {
                        chkFrench.IsChecked = true;
                    }
                    else
                    {
                        chkFrench.IsChecked = false;
                    }
                }

                if (IS.GetSettingStringValue("BabyApp-Irish") == string.Empty)
                {
                    chkIrish.IsChecked = false;
                }
                else
                {
                    settingValue = IS.GetSettingStringValue("BabyApp-Irish").ToString();
                    if (settingValue == "Yes")
                    {
                        chkIrish.IsChecked = true;
                    }
                    else
                    {
                        chkIrish.IsChecked = false;
                    }
                }

                if (IS.GetSettingStringValue("BabyApp-German") == string.Empty)
                {
                    chkGerman.IsChecked = false;
                }
                else
                {
                    settingValue = IS.GetSettingStringValue("BabyApp-German").ToString();
                    if (settingValue == "Yes")
                    {
                        chkGerman.IsChecked = true;
                    }
                    else
                    {
                        chkGerman.IsChecked = false;
                    }
                }

                if (IS.GetSettingStringValue("BabyApp-Portuguese") == string.Empty)
                {
                    chkPortuguese.IsChecked = false;
                }
                else
                {
                    settingValue = IS.GetSettingStringValue("BabyApp-Portuguese").ToString();
                    if (settingValue == "Yes")
                    {
                        chkPortuguese.IsChecked = true;
                    }
                    else
                    {
                        chkPortuguese.IsChecked = false;
                    }
                }

                if (IS.GetSettingStringValue("BabyApp-Japanese") == string.Empty)
                {
                    chkJapanese.IsChecked = false;
                }
                else
                {
                    settingValue = IS.GetSettingStringValue("BabyApp-Japanese").ToString();
                    if (settingValue == "Yes")
                    {
                        chkJapanese.IsChecked = true;
                    }
                    else
                    {
                        chkJapanese.IsChecked = false;
                    }
                }

                if (IS.GetSettingStringValue("BabyApp-Chinese").ToString() == string.Empty)
                {
                    chkChinese.IsChecked = false;
                }
                else
                {
                    settingValue = IS.GetSettingStringValue("BabyApp-Chinese").ToString();
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