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
            Func<int, int, int> getValue = (mode, index) => mode == 0 ? instructionParams[instructionParams[index]] : instructionParams[index];
            int fullOptcode = instructionParams[instructionPosition];
            var optCode = fullOptcode % 100;
            var nextInstructionPostion = GetNextInstructionPosition(optCode);
            var (firstParamMode, secondParamMode, positionMode) = GetModes(fullOptcode / 100);
            switch (optCode)
            {
                case 1:
                case 2:
                    return new Instruction
                    {
                        Optcode = optCode,
                        FirstParameter = firstParamMode == 0 ? instructionParams[instructionPosition + 1] : instructionPosition + 1,
                        SecondParameter = secondParamMode == 0 ? instructionParams[instructionPosition + 2] : instructionPosition + 2,
                        Position = positionMode == 0 ? instructionParams[instructionPosition + 3] : instructionPosition + 3,
                        NextInstructionPosition = instructionPosition + nextInstructionPostion,
                        HasNextInstruction = nextInstructionPostion > 0
                    };
                case 3:
                    return new Instruction
                    {
                        Optcode = optCode,
                        FirstParameter = input.Value,
                        Position = positionMode == 0 ? instructionParams[instructionPosition + 1] : instructionPosition + 1,
                        NextInstructionPosition = instructionPosition + nextInstructionPostion,
                        HasNextInstruction = nextInstructionPostion > 0
                    };
                case 4:
                    return new Instruction
                    {
                        Optcode = optCode,
                        Position = firstParamMode == 0 ? instructionParams[instructionPosition + 1] : instructionPosition + 1,
                        NextInstructionPosition = instructionPosition + nextInstructionPostion,
                        HasNextInstruction = nextInstructionPostion > 0
                    };
                case 5:
                    var firstParam = firstParamMode == 0 ? instructionParams[instructionParams[instructionPosition + 1]] : instructionParams[instructionPosition + 1];
                    var secondParam = secondParamMode == 0 ? instructionParams[instructionParams[instructionPosition + 2]] : instructionParams[instructionPosition + 2];
                    var instructionPointer = firstParam != 0 ? secondParam : instructionPosition + nextInstructionPostion;
                    return new Instruction
                    {
                        Optcode = optCode,
                        NextInstructionPosition = instructionPointer,
                        HasNextInstruction = instructionPointer > 0
                    };
                case 6:
                    var firstParam1 = firstParamMode == 0 ? instructionParams[instructionParams[instructionPosition + 1]] : instructionParams[instructionPosition + 1];
                    var secondParam1 = secondParamMode == 0 ? instructionParams[instructionParams[instructionPosition + 2]] : instructionParams[instructionPosition + 2];
                    var instructionPointer1 = firstParam1 == 0 ? secondParam1 : instructionPosition + nextInstructionPostion;
                    return new Instruction
                    {
                        Optcode = optCode,
                        NextInstructionPosition = instructionPointer1,
                        HasNextInstruction = instructionPointer1 > 0
                    };
                case 7:
                    var firstParam2 = firstParamMode == 0 ? instructionParams[instructionParams[instructionPosition + 1]] : instructionParams[instructionPosition + 1];
                    var secondParam2 = secondParamMode == 0 ? instructionParams[instructionParams[instructionPosition + 2]] : instructionParams[instructionPosition + 2];
                    var firstParamVal = firstParam2 < secondParam2 ? 1 : 0;
                    return new Instruction
                    {
                        Optcode = optCode,
                        FirstParameter = firstParamVal,
                        Position = instructionParams[instructionPosition + 3],
                        NextInstructionPosition = instructionPosition + nextInstructionPostion,
                        HasNextInstruction = nextInstructionPostion > 0
                    };
                case 8:
                    var firstParam3 = firstParamMode == 0 ? instructionParams[instructionParams[instructionPosition + 1]] : instructionParams[instructionPosition + 1];
                    var secondParam3 = secondParamMode == 0 ? instructionParams[instructionParams[instructionPosition + 2]] : instructionParams[instructionPosition + 2];
                    var firstParamVal1 = firstParam3 == secondParam3 ? 1 : 0;
                    return new Instruction
                    {
                        Optcode = optCode,
                        FirstParameter = firstParamVal1,
                        Position = instructionParams[instructionPosition + 3],
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
                case 7:
                case 8:
                    return 4;
                case 5:
                case 6:
                    return 3;
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
