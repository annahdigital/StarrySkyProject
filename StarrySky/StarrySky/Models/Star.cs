using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace StarrySky
{
    [DataContract]
    public class Star : IEquatable<Star>, IEnumerable<Planet>
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

        private string _name, _starType;

        [DataMember]
        public string Name //имя звезды
        {
            get => _name;
            set
            {
                if (value == _name)
                    return;
                _name = value;
                OnPropertyChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Reset, _name);
            }
        }

        private double _radius, _luminosity, _mass;

        [DataMember]
        public double Radius     //радиус (в солнечных радиусах)
        {
            get => _radius;
            set
            {
                if (value == _radius)
                    return;
                _radius = value;
                OnPropertyChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Reset, _radius);
            }
        }

        [DataMember]
        public double Mass      //масса (в солнечных массах)
        {
            get => _mass;
            set
            {
                if (value == _mass)
                    return;
                _mass = value;
                OnPropertyChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Reset, _mass);
            }
        }

        [DataMember]
        public double Luminosity    //светимость (в солнечных светимостях)
        {
            get => _luminosity;
            set
            {
                if (value == _luminosity)
                    return;
                _luminosity = value;
                OnPropertyChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Reset, _luminosity);
            }
        }

        [DataMember]
        public string StarType   //тип
        {
            get => _starType;
            set
            {
                if (value == _starType)
                    return;
                _starType = value;
                OnPropertyChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Reset, _starType);
            }
        }


        private List<Planet> thatplanets = new List<Planet>();        //планеты-спутники

        public void AddPlanets(string nm, double r, double m, double op, double sp, double or)
        {
            if (!thatplanets.Contains(new Planet(nm, r, m, op, sp, or)))
                thatplanets.Add(new Planet(nm, r, m, op, sp, or));
        }

        public void DeletePlanet(string nm)
        {
            thatplanets.Remove(FindPlanet(nm));
        }

        public Planet FindPlanet(string nm)
        {
            return thatplanets.Find(x => x.Name.Contains(nm));
        }

        public Star(string nm, double r, double l, double m, string t)
        {
            Name = nm;
            Radius = r;
            Mass = m;
            Luminosity = l;
            StarType = t;
        }

        public bool Equals(Star other)
        {
            if (this.Name == other.Name && this.Radius == other.Radius && this.Mass == other.Mass
                && this.Luminosity == other.Luminosity
                 && this.StarType == other.StarType)
                return true;
            else return false;
        }

        public IEnumerator<Planet> GetEnumerator()
        {
            return thatplanets.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


}

