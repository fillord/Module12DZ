using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module12DZ
{
    public abstract class Car
    {
        public string Name { get; set; }
        public int Speed { get; set; }

        public delegate void RaceEventHandler(string carName, int distance);
        public event RaceEventHandler RaceEvent;

        public void Drive()
        {
            Random random = new Random();
            for (int distance = 0; distance <= 100; distance += Speed)
            {
                if (RaceEvent != null)
                {
                    RaceEvent(Name, distance);
                }
                System.Threading.Thread.Sleep(100); 
            }
        }
    }

    public class SportsCar : Car
    {
        public SportsCar(string name, int speed)
        {
            Name = name;
            Speed = speed;
        }
    }

    public class Sedan : Car
    {
        public Sedan(string name, int speed)
        {
            Name = name;
            Speed = speed;
        }
    }

    public class Truck : Car
    {
        public Truck(string name, int speed)
        {
            Name = name;
            Speed = speed;
        }
    }

    public class Bus : Car
    {
        public Bus(string name, int speed)
        {
            Name = name;
            Speed = speed;
        }
    }

    public class RaceGame
    {
        public delegate void StartRaceHandler();
        public event StartRaceHandler StartRace;

        public delegate void RaceFinishHandler(string winner);
        public event RaceFinishHandler OnRaceFinish;

        public void Start()
        {
            if (StartRace != null)
            {
                StartRace();
            }
        }

        public void OnRaceEvent(string carName, int distance)
        {
            Console.WriteLine($"{carName} проехал {distance} км.");
        }

        public void FinishRace(string winner)
        {
            Console.WriteLine($"Гонка завершена! {winner} победил!");
            if (OnRaceFinish != null)
            {
                OnRaceFinish(winner);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            RaceGame raceGame = new RaceGame();

            SportsCar sportsCar = new SportsCar("Спортивный автомобиль", 10);
            Sedan sedan = new Sedan("Легковой автомобиль", 8);
            Truck truck = new Truck("Грузовик", 5);
            Bus bus = new Bus("Автобус", 6);

            raceGame.StartRace += sportsCar.Drive;
            raceGame.StartRace += sedan.Drive;
            raceGame.StartRace += truck.Drive;
            raceGame.StartRace += bus.Drive;

            sportsCar.RaceEvent += raceGame.OnRaceEvent;
            sedan.RaceEvent += raceGame.OnRaceEvent;
            truck.RaceEvent += raceGame.OnRaceEvent;
            bus.RaceEvent += raceGame.OnRaceEvent;

            raceGame.StartRace += () => Console.WriteLine("Гонка началась!");

            raceGame.OnRaceFinish += raceGame.FinishRace;

            raceGame.Start();
        }
    }
}
