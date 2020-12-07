using Aoc.Shared;

namespace Aoc._2019.Day04
{
    public class DayFour : FileManager
    {
        private const int _minRange = 158126;
        private const int _maxRange = 624574;

        public long PuzzleOne()
        {
            var validPasswords = 0;
            for (int i = _minRange; i < _maxRange; i++)
            {
                var passValidator = new PasswordValidator(i);

                if (passValidator.IsPasswordValidFirstCriteria)
                {
                    validPasswords++;
                }
            }

            return validPasswords;
        }

        public long PuzzleTwo()
        {
            var validPasswords = 0;
            for (int i = _minRange; i < _maxRange; i++)
            {
                var passValidator = new PasswordValidator(i);

                if (passValidator.IsPasswordValidSecondCriteria)
                {
                    validPasswords++;
                }
            }

            return validPasswords;
        }
    }
}
