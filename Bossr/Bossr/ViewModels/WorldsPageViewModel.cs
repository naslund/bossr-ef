using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Bossr.Annotations;
using Bossr.Lib;

namespace Bossr.ViewModels
{
    public class WorldsPageViewModel : INotifyPropertyChanged
    {
        private bool areWorldsLoading;
        private List<World> worlds;

        public bool AreWorldsLoading
        {
            get { return areWorldsLoading; }
            set
            {
                areWorldsLoading = value;
                OnPropertyChanged(nameof(AreWorldsLoading));
            }
        }

        public List<World> Worlds
        {
            get { return worlds; }
            set
            {
                worlds = value;
                OnPropertyChanged(nameof(Worlds));
            }
        }

        public async Task ReadWorlds()
        {
            AreWorldsLoading = true;
            Worlds = await App.RestService.ReadWorldsAsync();
            AreWorldsLoading = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
