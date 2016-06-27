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
    public class StatusPageViewModel : INotifyPropertyChanged
    {
        private World selectedWorld;

        public StatusPageViewModel(World selectedWorld)
        {
            this.selectedWorld = selectedWorld;
        }

        public World SelectedWorld
        {
            get { return selectedWorld; }
            set
            {
                selectedWorld = value;
                OnPropertyChanged(nameof(SelectedWorld));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
