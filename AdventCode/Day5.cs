using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventCode
{
    internal class Day5
    {
        struct LineSegment
        {
            public LineSegment(string segment) 
            {
                segment = segment.Replace(" -> ", ",");
                string[] segments = segment.Split(",");
                x1 = int.Parse(segments[0]);
                y1 = int.Parse(segments[1]);
                x2 = int.Parse(segments[2]);
                y2 = int.Parse(segments[3]);
            }

            public int x1, x2, y1, y2;
        }

        List<string> inputs;
        List<LineSegment> segments;
        int[,] map;
        public Day5()
        {
            inputs = File.ReadAllText("..\\..\\..\\Day5_Input.txt").Split("\r\n").ToList<string>();
            segments = inputs.Select(x => new LineSegment(x)).ToList();

            Console.WriteLine();


        }
        public void PartOne()
        {
            map = new int[1000, 1000];
            for (int i = 0; i < segments.Count; i++)
            {
                LineSegment segment = segments[i];

                if (segment.x1 == segment.x2 || segment.y1 == segment.y2)
                {
                    for (int x = Math.Min(segment.x1,segment.x2); x <= Math.Max(segment.x1,segment.x2); x++)
                    {
                        for (int y = Math.Min(segment.y1, segment.y2); y <= Math.Max(segment.y1, segment.y2); y++)
                        {

                            map[x, y] += 1;
                        }
                    }
                }
            }

            int sum = 0;
            var s = new StringBuilder();
            for (int x = 0; x < map.GetLength(0); x++)
            {           
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] > 1) sum++;
                }
            }

            Console.WriteLine(" Poins with at least 2 lines overlap : " + sum);

        }
        public void PartTwo()
        {
          
            for (int i = 0; i < segments.Count; i++)
            {
                LineSegment segment = segments[i];

                int xDir = (segment.x2 - segment.x1) < 0 ? -1 : 1;
                int yDir = (segment.y2 - segment.y1) < 0 ? -1 : 1;
                int xSteps = Math.Abs(segment.x2 - segment.x1);
                int ySteps = Math.Abs(segment.y2 - segment.y1);

                if ( xSteps == ySteps)
                {
                    for (int x = 0; x <= xSteps; x++)
                    {      
                        map[segment.x1 + (x*xDir),segment.y1 +(x*yDir)] += 1;
                     
                    }
                }
            }

            int sum = 0;
            var s = new StringBuilder();
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] > 1) sum++;
                }
            }

            Console.WriteLine(" Poins with at least 2 lines overlap : " + sum);

        }
    }
}