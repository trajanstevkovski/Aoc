using Aoc.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Aoc._2019.Day06
{
    public class DaySix : FileManager
    {
        private readonly string _inputPath;

        public DaySix()
        {
            _inputPath = Directory.GetCurrentDirectory() + "/Inputs/" + "Day06.txt";
        }

        public long PuzzleOne()
        {
            const string centerOfMass = "COM"; 
            var input = GetFileContent(_inputPath).OrderByDescending(x => x.StartsWith(centerOfMass)).ToArray();

            var findCom = input.FirstOrDefault(x => x.StartsWith(centerOfMass)).Split(')');
            var center = new Planet { Name = findCom[0] };
            var first = new Planet { Name = findCom[1], PlanetThatOrbitsAround = center };
            center.PlanetsThatOrbitsAroundMe.Add(first);

            var planets = CreateOrbitMap(first, ref input);
            //var result = CountPlanetOrbits(center);

            return CountPlanetOrbits(center);
        }

        public long PuzzleTwo()
        {
            const string centerOfMass = "COM";
            var input = GetFileContent(_inputPath).OrderByDescending(x => x.StartsWith(centerOfMass)).ToArray();

            var findCom = input.FirstOrDefault(x => x.StartsWith(centerOfMass)).Split(')');
            var center = new Planet { Name = findCom[0] };
            var first = new Planet { Name = findCom[1], PlanetThatOrbitsAround = center };
            center.PlanetsThatOrbitsAroundMe.Add(first);

            var planets = CreateOrbitMap(first, ref input);
            var result = GetSanta(center);

            return result.OrbitsApart;
        }

        public void PrintPlanetNames(Planet planet)
        {
            Console.WriteLine(planet.Name);
            if(planet.PlanetsThatOrbitsAroundMe.Count != 0)
            {
                foreach (var planet1 in planet.PlanetsThatOrbitsAroundMe)
                {
                    PrintPlanetNames(planet1);
                }
            }
        }

        public SantaResult GetSanta(Planet planet)
        {
            //Debug.WriteLine($"Planet ON: {planet.Name}");
            if (planet.Name == "SAN")
            {
                return new SantaResult
                {
                    IsSanta = true,
                };
            }

            if(planet.Name == "YOU")
            {
                return new SantaResult
                {
                    IsYou = true,
                };
            }

            if(planet.PlanetsThatOrbitsAroundMe.Count == 0)
            {
                return new SantaResult();
            }

            var santaResultLeft = new SantaResult();
            var santaResultRight = new SantaResult();
            if (planet.PlanetsThatOrbitsAroundMe.Count == 1)
            {
                santaResultLeft = GetSanta(planet.PlanetsThatOrbitsAroundMe[0]);
            }
            else if(planet.PlanetsThatOrbitsAroundMe.Count == 2)
            {
                santaResultLeft = GetSanta(planet.PlanetsThatOrbitsAroundMe[0]);
                santaResultRight = GetSanta(planet.PlanetsThatOrbitsAroundMe[1]);
            }

            if ((santaResultLeft.IsSanta || santaResultLeft.IsYou) && (santaResultRight.IsSanta || santaResultRight.IsYou))
            {
                Debug.WriteLine($"Planet ON: {planet.Name} BOTH");
                return new SantaResult
                {
                    IsSanta = false,
                    IsYou = false,
                    OrbitsApart = santaResultLeft.OrbitsApart + santaResultRight.OrbitsApart,
                };
            }

            if (santaResultLeft.IsSanta ||  santaResultLeft.IsYou)
            {
                Debug.WriteLine($"Planet ON: {planet.Name} LEFT");
                santaResultLeft.OrbitsApart += 1;
            }

            if (santaResultRight.IsSanta || santaResultRight.IsYou)
            {
                Debug.WriteLine($"Planet ON: {planet.Name} RIGHT");
                santaResultRight.OrbitsApart += 1;
            }

            Debug.WriteLine(santaResultLeft.OrbitsApart + santaResultRight.OrbitsApart);

            return new SantaResult
            {
                IsSanta = santaResultLeft.IsSanta || santaResultRight.IsSanta,
                IsYou = santaResultLeft.IsYou || santaResultRight.IsYou,
                OrbitsApart = santaResultLeft.OrbitsApart + santaResultRight.OrbitsApart,
            };
        }

        public long CountPlanetOrbits(Planet planet)
        {
            Console.WriteLine(planet.Name);
            long count = planet.OrbitsAround();
            
            if (planet.PlanetsThatOrbitsAroundMe.Count == 0) return count;

            foreach (var p1 in planet.PlanetsThatOrbitsAroundMe)
            {
                count += CountPlanetOrbits(p1);
            }

            return count;
        }

        public Planet CreateOrbitMap(Planet prevPlanet, ref string[] planets)
        {
            var planetNames = planets.Where(x => x.StartsWith(prevPlanet.Name, StringComparison.InvariantCultureIgnoreCase));

            if(planetNames.Count() == 0)
            {
                return prevPlanet;
            }

            foreach (var names in planetNames)
            {
                if (names.StartsWith("COM"))
                {
                    continue;
                }
                var str = names.Split(')');
                var planet = new Planet
                {
                    Name = str[1],
                    PlanetThatOrbitsAround = prevPlanet
                };
                prevPlanet.PlanetsThatOrbitsAroundMe.Add(CreateOrbitMap(planet, ref planets));
            }

            return prevPlanet;
        }
    }
}