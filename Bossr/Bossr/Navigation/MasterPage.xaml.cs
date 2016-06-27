using System.Collections.Generic;
using Bossr.Pages;
using Xamarin.Forms;

namespace Bossr.Navigation
{
    public partial class MasterPage : ContentPage
    {
        public ListView ListView => listView;

        public MasterPage()
        {
            InitializeComponent();

            var masterPageItems = new List<MasterPageItem>
            {
                new MasterPageItem
                {
                    Title = "Status",
                    TargetType = typeof(WorldsPage)
                },
                new MasterPageItem
                {
                    Title = "Creatures",
                    TargetType = typeof(CreaturesPage)
                }
            };

            listView.ItemsSource = masterPageItems;
        }
    }
}
