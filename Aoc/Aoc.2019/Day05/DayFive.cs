﻿using Aoc._2019.Day02;
using Aoc.Shared;
using System.IO;
using System.Linq;

namespace Aoc._2019.Day05
{
    public class DayFive : FileManager
    {
        private readonly string _inputPath;

        public DayFive()
        {
            _inputPath = Directory.GetCurrentDirectory() + "/Inputs/" + "Day05.txt";
        }

        public long PuzzleOne()
        {
            var inputArr = GetStringContent(_inputPath);
            //var inputStr = "3,0,4,0,99";
            var splitInput = inputArr.Split(',').Select(x => int.Parse(x)).ToArray();

            var intcode = new Intcode();

            return intcode.TheThermalEnvironmentSupervisionTerminal(splitInput, 1);
        }

        public long PuzzleTwo()
        {
            return 0;
        }
    }
}
