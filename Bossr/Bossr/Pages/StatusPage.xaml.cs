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
        public StatusPage()
        {
            InitializeComponent();
            BindingContext = new StatusPageViewModel();
        }
    }
}
