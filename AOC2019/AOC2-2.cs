﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2019
{
    class AOC2_2
    {
        string input = @"1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,1,10,19,1,9,19,23,1,13,23,27,1,5,27,31,2,31,6,35,1,35,5,39,1,9,39,43,1,43,5,47,1,47,5,51,2,10,51,55,1,5,55,59,1,59,5,63,2,63,9,67,1,67,5,71,2,9,71,75,1,75,5,79,1,10,79,83,1,83,10,87,1,10,87,91,1,6,91,95,2,95,6,99,2,99,9,103,1,103,6,107,1,13,107,111,1,13,111,115,2,115,9,119,1,119,6,123,2,9,123,127,1,127,5,131,1,131,5,135,1,135,5,139,2,10,139,143,2,143,10,147,1,147,5,151,1,151,2,155,1,155,13,0,99,2,14,0,0";

        public int Solve()
        {
            int target = 19690720;
            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    if (RunProgram(noun, verb) == target)
                    {
                        return (100 * noun) + verb;
                    }
                }
            }
            throw new Exception("HUH?");
        }

        public int RunProgram(int noun, int verb)
        {
            int[] data = input.Split(',').Select(x => int.Parse(x)).ToArray();
            data[1] = noun;
            data[2] = verb;
            for (int position = 0; position < data.Length; )
            {
                int opcode = data[position];
                if (opcode == 99)
                {
                    break;
                }
                int op1 = data[data[position + 1]];
                int op2 = data[data[position + 2]];
                int op3 = data[position + 3];
                if (opcode == 1)
                {
                    data[op3] = op1 + op2;
                    position += 4;
                }
                else if (opcode == 2)
                {
                    data[op3] = op1 * op2;
                    position += 4;
                }
                else
                {
                    throw new Exception("AAAH!");
                }
            }
            return data[0];
        }

    }
}
