using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2019
{
    class AOC7_1
    {
        static string input = @"3,8,1001,8,10,8,105,1,0,0,21,42,67,84,109,126,207,288,369,450,99999,3,9,102,4,9,9,1001,9,4,9,102,2,9,9,101,2,9,9,4,9,99,3,9,1001,9,5,9,1002,9,5,9,1001,9,5,9,1002,9,5,9,101,5,9,9,4,9,99,3,9,101,5,9,9,1002,9,3,9,1001,9,2,9,4,9,99,3,9,1001,9,2,9,102,4,9,9,101,2,9,9,102,4,9,9,1001,9,2,9,4,9,99,3,9,102,2,9,9,101,5,9,9,1002,9,2,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,99,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,99";

        public static int Run(int[] inputs)
        {
            int inputPosition = 0;
            int[] data = input.Split(',').Select(x => int.Parse(x)).ToArray();
            int maxParameters = 3;
            for (int position = 0; position < data.Length;)
            {
                int opcode = data[position];
                if (opcode == 99)
                {
                    break;
                }
                int[] opModes = Enumerable.Range(0, maxParameters).Select(x => (opcode / ((int)Math.Pow(10, 2 + x))) % 2).ToArray();
                opcode = opcode % 100;

                int op1Raw = data[position + 1];

                if (opcode == 3)
                {
                    data[op1Raw] = inputs[inputPosition];
                    inputPosition++;
                    position += 2;
                    continue;
                }

                if (opcode == 4)
                {
                    return data[op1Raw];
                }

                int op2Raw = data[position + 2];
                int op1 = opModes[0] == 0 ? data[op1Raw] : op1Raw;
                int op2 = opModes[1] == 0 ? data[op2Raw] : op2Raw;

                if (opcode == 5)
                {
                    position += 3;
                    if (op1 != 0)
                    {
                        position = op2;
                    }
                    continue;
                }
                else if (opcode == 6)
                {
                    position += 3;
                    if (op1 == 0)
                    {
                        position = op2;
                    }
                    continue;
                }

                int op3Raw = data[position + 3];

                if (opcode == 1)
                {
                    data[op3Raw] = op1 + op2;
                    position += 4;
                }
                else if (opcode == 2)
                {
                    data[op3Raw] = op1 * op2;
                    position += 4;
                }
                else if (opcode == 7)
                {
                    data[op3Raw] = (op1 < op2) ? 1 : 0;
                    position += 4;
                }
                else if (opcode == 8)
                {
                    data[op3Raw] = (op1 == op2) ? 1 : 0;
                    position += 4;
                }
                else
                {
                    throw new Exception("AAAH!");
                }
            }
            throw new Exception("ARGH!");
        }

        public static int RunSequence(int[] sequence)
        {
            int signal = 0;
            for (int i = 0; i < sequence.Length; i++)
            {
                int[] inputs = new int[2];
                inputs[0] = sequence[i];
                inputs[1] = signal;
                signal = Run(inputs);
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
            var permutations = GetPermutations(new List<int>() { 0, 1, 2, 3, 4 });
            int answer = permutations.Max(x => RunSequence(x.ToArray()));
            Console.WriteLine(answer);
        }
    }
}
