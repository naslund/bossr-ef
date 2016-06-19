using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Bossr.Annotations;
using Bossr.Lib;

namespace Bossr.ViewModels
{
    public class StatusPageViewModel : INotifyPropertyChanged
    {
        private List<Status> statuses;
        private List<World> worlds;
        private World selectedWorld;
        private bool areStatusesLoading;
        private bool areWorldsLoading;

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

        public bool AreStatusesLoading
        {
            get { return areStatusesLoading; }
            set
            {
                areStatusesLoading = value;
                OnPropertyChanged(nameof(AreStatusesLoading));
            }
        }

        public bool AreWorldsLoading
        {
            get { return areWorldsLoading; }
            set
            {
                areWorldsLoading = value;
                OnPropertyChanged(nameof(AreWorldsLoading));
                OnPropertyChanged(nameof(AreWorldsLoaded));
            }
        }

        public bool AreWorldsLoaded => AreWorldsLoading == false;

        public async Task ReadStatuses()
        {
            AreStatusesLoading = true;
            Statuses = null;
            if (SelectedWorld != null)
                Statuses = await App.RestService.ReadStatusAsync(SelectedWorld.Id);
            AreStatusesLoading = false;
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
