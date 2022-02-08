using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode
{
    internal class Day14
    {
        string[] inputs;
        List<string> templates = new List<string>();
        Dictionary<string, string> pairInsertions = new Dictionary<string, string>();
        public Day14()
        {
            inputs = File.ReadAllLines("..\\..\\..\\Day14_Input.txt");
            templates.Add(inputs[0]);

            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i].Contains("->"))
                {
                    string[] pairInsertion = inputs[i].Split("->");
                    pairInsertions.Add(pairInsertion[0].TrimEnd(), pairInsertion[1].TrimStart());
                }
            }  
        }

        internal void PartOne()
        {                   
            Dictionary<char, ulong> result = DoStringWay(10);

            Console.WriteLine("most character :{0} for {1} times , less character {2} for {3} times",
                result.Where(x => x.Value == result.Values.Max()).First().Key,
                result.Values.Max(),
                result.Where(x => x.Value == result.Values.Min()).First().Key,
                result.Values.Min());

            Console.WriteLine("Substract : " + (result.Values.Max() - result.Values.Min()));
        }

        internal void PartTwo()
        {
            Dictionary<string, ulong> charactersCount = DoCountingPairsWay(40);

            Console.WriteLine("\nMost character :{0} for {1} times , less character {2} for {3} times",
               charactersCount.Where(x => x.Value == charactersCount.Values.Max()).First().Key,
               charactersCount.Values.Max(),
               charactersCount.Where(x => x.Value == charactersCount.Values.Min()).First().Key,
               charactersCount.Values.Min());

            ulong substract = (charactersCount.Values.Max() / 2L - charactersCount.Values.Min() / 2L) + 1;
            Console.WriteLine("Substract : " + substract);
        }

        Dictionary<string, ulong> DoCountingPairsWay(int steps)
        {
            Dictionary<string, ulong> pairs = new Dictionary<string, ulong>();

            for (int i = 0; i < templates[0].Length -1 ; i++)
            {
                string pair = templates[0].ElementAt(i) + templates[0].ElementAt(i + 1).ToString();
                if (!pairs.ContainsKey(pair))
                    pairs.Add(pair, 1);
                else
                    pairs[pair] += 1;
            }

            for (int s = 0; s <steps; s++)
            {                                                 
                Console.WriteLine("\n \nStep : " + s + " stepPairs : " + pairs.Keys.Count );
                for (int i = 0; i < pairs.Keys.Count; i++)
                {
                    Console.Write("{" + pairs.Keys.ElementAt(i) + "->" + pairs[pairs.Keys.ElementAt(i)] + "}");
                }

                Dictionary<string, ulong> newPairs = new Dictionary<string, ulong>();

                for (int p = 0; p < pairs.Keys.Count; p++)
                {
                    string pair = pairs.Keys.ElementAt(p);
                    ulong numberOfElement = pairs[pair];

                    string insertion = "";
                    if (pairInsertions.Keys.Contains(pair))
                    {
                        insertion = pairInsertions[pair];                        
                    }

                    if(insertion != "")
                    {    
                        if (!newPairs.ContainsKey(pair[0] + insertion))
                            newPairs.Add(pair[0] + insertion, numberOfElement);
                        else
                            newPairs[pair[0] + insertion] += numberOfElement;

                        if (!newPairs.ContainsKey(insertion + pair[1]))
                            newPairs.Add(insertion + pair[1], numberOfElement);
                        else
                            newPairs[insertion + pair[1]] += numberOfElement;
                    }
                }

                pairs = newPairs;
            }

            Dictionary<string, ulong> charactersCount = new Dictionary<string, ulong>();

            for (int p = 0; p < pairs.Keys.Count; p++)
            {
                string pair = pairs.Keys.ElementAt(p);
                ulong numberOfElement = pairs[pair];

                string char1 = pair[0] + "";
                string char2 = pair[1] + "";

                if (!charactersCount.ContainsKey(char1))
                    charactersCount.Add(char1, numberOfElement);
                else
                    charactersCount[char1] += numberOfElement;
                
                if (!charactersCount.ContainsKey(char2))
                    charactersCount.Add(char2, numberOfElement);
                else
                    charactersCount[char2] += numberOfElement;

            }

            return charactersCount;
            
        }
        Dictionary<char, ulong>  DoStringWay(int steps)
        {
            for (int s = 0; s < steps; s++)
            {
                templates.Add("");

                for (int t = 0; t < templates[s].Length - 1; t++)
                {
                    string pair = templates[s].ElementAt(t).ToString() + templates[s].ElementAt(t + 1).ToString();

                    if (pairInsertions.Keys.Contains(pair))
                    {
                        string insertion = pairInsertions[pair];
                        templates[s + 1] += pair[0] + insertion;
                    }
                }
                templates[s + 1] += templates[s].Last();
            }

            Dictionary<char, ulong> result = templates.Last().GroupBy(x => x).ToDictionary(x => x.Key, x => (ulong)x.Count());   

            return result;
        }
    }
}