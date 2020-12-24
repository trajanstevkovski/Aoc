using System.Collections.Generic;

namespace Aoc._2019.Day06
{
    public class Planet
    {
        public string Name { get; set; }
        public Planet PlanetThatOrbitsAround { get; set; }
        public List<Planet> PlanetsThatOrbitsAroundMe { get; set; } = new List<Planet>();

        public long OrbitsAround()
        {
            if (PlanetThatOrbitsAround == null) return 0;

            return PlanetThatOrbitsAround.OrbitsAround() + 1;
        }
    }
}
