using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Bossr.Annotations;
using Bossr.Lib;

namespace Bossr.ViewModels
{
    public class RecentPageViewModel : INotifyPropertyChanged
    {
        private World _selectedWorld;

        private List<Kill> _recent;
        private bool _isLoading;

        public List<Kill> Recent
        {
            get { return _recent; }
            set
            {
                _recent = value;
                OnPropertyChanged(nameof(Recent));
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

        public async Task ReadRecent()
        {
            IsLoading = true;
            Recent = null;
            if (SelectedWorld != null)
                Recent = await App.RestService.ReadRecentAsync(SelectedWorld.Id);
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