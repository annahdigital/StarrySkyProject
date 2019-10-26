using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using Xamarin.Essentials;

namespace StarrySky
{
    [DataContract]
    public class AppSettings
    {

        private bool _isAnimated;
        private bool _RussianMode;

        [DataMember]
        public bool IsAnimated {
            get => _isAnimated;
            set
            {
                _isAnimated = value;
            }
        }

        [DataMember]
        public bool RussianMode
        {
            get => _RussianMode;
            set
            {
                _RussianMode = value;
            }
        }
        
    }
}
