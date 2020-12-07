using Aoc.Shared;
using System;
using System.IO;
using System.Linq;

namespace Aoc._2019.Day01
{
    public class DayOne : FileManager
    {
        private readonly string _inputPath;
        private Func<double, int> _fuelCalculationFormula = x => (int)Math.Floor(x / 3) - 2;

        public DayOne()
        {
            _inputPath = Directory.GetCurrentDirectory() + "/Inputs/" + "Day01.txt";
        }

        public long PuzzleOne()
        {
            var input = GetFileContent(_inputPath);
            return (long)input.Select(x => double.Parse(x)).Select(x => Math.Floor(x / 3) - 2).Sum();
        }

        public long PuzzleTwo()
        {
            var input = GetFileContent(_inputPath);
            long result = 0;
            for (int i = 0; i < input.Length; i++)
            {
                result += CalculateFuelToZero(int.Parse(input[i]));
            }
            return result;
            // return input.Select(x => double.Parse(x)).Select(CalculateFuelToZero).Sum();
        }

        private long CalculateFuelToZero(double fuel)
        {
            if (_fuelCalculationFormula(fuel) <= 0)
            {
                return 0;
            }

            int result = _fuelCalculationFormula(fuel);
            return result + CalculateFuelToZero(result);
        }
    }
}
