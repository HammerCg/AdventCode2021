using System;
using System.IO;
using System.Linq;

namespace AdventCode
{
    internal class Day1_2
    {
        public Day1_2()
        {
        }
        public void Execute()
        {
            //file lines
            int[] inputs = File.ReadAllLines("..\\..\\..\\Day1_Input.txt").Select(x => int.Parse(x)).ToArray();
            Console.WriteLine(inputs.Length + " Inputs");

            int largerCount = 0;
            int previous = inputs[0] + inputs[1] + inputs[2];

            for (int i = 3; i < inputs.Length; i++)
            {
                int current = inputs[i-2] + inputs[i-1] + inputs[i];

                if (previous < current)
                {
                    largerCount++;
                    Console.WriteLine("increased");
                }
                else if (previous == current)
                {
                    Console.WriteLine("same");
                }
                else
                {
                    Console.WriteLine("decreased");
                }

                previous = current;
            }

            Console.WriteLine(largerCount);
        }
    }
}