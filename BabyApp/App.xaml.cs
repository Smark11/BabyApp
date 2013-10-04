using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Resources;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BabyApp.Resources;
using Common.IsolatedStoreage;
using Microsoft.Phone.Marketplace;

namespace BabyApp
{
    public partial class App : Application
    {
        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public static PhoneApplicationFrame RootFrame { get; private set; }

        public static List<string> gLanguages = new List<string>();
        public static string gPlaySoundSetting;
        public static string gPlayMusicSetting;
        public static string gShowTextSetting;
        public static string gCategory = "Animals";

        private static LicenseInformation _licenseInfo = new LicenseInformation();

        private static bool _isTrial;
        public bool IsTrial
        {
            get
            {        
                return _isTrial;
            }
        }

        //private static bool _isFreeVersion = false;
        //public bool IsFreeVersion
        //{
        //    get
        //    {
        //        return _isFreeVersion;
        //    }
        //}

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions.
            UnhandledException += Application_UnhandledException;

            // Standard XAML initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Language display initialization
            InitializeLanguage();

            //Baby App Settings
            LoadPlaySoundSetting();
            LoadPlayMusicSetting();
            LoadShowTextSetting();
            LoadLanguageSettings();
            

            // Show graphics profiling information while debugging.
            if (Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode,
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Prevent the screen from turning off while under the debugger by disabling
                // the application's idle detection.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

        }

