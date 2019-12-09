using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2019
{
    class AOC4_2
    {
        public int Solve()
        {
            int count = 0;
            for (int i = 136818; i <= 685979; i++)
            {
                if (Check(i))
                    count++;
            }
            return count;
        }

        bool Check(int input)
        {
            int lastDigit = GetDigit(input, 0);
            int currentRunDigit = lastDigit;
            int currentRunLength = 1;
            bool hasPair = false;
            for (int i = 1; i < 6; i++)
            {
                int digit = GetDigit(input, i);
                if (digit < lastDigit)
                    return false;
                if (digit == lastDigit)
                {
                    currentRunLength++;
                }
                else
                {
                    if (currentRunLength == 2)
                    {
                        hasPair = true;
                    }
                    currentRunLength = 1;
                }
                lastDigit = digit;
            }
            if (currentRunLength == 2)
            {
                hasPair = true;
            }
            return hasPair;
        }

        int GetDigit(int input, int pos)
        {
            return int.Parse(input.ToString().Substring(pos, 1));
        }
    }
}
