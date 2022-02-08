using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode
{
    internal class Day8
    {
        List<string> lines = new List<string>();

        public Day8()
        {
            lines = File.ReadAllText("..\\..\\..\\Day8_Input.txt").Split("\r\n").ToList<string>();
            Console.WriteLine();
        }

        public void PartOne()
        {
            string[] digits = new[] { "cd", "acf", "bcdf", "abcdefg" };
            int sum = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                string[] outputs = lines[i].Split("|")[1].Split(" ");
                for (int n = 0; n < outputs.Length; n++)
                {
                    sum += digits.Where(x => x.Length == outputs[n].Length).Count() > 0 ? 1 : 0;
                }
            }
            Console.WriteLine(" Instances " + sum);
        }

        public void PartTwo()
        {
            //string[] digits = new string[] { "abcefg","cf","acdeg","acdfg","bcdf","abdfg","abdefg","acf","abcdefg","abcdfg" };
            string[] digits = new string[] { "cagedb", "ab", "gcdfa", "fbcad", "eafb", "cdfbe", "cdfgeb", "dab", "acedgfb", "cefabd" };

            int sum = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                string[] inputs = lines[i].Split("|");

                digits = LookupDigits(inputs[0].Split(" "));

                string[] outputs = inputs[1].Split(" ");
                string number = "";

                for (int n = 0; n < outputs.Length; n++)
                {
                    if (outputs[n] != "") 
                        number += SearchNumber(outputs[n], digits);
                }
                if (number != "")
                    sum += int.Parse(number);

            }
            Console.WriteLine("Output sum : " + sum);
        }

        private string[] LookupDigits(string[] pattern)
        {   /*
                8 (7)
               
                9 (6)
                6 (6)
                0 (6)
                
                5 (5)
                3 (5)
                2 (5)

                4 (4)
                7 (3)
                1 (2)
            */
            string[] digits = new string[10];

            digits[8] = pattern.First(x => x.Length == 7);
            digits[7] = pattern.First(x => x.Length == 3);
            digits[4] = pattern.First(x => x.Length == 4);
            digits[1] = pattern.First(x => x.Length == 2);

            string[] sixPattern = pattern.Where(x => x.Length == 6).ToArray();
            digits[0] = sixPattern.Single(x => x.Intersect(digits[1]).Count() == 2 && x.Intersect(digits[4]).Count() == 3);
            digits[6] = sixPattern.Single(x => x.Intersect(digits[1]).Count() == 1 && x.Intersect(digits[4]).Count() == 3);
            digits[9] = sixPattern.Single(x => x.Intersect(digits[1]).Count() == 2 && x.Intersect(digits[4]).Count() == 4);
            
            string[] fivePattern = pattern.Where(x => x.Length == 5).ToArray();
            digits[2] = fivePattern.Single(x => x.Intersect(digits[1]).Count() == 1 && x.Intersect(digits[4]).Count() == 2);
            digits[3] = fivePattern.Single(x => x.Intersect(digits[1]).Count() == 2 && x.Intersect(digits[4]).Count() == 3);
            digits[5] = fivePattern.Single(x => x.Intersect(digits[1]).Count() == 1 && x.Intersect(digits[4]).Count() == 3);

            return digits;
        }
        private string SearchNumber(string input , string[] digits)
        {
            for (int j = 0; j < digits.Length; j++)
            {
                if (digits[j].Length == input.Length)
                {
                    bool containsAll = true;
                    for (int k = 0; k < input.Length; k++)
                    {
                        if (!digits[j].Contains(input[k]))
                        {
                            containsAll = false;
                        }
                    }
                    if (containsAll)
                        return j+"";
                }
            }
            return "";
        }
    }
}