﻿using System;
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
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private List<World> worlds;
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
            IsLoading = true;
            Worlds = await App.RestService.ReadWorldsAsync();
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
