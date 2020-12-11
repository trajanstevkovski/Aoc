using System;
using System.Linq;

namespace Aoc._2019.Day02
{
    public class Intcode
    {
        public int[] ProcessIntcode(int noun, int verb, int[] inputMemory)
        {
            inputMemory[1] = noun;
            inputMemory[2] = verb;

            var instruction = Instruction.CreateInstruction(0, inputMemory);

            do
            {
                _ = ProcessInstruction(instruction, ref inputMemory);
                instruction = Instruction.CreateInstruction(instruction.NextInstructionPosition, inputMemory);

            } while (instruction.HasNextInstruction);


            return inputMemory;
        }

        public int TheThermalEnvironmentSupervisionTerminal(int[] inputMemory, int input)
        {
            var instruction = Instruction.CreateInstruction(0, inputMemory, input);
            while (instruction.HasNextInstruction)
            {
                var output = ProcessInstruction(instruction, ref inputMemory);
                if (output != -1)
                {
                    input = output;
                }
                instruction = Instruction.CreateInstruction(instruction.NextInstructionPosition, inputMemory, input);
            }


            return input;
        }

        private int ProcessInstruction(Instruction instruction, ref int[] inputMemory)
        {
            switch (instruction.Optcode)
            {
                case 1:
                    inputMemory[instruction.Position.Value] = inputMemory[instruction.FirstParameter.Value] + inputMemory[instruction.SecondParameter.Value];
                    break;
                case 2:
                    inputMemory[instruction.Position.Value] = inputMemory[instruction.FirstParameter.Value] * inputMemory[instruction.SecondParameter.Value];
                    break;
                case 3:
                    inputMemory[instruction.Position.Value] = instruction.FirstParameter.Value;
                    break;
                case 4:
                    return inputMemory[instruction.Position.Value];
                case 5:
                case 6:
                    break;
                case 7:
                case 8:
                    inputMemory[instruction.Position.Value] = instruction.FirstParameter.Value;
                    break;
                case 99:
                    instruction.HasNextInstruction = false;
                    break;
                default:
                    throw new ArgumentException("Not valid instruction");
            }
            return -1;
        }
    }
}
