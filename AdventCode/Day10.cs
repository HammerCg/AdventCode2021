using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode
{
    internal class Day10
    {
        string[] inputs;
        char[][] map;

        char[] openSymbols = new char[] { '(', '[', '{', '<' };
        Dictionary<char, int> closeSymbols = new Dictionary<char, int>()
        {
            {')',0 },
            {']',0 },
            {'}',0 },
            {'>',0 }
        };

        List<int> closedLines = new List<int>();

        public Day10()
        {
            inputs = File.ReadAllLines("..\\..\\..\\Day10_Input.txt");
            map = new char[inputs.Length][];
            for (int i = 0; i < inputs.Length; i++)
            {
                map[i] = new char[inputs[i].Length];

                for (int n = 0; n < inputs[i].Length; n++)
                {                                                 
                    map[i][n] = inputs[i][n];
                }                              
            }
        }

        internal void PartOne()
        {

            for (int i = 0; i < map.Length; i++)
            {
                List<char> line = new List<char>();
                Console.WriteLine("");
                for (int n = 0; n < map[i].Length; n++)
                {
                    
                    char closer = map[i][n];
                    char last = line.Count > 0 ? line.Last() : ' ';
                    Console.Write(closer);

                    if (openSymbols.Contains(map[i][n]))
                    {
                        line.Add(map[i][n]);
                    }
                    else
                    {
                        bool closed = false;
                        switch (closer)
                        {
                            case ')': if (last == '(') { line.RemoveAt(line.Count - 1); closed = true; } break;
                            case ']': if (last == '[') { line.RemoveAt(line.Count - 1); closed = true; } break;
                            case '}': if (last == '{') { line.RemoveAt(line.Count - 1); closed = true; } break;
                            case '>': if (last == '<') { line.RemoveAt(line.Count - 1); closed = true; } break;
                        }                          
                        if(!closed)
                        {
                            closeSymbols[map[i][n]] += 1;
                            closedLines.Add(i);
                            break;
                        }
                    }
                }
            }
            int sum = 0;
            for (int i = 0; i < closeSymbols.Keys.Count; i++)
            {
                switch (closeSymbols.ElementAt(i).Key)
                {
                    case ')': sum += (closeSymbols.ElementAt(i).Value * 3); break;
                    case ']': sum += (closeSymbols.ElementAt(i).Value * 57); break;
                    case '}': sum += (closeSymbols.ElementAt(i).Value * 1197); break;
                    case '>': sum += (closeSymbols.ElementAt(i).Value * 25137); break;
                }
            }
            Console.WriteLine(" \nsum wrong character " + sum);
        }

        internal void PartTwo()
        {
            List<ulong> lineScores = new List<ulong>();

            for (int i = 0; i < map.Length; i++)
            {
                if (!closedLines.Contains(i))
                {
                    List<char> line = new List<char>();
                    for (int n = 0; n < map[i].Length; n++)
                    {

                        char closer = map[i][n];
                        char last = line.Count > 0 ? line.Last() : ' ';

                        if (openSymbols.Contains(map[i][n]))
                        {
                            line.Add(map[i][n]);
                        }
                        else
                        {
                            switch (closer)
                            {
                                case ')': if (last == '(') { line.RemoveAt(line.Count - 1);  } break;
                                case ']': if (last == '[') { line.RemoveAt(line.Count - 1);  } break;
                                case '}': if (last == '{') { line.RemoveAt(line.Count - 1);  } break;
                                case '>': if (last == '<') { line.RemoveAt(line.Count - 1);  } break;
                            }
                        }
                    }

                    ulong score = 0;
                    for (int n = line.Count-1; n >= 0; n--)
                    {                           
                        switch (line[n])
                        {
                            case '(': score = (score * 5) + 1; break;
                            case '[': score = (score * 5) + 2; break;
                            case '{': score = (score * 5) + 3; break;
                            case '<': score = (score * 5) + 4; break;
                        }
                    }
                    lineScores.Add(score);
                }
            }

            ulong medianScore = lineScores.OrderBy(x => x).ElementAt(lineScores.Count() / 2);
            Console.WriteLine("Median score " + medianScore);
        }
    }
}