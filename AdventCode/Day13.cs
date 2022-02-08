using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode
{
    internal class Day13
    {
        string[] inputs;
        List<Point> pointsList = new List<Point>();
        List<string> folds = new List<string>();

        public Day13()
        {
            inputs = File.ReadAllLines("..\\..\\..\\Day13_Input.txt");


            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i].Contains(","))
                {
                    string[] xy = inputs[i].Split(",");
                    pointsList.Add(new Point(int.Parse(xy[0]), int.Parse(xy[1])));
                }
                else if (inputs[i].Contains("fold"))
                {
                    folds.Add(inputs[i].Split(" ")[2]);
                }
            }

            pointsList = pointsList.OrderBy(y => y.y).OrderBy(x => x.x).ToList<Point>();
            Console.WriteLine("");
        }

        internal void PartOne()
        {
            for (int i = 0; i < folds.Count; i++)
            {
                if (folds[i].Contains("x"))
                {
                    Fold_X(int.Parse(folds[i].Split("=")[1]));
                }
                else
                {
                    Fold_Y(int.Parse(folds[i].Split("=")[1]));
                }
                Console.WriteLine( "\nPoints at {i} fold " + pointsList.Distinct().ToList().Count);
            }
        }
        internal void PartTwo()
        {
            PrintTable();            
        }    

        void PrintTable()
        {
            Console.WriteLine("");
            int maxX = 0;
            int maxY = 0;
            for (int i = 0; i < pointsList.Count; i++)
            {
                if (pointsList[i].x > maxX) maxX = pointsList[i].x;
                if (pointsList[i].y > maxY) maxY = pointsList[i].y;
            }

            for (int y = 0; y <= maxY; y++)
            {
                Console.WriteLine();
                for (int x = 0; x <= maxX; x++)
                {
                    bool hasPoint = false;
                    for (int i = 0; i < pointsList.Count; i++)
                    {
                        if (pointsList[i].x == x && pointsList[i].y == y)
                            hasPoint = true;
                    }
                    
                    Console.Write( hasPoint ? "#" : ".");
                }
            }

        }
        void Fold_X(int lineX)
        {
            for (int i = 0; i < pointsList.Count; i++)
            {
                if(pointsList[i].x >= lineX)
                {
                    pointsList[i] = new Point(2 * lineX - pointsList[i].x, pointsList[i].y);
                }
            }
        }
        void Fold_Y(int lineY)
        {
            for (int i = 0; i < pointsList.Count; i++)
            {
                if (pointsList[i].y >= lineY)
                {
                    pointsList[i] = new Point(pointsList[i].x, 2 * lineY - pointsList[i].y);
                }
            }
        }

        struct Point
        {
            public int x;
            public int y;
            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
    }
}