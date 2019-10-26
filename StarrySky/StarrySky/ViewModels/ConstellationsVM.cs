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
using System.Reflection;

namespace StarrySky.ViewModels
{
   
    public class ConstellationsVM: INotifyPropertyChanged, INotifyCollectionChanged
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
       
        private ObservableCollection<Constellation> _constellations_collection;
        private Constellation _selectedConstellation;

        public Constellation SelectedConstellation
        {
            get => _selectedConstellation;
            set
            {
                if (value == _selectedConstellation)
                    return;
                _selectedConstellation = value;
                OnPropertyChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Reset, _selectedConstellation);
            }
        }

       
        public ObservableCollection<Constellation> ConstellationsCollection {
            get =>_constellations_collection;
            set
            {
                if (value == _constellations_collection)
                    return;
                _constellations_collection = value;
                OnPropertyChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Reset, _constellations_collection);
            }
        }

        public ConstellationsVM()
        {
            /*  _constellations_collection = new ObservableCollection<Constellation>(){
                   new Constellation("Ursa Major", "7h 58m", "14h 25m","+29°", "+73° 30′" ),
                   new Constellation("Ursa Minor",  "0h 00m", "24h 00m", "+66°", "+90°"),
                    new Constellation("A",  "0h 00m", "24h 00m", "+66°", "+90°"),
                     new Constellation("B",  "0h 00m", "24h 00m", "+66°", "+90°"),
                     new Constellation("C",  "0h 00m", "24h 00m", "+66°", "+90°")
               };*/
            Initialize();
            SelectedConstellation = null;
        }

        public void Initialize()
        {
            bool fileExists = File.Exists(FileSystem.AppDataDirectory + "/ConstCollection.json");

            if (fileExists)
            {
                _constellations_collection = ReadFromSerializedFile();
            }
            else
            {
                _constellations_collection = new ObservableCollection<Constellation>(){
                  new Constellation("Ursa Major", "7h 58m", "14h 25m","+29°", "+73° 30′" ),
                  new Constellation("Ursa Minor",  "0h 00m", "24h 00m", "+66°", "+90°"),
                   new Constellation("Andrómeda",  "22h 52m", "2h 31m", "+21°", "+52°"),
                    new Constellation("Gemini",  "5h 53m", "8h 00m", "+10°", "+35°"),
                    new Constellation("Canis Major",  "6h 07m", "7h 22m", "−33°", "−11°"),
                    new Constellation("Libra",  "14h 15m", "15h 55m", "−29°", "0°"),
                    new Constellation("Aquárius",  "20h 32m", "23h 50m", "−25°", "+2°"),
                    new Constellation("Auríga",  "4h 30m", "7h 22m", "+28°", "+56°°"),
                    new Constellation("Lúpus",  "14h 10m", "16h 00m", "−55°", "−29°"),
                    new Constellation("Boótes",  "13h 30m", "15h 45m", "+8°", "+55°"),
                    new Constellation("Cóma Bereníces",  "11h 52m", "13h 30m", "+14°", "+34°")
              };
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ObservableCollection<Constellation>));
                using (var fileStream = new FileStream(FileSystem.AppDataDirectory + "/ConstCollection.json", FileMode.Create))
                {
                    jsonFormatter.WriteObject(fileStream, _constellations_collection);
                }
            }

        }

        private ObservableCollection<Constellation> ReadFromSerializedFile()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ObservableCollection<Constellation>));
            using (var fileStream = new FileStream(FileSystem.AppDataDirectory + "/ConstCollection.json", FileMode.Open))
            {
                fileStream.Position = 0;
                StreamReader streamReader = new StreamReader(fileStream);
                return (ObservableCollection<Constellation>)jsonFormatter.ReadObject(fileStream);
            }
        }

        public void SaveChangesInFile()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ObservableCollection<Constellation>));
            using (var fileStream = new FileStream(FileSystem.AppDataDirectory + "/ConstCollection.json", FileMode.Create))
            {
                jsonFormatter.WriteObject(fileStream, _constellations_collection);
            }
        }

        public bool DeleteConstellation()
        {
             if (_constellations_collection.Contains(SelectedConstellation))
             {
                 _constellations_collection.Remove(SelectedConstellation);
                  return true;
             }
             else return false;
        }
    }
}
