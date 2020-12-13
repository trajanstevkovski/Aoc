using Aoc.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc._2019.Day03
{
    public class DayThree : FileManager
    {
        private readonly string _inputPath;

        public DayThree()
        {
            _inputPath = Directory.GetCurrentDirectory() + "/Inputs/" + "Day03.txt";
        }

        private Func<char, Direction> getDirection = character => (Direction)character;

        public long PuzzleOne()
        {
            // var testInput = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7";
            var inputStr = GetStringContent(_inputPath);

            var inputs = inputStr
                .Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(',').SelectMany(str => GetLocations(str)))
                .ToArray();

            var startPoint = new Point();
            var wires = new Wire[inputs.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                var wire = new Wire
                {
                    Points = new Point[inputs[i].Sum(x => x.Moves)]
                };
                var listOfPoints = new List<Point>();
                foreach (var location in inputs[i])
                {
                    var points = location.MoveFromPoint(startPoint);
                    var lastPoint = points.Last();
                    startPoint.X = lastPoint.X;
                    startPoint.Y = lastPoint.Y;
                    listOfPoints.AddRange(points);
                }
                wire.Points = listOfPoints.ToArray();
                wires[i] = wire;
                startPoint = new Point();
            }

            if (!wires[0].IsIntersecting(wires[1], out Point[] intersections))
            {
                return 0;
            }
            return intersections.Select(x => Math.Abs(x.X) + Math.Abs(x.Y)).OrderBy(x => x).First();
        }

        public Location[] GetLocations(string input)
        {
            const string pattern = @"(^[RULD])(\d+)$";
            var regex = new Regex(pattern, RegexOptions.Compiled);
            return input.Split(',').Select(x =>
            {
                var match = regex.Match(x);
                return new Location
                {
                    Direction = getDirection(char.Parse(match.Groups[1].Value)),
                    Moves = int.Parse(match.Groups[2].Value)
                };
            }).ToArray();
        }
    }
}