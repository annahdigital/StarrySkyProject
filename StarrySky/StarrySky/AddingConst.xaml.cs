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
    public partial class AddingConst : ContentPage
    {
        public AddingConst()
        {
            InitializeComponent();
            ConstStack.Opacity = 1;

            if (CurrentData.currentSettings.RussianMode)
            {
                this.Title = "Добавление созвездия";
                NameEntry.Placeholder = NameLabel.Text = "Название";
                RAFEntry.Placeholder = RAFLabel.Text = "Прямое восхождение: от";
                RATEntry.Placeholder = RATLabel.Text = "Прямое восхождение: до";
                DFEntry.Placeholder = DFLabel.Text = "Склонение: от";
                DTEntry.Placeholder = DTLabel.Text = "Склонение: до";
                Add.Text = "+";
                Cancel.Text = "Отм ена";
            }

            if (CurrentData.editingMode == true)
            {
                if (CurrentData.currentSettings.RussianMode)
                {
                    this.Title = "Редактирование созвездия";
                }
                else this.Title = "Editing constellation";
                NameEntry.Text = CurrentData.constellationsVM.SelectedConstellation.Name;
                RAFEntry.Text = CurrentData.constellationsVM.SelectedConstellation.RightAscensionFirst;
                RATEntry.Text = CurrentData.constellationsVM.SelectedConstellation.RightAscensionLast;
                DFEntry.Text = CurrentData.constellationsVM.SelectedConstellation.DeclinationFirst;
                DTEntry.Text = CurrentData.constellationsVM.SelectedConstellation.DeclinationLast;
            }
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            if (CurrentData.currentSettings.IsAnimated)
            {
                await Cancel.ScaleTo(1.5, 20);
                await ConstStack.FadeTo(0, 500);
                await Cancel.ScaleTo(1, 20);
            }
            await Navigation.PopAsync();
            CurrentData.editingMode = false;
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            if (CurrentData.editingMode == true)
            {
                CurrentData.constellationsVM.ConstellationsCollection.Remove(CurrentData.constellationsVM.SelectedConstellation);
                CurrentData.editingMode = false;
            }
            if (CurrentData.currentSettings.IsAnimated)
            {
                await Add.ScaleTo(1.5, 20);
            }
                if (NameEntry.Text == null || RAFEntry.Text == null || RATEntry.Text == null ||
                    DFEntry.Text == null || DTEntry.Text == null)
                {
                    if (CurrentData.currentSettings.IsAnimated)
                {
                        await Add.ScaleTo(1, 20);
                    }
                    if (CurrentData.currentSettings.RussianMode)
                        {
                            await DisplayAlert("Ошибка", "Некоторые поля не заполнены. Пожалуйста, введите значения.", "Ок");
                        }
                     else await DisplayAlert("Error", "There are empty fields. Please, enter values.", "Ok");
                }
                else
                {
                    bool conste = CurrentData.constellationsVM.ConstellationsCollection.Any(i => i.Name.ToLower() == NameEntry.Text.ToLower());
                    if (conste == false)
                    {
                        CurrentData.constellationsVM.ConstellationsCollection.Add(new Constellation(NameEntry.Text, RAFEntry.Text,
                            RATEntry.Text, DFEntry.Text, DTEntry.Text));
                        if (CurrentData.currentSettings.IsAnimated)
                        {
                            await ConstStack.FadeTo(0, 500);
                            await Add.ScaleTo(1, 20);
                        }
                        await Navigation.PopAsync();
                        ConstStack.Opacity = 1;
                    }
                    else
                    {
                        if (CurrentData.currentSettings.IsAnimated)
                        {
                                await Add.ScaleTo(1, 20);
                        }
                                if (CurrentData.currentSettings.RussianMode)
                                {
                                await DisplayAlert("Ошибка", "Это созвездие уже было добавлено.", "Ок");
                                }
                              else await DisplayAlert("Error", "This constellation is already saved.", "Ok");
                    }
                }
        }

    }
}