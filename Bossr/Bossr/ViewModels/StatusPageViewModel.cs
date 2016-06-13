using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Bossr.Annotations;
using Bossr.Lib;

namespace Bossr.ViewModels
{
    public class StatusPageViewModel : INotifyPropertyChanged
    {
        private List<Status> statuses;
        private List<World> worlds;
        private World selectedWorld;

        public List<Status> Statuses
        {
            get { return statuses; }
            set
            {
                statuses = value;
                OnPropertyChanged(nameof(Statuses));
            }
        }
        
        public World SelectedWorld
        {
            get { return selectedWorld; }
            set
            {
                selectedWorld = value;
                ReadStatuses();
                OnPropertyChanged(nameof(SelectedWorld));
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

        public StatusPageViewModel()
        {
            ReadWorlds();
        }

        public async void ReadStatuses()
        {
            if (SelectedWorld != null)
                Statuses = await App.RestService.ReadStatusAsync(SelectedWorld.Id);
        }

        public async void ReadWorlds()
        {
            Worlds = await App.RestService.ReadWorldsAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
