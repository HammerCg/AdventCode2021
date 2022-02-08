using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode
{
    internal class Day11
    {
        string[] inputs;
        int[][] map;
        public Day11()
        {
            inputs = File.ReadAllLines("..\\..\\..\\Day11_Input.txt");
            map = new int[inputs.Length][];
            for (int i = 0; i < inputs.Length; i++)
            {
                map[i] = new int[inputs[i].Length];

                for (int n = 0; n < inputs[i].Length; n++)
                {
                    string character = inputs[i][n].ToString();
                    map[i][n] = int.Parse(character);
                }
            }
        }

        internal void PartOne()
        {
            int steps = 100;
            int flashCount = 0;
            for (int k = 0; k < steps; k++)
            {
                Queue<Point> queueFlash = new Queue<Point>();
                List<Point> flashPoint = new List<Point>();

                for (int i = 0; i < map.Length; i++)
                {
                    for (int n = 0; n < map[i].Length; n++)
                    {
                        map[i][n]++;
                        if (map[i][n] == 10)
                        {
                            queueFlash.Enqueue(new Point(n, i));
                        }
                    }
                }
                while (queueFlash.Any())
                {
                    Point point = queueFlash.Dequeue();
                    flashPoint.Add(point);

                    Point[] neighboursPoints = GetNeigbours(point);
                    for (int i = 0; i < neighboursPoints.Length; i++)
                    {
                        Point neighbourPoint = neighboursPoints[i];
                        map[neighbourPoint.y][neighbourPoint.x]++;

                        if (map[neighbourPoint.y][neighbourPoint.x] == 10)
                        {
                            queueFlash.Enqueue(new Point(neighbourPoint.x, neighbourPoint.y));
                        }
                    }

                }
                for (int i = 0; i < flashPoint.Count; i++)
                {
                    map[flashPoint[i].y][flashPoint[i].x] = 0;
                }
                flashCount += flashPoint.Count;
            }

            Console.WriteLine("\nFlash Count " + flashCount);

        }

        internal void PartTwo()
        { 
            int steps = 100;
            while (true)
            {
                steps++;                 

                Queue<Point> queueFlash = new Queue<Point>();
                List<Point> flashPoint = new List<Point>();

                for (int i = 0; i < map.Length; i++)
                {
                    for (int n = 0; n < map[i].Length; n++)
                    {
                        map[i][n]++;
                        if (map[i][n] == 10)
                        {
                            queueFlash.Enqueue(new Point(n, i));
                        }
                    }
                }
                while (queueFlash.Any())
                {
                    Point point = queueFlash.Dequeue();
                    flashPoint.Add(point);

                    Point[] neighboursPoints = GetNeigbours(point);
                    for (int i = 0; i < neighboursPoints.Length; i++)
                    {
                        Point neighbourPoint = neighboursPoints[i];
                        map[neighbourPoint.y][neighbourPoint.x]++;

                        if (map[neighbourPoint.y][neighbourPoint.x] == 10)
                        {
                            queueFlash.Enqueue(new Point(neighbourPoint.x, neighbourPoint.y));
                        }
                    }

                }
                for (int i = 0; i < flashPoint.Count; i++)
                {
                    map[flashPoint[i].y][flashPoint[i].x] = 0;
                }
                if(flashPoint.Count == (map.Count() * map[0].Count()))
                {
                    break;
                }  
            }

            Console.WriteLine("\nSyncronized Flash Step " + steps);
        }

        private struct Point
        {
            public int x;
            public int y;

            public Point (int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        private Point[] GetNeigbours(Point point)
        {
            List<Point> neighbours = new List<Point>();

            if (point.x > 0)                    neighbours.Add(new Point(point.x - 1, point.y));
            if (point.x < map.Length - 1)       neighbours.Add(new Point(point.x + 1, point.y));
            if (point.y > 0)                    neighbours.Add(new Point(point.x, point.y - 1));
            if (point.y < map[0].Length - 1)    neighbours.Add(new Point(point.x, point.y + 1));

            if (point.x > 0 && point.y > 0)                                     neighbours.Add(new Point(point.x - 1, point.y -1));
            if (point.x > 0 && point.y < map[0].Length -1)                      neighbours.Add(new Point(point.x - 1, point.y + 1));
            if (point.x < map.Length - 1 && point.y > 0)                        neighbours.Add(new Point(point.x + 1, point.y -1));
            if (point.x < map.Length - 1 && point.y < map[0].Length -1)         neighbours.Add(new Point(point.x + 1, point.y + 1));
           
            return neighbours.ToArray();
        }


    }
}