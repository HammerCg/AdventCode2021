using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode
{
    internal class Day4
    {
        List<string> inputs;
        List<int> numbers;
        List<List<int>> tables = new List<List<int>>();

        List<int> playedNumbers = new List<int>();

        public Day4()
        {
            inputs = File.ReadAllText("..\\..\\..\\Day4_Input.txt").Split("\r\n").ToList<string>();
            numbers = inputs[0].Split(",").Select(int.Parse).ToList();
            
            inputs.RemoveAt(0);

            int nTable = -1;
            int nRow = 0;
            for (int i = 0; i < inputs.Count; i++)
            {
                if(inputs[i] == "") continue;
                if(nRow == 0)
                {
                    tables.Add(new List<int>());
                    nTable++;
                }

                List<string> split = inputs[i].Split(" ").ToList();
                split.RemoveAll(x => x == "");

                tables.ElementAt(nTable).AddRange(split.Select(int.Parse).ToList());
                nRow++;
                
                if (nRow >= 5) nRow = 0; 
                
            }
            
            Console.WriteLine(inputs.Count + " Inputs");
        }
        public void PartOne()
        {
            playedNumbers = new List<int>();
            for (int i = 0; i < numbers.Count; i++)
            {
                playedNumbers.Add(numbers[i]);

                List<int> lineTables = CheckLines();
                
                if(lineTables.Count > 0)
                {
                    for (int n = 0; n < lineTables.Count; n++)
                    {
                        List<int> winner = tables[lineTables[n]];

                        int sum = 0;           
                        for (int j = 0; j < winner.Count; j++)
                        {
                            if (!playedNumbers.Contains(winner[j]))
                            {
                                sum += winner[j];    
                            }        
                        }   
                        int score = sum * playedNumbers.Last();

                        Console.WriteLine(" 1.-Score of table " + lineTables[n] + " sum : " + sum + " last : " + playedNumbers.Last() +  " => " + score);

                    }
                    break;
                }
            }
        }
        public void PartTwo()
        {
            playedNumbers = new List<int>();
            List<int> lineTables = new List<int>();

            for (int i = 0; i < numbers.Count; i++)
            {
                playedNumbers.Add(numbers[i]);

                List<int> checkedLineTables = CheckLines();

                for (int n = 0; n < checkedLineTables.Count; n++)
                {
                    if (!lineTables.Contains(checkedLineTables[n]))
                    {
                        lineTables.Add(checkedLineTables[n]);
                    }
                }

                if (lineTables.Count == tables.Count || playedNumbers.Count == numbers.Count )
                {
                    List<int> winner = tables[lineTables.Last()];

                    int sum = 0;
                    for (int j = 0; j < winner.Count; j++)
                    {
                        if (!playedNumbers.Contains(winner[j]))
                        {
                            sum += winner[j];
                        }
                    }
                    int score = sum * playedNumbers.Last();

                    Console.WriteLine(" 2.-Score of table " + lineTables.Last() + " sum : " + sum + " last : " + playedNumbers.Last() + " => " + score);
                    
                    break;
                }
               
            }
        }
        private List<int> CheckLines()
        {
            List<int> lineTables = new List<int>();
            for (int i = 0; i < tables.Count; i++)
            {                   
                for (int y = 0; y < 5; y++)
                {
                    int nLineNumbers = 0;
                    int nColumnNumbers = 0;
                    for (int x = 0; x < 5; x++)
                    {
                        if(playedNumbers.Contains(tables[i][x + (y*5)]))
                        {
                            nLineNumbers++;
                        }
                        if (playedNumbers.Contains(tables[i][(x * 5) + y]))
                        {
                            nColumnNumbers++;
                        }
                    }
                    if (nLineNumbers == 5) 
                        lineTables.Add(i);
                    if (nColumnNumbers == 5)
                        lineTables.Add(i);
                }                  
            }
            return lineTables;
        }
    }
}