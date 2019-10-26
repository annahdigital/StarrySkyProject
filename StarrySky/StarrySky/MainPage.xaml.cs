using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StarrySky
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            //CurrentData.InitializeData();
            Detail = new NavigationPage(new MainMenu()) { BarBackgroundColor = Color.Thistle, BarTextColor = Color.Black };
            IsPresented = true;
            if (CurrentData.currentSettings.RussianMode == true)
            {
                opts.Title = "Опции";
                Nammee.Title = "Начало работы";
                xplore.Title = "Исследуйте свое StarrySky!";
                App.Current.Resources["Starsss"] = "Звезды";
                App.Current.Resources["Constsss"] = "Созвездия";
                App.Current.Resources["Planetssss"] = "Планеты";
                App.Current.Resources["MainMenu"] = "Главное меню";
                App.Current.Resources["Setsss"] = "Настройки";
            }
            else
            {
                App.Current.Resources["Starsss"] = "Stars";
                App.Current.Resources["Constsss"] = "Constellations";
                App.Current.Resources["Planetssss"] = "Planets";
                App.Current.Resources["MainMenu"] = "Main menu";
                App.Current.Resources["Setsss"] = "Settings";
            }
        }

        private async void ViewCell_Tapped(object sender, EventArgs e)
        {
            if (CurrentData.DONTMOVE == false)
            {
                if (CurrentData.currentSettings.IsAnimated)
                {
                    await MilkyWay.RotateTo(360, 500);
                    MilkyWay.Rotation = 0;
                }
                Detail = new NavigationPage(new MainMenu()) { BarBackgroundColor = Color.Thistle, BarTextColor = Color.Black };
                IsPresented = false;
            }
            else
            {
                if (CurrentData.currentSettings.RussianMode)
                {
                    DisplayAlert("Ошибка", "Вы должны перезагрузить приложение.", "Ок");
                }
                else DisplayAlert("Error", "You need to restart app.", "Ok");
            }

        }

        private  async void ViewCell_Tapped_1(object sender, EventArgs e)
        {
            if (CurrentData.DONTMOVE == false)
            {
                if (CurrentData.currentSettings.IsAnimated)
                {
                    await MilkyWay.RotateTo(360, 500);
                    MilkyWay.Rotation = 0;
                }
                Detail = new NavigationPage(new Constellations()) { BarBackgroundColor = Color.Thistle, BarTextColor = Color.Black };
                IsPresented = false;
            }
            else
            {
                if (CurrentData.currentSettings.RussianMode)
                {
                    DisplayAlert("Ошибка", "Вы должны перезагрузить приложение.", "Ок");
                }
                else DisplayAlert("Error", "You need to restart app.", "Ok");
            }
        }

        private async void ViewCell_Tapped_2(object sender, EventArgs e)
        {
            if (CurrentData.DONTMOVE == false)
            {
                if (CurrentData.currentSettings.IsAnimated)
                {
                    await MilkyWay.RotateTo(360, 500);
                    MilkyWay.Rotation = 0;
                }
                Detail = new NavigationPage(new Stars()) { BarBackgroundColor = Color.Thistle, BarTextColor = Color.Black };
                IsPresented = false;
            }
            else
            {
                if (CurrentData.currentSettings.RussianMode)
                {
                    DisplayAlert("Ошибка", "Вы должны перезагрузить приложение.", "Ок");
                }
                else DisplayAlert("Error", "You need to restart app.", "Ok");
            }
        }

        private async void ViewCell_Tapped_3(object sender, EventArgs e)
        {
            if (CurrentData.DONTMOVE == false)
            {
                if (CurrentData.currentSettings.IsAnimated)
                {
                    await MilkyWay.RotateTo(360, 500);
                    MilkyWay.Rotation = 0;
                }
                Detail = new NavigationPage(new Planets()) { BarBackgroundColor = Color.Thistle, BarTextColor = Color.Black };
                IsPresented = false;
            }
            else
            {
                if (CurrentData.currentSettings.RussianMode)
                {
                    DisplayAlert("Ошибка", "Вы должны перезагрузить приложение.", "Ок");
                }
                else DisplayAlert("Error", "You need to restart app.", "Ok");
            }
        }

        private async void ViewCell_Tapped_4(object sender, EventArgs e)
        {
            if (CurrentData.DONTMOVE == false)
            {
                if (CurrentData.currentSettings.IsAnimated)
                {
                    await MilkyWay.RotateTo(360, 500);
                    MilkyWay.Rotation = 0;
                }
                Detail = new NavigationPage(new Settings()) { BarBackgroundColor = Color.Thistle, BarTextColor = Color.Black };
                IsPresented = false;
            }
            else
            {
                if (CurrentData.currentSettings.RussianMode)
                {
                    DisplayAlert("Ошибка", "Вы должны перезагрузить приложение.", "Ок");
                }
                else DisplayAlert("Error", "You need to restart app.", "Ok");
            }
        }
    }
}
