using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode
{
    internal class Day15
    {

        string[] inputs;
        int[,] map;
        int[,] mapScaled;
        int[,] cost;
        Path riskLessPath;
        PriorityPathList priorityPathList;

        public Day15()
        {
            inputs = File.ReadAllLines("..\\..\\..\\Day15_Input.txt");
            map = new int[inputs.Length, inputs[0].Length];
            mapScaled = new int[inputs.Length * 5, inputs[0].Length * 5];

            for (int r = 0; r < inputs.Length; r++)
            {
                for (int c = 0; c < inputs[r].Length; c++)
                {
                    map[r, c] = int.Parse(inputs[r].ElementAt(c) + "");

                    for (int rScaled = 0; rScaled < 5; rScaled++)
                    {
                        for (int cScaled = 0; cScaled < 5; cScaled++)
                        {
                            int value = map[r, c] + rScaled + cScaled;
                            while (value > 9)
                                value -= 9;

                            mapScaled[r + (rScaled * inputs.Length), c + (cScaled * inputs[r].Length)] = value;
                        }
                    }
                }
            }
        }
        internal void PartOne()
        {
            Path path = new Path();
            path.AddStep(new Point(0, 0, 0));

            priorityPathList = new PriorityPathList();
            priorityPathList.Push(path);

            cost = new int[map.GetLength(0), map.GetLength(1)];
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    cost[x, y] = int.MaxValue;
                }
            }

            PriorityPathFinder(map);

            Console.WriteLine("Path Total Cost :" + riskLessPath.GetTotalCost());
        }
        internal void PartTwo()
        {
            Path path = new Path();
            path.AddStep(new Point(0, 0, 0));

            priorityPathList = new PriorityPathList();
            priorityPathList.Push(path);

            cost = new int[mapScaled.GetLength(0), mapScaled.GetLength(1)];
            for (int x = 0; x < mapScaled.GetLength(0); x++)
            {
                for (int y = 0; y < mapScaled.GetLength(1); y++)
                {
                    cost[x, y] = int.MaxValue;
                }
            }
            riskLessPath = null;
            PriorityPathFinder(mapScaled);

            Console.WriteLine("Path Total Cost :" + riskLessPath.GetTotalCost());
        }

        void PriorityPathFinder(int[,] targetMap)
        {
            while (priorityPathList.Any())
            {
                Path path = priorityPathList.Peek();

                Point[] neighbours = GetNeigbours(path.Last(), targetMap);
                foreach (Point n in neighbours)
                {
                    if (path.Contains(n))
                        continue;
                    int maxRisk = (targetMap.GetLength(0) + targetMap.GetLength(1)) * 9;
                    if ((path.GetTotalCost() + n.cost) > maxRisk)
                        continue;
                    if (riskLessPath != null && riskLessPath.GetTotalCost() < (path.GetTotalCost() + n.cost))
                        continue;

                    if (n.x == targetMap.GetLength(0) - 1 && n.y == targetMap.GetLength(1) - 1)
                    {
                        Path newPath = CloneList(path, n);
                        riskLessPath = newPath;
                        cost[n.x, n.y] = newPath.GetTotalCost();
                        break;
                    }

                    if (cost[n.x, n.y] > path.GetTotalCost() + n.cost)
                    {
                        cost[n.x, n.y] = path.GetTotalCost() + n.cost;
                        priorityPathList.Push(CloneList(path, n));
                    }
                    else
                        continue;
                                                                   
                }

                priorityPathList.Pop();
            }
        }   
        Path CloneList(Path listToClone, Point addItem = null)
        {
            Path newList = new Path();
            for (int i = 0; i < listToClone.Length; i++)
            {
                newList.AddStep(listToClone.At(i));
            }
            if (addItem != null)
                newList.AddStep(addItem);
            return newList;
        }
        int GetCost(int x, int y, int[,] map)
        {
            return map[x, y];
        }
        Point[] GetNeigbours(Point point, int[,] targetMap)
        {
            List<Point> neighbours = new List<Point>();

            if (point.x > 0) neighbours.Add(new Point(point.x - 1, point.y, GetCost(point.x - 1, point.y, targetMap)));
            if (point.x < targetMap.GetLength(0) - 1) neighbours.Add(new Point(point.x + 1, point.y, GetCost(point.x + 1, point.y, targetMap)));
            if (point.y > 0) neighbours.Add(new Point(point.x, point.y - 1, GetCost(point.x, point.y - 1, targetMap)));
            if (point.y < targetMap.GetLength(1) - 1) neighbours.Add(new Point(point.x, point.y + 1, GetCost(point.x, point.y + 1, targetMap)));

            return neighbours.OrderBy(x=> x.cost).ToArray();
        }



        class PriorityPathList
        {
            public PriorityPathList() { }

            private List<Path> pathsList = new List<Path>();

            //Insert new element ordered in list with his cost/priority
            public void Push(Path path)
            {
                pathsList.Add(path);
                pathsList = pathsList.OrderBy(x => x.GetTotalCost()).ToList();
            }
            //Eliminates lowest cast element
            public void Pop()
            {
                if (pathsList.Count > 0)
                    pathsList.RemoveAt(0);
            }
            //Returns lowest cost element
            public Path Peek()
            {
                if (pathsList.Count > 0)
                    return pathsList[0];
                else 
                    return null;
            }
            //Resturn if any element in list
            public bool Any()
            {
                return pathsList.Count > 0;
            }

        }
        class Path
        {
            private List<Point> path;

            public Path()
            {
                path = new List<Point>();
            }
            public void AddStep(Point point)
            {
                path.Add(point);
            }
            public Point Last()
            {
                return path.Last();
            }
            public Point At(int position)
            {
                return path[position];
            }
            public bool Contains(Point point)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    if (path[i].Equals(point))
                        return true;
                }
                return false;
            }
            public int GetTotalCost()
            {
                int cost = 0;
                for (int i = 0; i < path.Count(); i++)
                {
                    cost += path[i].cost;
                }
                return cost;
            }
            public int Length
            {
                get { return path.Count(); }
            }
        }
        class Point : IEquatable<Point>
        {
            public int x { get; private set; }
            public int y { get; private set; }
            public int cost { get; private set; }

            public Point(int x, int y, int cost)
            {
                this.x = x;
                this.y = y;
                this.cost = cost;
            }

            public override bool Equals(object obj) { return Equals(obj as Point); }
            public bool Equals(Point other) { return other != null && x == other.x && y == other.y; }
            public override int GetHashCode() { return HashCode.Combine(x, y); }
            public static bool operator ==(Point left, Point right) { return EqualityComparer<Point>.Default.Equals(left, right); }
            public static bool operator !=(Point left, Point right) { return !(left == right); }
        }


    }
}