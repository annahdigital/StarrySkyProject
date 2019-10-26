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
	public partial class AddingPlanets : ContentPage
	{
		public AddingPlanets ()
		{
			InitializeComponent ();
            PlanetStack.Opacity = 1;

            if (CurrentData.currentSettings.RussianMode)
            {
                this.Title = "Добавление планеты";
                NameEntry.Placeholder = NameLabel.Text = "Название";
                REntry.Placeholder = RLabel.Text = "Радиус";
                MEntry.Placeholder = MLabel.Text = " Масса";
                OPEntry.Placeholder = OPLabel.Text = "Орбитальный период";
                SPEntry.Placeholder = SPLabel.Text = "Период вращения вокруг звезды";
                OREntry.Placeholder = ORLabel.Text = "Орбитальный радиус";
                Add.Text = "+";
                Cancel.Text = "Отм ена";
            }

            if (CurrentData.editingMode == true)
            {
                if (CurrentData.currentSettings.RussianMode)
                {
                    this.Title = "Редактирование планеты";
                }
                else this.Title = "Editing planet";
                NameEntry.Text = CurrentData.planetsVM.SelectedPlanet.Name;
                MEntry.Text = CurrentData.planetsVM.SelectedPlanet.Mass.ToString();
                REntry.Text = CurrentData.planetsVM.SelectedPlanet.Radius.ToString();
                OPEntry.Text = CurrentData.planetsVM.SelectedPlanet.OrbitalPeriod.ToString();
                SPEntry.Text = CurrentData.planetsVM.SelectedPlanet.StarPeriod.ToString();
                OREntry.Text = CurrentData.planetsVM.SelectedPlanet.OrbitalRadius.ToString();
            }
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            if (CurrentData.editingMode == true)
            {
                CurrentData.planetsVM.PlanetsCollection.Remove(CurrentData.planetsVM.SelectedPlanet);
                CurrentData.editingMode = false;
            }
            if (CurrentData.currentSettings.IsAnimated)
                await Add.ScaleTo(1.5, 20);
            if (NameEntry.Text == null || MEntry.Text == null || REntry.Text == null ||
                OPEntry.Text == null || SPEntry.Text == null || OREntry.Text == null ||
                 MEntry.Text == "." || REntry.Text == "." ||
                OPEntry.Text == "." || SPEntry.Text == "." || OREntry.Text == "." ||
                 MEntry.Text == "-" || REntry.Text == "-" ||
                OPEntry.Text == "-" || SPEntry.Text == "-" || OREntry.Text == "-")
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
                bool conste = CurrentData.planetsVM.PlanetsCollection.Any(i => i.Name.ToLower() == NameEntry.Text.ToLower());
                if (conste == false)
                {
                    CurrentData.planetsVM.PlanetsCollection.Add(new Planet(NameEntry.Text, Convert.ToDouble(REntry.Text),
                        Convert.ToDouble(MEntry.Text), Convert.ToDouble(OPEntry.Text), Convert.ToDouble(SPEntry.Text),
                        Convert.ToDouble(OREntry.Text)));
                    if (CurrentData.currentSettings.IsAnimated)
                    { 
                        await PlanetStack.FadeTo(0, 500);
                        await Add.ScaleTo(1, 20);
                    }
                    await Navigation.PopAsync();
                    PlanetStack.Opacity = 1;
                }
                else
                {
                    if (CurrentData.currentSettings.IsAnimated)
                        await Add.ScaleTo(1, 20);
                    if (CurrentData.currentSettings.RussianMode)
                    {
                        await DisplayAlert("Ошибка", "Эта планета уже была добавлена.", "Ок");
                    }
                    else await DisplayAlert("Error", "This planet is already saved.", "Ok");
                }
            }
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            CurrentData.editingMode = false;
            if (CurrentData.currentSettings.IsAnimated)
            { 
                await Cancel.ScaleTo(1.5, 20);
                await PlanetStack.FadeTo(0, 500);
                await Cancel.ScaleTo(1, 20);
            }
            await Navigation.PopAsync();
            PlanetStack.Opacity = 1;
        }
    }
}