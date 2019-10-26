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
	public partial class Stars : ContentPage
	{
        ObservableCollection<Star> SortedCollection;
        bool wasSorted = false;
        bool wasSearched = false;

        public Stars()
        {
            InitializeComponent();
            BindingContext = CurrentData.starsVM;
            StarList.Opacity = 1;
            if (CurrentData.currentSettings.RussianMode)
            {
                AddStar.Text = "+";
                this.Title = "Звезды";
                OptButton.Text = "Опции";
                SearchIt.Placeholder = "Введите название.";
            }
        }

        private void StarList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CurrentData.starsVM.SelectedStar = e.SelectedItem as Star;
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
                    bool mes = CurrentData.starsVM.DeleteStar();
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
                        SortedCollection.Remove(CurrentData.starsVM.SelectedStar);
                    }
                    if (wasSearched == true && mes == true)
                    {
                        StarList.ItemsSource = CurrentData.starsVM.StarsCollection;
                        wasSearched = false;
                    }
                }
            }
        }

        private async void OptButton_Clicked(object sender, EventArgs e)
        {
            string action;
            if (CurrentData.currentSettings.RussianMode)
            {
                action = await DisplayActionSheet("Дополнительные действия", "Назад", "Ок", "Сортировать", "Вернуть исходный порядок",
                "Наименьшие величины", "Наибольшие величины");
            }
              else  action = await DisplayActionSheet("Additional actions", "Back", "Ok", "Sort", "Back to the normal order",
                "The smallest values", "The greatest values");
            if (action != "Back" || action != "Ok" || action != "Назад" || action != "Ок")
                wasSearched = false;
            if (action == "Sort" || action == "Сортировать")
            {
                string newAction;
                if (CurrentData.currentSettings.RussianMode)
                {
                    newAction = await DisplayActionSheet("Сортировка", "Отмена", "Ок", "По возрастанию радиуса", "По убыванию радиуса",
                "По возрастанию массы", "По убыванию массы", "По возрастанию светимости", "По убыванию светимости");
                }
                else
                {
                    newAction = await DisplayActionSheet("Sorting", "Cancel", "Ok", "Sort by radius (ascending)", "Sort by radius (descending)",
                    "Sort by mass (ascending)", "Sort by mass (descending)", "Sort by luminosity (ascending)", "Sort by luminosity (descending)");
                }
                if (newAction == "Sort by radius (ascending)" || newAction == "По возрастанию радиуса")
                {
                    wasSorted = true;
                    SortedCollection = new ObservableCollection<Star>();
                    var sortedItems =
                        from stars in CurrentData.starsVM.StarsCollection
                        orderby stars.Radius
                        select stars;
                    foreach (Star thisStar in sortedItems)
                    {
                        SortedCollection.Add(thisStar);
                    }

                    StarList.ItemsSource = SortedCollection;
                }
                if (newAction == "Sort by radius (descending)" || newAction == "По убыванию радиуса")
                {
                    wasSorted = true;
                    SortedCollection = new ObservableCollection<Star>();
                    var sortedItems =
                        from stars in CurrentData.starsVM.StarsCollection
                        orderby stars.Radius descending
                        select stars;
                    foreach (Star thisStar in sortedItems)
                    {
                        SortedCollection.Add(thisStar);
                    }

                    StarList.ItemsSource = SortedCollection;
                }
                if (newAction == "Sort by mass (ascending)" || newAction == "По возрастанию массы")
                {
                    wasSorted = true;
                    SortedCollection = new ObservableCollection<Star>();
                    var sortedItems =
                        from stars in CurrentData.starsVM.StarsCollection
                        orderby stars.Mass
                        select stars;
                    foreach (Star thisStar in sortedItems)
                    {
                        SortedCollection.Add(thisStar);
                    }

                    StarList.ItemsSource = SortedCollection;
                }
                if (newAction == "Sort by mass (descending)" || newAction == "По убыванию массы")
                {
                    wasSorted = true;
                    SortedCollection = new ObservableCollection<Star>();
                    var sortedItems =
                        from stars in CurrentData.starsVM.StarsCollection
                        orderby stars.Mass descending
                        select stars;
                    foreach (Star thisStar in sortedItems)
                    {
                        SortedCollection.Add(thisStar);
                    }

                    StarList.ItemsSource = SortedCollection;
                }
                if (newAction == "Sort by luminosity (ascending)" || newAction == "По возрастанию светимости" )
                {
                    wasSorted = true;
                    SortedCollection = new ObservableCollection<Star>();
                    var sortedItems =
                        from stars in CurrentData.starsVM.StarsCollection
                        orderby stars.Luminosity
                        select stars;
                    foreach (Star thisStar in sortedItems)
                    {
                        SortedCollection.Add(thisStar);
                    }

                    StarList.ItemsSource = SortedCollection;
                }
                if (newAction == "Sort by luminosity (descending)" || newAction == "По убыванию светимости")
                {
                    wasSorted = true;
                    SortedCollection = new ObservableCollection<Star>();
                    var sortedItems =
                        from stars in CurrentData.starsVM.StarsCollection
                        orderby stars.Luminosity descending
                        select stars;
                    foreach (Star thisStar in sortedItems)
                    {
                        SortedCollection.Add(thisStar);
                    }

                    StarList.ItemsSource = SortedCollection;
                }
            }
            if (action == "Back to the normal order" || action == "Вернуть обычный порядок")
            {
                StarList.ItemsSource = null;
                SortedCollection = null;
                StarList.ItemsSource = CurrentData.starsVM.StarsCollection;
                wasSorted = false;
            }
            if (action == "The greatest values" || action == "Наибольшие величины")
            {
                var biggestRadius = CurrentData.starsVM.StarsCollection.Select(i => i.Radius).Max();
                var biggestMass = CurrentData.starsVM.StarsCollection.Select(i => i.Mass).Max();
                var biggestLuminosity = CurrentData.starsVM.StarsCollection.Select(i => i.Luminosity).Max();
                var itemWithBR = CurrentData.starsVM.StarsCollection.Where(i => i.Radius == biggestRadius);
                var itemWithBM = CurrentData.starsVM.StarsCollection.Where(i => i.Mass == biggestMass);
                var itemWithL = CurrentData.starsVM.StarsCollection.Where(i => i.Luminosity == biggestLuminosity);
                string BR = "", BM = "", L = "";
                foreach (var i in itemWithBR)
                    BR = i.Name;
                foreach (var i in itemWithBM)
                    BM = i.Name;
                foreach (var i in itemWithL)
                    L = i.Name;
                if (CurrentData.currentSettings.RussianMode)
                {
                    string result = "Наибольший радиус: " + biggestRadius + ", звезда: " + BR + "\nНаибольшая масса: " +
                    biggestMass + ", звезда: " + BM + "\nНаибольшая светимость: " + biggestLuminosity + ", звезда: " + L;
                    await DisplayAlert("Statistics", result, "Back");
                }
                else
                {
                    string result = "The biggest radius: " + biggestRadius + ", star: " + BR + "\nThe biggest mass: " +
                    biggestMass + ", star: " + BM + "\nThe greatest luminosity: " + biggestLuminosity + ", star: " + L;
                    await DisplayAlert("Statistics", result, "Back");
                }
            }
            if (action == "The smallest values" || action == "Наименьшие величины")
            {
                var smallestRadius = CurrentData.starsVM.StarsCollection.Select(i => i.Radius).Min();
                var smallestMass = CurrentData.starsVM.StarsCollection.Select(i => i.Mass).Min();
                var smallestLuminosity = CurrentData.starsVM.StarsCollection.Select(i => i.Luminosity).Min();
                var itemWithSR = CurrentData.starsVM.StarsCollection.Where(i => i.Radius == smallestRadius);
                var itemWithSM = CurrentData.starsVM.StarsCollection.Where(i => i.Mass == smallestMass);
                var itemWithL = CurrentData.starsVM.StarsCollection.Where(i => i.Luminosity == smallestLuminosity);
                string SR = "", SM = "", L = "";
                foreach (var i in itemWithSR)
                    SR = i.Name;
                foreach (var i in itemWithSM)
                    SM = i.Name;
                foreach (var i in itemWithL)
                    L = i.Name;
                if (CurrentData.currentSettings.RussianMode)
                {
                    string result = "Наименьший радиус: \t" + smallestRadius + ", звезда: \t" + SR + "\nНаименьшая масса: \t" +
                    smallestMass + ", звезда: \t" + SM + "\nНаименьшая светимость: \t" + smallestLuminosity + ", звезда: \t" + L;
                    await DisplayAlert("Статистика", result, "Назад");
                }
                else
                {
                    string result = "The smallest radius: \t" + smallestRadius + ", star: \t" + SR + "\nThe smallest mass: \t" +
                    smallestMass + ", star: \t" + SM + "\nThe smallest luminosity: \t" + smallestLuminosity + ", star: \t" + L;
                    await DisplayAlert("Statistics", result, "Back");
                }
            }
        }


        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            StarList.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                StarList.ItemsSource = CurrentData.starsVM.StarsCollection;
                wasSearched = false;
            }
            else
            {
                StarList.ItemsSource = CurrentData.starsVM.StarsCollection.Where(i => i.Name.ToLower().Contains(e.NewTextValue.ToLower()));
                wasSearched = true;
            }

            StarList.EndRefresh();
        }

        private void StarList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            CurrentData.starsVM.SelectedStar = e.Item as Star;
        }

        private async void AddStar_Clicked(object sender, EventArgs e)
        {
            if (CurrentData.currentSettings.IsAnimated)
            {
                await AddStar.ScaleTo(1.5, 100);
                await StarList.FadeTo(0, 500);
                await AddStar.ScaleTo(1, 100);
            }
            await Navigation.PushAsync(new AddingStars());
            StarList.Opacity = 1;
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            if (CurrentData.starsVM.SelectedStar == null)
            {
                if (CurrentData.currentSettings.RussianMode)
                {
                    await DisplayAlert("Удаление", "Элемент выбран неверно. Попробуйте еще раз.", "Ок", "Отмена");
                }
                else await DisplayAlert("Error", "Try to select item properly.", "Ok", "Cancel");
            }
            else
            {
                CurrentData.editingMode = true;
                if (CurrentData.currentSettings.IsAnimated)
                    await StarList.FadeTo(0, 500);
                await Navigation.PushAsync(new AddingStars());
                StarList.Opacity = 1;
            }
        }
    }
}
 
 