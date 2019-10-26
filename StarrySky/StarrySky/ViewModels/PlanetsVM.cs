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
    public class PlanetsVM : INotifyPropertyChanged, INotifyCollectionChanged
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

        private ObservableCollection<Planet> _planets_collection;
        private Planet _selectedPlanet;

        public Planet SelectedPlanet
        {
            get => _selectedPlanet;
            set
            {
                if (value == _selectedPlanet)
                    return;
                _selectedPlanet = value;
                OnPropertyChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Reset, _selectedPlanet);
            }
        }

        public ObservableCollection<Planet> PlanetsCollection
        {
            get => _planets_collection;
            set
            {
                if (value == _planets_collection)
                    return;
                _planets_collection = value;
                OnPropertyChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Reset, _planets_collection);
            }
        }

        public PlanetsVM()
        {
            /*_planets_collection = new ObservableCollection<Planet>(){
                new Planet("Earth", 6371, 5.97237, 1, 365.256, 6356.8),
                new Planet("Venus", 6.051, 4.8675, 224.701, 583.92, 6.051),
                new Planet("TryPlanet1", 1, 1, 1, 1, 1) ,
                new Planet("TryPlanet2", 2, 2, 2, 2, 2),
                new Planet("TryPlanet3", 3, 3, 3, 3, 3),
                new Planet("TryPlanet4", 4, 4, 4, 4, 4) };*/
            Initialize();
            SelectedPlanet = null;
        }

        public void Initialize()
        {
            bool fileExists = File.Exists(FileSystem.AppDataDirectory + "/PlanetCollection.json");
            if (fileExists)
            {
                _planets_collection = ReadFromSerializedFile();
            }
            else
            {
                _planets_collection = new ObservableCollection<Planet>(){
                new Planet("Earth", 6371, 5.97237, 1, 365.256, 6356.8),
                new Planet("Venus", 6.051, 4.8675, 224.701, 583.92, 6.051),
                new Planet("Mercury", 2.439, 3.3011, 58.646, 115.88, 3829.1) ,
                new Planet("Mars", 3.3895, 6.4171, 1.025957, 779.96, 686.971),
                new Planet("Ceres", 4.73, 9.393, 3.23, 1681.63, 212.3),
                new Planet("Jupiter", 69.911, 1.8982, 9.925, 11.8624, 778.57),
                new Planet("Saturn", 58.232, 56.6834, 10.55, 378.09, 478.59),
                new Planet("Uranus", 25.362, 24.973, 0.8, 369.66, 129.218),
                new Planet("Neptune", 1.02413, 24.622, 0.6713, 367.49, 779.96),
                new Planet("Makemake", 715, 4.4, 7.771, 309.09, 739),
                new Planet("Haumea", 816, 4.006, 0.163146, 284.12, 224.701)
            };
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ObservableCollection<Planet>));
                using (var fileStream = new FileStream(FileSystem.AppDataDirectory + "/PlanetCollection.json", FileMode.Create))
                {
                    jsonFormatter.WriteObject(fileStream, _planets_collection);
                }
            }

        }

        private ObservableCollection<Planet> ReadFromSerializedFile()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ObservableCollection<Planet>));
            using (var fileStream = new FileStream(FileSystem.AppDataDirectory + "/PlanetCollection.json", FileMode.Open))
            {
                fileStream.Position = 0;
                StreamReader streamReader = new StreamReader(fileStream);
                return (ObservableCollection<Planet>)jsonFormatter.ReadObject(fileStream);
            }
        }

        public void SaveChangesInFile()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ObservableCollection<Planet>));
            using (var fileStream = new FileStream(FileSystem.AppDataDirectory + "/PlanetCollection.json", FileMode.Create))
            {
                jsonFormatter.WriteObject(fileStream, _planets_collection);
            }
        }

        public bool DeletePlanet()
        {
            if (_planets_collection.Contains(SelectedPlanet))
            {
                _planets_collection.Remove(SelectedPlanet);
                return true;
            }
            else return false;
        }
    }
}
