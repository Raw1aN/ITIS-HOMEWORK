using System;
using System.Collections.Generic;
using System.Linq;

namespace TRAINS
{
    class Carriage
    {
        public string Name { get; set; }

        public Carriage(string name)
        {
            Name = name;
        }

    }
    class Station
    {
        public string Name { get; set; }
        public DateTime TimeOfArrival { get; set; }
        public DateTime TimeOfDeparture { get; set; }
    }
    class Train
    {
        public string Name { get; set; }
        public List<Carriage> Carriages { get; set; }
        public List<Station> Stations { get; set; }
        public Train(List<Carriage> carriages, List<Station> stations)
        {
            Carriages = carriages;
            Stations = stations;
        }
        public Station this[string name]
        {
            get
            {
                return Stations.FirstOrDefault(x => x.Name == name);
            }
            set
            {
                var index = Stations.FindIndex(x => x.Name == name);
                Stations[index] = value;
            }
        }
        public TimeSpan TimeGoing(Station station1, Station station2)
        {
            return station2.TimeOfArrival - station1.TimeOfDeparture;
        }
        public void Print()
        {
            Console.WriteLine($"Имя поезда : {Name}");
            Console.WriteLine("Вагоны :");
            foreach (var c in Carriages)
            {
                Console.WriteLine($"{c.Name}");
            }
            foreach (var s in Stations)
            {
                Console.WriteLine($"Станция :{s.Name}");
                Console.WriteLine($"Время отправки : {s.TimeOfArrival}");
                Console.WriteLine($"Время прибытия : {s.TimeOfDeparture}");
            }
            Station last = null;
            Console.WriteLine("Время путей между станциями ");
            foreach (var s in Stations)
            {
                if (last is null)
                {
                    last = s;
                    continue;
                }
                var t = TimeGoing(last, s);
                Console.WriteLine($"Время в пути между станцией {s.Name} и {last.Name} : {t}");
            }

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var carriages = new List<Carriage>();
            var stations = new List<Station>();

            carriages.Add(new Carriage("l-1"));
            carriages.Add(new Carriage("l-2"));
            carriages.Add(new Carriage("l-3"));

            var station1 = new Station();
            var station2 = new Station();
            var station3 = new Station();
            station1.Name = "Moscow";
            station2.Name = "Samara";
            station3.Name = "Kazan";
            station1.TimeOfArrival = DateTime.Parse("13:05");
            station2.TimeOfArrival = DateTime.Parse("16:30");
            station3.TimeOfArrival = DateTime.Parse("19:15");

            station1.TimeOfDeparture = DateTime.Parse("15:05");
            station2.TimeOfDeparture = DateTime.Parse("17:40");
            station3.TimeOfDeparture = DateTime.Parse("22:20");

            stations.Add(station1);
            stations.Add(station2);
            stations.Add(station3);
            var train = new Train(carriages, stations);
            train.Name = "Ласточка";
            train.Print();

            var st = train.Stations.FirstOrDefault(x => x.Name == "Moscow");
            var st2 = train["Moscow"];
            Console.WriteLine(st2.TimeOfArrival);

            train["Moscow"] = new Station()
            {
                Name = "Ukraina"
            };
            foreach (var item in train.Stations)
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}
