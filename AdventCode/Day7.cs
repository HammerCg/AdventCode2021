using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode
{
    internal class Day7
    {
        List<int> inputs = new List<int>();

        public Day7()
        {
            inputs = File.ReadAllText("..\\..\\..\\Day7_Input.txt").Split(",").Select(x => int.Parse(x)).ToList<int>();
        }

        public void PartOne()
        {
            inputs.Sort();
            int minPos = inputs.Min();
            int maxPos = inputs.Max();
            Dictionary<int, int> amountCrabs = new Dictionary<int, int>();    

            for (int n = minPos; n < maxPos; n++)
            {
                for (int i = 0; i < inputs.Count; i++)
                {
                    int dist = Math.Abs(n - inputs[i]);
                    if (!amountCrabs.ContainsKey(n))
                    {
                        amountCrabs.Add(n, dist);
                    }
                    else
                    {
                        amountCrabs[n] += dist;
                    }
                }
            }

            int minValue = amountCrabs.Values.Min();   
            
            Console.WriteLine("Fuel needed : " + minValue);
        }
        public void PartTwo()
        {
            inputs.Sort();
            int minPos = inputs.Min();
            int maxPos = inputs.Max();
            Dictionary<int, ulong> amountCrabs = new Dictionary<int, ulong>();

            for (int n = minPos; n < maxPos; n++)
            {
                for (int i = 0; i < inputs.Count; i++)
                {
                    ulong dist = (ulong)Math.Abs(n - inputs[i]);
                    dist = dist * (dist+1) /2;

                    if (!amountCrabs.ContainsKey(n))
                    {
                        amountCrabs.Add(n, dist);
                    }
                    else
                    {
                        amountCrabs[n] += dist;
                    }
                }
            }

            ulong minValue = amountCrabs.Values.Min();

            Console.WriteLine("Fuel needed : " + minValue);
        }
    }
}