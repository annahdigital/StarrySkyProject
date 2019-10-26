using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StarrySky
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Planets : ContentPage
	{

        ObservableCollection<Planet> SortedCollection;
        bool wasSorted = false;
        bool wasSearched = false;

        public Planets ()
		{
			InitializeComponent ();
            BindingContext = CurrentData.planetsVM;
            PlanetList.Opacity = 1;
            if (CurrentData.currentSettings.RussianMode)
            {
                AddPlanet.Text = "+";
                this.Title = "Планеты";
                OptButton.Text = "Опции";
                SearchIt.Placeholder = "Введите название.";
            }
    }

        private void PlanetList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CurrentData.planetsVM.SelectedPlanet = e.SelectedItem as Planet;
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                bool answer;
                if (CurrentData.currentSettings.RussianMode)
                {
                    answer = await DisplayAlert("Удаление", "Вы действительно хотите удалить этот элемент?", "Удалить", "Отмена");
                }
                else answer = await DisplayAlert("Deleting", "Do you want to delete this element?", "Delete", "Cancel");
                if (answer == true)
                {
                    bool mes = CurrentData.planetsVM.DeletePlanet();
                    if (mes == false)
                    {
                        if (CurrentData.currentSettings.RussianMode)
                        {
                            await DisplayAlert("Удаление", "Элемент выбран неверно. Попробуйте еще раз.", "Ок", "Отмена");
                        }
                        else await DisplayAlert("Deleting", "Try to select item properly.", "Ok", "Cancel");

                    }
                    if (wasSorted == true)
                    {
                        SortedCollection.Remove(CurrentData.planetsVM.SelectedPlanet);
                    }
                    if (wasSearched == true && mes == true)
                    {
                        PlanetList.ItemsSource = CurrentData.planetsVM.PlanetsCollection;
                    }
                }
            }
        }

        private async void OptButton_Clicked(object sender, EventArgs e)
        {
            string action;
            if (CurrentData.currentSettings.RussianMode)
            {
                action = await DisplayActionSheet("Дополнительные действия", "Назад", "Ок", "Сортировать", "Вернуть обычный порядок",
                "Наименьшие величины", "Наибольшие величины");
            }
            else
            {
                action = await DisplayActionSheet("Additional actions", "Back", "Ok", "Sort", "Back to the normal order",
                    "The smallest values", "The greatest values");
            }
            if (action != "Back" || action != "Ok" || action != "Назад" || action != "Ок")
                wasSearched = false;
            if (action == "Sort" || action == "Сортировать")
            {
                string newAction;
                if (CurrentData.currentSettings.RussianMode)
                {
                    newAction = await DisplayActionSheet("Сортировка", "Отмена", "Ок", "По возрастанию радиуса", "По убыванию радиуса",
                    "По возрастанию массы", "По убыванию массы");
                }
                else newAction = await DisplayActionSheet("Sorting", "Cancel", "Ok", "Sort by radius (ascending)", "Sort by radius (descending)",
                  "Sort by mass (ascending)", "Sort by mass (descending)");
                if  (newAction == "Sort by radius (ascending)" || newAction == "По возрастанию радиуса")
                {
                    wasSorted = true;
                    SortedCollection = new ObservableCollection<Planet>();
                    var sortedItems =
                        from planets in CurrentData.planetsVM.PlanetsCollection
                        orderby planets.Radius
                        select planets;
                    foreach (Planet thisPlanet in sortedItems)
                    {
                        SortedCollection.Add(thisPlanet);
                    }

                PlanetList.ItemsSource = SortedCollection;
                }
                if (newAction == "Sort by radius (descending)" || newAction == "По убыванию радиуса")
                {
                    wasSorted = true;
                    SortedCollection = new ObservableCollection<Planet>();
                    var sortedItems =
                        from planets in CurrentData.planetsVM.PlanetsCollection
                        orderby planets.Radius descending
                        select planets;
                    foreach (Planet thisPlanet in sortedItems)
                    {
                        SortedCollection.Add(thisPlanet);
                    }

                    PlanetList.ItemsSource = SortedCollection;
                }
                if (newAction == "Sort by mass (ascending)" || newAction == "По возрастанию массы")
                {
                    wasSorted = true;
                    SortedCollection = new ObservableCollection<Planet>();
                    var sortedItems =
                        from planets in CurrentData.planetsVM.PlanetsCollection
                        orderby planets.Mass
                        select planets;
                    foreach (Planet thisPlanet in sortedItems)
                    {
                        SortedCollection.Add(thisPlanet);
                    }

                    PlanetList.ItemsSource = SortedCollection;
                }
                if (newAction == "Sort by mass (descending)" || newAction == "По убыванию массы")
                {
                    wasSorted = true;
                    SortedCollection = new ObservableCollection<Planet>();
                    var sortedItems =
                        from planets in CurrentData.planetsVM.PlanetsCollection
                        orderby planets.Mass descending
                        select planets;
                    foreach (Planet thisPlanet in sortedItems)
                    {
                        SortedCollection.Add(thisPlanet);
                    }

                    PlanetList.ItemsSource = SortedCollection;
                }
            }
            if (action == "Back to the normal order" || action == "Вернуть обычный порядок" )
            {
                PlanetList.ItemsSource = null;
                SortedCollection = null;
                PlanetList.ItemsSource = CurrentData.planetsVM.PlanetsCollection;
                wasSorted = false;
            }
            if (action == "The greatest values" || action == "Наименьшие величины")
            {
                
                var biggestRadius = CurrentData.planetsVM.PlanetsCollection.Select(i => i.Radius).Max();
                var biggestMass = CurrentData.planetsVM.PlanetsCollection.Select(i => i.Mass).Max();
                var itemWithBR =  CurrentData.planetsVM.PlanetsCollection.Where(i => i.Radius == biggestRadius);
                var itemWithBM = CurrentData.planetsVM.PlanetsCollection.Where(i => i.Mass == biggestMass);
                string BR = "", BM = "";
                foreach (var i in itemWithBR)
                    BR = i.Name;
                foreach (var i in itemWithBM)
                    BM = i.Name;

                if (CurrentData.currentSettings.RussianMode)
                {
                    string result = "Наибольший радиус: \t" + biggestRadius + ", планета: \t" + BR + "\nНаибольшая масса: \t" +
                    biggestMass + ", планета: \t" + BM;
                    await DisplayAlert("Статистика", result, "Назад");
                }
                else
                {
                    string result = "The biggest radius: \t" + biggestRadius + ", planet: \t" + BR + "\nThe biggest mass: \t" +
                    biggestMass + ", planet: \t" + BM;
                    await DisplayAlert("Statistics", result, "Back");
                }
            }
            if (action == "The smallest values" || action == "Наибольшие величины")
            {
                var smallestRadius = CurrentData.planetsVM.PlanetsCollection.Select(i => i.Radius).Min();
                var smallestMass = CurrentData.planetsVM.PlanetsCollection.Select(i => i.Mass).Min();
                var itemWithSR = CurrentData.planetsVM.PlanetsCollection.Where(i => i.Radius == smallestRadius);
                var itemWithSM = CurrentData.planetsVM.PlanetsCollection.Where(i => i.Mass == smallestMass);
                string SR = "", SM = "";
                foreach (var i in itemWithSR)
                    SR = i.Name;
                foreach (var i in itemWithSM)
                    SM = i.Name;
                if (CurrentData.currentSettings.RussianMode)
                {
                    string result = "Наименьший радиус: " + smallestRadius + ", планета: " + SR + "\nНаименьшая масса: " +
                    smallestMass + ", планета: " + SM;
                    await DisplayAlert("Статистика", result, "Назад");
                }
                else
                {
                    string result = "The smallest radius: " + smallestRadius + ", planet: " + SR + "\nThe smallest mass: " +
                    smallestMass + ", planet: " + SM;
                    await DisplayAlert("Statistics", result, "Back");
                }
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
           PlanetList.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                PlanetList.ItemsSource = CurrentData.planetsVM.PlanetsCollection;
                wasSearched = false;
            }
            else
            {
                PlanetList.ItemsSource = CurrentData.planetsVM.PlanetsCollection.Where(i => i.Name.ToLower().Contains(e.NewTextValue.ToLower()));
                wasSearched = true;
            }

           PlanetList.EndRefresh();
        }

        private void PlanetList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            CurrentData.planetsVM.SelectedPlanet = e.Item as Planet;
        }

        private async void AddPlanet_Clicked(object sender, EventArgs e)
        {
            if (CurrentData.currentSettings.IsAnimated)
            {
                await AddPlanet.ScaleTo(1.5, 100);
                await PlanetList.FadeTo(0, 500);
                await AddPlanet.ScaleTo(1, 100);
            }
            await Navigation.PushAsync(new AddingPlanets());
            PlanetList.Opacity = 1;

        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            if (CurrentData.planetsVM.SelectedPlanet == null)
            {
                if (CurrentData.currentSettings.RussianMode)
                {
                    await DisplayAlert("Удаление", "Элемент выбран неверно. Попробуйте еще раз.", "Ок", "Отмена");
                }
                  else  await DisplayAlert("Error", "Try to select item properly.", "Ok", "Cancel");
            }
            else
            {
                CurrentData.editingMode = true;
                if (CurrentData.currentSettings.IsAnimated)
                    await PlanetList.FadeTo(0, 500);
                await Navigation.PushAsync(new AddingPlanets());
                PlanetList.Opacity = 1;
            }
        }
    }
}