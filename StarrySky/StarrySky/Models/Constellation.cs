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
    public class Constellation : IEnumerable<Star>, INotifyPropertyChanged, INotifyCollectionChanged
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

        private string _name;  //имя созвездия
        private string _imageLink; // +картиночка

        [DataMember]
        public string Name {
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

       
        public string ImageLink {
            get => _imageLink;
            set
            {
                if (value == _imageLink)
                    return;
                _imageLink = value;
                OnPropertyChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Reset, _imageLink);
            }
        } 

        private string _rightAscensionFirst;    //прямое восхождение от ... (для положения на звездной карте)
        private string _rightAscensionLast;    //прямое восхождение до ...(для положения на звездной карте)
        private string _declinationFirst;     //склонение от ... (для положения на звездной карте)
        private string _declinationLast;     //склонение до ... (для положения на звездной карте)

        [DataMember]
        public string RightAscensionFirst
        {
            get => _rightAscensionFirst;
            set
            {
                if (value == _rightAscensionFirst)
                    return;
                _rightAscensionFirst = value;
                OnPropertyChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Reset, _rightAscensionFirst);
            }
        }

        [DataMember]
        public string RightAscensionLast
        {
            get => _rightAscensionLast;
            set
            {
                if (value == _rightAscensionLast)
                    return;
                _rightAscensionLast = value;
                OnPropertyChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Reset, _rightAscensionLast);
            }
        }

        [DataMember]
        public string DeclinationFirst
        {
            get => _declinationFirst;
            set
            {
                if (value == _declinationFirst)
                    return;
                _declinationFirst = value;
                OnPropertyChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Reset, _declinationFirst);
            }
        }

        [DataMember]
        public string DeclinationLast
        {
            get => _declinationLast;
            set
            {
                if (value == _declinationLast)
                    return;
                _declinationLast = value;
                OnPropertyChanged();
                OnCollectionChanged(NotifyCollectionChangedAction.Reset, _declinationLast);
            }
        }
        public Constellation(string nm,  string raf, string ral, string df, string dl)
        {
            Name = nm;
            RightAscensionFirst = raf;
            RightAscensionLast = ral;
            DeclinationFirst = df;
            DeclinationLast = dl;
        }
        private List<Star> thatstars = new List<Star>();    //звезды в созвездии

        public void AddStar(string nm, double r, double l, double m, string t)
        {
            if (!thatstars.Contains(new Star(nm, r, l, m, t)))
                thatstars.Add(new Star(nm, r, l, m, t));
        }

        public void DeleteStar(string nm)
        {
            thatstars.Remove(FindStar(nm));
        }

        public Star FindStar(string nm)
        {
            return thatstars.Find(x => x.Name.Contains(nm));
        }

        public Star FindStar(double r)
        {
            return thatstars.Find(x => x.Radius == r);
        }

        public IEnumerator<Star> GetEnumerator()
        {
            return thatstars.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
