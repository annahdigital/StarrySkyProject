using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using Xamarin.Essentials;

namespace StarrySky.ViewModels
{
    public class StarsVM : INotifyPropertyChanged, INotifyCollectionChanged
    {
        #region interface
        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action, Object source = null)
        {
            CollectionChanged?.Invoke(source is null ? this : source, new NotifyCollectionChangedEventArgs(action, source));
        }

        protected virtual void OnPropertyChanged([CallerMemberName]String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private ObservableCollection<Star> _stars_collection;
        private Star _selectedStar;

        public Star SelectedStar
        {
            get => _selectedStar;
            set
            {
                if (value == _selectedStar)
                    return;
                _selectedStar = value;
                OnPropertyChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Reset, _selectedStar);
            }
        }

        public ObservableCollection<Star> StarsCollection
        {
            get => _stars_collection;
            set
            {
                if (value == _stars_collection)
                    return;
                _stars_collection = value;
                OnPropertyChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Reset, _stars_collection);
            }
        }

        public StarsVM()
        {
            /*_stars_collection = new ObservableCollection<Star>(){
                new Star("Lyra", 2.362, 40.12, 2.135, "blue-tinged white main sequence star"),
                 new Star("Polaris", 1.04, 3, 1.39, "yellow supergiant"),
                 new Star("TryStar1", 1, 1, 1, "1"),
                 new Star("TryStar2", 2, 2, 2, "2"),
                 new Star("TryStar3", 3, 3, 3, "3"),
                 new Star("TryStar4", 4, 4, 4, "4")
             };*/
            Initialize();
            SelectedStar = null;
        }

        public void Initialize()
        {
            bool fileExists = File.Exists(FileSystem.AppDataDirectory + "/StarCollection.json");
            if (fileExists)
            {
                _stars_collection = ReadFromSerializedFile();
            }
            else
            {
                _stars_collection = new ObservableCollection<Star>(){
                new Star("Lyra", 2.362, 40.12, 2.135, "blue-tinged white main sequence star"),
                 new Star("Polaris", 1.04, 3, 1.39, "yellow supergiant"),
                 new Star("Proxima Centauri",   0.1542, 0.1221, 0.0017, "red dwarf"),
                 new Star("Sirius", 1.711, 2.063, 0.056, "white dwarfs"),
                 new Star("Antares", 680, 12, 9.7700, "blue-white main-sequence star"),
                 new Star("Barnard's Star", 0.196 , 0.144, 0.0035, "red dwarf"),
                 new Star("Arcturus", 25.4, 1.08, 170, "red giant"),
                 new Star("Altair", 1.79, 1.63, 10.6, " type-A main sequence star"),
                 new Star("Regulus", 3.092, 3.8, 288, "white dwarf"),
                 new Star("Alterf", 45, 8, 472, "variable star")
             };
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ObservableCollection<Star>));
                using (var fileStream = new FileStream(FileSystem.AppDataDirectory + "/StarCollection.json", FileMode.Create))
                {
                    jsonFormatter.WriteObject(fileStream, _stars_collection);
                }
            }

        }

        private ObservableCollection<Star> ReadFromSerializedFile()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ObservableCollection<Star>));
            using (var fileStream = new FileStream(FileSystem.AppDataDirectory + "/StarCollection.json", FileMode.Open))
            {
                fileStream.Position = 0;
                StreamReader streamReader = new StreamReader(fileStream);
                return (ObservableCollection<Star>)jsonFormatter.ReadObject(fileStream);
            }
        }

        public void SaveChangesInFile()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ObservableCollection<Star>));
            using (var fileStream = new FileStream(FileSystem.AppDataDirectory + "/StarCollection.json", FileMode.Create))
            {
                jsonFormatter.WriteObject(fileStream, _stars_collection);
            }
        }

        public bool DeleteStar()
        {
            if (_stars_collection.Contains(SelectedStar))
            {
                _stars_collection.Remove(SelectedStar);
                return true;
            }
            else return false;
        }
    }
}
