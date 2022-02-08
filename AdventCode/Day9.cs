using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode
{
    internal class Day9
    {
        string[] inputs;
        int[][] heightmap;
        List<Point> lowestPoints = new List<Point>();

        public Day9()
        {
            inputs = File.ReadAllLines("..\\..\\..\\Day9_Input.txt");
            heightmap = new int[inputs.Length][];
            for (int i = 0; i < inputs.Length; i++)
            {
                heightmap[i] = new int[inputs[i].Length];

                for (int n = 0; n < inputs[i].Length; n++)
                {
                    string character = inputs[i][n].ToString();
                    heightmap[i][n] = int.Parse(character);
                }
            }
        }
        internal void PartOne()
        {

            for (int i = 0; i < heightmap.Length; i++)
            {
                for (int n = 0; n < heightmap[i].Length; n++)
                {
                    Point point = new Point(i, n);
                    int currentValue = GetPointValue(point);

                    bool up = true, down = true, left = true, right = true;
                    if (i > 0)
                    {
                        if (currentValue < GetPointValue(i - 1, n))
                            up = true;
                        else
                            up = false;
                    }
                    if (i < heightmap.Length - 1)
                    {
                        if (currentValue < GetPointValue(i + 1, n))
                            down = true;
                        else
                            down = false;
                    }
                    if (n > 0)
                    {
                        if (currentValue < GetPointValue(i, n-1))
                            left = true;
                        else
                            left = false;
                    }
                    if (n < heightmap[i].Length - 1)
                    {
                        if (currentValue < GetPointValue(i, n+1))
                            right = true;
                        else
                            right = false;
                    }
                    if (up && down && left && right)
                        lowestPoints.Add(point);
                }
            }
            Console.WriteLine("Sum of lowestPoint " + (lowestPoints.Select(x=> GetPointValue(x)).Sum() + lowestPoints.Count));
        }
        internal void PartTwo()
        {
            ulong multiply = 1;
            List<int> multiplyNumbersr = new List<int>();
            for (int i = 0; i < lowestPoints.Count; i++)
            {
                List<Point> alreadyBassin = new List<Point>();
                alreadyBassin.Add(lowestPoints[i]);
                multiplyNumbersr.Add( Bassin(lowestPoints[i], alreadyBassin));   
            }
            multiplyNumbersr = multiplyNumbersr.OrderByDescending(x => x).Take(3).ToList();
            for (int i = 0; i < multiplyNumbersr.Count; i++)
            {
                multiply *= (ulong)multiplyNumbersr[i];
            }

            Console.WriteLine("Bassin multiply " + multiply);
        }
        private int Bassin(Point point, List<Point> alreadyBassin)
        {
            Point[] neighbours = GetNeighbours(point);

            bool all = true;
            for (int i = 0; i < neighbours.Count(); i++)
            {
                if (!alreadyBassin.Contains(neighbours[i]))
                {
                    if(GetPointValue(point) == 9 || GetPointValue(point) > GetPointValue(neighbours[i]))
                    {
                        all = false;
                    }
                }
            }
            
            if (all)
            {
                if(!alreadyBassin.Contains(point) && GetPointValue(point) != 9)
                    alreadyBassin.Add(point);
                for (int i = 0; i < neighbours.Count(); i++)
                {
                    if (!alreadyBassin.Contains(neighbours[i]))
                    {                              
                        Bassin(neighbours[i], alreadyBassin);
                    }
                }

            }

            return alreadyBassin.Count();   
        }

        private Point[] GetNeighbours(Point point)
        {
            List<Point> neighbours = new List<Point>();
            if (point.x > 0)
            {
                neighbours.Add(new Point(point.x-1,point.y));                
            }
            if (point.x < heightmap.Length - 1)
            {
                neighbours.Add(new Point(point.x+1, point.y));
            }
            if (point.y > 0)
            {
                neighbours.Add(new Point(point.x, point.y-1));
            }
            if (point.y < heightmap[0].Length - 1)
            {
                neighbours.Add(new Point(point.x , point.y+1));
            }
            return neighbours.ToArray();
        }   
        struct Point{
            public int x;
            public int y;

            public Point( int x, int y)
            {
                this.x = x;
                this.y = y;
            }               
        }  
        int GetPointValue(Point point)
        {
            try
            {
                return heightmap[point.x][point.y];
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return 0;
        }
        int GetPointValue(int x, int y)
        {
            return heightmap[x][y];
        }

    }
}