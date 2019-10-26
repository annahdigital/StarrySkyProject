using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StarrySky
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Settings : ContentPage
	{
		public Settings ()
		{
			InitializeComponent ();
            if (CurrentData.currentSettings.RussianMode)
            {
                this.Title = "Настройки";
                Lang1.Title = "Настройки языка";
                Anim1.Title = "Настройки анимации";
                ForAnimation.Text = "Включить/выключить анимацию";
                LanguageSet.Title = "Выберите язык";
                AnimationSwitch.IsToggled = CurrentData.currentSettings.IsAnimated;
            }
        }

        private async void AcceptLang_Clicked(object sender, EventArgs e)
        {
            if (CurrentData.currentSettings.IsAnimated)
            {
                await AcceptLang.ScaleTo(0.5, 20);
                await AcceptLang.ScaleTo(1, 20);
            }
            if (LanguageSet.SelectedItem != null)
            {
                CurrentData.DONTMOVE = true;
                string value = (string)LanguageSet.SelectedItem;
                if (value == "Русский")
                {
                    CurrentData.currentSettings.RussianMode = true;
                    DisplayAlert("Смена языка", "Язык будет сменен на русский полностью после перезагрузки приложения.", "Ок");

                }
                else
                {
                    CurrentData.currentSettings.RussianMode = false;
                    DisplayAlert("Language settings", "Language will be changed completely after app restart.", "Ok");
                }
            }

           /* var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                try
                {
                    Navigation.RemovePage(page);
                }
                catch (Exception ex) { }
            }*/


        }

        private void AnimationSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
            {
                CurrentData.currentSettings.IsAnimated = true;
                if (CurrentData.currentSettings.RussianMode == false)
                {
                    DisplayAlert("Animation", "Animation is enabled.", "Ok");
                }
                else DisplayAlert("Анимация", "Анимация включена.", "Ок");
            }
            else
            {
                CurrentData.currentSettings.IsAnimated = false;
                if (CurrentData.currentSettings.RussianMode == false)
                {
                    DisplayAlert("Animation", "Animation is disabled.", "Ok");
                }
                else DisplayAlert("Анимация", "Анимация выключена.", "Ок");

            }
        }
    }
}