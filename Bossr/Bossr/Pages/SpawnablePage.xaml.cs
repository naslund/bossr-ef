using System;
using Bossr.Lib;
using Bossr.ViewModels;
using Xamarin.Forms;

namespace Bossr.Pages
{
    public partial class SpawnablePage : ContentPage
    {
        private SpawnablePageViewModel SpawnablePageViewModel => (SpawnablePageViewModel) BindingContext;

        public SpawnablePage(World selectedWorld)
        {
            InitializeComponent();
            BindingContext = new SpawnablePageViewModel();
            SpawnablePageViewModel.SelectedWorld = selectedWorld;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await SpawnablePageViewModel.ReadSpawnable();
        }

        private async void ListView_OnRefreshing(object sender, EventArgs e)
        {
            await SpawnablePageViewModel.ReadSpawnable();
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView) sender).SelectedItem = null;
        }
    }
}
