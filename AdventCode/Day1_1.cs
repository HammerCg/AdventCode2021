using System;
using System.IO;
using System.Linq;          

namespace AdventCode
{
    internal class Day1_1
    {
        public Day1_1()
        {
        }

        public void Execute()
        {
            //file lines
            int[] inputs = File.ReadAllLines("..\\..\\..\\Day1_Input.txt").Select(x => int.Parse(x)).ToArray();
            Console.WriteLine(inputs.Length + " Inputs");

            int largerCount = 0;
            int previous =  inputs[0];

            for (int i = 1; i < inputs.Length; i++)
            {
                int current = inputs[i];

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