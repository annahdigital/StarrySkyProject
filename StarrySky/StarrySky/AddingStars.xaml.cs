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
	public partial class AddingStars : ContentPage
	{
		public AddingStars ()
		{
			InitializeComponent ();
            StarStack.Opacity = 1;

            if (CurrentData.currentSettings.RussianMode)
            {
                this.Title = "Добавление планеты";
                NameEntry.Placeholder = NameLabel.Text = "Название";
                REntry.Placeholder = RLabel.Text = "Радиус";
                MEntry.Placeholder = MLabel.Text = " Масса";
                LEntry.Placeholder = LLabel.Text = "Светимость";
                STEntry.Placeholder = STLabel.Text = "Тип звезды";
                Add.Text = "+";
                Cancel.Text = "Отм ена";
            }

            if (CurrentData.editingMode == true)
            {
                if (CurrentData.currentSettings.RussianMode)
                {
                    this.Title = "Редактирование звезды";
                }
                else this.Title = "Editing star";
                NameEntry.Text = CurrentData.starsVM.SelectedStar.Name;
                MEntry.Text = CurrentData.starsVM.SelectedStar.Mass.ToString();
                REntry.Text = CurrentData.starsVM.SelectedStar.Radius.ToString();
                LEntry.Text = CurrentData.starsVM.SelectedStar.Luminosity.ToString();
                STEntry.Text = CurrentData.starsVM.SelectedStar.StarType;
            }
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            if (CurrentData.editingMode == true)
            {
                CurrentData.starsVM.StarsCollection.Remove(CurrentData.starsVM.SelectedStar);
                CurrentData.editingMode = false;
            }
            if (CurrentData.currentSettings.IsAnimated)
                await Add.ScaleTo(1.5, 20);
            if (NameEntry.Text == null || REntry.Text == null || MEntry.Text == null ||
                LEntry.Text == null || STEntry.Text == null  ||
              MEntry.Text == "." || LEntry.Text == "." ||  REntry.Text == "." ||
              MEntry.Text == "-" || LEntry.Text == "-" || REntry.Text == "-")
            {
                if (CurrentData.currentSettings.IsAnimated)
                    await Add.ScaleTo(1, 20);
                if (CurrentData.currentSettings.RussianMode)
                {
                    await DisplayAlert("Ошибка", "Некоторые поля не заполнены. Пожалуйста, введите значения.", "Ок");
                }
                else await DisplayAlert("Error", "There are empty fields. Please, enter values.", "Ok");
            }
            else
            {
                bool conste = CurrentData.starsVM.StarsCollection.Any(i => i.Name.ToLower() == NameEntry.Text.ToLower());
                if (conste == false)
                {
                    CurrentData.starsVM.StarsCollection.Add(new Star(NameEntry.Text,Convert.ToDouble(REntry.Text), Convert.ToDouble(LEntry.Text),
                        Convert.ToDouble(MEntry.Text), STEntry.Text));
                    if (CurrentData.currentSettings.IsAnimated)
                    {
                        await StarStack.FadeTo(0, 500);
                        await Add.ScaleTo(1, 20);
                    }
                    await Navigation.PopAsync();
                    StarStack.Opacity = 1;
                }
                else
                {
                    if (CurrentData.currentSettings.IsAnimated)
                        await Add.ScaleTo(1, 20);
                    if (CurrentData.currentSettings.RussianMode)
                    {
                        await DisplayAlert("Ошибка", "Эта звезда уже была добавлена.", "Ок");
                    }
                    else await DisplayAlert("Error", "This star is already saved.", "Ok");
                }
            }
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            CurrentData.editingMode = false;
            if (CurrentData.currentSettings.IsAnimated)
            {
                await Cancel.ScaleTo(1.5, 20);
                await StarStack.FadeTo(0, 500);
                await Cancel.ScaleTo(1, 20);
            }
            await Navigation.PopAsync();
            StarStack.Opacity = 1;
        }
    }
}