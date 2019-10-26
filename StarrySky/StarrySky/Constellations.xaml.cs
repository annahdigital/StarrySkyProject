using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StarrySky
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Constellations : ContentPage
    {
        ObservableCollection<Constellation> SortedCollection;
        bool wasSorted = false;
        bool wasSearched = false;

        public Constellations()
        {
            InitializeComponent();
            BindingContext = CurrentData.constellationsVM;
            ConstList.Opacity = 1;

            if (CurrentData.currentSettings.RussianMode)
            {
                AddConst.Text = "+";
                this.Title = "Созвездия";
                OptButton.Text = "Опции";
                SearchIt.Placeholder = "Введите название.";
            }
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
             CurrentData.constellationsVM.SelectedConstellation = e.SelectedItem as Constellation;
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
                else  answer = await DisplayAlert("Deleting", "Do you want to delete this element?", "Delete", "Cancel");
                if (answer == true)
                {
                    bool mes = CurrentData.constellationsVM.DeleteConstellation();
                    if (mes == false)
                    {
                        if (CurrentData.currentSettings.RussianMode)
                        {
                            await DisplayAlert("Удаление", "Элемент выбран неверно. Попробуйте еще раз.", "Ок", "Отмена");
                        }
                         else   await DisplayAlert("Deleting", "Try to select item properly.", "Ok", "Cancel");                   
                    } 
                    if (wasSorted == true)
                    {
                        SortedCollection.Remove(CurrentData.constellationsVM.SelectedConstellation);
                    }
                    if (wasSearched == true && mes == true)
                    {
                        ConstList.ItemsSource = CurrentData.constellationsVM.ConstellationsCollection;
                    }
                }
            }
        }

        private async void OptButton_Clicked(object sender, EventArgs e)
        {
            string action;
            if (CurrentData.currentSettings.RussianMode)
            {
                action = await DisplayActionSheet("Дополнительные опции", "Назад", "Ок", "Сортировать", "Вернуть обычный порядок");
            }
              else  action = await DisplayActionSheet("Additional options", "Back", "Ok", "Sort", "Back to the normal order");
            if (action != "Back" || action != "Ok" || action != "Назад" || action != "Ок")
                wasSearched = false;
            if (action == "Sort" || action == "Сортировать")
            {
                string newAction;
                if (CurrentData.currentSettings.RussianMode)
                {
                    newAction = await DisplayActionSheet("Сортировка", "Отмена", "Ок", "По имени (в алфавитном порядке)", "По имени (в обратном порядке)");
                }
                   else newAction = await DisplayActionSheet("Sorting", "Cancel", "Ok", "Sort by name (ascending)", "Sort by name (descending)");
                if (newAction == "Sort by name (ascending)" || newAction == "По имени (в алфавитном порядке)")
                {
                    wasSorted = true;
                    SortedCollection = new ObservableCollection<Constellation>();
                    var sortedItems =
                        from constellations in CurrentData.constellationsVM.ConstellationsCollection
                        orderby constellations.Name
                        select constellations;
                    foreach (Constellation thisConstellation in sortedItems)
                    {
                        SortedCollection.Add(thisConstellation);
                    }

                    ConstList.ItemsSource = SortedCollection;
                }
                if (newAction == "Sort by name (descending)" || newAction == "По имени (в обратном порядке)")
                {
                    wasSorted = true;
                    SortedCollection = new ObservableCollection<Constellation>();
                    var sortedItems =
                        from constellations in CurrentData.constellationsVM.ConstellationsCollection
                        orderby constellations.Name descending
                        select constellations;
                    foreach (Constellation thisConstellation in sortedItems)
                    {
                        SortedCollection.Add(thisConstellation);
                    }

                    ConstList.ItemsSource = SortedCollection;
                }
            }
            if (action == "Back to the normal order" || action == "Вернуть обычный порядок")
            {
                ConstList.ItemsSource = null;
                SortedCollection = null;
                ConstList.ItemsSource = CurrentData.constellationsVM.ConstellationsCollection;
                wasSorted = false;
            }
        }

        private void SearchIt_TextChanged(object sender, TextChangedEventArgs e)
        {
            ConstList.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                ConstList.ItemsSource = CurrentData.constellationsVM.ConstellationsCollection;
                wasSearched = false;
            }
            else
            {
                ConstList.ItemsSource = CurrentData.constellationsVM.ConstellationsCollection.Where(i => i.Name.ToLower().Contains(e.NewTextValue.ToLower()));
                wasSearched = true;
            }

            ConstList.EndRefresh();
        }

        private void ConstList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            CurrentData.constellationsVM.SelectedConstellation = e.Item as Constellation;
        }

        private async void AddConst_Clicked(object sender, EventArgs e)
        {
            if (CurrentData.currentSettings.IsAnimated)
            {
                await AddConst.ScaleTo(1.5, 100);
                await ConstList.FadeTo(0, 500);
                await AddConst.ScaleTo(1, 100);
            }
            await Navigation.PushAsync(new AddingConst());
            ConstList.Opacity = 1;
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            if (CurrentData.constellationsVM.SelectedConstellation == null)
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
                    await ConstList.FadeTo(0, 500);
                await Navigation.PushAsync(new AddingConst());
                ConstList.Opacity = 1;
            }
        }
    }
}