        private void CheckLicence()
        {
            //_isTrial = true;
            _isTrial = _licenseInfo.IsTrial();
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            CheckLicence();
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            CheckLicence();
            MainPage mainClient = MainPage._mainPageInstance;
            mainClient.AppHasComeBackIntoFocus();
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Handle reset requests for clearing the backstack
            RootFrame.Navigated += CheckForResetNavigation;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        private void CheckForResetNavigation(object sender, NavigationEventArgs e)
        {
            // If the app has received a 'reset' navigation, then we need to check
            // on the next navigation to see if the page stack should be reset
            if (e.NavigationMode == NavigationMode.Reset)
                RootFrame.Navigated += ClearBackStackAfterReset;
        }

        private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
        {
            // Unregister the event so it doesn't get called again
            RootFrame.Navigated -= ClearBackStackAfterReset;

            // Only clear the stack for 'new' (forward) and 'refresh' navigations
            if (e.NavigationMode != NavigationMode.New && e.NavigationMode != NavigationMode.Refresh)
                return;

            // For UI consistency, clear the entire page stack
            while (RootFrame.RemoveBackEntry() != null)
            {
                ; // do nothing
            }
        }

        #endregion

        // Initialize the app's font and flow direction as defined in its localized resource strings.
        //
        // To ensure that the font of your application is aligned with its supported languages and that the
        // FlowDirection for each of those languages follows its traditional direction, ResourceLanguage
        // and ResourceFlowDirection should be initialized in each resx file to match these values with that
        // file's culture. For example:
        //
        // AppResources.es-ES.resx
        //    ResourceLanguage's value should be "es-ES"
        //    ResourceFlowDirection's value should be "LeftToRight"
        //
        // AppResources.ar-SA.resx
        //     ResourceLanguage's value should be "ar-SA"
        //     ResourceFlowDirection's value should be "RightToLeft"
        //
        // For more info on localizing Windows Phone apps see http://go.microsoft.com/fwlink/?LinkId=262072.
        //
        private void InitializeLanguage()
        {
            try
            {
                // Set the font to match the display language defined by the
                // ResourceLanguage resource string for each supported language.
                //
                // Fall back to the font of the neutral language if the Display
                // language of the phone is not supported.
                //
                // If a compiler error is hit then ResourceLanguage is missing from
                // the resource file.
                RootFrame.Language = XmlLanguage.GetLanguage(AppResources.ResourceLanguage);

                // Set the FlowDirection of all elements under the root frame based
                // on the ResourceFlowDirection resource string for each
                // supported language.
                //
                // If a compiler error is hit then ResourceFlowDirection is missing from
                // the resource file.
                FlowDirection flow = (FlowDirection)Enum.Parse(typeof(FlowDirection), AppResources.ResourceFlowDirection);
                RootFrame.FlowDirection = flow;
            }
            catch
            {
                // If an exception is caught here it is most likely due to either
                // ResourceLangauge not being correctly set to a supported language
                // code or ResourceFlowDirection is set to a value other than LeftToRight
                // or RightToLeft.

                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                throw;
            }
        }

        private void LoadPlaySoundSetting()
        {
            string settingValue = string.Empty;

            if (IS.GetSetting("BabyApp-PlaySounds") == null)
            {
                gPlaySoundSetting = "On";
                IS.SaveSetting("BabyApp-PlaySounds", "On");
            }
            else
            {
                settingValue = IS.GetSetting("BabyApp-PlaySounds").ToString();
                if (settingValue == "On")
                {
                    gPlaySoundSetting = "On";
                }
                else
                {
                    gPlaySoundSetting = "Off";
                }
            }
        }

        private void LoadPlayMusicSetting()
        {
            string settingValue = string.Empty;

            if (IS.GetSetting("BabyApp-PlayMusic") == null)
            {
                gPlayMusicSetting = "On";
                IS.SaveSetting("BabyApp-PlayMusic", "On");
            }
            else
            {
                settingValue = IS.GetSetting("BabyApp-PlayMusic").ToString();
                if (settingValue == "On")
                {
                    gPlayMusicSetting = "On";
                }
                else
                {
                    gPlayMusicSetting = "Off";
                }
            }
        }

        private void LoadShowTextSetting()
        {
            string settingValue = string.Empty;

            if (IS.GetSetting("BabyApp-ShowText") == null)
            {
                gShowTextSetting = "On";
                IS.SaveSetting("BabyApp-ShowText", "On");
            }
            else
            {
                settingValue = IS.GetSetting("BabyApp-ShowText").ToString();
                if (settingValue == "On")
                {
                    gShowTextSetting = "On";
                }
                else
                {
                    gShowTextSetting = "Off";
                }
            }
        }


        private void LoadLanguageSettings()
        {
            string settingValue = string.Empty;

            try
            {
                if (IS.GetSetting("BabyApp-English") == null)
                {
                    gLanguages.Remove("English");
                }
                else
                {
                    settingValue = IS.GetSetting("BabyApp-English").ToString();
                    if (settingValue == "Yes")
                    {
                        gLanguages.Add("English");
                    }
                    else
                    {
                        gLanguages.Remove("English");
                    }
                }

                if (IS.GetSetting("BabyApp-Spanish") == null)
                {
                    gLanguages.Remove("Spanish");
                }
                else
                {
                    settingValue = IS.GetSetting("BabyApp-Spanish").ToString();
                    if (settingValue == "Yes")
                    {
                        gLanguages.Add("Spanish");
                    }
                    else
                    {
                        gLanguages.Remove("Spanish");
                    }
                }

                if (IS.GetSetting("BabyApp-Italian") == null)
                {
                    gLanguages.Remove("Italian");
                }
                else
                {
                    settingValue = IS.GetSetting("BabyApp-Italian").ToString();
                    if (settingValue == "Yes")
                    {
                        gLanguages.Add("Italian");
                    }
                    else
                    {
                        gLanguages.Remove("Italian");
                    }
                }

                if (IS.GetSetting("BabyApp-French") == null)
                {
                    gLanguages.Remove("French");
                }
                else
                {
                    settingValue = IS.GetSetting("BabyApp-French").ToString();
                    if (settingValue == "Yes")
                    {
                        gLanguages.Add("French");
                    }
                    else
                    {
                        gLanguages.Remove("French");
                    }
                }

                if (IS.GetSetting("BabyApp-Polish") == null)
                {
                    gLanguages.Remove("Polish");
                }
                else
                {
                    settingValue = IS.GetSetting("BabyApp-Polish").ToString();
                    if (settingValue == "Yes")
                    {
                        gLanguages.Add("Polish");
                    }
                    else
                    {
                        gLanguages.Remove("Polish");
                    }
                }

                if (IS.GetSetting("BabyApp-German") == null)
                {
                    gLanguages.Remove("German");
                }
                else
                {
                    settingValue = IS.GetSetting("BabyApp-German").ToString();
                    if (settingValue == "Yes")
                    {
                        gLanguages.Add("German");
                    }
                    else
                    {
                        gLanguages.Remove("German");
                    }
                }

                if (IS.GetSetting("BabyApp-Portuguese") == null)
                {
                    gLanguages.Remove("Portuguese");
                }
                else
                {
                    settingValue = IS.GetSetting("BabyApp-Portuguese").ToString();
                    if (settingValue == "Yes")
                    {
                        gLanguages.Add("Portuguese");
                    }
                    else
                    {
                        gLanguages.Remove("Portuguese");
                    }
                }

                if (IS.GetSetting("BabyApp-Japanese") == null)
                {
                    gLanguages.Remove("Japanese");
                }
                else
                {
                    settingValue = IS.GetSetting("BabyApp-Japanese").ToString();
                    if (settingValue == "Yes")
                    {
                        gLanguages.Add("Japanese");
                    }
                    else
                    {
                        gLanguages.Remove("Japanese");
                    }
                }

                if (IS.GetSetting("BabyApp-Chinese") == null)
                {
                    gLanguages.Remove("Chinese");
                }
                else
                {
                    settingValue = IS.GetSetting("BabyApp-Chinese").ToString();
                    if (settingValue == "Yes")
                    {
                        gLanguages.Add("Chinese");
                    }
                    else
                    {
                        gLanguages.Remove("Chinese");
                    }
                }

                if (gLanguages.Count == 0)
                {
                    gLanguages.Add("English");
                    IS.SaveSetting("BabyApp-English", "Yes");
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}