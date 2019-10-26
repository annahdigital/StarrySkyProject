using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StarrySky
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainMenu : ContentPage
	{
		public MainMenu ()
		{
			InitializeComponent ();
            AnimatedStack.Opacity = 1;
            if (CurrentData.currentSettings.RussianMode)
            {
                this.Title = "Меню";
                Cst.Text = "Созвездия";
                Strs.Text = "Звезды";
                Plnts.Text = "Планеты";

            }
        }

        private async void ViewCell_Tapped(object sender, EventArgs e)
        {
            if (CurrentData.currentSettings.IsAnimated)
            {
                await constImage.ScaleTo(0.5, 100);
                await AnimatedStack.FadeTo(0, 300);
                await constImage.ScaleTo(1, 100);
            }
            await Navigation.PushAsync(new Constellations());
            AnimatedStack.Opacity = 1;
        }

        private async void ViewCell_Tapped_1(object sender, EventArgs e)
        {
            if (CurrentData.currentSettings.IsAnimated)
            {
                await starImage.ScaleTo(0.5, 100);
                await AnimatedStack.FadeTo(0, 300);
                await starImage.ScaleTo(1, 100);
            }
            await Navigation.PushAsync(new Stars());
            AnimatedStack.Opacity = 1;
        }

        private async void ViewCell_Tapped_2(object sender, EventArgs e)
        {
            if (CurrentData.currentSettings.IsAnimated)
            {
                await planetImage.ScaleTo(0.5, 100);
                await AnimatedStack.FadeTo(0, 300);
                await planetImage.ScaleTo(1, 100);
            }
            await Navigation.PushAsync(new Planets());
            AnimatedStack.Opacity = 1;
        }
    }
}