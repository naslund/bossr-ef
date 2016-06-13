using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bossr.Navigation;
using Bossr.Services;
using Xamarin.Forms;

namespace Bossr
{
    public partial class App : Application
    {
        public static RestService RestService { get; private set; }

        public App()
        {
            InitializeComponent();
            
            RestService = new RestService();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
