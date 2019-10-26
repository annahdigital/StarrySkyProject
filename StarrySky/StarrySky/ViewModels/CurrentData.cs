using System;
using System.Collections.Generic;
using System.Text;

namespace StarrySky
{
    public static class CurrentData
    {
        static public ViewModels.ConstellationsVM constellationsVM;
        static public ViewModels.PlanetsVM planetsVM;
        static public ViewModels.StarsVM starsVM;
        static public bool DONTMOVE;
        static public bool editingMode;
        static public AppSettings currentSettings = new AppSettings();
        static public string Name, RAF, RAT, DF, DT, DelIt, EditIt, Radius, Mass, OrbitalPeriod, StarPeriod, OrbitalRadius, Luminosity, StarType;

        static public void InitializeData()
        {
            constellationsVM = new ViewModels.ConstellationsVM();
            planetsVM = new ViewModels.PlanetsVM();
            starsVM = new ViewModels.StarsVM();
            editingMode = false;
            if (currentSettings.RussianMode)
            {
                Name = "Название"; RAF = "Прямое восхождение: от"; RAT = "Прямое восхождение: до";
                DF = "Склонение: от"; DT = "Склонение: до"; DelIt = "Удалить"; EditIt = "Редактировать";
                Radius = "Радиус"; Mass = "Масса"; OrbitalPeriod = "Орбитальный период"; StarPeriod = "Период (звездн.)";
                OrbitalRadius = "Радиус орбиты"; Luminosity = "Светимость"; StarType = "Тип звезды";

            }
            else
            {
                Name = "Name"; RAF = "Right ascension: from"; RAT = "Right ascension: to";
                DF = "Declination: from"; DT = "Declination: to"; DelIt = "Delete"; EditIt = "Edit";
                Radius = "Radius"; Mass = "Mass"; OrbitalPeriod = "Orbital period"; StarPeriod = "Star period";
                OrbitalRadius = "Orbital radius"; Luminosity = "Luminosity"; StarType = "Star type";
            }
        }


    }
}
