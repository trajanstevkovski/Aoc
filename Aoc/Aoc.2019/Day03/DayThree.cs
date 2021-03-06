﻿using Aoc.Shared;
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
            // var testInput = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\r\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7";
            var inputStr = GetStringContent(_inputPath);

            var wires = CreateWires(inputStr);

            if (!wires[0].IsIntersecting(wires[1], out Point[] intersections))
            {
                return 0;
            }
            return intersections.Select(x => Math.Abs(x.X) + Math.Abs(x.Y)).OrderBy(x => x).First();
        }

        public long PuzzleTwo()
        {
            // var testInput = "R75,D30,R83,U83,L12,D49,R71,U7,L72\r\nU62,R66,U55,R34,D71,R55,D58,R83";
            var inputStr = GetStringContent(_inputPath);

            var wires = CreateWires(inputStr);

            _ = wires[0].IsIntersecting(wires[1], out Point[] intersections);

            var steps = int.MaxValue;
            foreach (var intersection in intersections)
            {
                var stepsTakenFirstWire = wires[0].Points.ToList().FindIndex(x => x.X == intersection.X && x.Y == intersection.Y);
                var stepsTakenSecondWire = wires[1].Points.ToList().FindIndex(x => x.X == intersection.X && x.Y == intersection.Y);

                if(stepsTakenFirstWire + stepsTakenSecondWire < steps)
                {
                    steps = stepsTakenFirstWire + stepsTakenSecondWire + 2; // + 2 because is indexed 0 :)
                }
            }

            return steps; 
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

        public Wire[] CreateWires(string inputStr)
        {
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

            return wires;
        }
    }
}