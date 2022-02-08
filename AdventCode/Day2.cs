using System;
using System.IO;

namespace AdventCode
{
    internal class Day2
    {
        public Day2()
        { }
        struct Position
        {
            public Position(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public int x;
            public int y;
            public override string ToString() => $"({x}, {y})";
        }
        public void Execute()
        {
            //file lines
            string[] inputs = File.ReadAllLines("..\\..\\..\\Day2_Input.txt");
            Console.WriteLine(inputs.Length + " Inputs");

            Position pos = new Position(0, 0);

            for (int i = 0; i < inputs.Length; i++)
            {
                string[] current = inputs[i].Split(' ');

                switch (current[0])
                {
                    case "forward":
                        pos.x += int.Parse(current[1]);
                        break;
                    case "up":
                        pos.y = int.Parse(current[1]);
                        break;
                    case "down":
                        pos.y -= int.Parse(current[1]);
                        break;
                }
            }

            Console.WriteLine(pos.ToString());
            Console.WriteLine(pos.x * pos.y);
        }

        struct Submarine
        {
            public Submarine(int horizontal, int depth, int aim)
            {
                this.horizontal = horizontal;
                this.depth = depth;
                this.aim = aim;
            }
            public int horizontal;
            public int depth;
            public int aim;
            public override string ToString() => $"({horizontal}, {depth})";
        }
        public void Execute2()
        {
            //file lines
            string[] inputs = File.ReadAllLines("..\\..\\..\\Day2_Input.txt");
            Console.WriteLine(inputs.Length + " Inputs");

            Submarine pos = new Submarine(0, 0, 0);

            for (int i = 0; i < inputs.Length; i++)
            {
                string[] current = inputs[i].Split(' ');

                switch (current[0])
                {
                    case "forward":
                        pos.horizontal += int.Parse(current[1]);
                        pos.depth += int.Parse(current[1]) * pos.aim;
                        break;
                    case "up":
                        pos.aim -= int.Parse(current[1]);
                        break;
                    case "down":
                        pos.aim += int.Parse(current[1]);
                        break;
                }
            }

            Console.WriteLine(pos.ToString());
            Console.WriteLine(pos.horizontal * pos.depth);
        }
    }
}
 