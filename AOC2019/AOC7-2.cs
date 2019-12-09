using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2019
{
    class AOC7_2
    {
        static string input = @"3,8,1001,8,10,8,105,1,0,0,21,42,67,84,109,126,207,288,369,450,99999,3,9,102,4,9,9,1001,9,4,9,102,2,9,9,101,2,9,9,4,9,99,3,9,1001,9,5,9,1002,9,5,9,1001,9,5,9,1002,9,5,9,101,5,9,9,4,9,99,3,9,101,5,9,9,1002,9,3,9,1001,9,2,9,4,9,99,3,9,1001,9,2,9,102,4,9,9,101,2,9,9,102,4,9,9,1001,9,2,9,4,9,99,3,9,102,2,9,9,101,5,9,9,1002,9,2,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,99,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,99";

        public static int GetAddress(int[] data, int position, int opMode)
        {
            if (opMode == 0) // Position mode
            {
                return data[position];
            }

            return position; // Immediate mode
        }

        public class Amplifier
        {
            private readonly int[] data = input.Split(',').Select(x => int.Parse(x)).ToArray();
            private int position = 0;
            public bool IsDone { get; set; }
            public bool NeedsNewInput { get; set; }

            public int Run(int input)
            {
                NeedsNewInput = false;
                int maxParameters = 3;

                while (position < data.Length)
                {
                    int opcode = data[position];
                    position++;

                    if (opcode == 99)
                    {
                        IsDone = true;
                        return input;
                    }

                    int[] opModes = Enumerable.Range(0, maxParameters).Select(x => (opcode / ((int)Math.Pow(10, 2 + x))) % 2).ToArray();
                    opcode = opcode % 100;

                    int op1 = GetAddress(data, position, opModes[0]);
                    position++;

                    if (opcode == 3)
                    {
                        if (NeedsNewInput)
                        {
                            position -= 2;
                            return input;
                        }
                        data[op1] = input;
                        NeedsNewInput = true;
                        continue;
                    }

                    if (opcode == 4)
                    {
                        return data[op1];
                    }

                    int op2 = GetAddress(data, position, opModes[1]);
                    position++;

                    if (opcode == 5)
                    {
                        if (data[op1] != 0)
                        {
                            position = data[op2];
                        }
                        continue;
                    }
                    else if (opcode == 6)
                    {
                        if (data[op1] == 0)
                        {
                            position = data[op2];
                        }
                        continue;
                    }

                    int op3 = GetAddress(data, position, opModes[2]);
                    position++;

                    if (opcode == 1)
                    {
                        data[op3] = data[op1] + data[op2];
                    }
                    else if (opcode == 2)
                    {
                        data[op3] = data[op1] * data[op2];
                    }
                    else if (opcode == 7)
                    {
                        data[op3] = (data[op1] < data[op2]) ? 1 : 0;
                    }
                    else if (opcode == 8)
                    {
                        data[op3] = (data[op1] == data[op2]) ? 1 : 0;
                    }
                    else
                    {
                        throw new Exception("AAAH!");
                    }
                }
                throw new Exception("ARGH!");
            }
        }

        public static int RunSequence(int[] sequence)
        {
            List<Amplifier> amps = new List<Amplifier>();
            for (int i = 0; i < sequence.Length; i++)
            {
                Amplifier amp = new Amplifier();
                amp.Run(sequence[i]);
                amps.Add(amp);
            }

            int signal = 0;
            int currentAmp = 0;
            while (true)
            {
                signal = amps[currentAmp].Run(signal);
                if (amps[currentAmp].IsDone && currentAmp == 4) break;
                currentAmp = currentAmp == 4 ? 0 : currentAmp + 1;
            }
            return signal;
        }

        public static List<List<int>> GetPermutations(List<int> inputs)
        {
            var permutations = new List<List<int>>();

            if (inputs.Count == 1)
            {
                permutations.Add(inputs);
                return permutations;
            }

            foreach (int input in inputs)
            {
                List<int> rest = inputs.Where(x => x != input).ToList();
                var subpermutations = GetPermutations(rest);
                subpermutations.ForEach(x => { x.Insert(0, input); permutations.Add(x); });
            }
            return permutations;
        }

        public static void Solve()
        {
            var permutations = GetPermutations(new List<int>() { 5, 6, 7, 8, 9 });
            int answer = permutations.Max(x => RunSequence(x.ToArray()));
            Console.WriteLine(answer);
        }
    }
}
