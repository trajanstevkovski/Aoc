using System;

namespace Aoc.Shared.Helpers
{
    public static class IntegerHelpers
    {
        public static int[] ToIntArray(this int input)
        {
            var intArr = new int[input.Digits_Log10()];
            int position = intArr.Length - 1;
            while(input != 0)
            {
                intArr[position] = input % 10;
                position--;

                input /= 10;
            }

            return intArr;
        }

        public static int Digits_Log10(this int n) =>
        n == 0 ? 1 : (n > 0 ? 1 : 2) + (int)Math.Log10(Math.Abs((double)n));
    }
}
