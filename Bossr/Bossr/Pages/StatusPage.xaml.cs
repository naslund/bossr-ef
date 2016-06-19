using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bossr.ViewModels;
using Xamarin.Forms;

namespace Bossr.Pages
{
    public partial class StatusPage : ContentPage
    {
        private StatusPageViewModel StatusPageViewModel => (StatusPageViewModel) BindingContext;

        public StatusPage()
        {
            InitializeComponent();
            BindingContext = new StatusPageViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await StatusPageViewModel.ReadWorlds();
        }

        private async void ListView_OnRefreshing(object sender, EventArgs e)
        {
            ((ListView)sender).IsRefreshing = false;
            await StatusPageViewModel.ReadStatuses();
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView) sender).SelectedItem = null;
        }
    }
}
