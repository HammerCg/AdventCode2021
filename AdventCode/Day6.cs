using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode
{
    internal class Day6
    {
        List<int> inputs = new List<int>();
        public Day6()
        {
            inputs = File.ReadAllText("..\\..\\..\\Day6_Input.txt").Split(",").Select(x => int.Parse(x)).ToList<int>();

        }
        public void PartOne()
        {
            Console.WriteLine();
            int nDays = 80;
            for (int i = 0; i < nDays; i++)
            {                                                 
                int nFishes = inputs.Count;
                for (int n = 0; n < nFishes; n++)
                {
                    if (inputs[n] == 0)
                    {
                        inputs[n] = 6;
                        inputs.Add(8);
                    }
                    else
                        inputs[n]--;                
                }
            }
            Console.WriteLine("There are a total of : " + inputs.Count + " fishes");
        }
        public void PartTwo()
        {
            Console.WriteLine();
            int nDays = 256;

            Dictionary<int, ulong> fishes = new Dictionary<int, ulong>();
            for (int i = 0; i <= 8; i++)
            {
                fishes.Add(i, 0);
            }
            for (int i = 0; i < inputs.Count; i++)
            {
                fishes[inputs[i]] += 1;
            }
            for (int i = 0; i < nDays; i++)
            {
                Dictionary<int, ulong> tempDictionary = new Dictionary<int, ulong>();
                for (int n = 0; n <= 8; n++)
                {
                    tempDictionary.Add(n, 0);
                }

                for (int n = 8; n >= 0; n--)
                {                       
                    if (n == 0)
                    {
                        tempDictionary[6] += fishes[n];
                        tempDictionary[8] += fishes[n];
                    }
                    else
                        tempDictionary[n-1] = fishes[n];
                }
                fishes = new Dictionary<int, ulong>(tempDictionary);
            }
            
            ulong sum = 0;
            for (int i = 0; i < fishes.Count; i++)
            {
                sum += fishes[i];
            }
            Console.WriteLine("There are a total of : " + sum + " fishes");
        }
    }
}