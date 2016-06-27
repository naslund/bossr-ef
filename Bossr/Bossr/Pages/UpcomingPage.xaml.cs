using System;
using Bossr.Lib;
using Bossr.ViewModels;
using Xamarin.Forms;

namespace Bossr.Pages
{
    public partial class UpcomingPage : ContentPage
    {
        private UpcomingPageViewModel UpcomingPageViewModel => (UpcomingPageViewModel) BindingContext;

        public UpcomingPage(World selectedWorld)
        {
            InitializeComponent();
            BindingContext = new UpcomingPageViewModel();
            UpcomingPageViewModel.SelectedWorld = selectedWorld;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await UpcomingPageViewModel.ReadUpcoming();
        }

        private async void ListView_OnRefreshing(object sender, EventArgs e)
        {
            await UpcomingPageViewModel.ReadUpcoming();
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView) sender).SelectedItem = null;
        }
    }
}
