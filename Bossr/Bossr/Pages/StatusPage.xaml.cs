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

        private void ListView_OnRefreshing(object sender, EventArgs e)
        {
            StatusPageViewModel.ReadStatuses();
            ((ListView) sender).IsRefreshing = false;
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView) sender).SelectedItem = null;
        }
    }
}
