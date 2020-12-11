using System;

namespace Aoc._2019.Day02
{
    public class Instruction
    {
        public int Optcode { get; set; }
        public int? FirstParameter { get; set; }
        public int? SecondParameter { get; set; }
        public int? Position { get; set; }
        public int NextInstructionPosition { get; set; }
        public bool HasNextInstruction { get; set; }

        public static Instruction CreateInstruction(int instructionPosition, int[] instructionParams, int? input = null)
        {
            int fullOptcode = instructionParams[instructionPosition];
            var optCode = fullOptcode % 100;
            var nextInstructionPostion = GetNextInstructionPosition(optCode);
            var (firstMode, secondMode, positionMode) = GetModes(fullOptcode / 100);
            switch (optCode)
            {
                case 1:
                case 2:
                    return new Instruction
                    {
                        Optcode = optCode,
                        FirstParameter = firstMode == 0 ? instructionParams[instructionPosition + 1] : instructionPosition + 1,
                        SecondParameter = secondMode == 0 ? instructionParams[instructionPosition + 2] : instructionPosition + 2,
                        Position = positionMode == 0 ? instructionParams[instructionPosition + 3] : instructionPosition + 3,
                        NextInstructionPosition = instructionPosition + nextInstructionPostion,
                        HasNextInstruction = nextInstructionPostion > 0
                    };
                case 3:
                    return new Instruction
                    {
                        Optcode = optCode,
                        FirstParameter = input.Value,
                        Position = positionMode == 0 ? instructionParams[instructionPosition + 3] : instructionPosition + 3,
                        NextInstructionPosition = instructionPosition + nextInstructionPostion,
                        HasNextInstruction = nextInstructionPostion > 0
                    };
                case 4:
                    return new Instruction
                    {
                        Optcode = optCode,
                        Position = firstMode == 0 ? instructionParams[instructionPosition + 1] : instructionPosition + 1,
                        NextInstructionPosition = instructionPosition + nextInstructionPostion,
                        HasNextInstruction = nextInstructionPostion > 0
                    };
                case 99:
                default:
                    return new Instruction();
            }
        }

        private static int GetNextInstructionPosition(int optcode)
        {
            switch (optcode)
            {
                case 1:
                case 2:
                    return 4;
                case 3:
                case 4:
                    return 2;
                case 99:
                default:
                    return 0;
            }
        }

        private static Tuple<int, int ,int> GetModes(int optcode)
        {
            var firstMode = optcode % 10;
            var secondMode = (optcode / 10) % 10;
            var positionMode = (optcode / 100) % 10;
            return new Tuple<int, int, int>(firstMode, secondMode, positionMode);
        }
    }
}
