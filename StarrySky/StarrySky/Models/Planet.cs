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
    public class Planet : IEquatable<Planet>
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

        private string _name, _starForThePlanet;
        private double _radius, _mass, _orbitalPeriod, _starPeriod, _orbitalRadius;

        [DataMember]
        public string Name {        //имя планеты
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

        [DataMember]
        public double Radius     //радиус (в радиусах Земли)
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
        public double Mass   //масса (в массах Земли)
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
        public double OrbitalPeriod  //период обращения вокруг своей оси (в днях)
        {
            get => _orbitalPeriod;
            set
            {
                if (value == _orbitalPeriod)
                    return;
                _orbitalPeriod = value;
                OnPropertyChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Reset, _orbitalPeriod);
            }
        }

        public string StarForThePlanet            //тело, вокруг которого вращается планета
        {
            get => _starForThePlanet;
            set
            {
                if (value == _starForThePlanet)
                    return;
                _starForThePlanet = value;
                OnPropertyChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Reset, _starForThePlanet);
            }
        }

        [DataMember]
        public double StarPeriod     //период обращения вокруг этого тела (в днях)
        {
            get => _starPeriod;
            set
            {
                if (value == _starPeriod)
                    return;
                _starPeriod = value;
                OnPropertyChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Reset, _starPeriod);
            }
        }

        [DataMember]
        public double OrbitalRadius  //радиус орбиты (в а.е.)
        {
            get => _orbitalRadius;
            set
            {
                if (value == _orbitalRadius)
                    return;
                _orbitalRadius = value;
                OnPropertyChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Reset, _orbitalRadius);
            }
        }

        public Planet(string nm, double r, double m, double op, double sp, double or)
        {
            Name = nm;
            Radius = r;
            Mass = m;
            OrbitalPeriod = op;
            StarPeriod = sp;
            OrbitalRadius = or;
        }

        public bool Equals(Planet other)
        {
            if (this.Name == other.Name && this.Radius == other.Radius && this.Mass == other.Mass 
                && this.OrbitalPeriod == other.OrbitalPeriod
                 && this.StarPeriod == other.StarPeriod && this.OrbitalRadius == other.OrbitalRadius)
                 return true;
            else  return false;
        }

    }
}

