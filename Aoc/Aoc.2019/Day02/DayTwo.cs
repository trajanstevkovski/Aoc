using Aoc.Shared;
using System.Collections.Generic;
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

            var result = ProcessIntcode(12, 2, splitInput);

            return result[0];
        }

        public long PuzzleTwo()
        {
            var inputArr = GetStringContent(_inputPath);
            for (int noun = 12; noun < 99; noun++)
            {
                for (int verb = 2; verb < 99; verb++)
                {
                    var splitInput = inputArr.Split(',').Select(x => int.Parse(x)).ToArray();
                    var output = ProcessIntcode(noun, verb, splitInput);

                    if(output[0] == 19690720)
                    {
                        return (100 * noun) + verb;
                    }
                }
            }

            return 0;
        }

        private int[] ProcessIntcode(int noun, int verb, int[] inputMemory)
        {
            inputMemory[1] = noun;
            inputMemory[2] = verb;
            for (int i = 0; i < inputMemory.Length; i += 4)
            {
                var input = inputMemory[i];

                if (input == 99)
                {
                    break;
                }

                var leftOperand = inputMemory[i + 1];
                var rightOperand = inputMemory[i + 2];
                var resultPosition = inputMemory[i + 3];

                if (input == 1)
                {
                    inputMemory[resultPosition] = inputMemory[leftOperand] + inputMemory[rightOperand];
                    continue;
                }

                if (input == 2)
                {
                    inputMemory[resultPosition] = inputMemory[leftOperand] * inputMemory[rightOperand];
                    continue;
                }
            }

            return inputMemory;
        }
    }
}
