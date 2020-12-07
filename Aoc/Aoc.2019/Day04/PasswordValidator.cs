using Aoc.Shared.Helpers;
using System.Collections.Generic;

namespace Aoc._2019.Day04
{
    public class PasswordValidator
    {
        private int[] Password { get; set; }

        public PasswordValidator(int password)
        {
            Password = password.ToIntArray();
        }

        public bool IsPasswordValidFirstCriteria
        {
            get
            {
                return IsDigitsIncrease() && ValidateAdjacentDigitsAreSame();
            }
        }

        public bool IsPasswordValidSecondCriteria
        {
            get
            {
                return IsDigitsIncrease() && ValidateAdjacentDigitsAreSamePairs();
            }
        }

        private bool IsDigitsIncrease()
        {
            bool isValid = true;
            for (int i = 0; i < Password.Length - 1; i++)
            {
                isValid &= Password[i] <= Password[i + 1];
            }
            return isValid;
        }

        private bool ValidateAdjacentDigitsAreSame()
        {
            for (int i = 0; i < Password.Length - 1; i++)
            {
                if (Password[i] == Password[i + 1])
                {
                    return true;
                }
            }
            return false;
        }

        private bool ValidateAdjacentDigitsAreSamePairs()
        {
            Dictionary<int, int> digits = new Dictionary<int, int>();

            for (int i = 0; i < Password.Length - 1; i++)
            {
                if (Password[i] == Password[i + 1])
                {
                    if (digits.ContainsKey(Password[i]))
                    {
                        digits[Password[i]] = digits[Password[i]] += 1;
                    }
                    else
                    {
                        digits.Add(Password[i], 1);
                    }
                }
            }

            foreach (var key in digits)
            {
                if (digits[key.Key] == 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
