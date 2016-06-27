using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Bossr.Annotations;
using Bossr.Lib;

namespace Bossr.ViewModels
{
    public class UpcomingPageViewModel : INotifyPropertyChanged
    {
        private World _selectedWorld;

        private List<Status> _upcoming;
        private bool _isLoading;

        public List<Status> Upcoming
        {
            get { return _upcoming; }
            set
            {
                _upcoming = value;
                OnPropertyChanged(nameof(Upcoming));
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task ReadUpcoming()
        {
            IsLoading = true;
            Upcoming = null;
            if (SelectedWorld != null)
                Upcoming = await App.RestService.ReadUpcomingAsync(SelectedWorld.Id);
            IsLoading = false;
        }
    }
}