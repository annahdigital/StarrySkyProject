using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace StarrySky
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("Language"))
            {
                CurrentData.currentSettings.RussianMode = (bool)Application.Current.Properties["Language"];
            }
            else CurrentData.currentSettings.RussianMode = false;
            if (Application.Current.Properties.ContainsKey("Animation"))
            {
                CurrentData.currentSettings.IsAnimated = (bool)Application.Current.Properties["Animation"];
            }
            else CurrentData.currentSettings.IsAnimated = true;
            CurrentData.InitializeData();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            CurrentData.DONTMOVE = false;
            if (Application.Current.Properties.ContainsKey("Language"))
            {
                CurrentData.currentSettings.RussianMode = (bool) Application.Current.Properties["Language"];
            }
            else CurrentData.currentSettings.RussianMode = false;
            if (Application.Current.Properties.ContainsKey("Animation"))
            {
                CurrentData.currentSettings.IsAnimated = (bool)Application.Current.Properties["Animation"];
            }
            else CurrentData.currentSettings.IsAnimated = true;
            CurrentData.InitializeData();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            CurrentData.DONTMOVE = false;
            CurrentData.constellationsVM.SaveChangesInFile();
            CurrentData.planetsVM.SaveChangesInFile();
            CurrentData.starsVM.SaveChangesInFile();

            Application.Current.Properties["Language"] = CurrentData.currentSettings.RussianMode;
            Application.Current.Properties["Animation"] = CurrentData.currentSettings.IsAnimated;
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            if (Application.Current.Properties.ContainsKey("Language"))
            {
                CurrentData.currentSettings.RussianMode = (bool)Application.Current.Properties["Language"];
            }
            else CurrentData.currentSettings.RussianMode = false;
            if (Application.Current.Properties.ContainsKey("Animation"))
            {
                CurrentData.currentSettings.IsAnimated = (bool)Application.Current.Properties["Animation"];
            }
            else CurrentData.currentSettings.IsAnimated = true;
            CurrentData.InitializeData();
            MainPage = new MainPage();
        }

        public void Help()
        {
            OnResume();
        }
    }

}
