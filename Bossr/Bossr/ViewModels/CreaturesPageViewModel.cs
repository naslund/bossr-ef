using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Bossr.Annotations;
using Bossr.Lib;
using Bossr.Utilities;

namespace Bossr.ViewModels
{
    public class CreaturesPageViewModel : INotifyPropertyChanged
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

        private List<Creature> _creatures;
        public List<Creature> Creatures
        {
            get { return _creatures; }
            set
            {
                _creatures = value;
                OnPropertyChanged(nameof(Creatures));
            }
        }

        private ObservableCollection<ObservableGroupCollection<Category, Creature>> _creaturesGrouped;

        public ObservableCollection<ObservableGroupCollection<Category, Creature>> CreaturesGrouped
        {
            get { return _creaturesGrouped; }
            set
            {
                _creaturesGrouped = value;
                OnPropertyChanged(nameof(CreaturesGrouped));
            }
        }


        public async Task ReadCreatures()
        {
            IsLoading = true;

            Creatures = await App.RestService.ReadCreaturesAsync();

            var v = Creatures
                .OrderByDescending(x => x.Monitored)
                .ThenBy(x => x.Category.Name)
                .ThenBy(x => x.Name)
                .GroupBy(x => x.Category)
                .Select(x => new ObservableGroupCollection<Category, Creature>(x))
                .ToList();
            
            CreaturesGrouped = new ObservableCollection<ObservableGroupCollection<Category, Creature>>(v);

            IsLoading = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class GroupedCreatures : ObservableCollection<Creature>
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
    }
}
