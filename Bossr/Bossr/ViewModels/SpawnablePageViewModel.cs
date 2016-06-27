using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Bossr.Annotations;
using Bossr.Lib;

namespace Bossr.ViewModels
{
    public class SpawnablePageViewModel : INotifyPropertyChanged
    {
        private World _selectedWorld;

        private List<Status> _spawnable;
        private bool _isLoading;

        public List<Status> Spawnable
        {
            get { return _spawnable; }
            set
            {
                _spawnable = value;
                OnPropertyChanged(nameof(Spawnable));
            }
        }
        
        public World SelectedWorld
        {
            get { return _selectedWorld; }
            set
            {
                _selectedWorld = value;
                OnPropertyChanged(nameof(SelectedWorld));
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public async Task ReadSpawnable()
        {
            IsLoading = true;
            Spawnable = null;
            if (SelectedWorld != null)
                Spawnable = await App.RestService.ReadSpawnableAsync(SelectedWorld.Id);
            IsLoading = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
