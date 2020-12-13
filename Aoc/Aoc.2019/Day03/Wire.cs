using System.Linq;

namespace Aoc._2019.Day03
{
    public class Wire
    {
        public Point[] Points { get; set; }

        public bool IsIntersecting(Wire wire, out Point[] intersections)
        {
            intersections = Points.Intersect(wire.Points).ToArray();
            return intersections.Length > 0;
        }
    }
}