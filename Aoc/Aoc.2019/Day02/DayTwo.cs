using Aoc.Shared;
using System.IO;
using System.Linq;

namespace Aoc._2019.Day02
{
    public class DayTwo : FileManager
    {
        //private string input = "1,1,1,4,99,5,6,0,99";
        private readonly string _inputPath;

        public DayTwo()
        {
            _inputPath = Directory.GetCurrentDirectory() + "/Inputs/" + "Day02.txt";
        }

        public long PuzzleOne()
        {
            var inputArr = GetStringContent(_inputPath);
            var splitInput = inputArr.Split(',').Select(x => int.Parse(x)).ToArray();

            var intcodeProcessor = new Intcode();

            var processed = intcodeProcessor.ProcessIntcode(12, 2, splitInput);

            return processed[0];
        }

        public long PuzzleTwo()
        {
            var inputArr = GetStringContent(_inputPath);
            for (int noun = 12; noun < 99; noun++)
            {
                for (int verb = 2; verb < 99; verb++)
                {
                    var splitInput = inputArr.Split(',').Select(x => int.Parse(x)).ToArray();

                    var intcodeProcessor = new Intcode();

                    var processed = intcodeProcessor.ProcessIntcode(noun, verb, splitInput);

                    if (processed[0] == 19690720)
                    {
                        return (100 * noun) + verb;
                    }
                }
            }

            return 0;
        }
    }
}
