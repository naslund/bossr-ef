using System;
using Bossr.Lib;
using Bossr.ViewModels;
using Xamarin.Forms;

namespace Bossr.Pages
{
    public partial class RecentPage : ContentPage
    {
        private RecentPageViewModel RecentPageViewModel => (RecentPageViewModel) BindingContext;

        public RecentPage(World selectedWorld)
        {
            InitializeComponent();
            BindingContext = new RecentPageViewModel();
            RecentPageViewModel.SelectedWorld = selectedWorld;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await RecentPageViewModel.ReadRecent();
        }

        private async void ListView_OnRefreshing(object sender, EventArgs e)
        {
            await RecentPageViewModel.ReadRecent();
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView) sender).SelectedItem = null;
        }
    }
}
