namespace AOC2019
{
    class AOC4_1
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
            int lastDigit = -1;
            bool hasPair = false;
            for (int i = 0; i < 6; i++)
            {
                int digit = GetDigit(input, i);
                if (digit < lastDigit)
                    return false;
                if (digit == lastDigit)
                    hasPair = true;
                lastDigit = digit;
            }
            return hasPair;
        }

        int GetDigit(int input, int pos)
        {
            return int.Parse(input.ToString().Substring(pos, 1));
        }
    }
}
