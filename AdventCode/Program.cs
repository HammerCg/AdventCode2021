using System;

namespace AdventCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            

            while (true)
            {
                Console.WriteLine("\nTake a valid day from 1 to 30");
                string key = Console.ReadLine();
                Console.WriteLine();

                switch (key)
                {
                    case "1":
                        var day1_1 = new Day1_1();
                        day1_1.Execute();
                        var day1_2 = new Day1_2();
                        day1_2.Execute();
                        break;
                    case "2":
                        var day2_1 = new Day2();
                        day2_1.Execute();
                        day2_1.Execute2();
                        break;
                    case "3":
                        var day3 = new Day3();
                        day3.PartOne();
                        day3.PartTwo();
                        break;
                    case "4":
                        var day4 = new Day4();
                        day4.PartOne();  
                        day4.PartTwo();
                        break;
                    case "5":
                        var day5 = new Day5();
                        day5.PartOne();
                        day5.PartTwo();
                        break;
                    case "6":
                        var day6 = new Day6();
                        day6.PartOne();
                        day6.PartTwo();
                        break;
                    case "7":
                        var day7 = new Day7();
                        day7.PartOne();
                        day7.PartTwo();
                        break;
                    case "8":
                        var day8 = new Day8();
                        day8.PartOne();
                        day8.PartTwo();
                        break;
                    case "9":
                        var day9 = new Day9();
                        day9.PartOne();
                        day9.PartTwo();
                        break;
                    case "10":
                        var day10 = new Day10();
                        day10.PartOne();
                        day10.PartTwo();
                        break;
                    case "11":
                        var day11 = new Day11();
                        day11.PartOne();
                        day11.PartTwo();
                        break;
                    case "12":
                        var day12 = new Day12();
                        day12.PartOne();
                        day12.PartTwo();
                        break;
                    case "13":
                        var day13 = new Day13();
                        day13.PartOne();
                        day13.PartTwo();
                        break;
                    case "14":
                        var day14 = new Day14();
                        day14.PartOne();
                        day14.PartTwo();
                        break;
                    case "15":
                        var day15 = new Day15();
                        day15.PartOne();
                        day15.PartTwo();
                        break;
                    case "16":
                        var day16 = new Day16();
                        day16.PartOne();
                        day16.PartTwo();
                        break;
                    case "17":
                        var day17 = new Day17();
                        day17.PartOne();
                        day17.PartTwo();
                        break;
                    case "18":
                        var day18 = new Day18();
                        day18.PartOne();
                        day18.PartTwo();
                        break;
                    default:
                        Console.WriteLine("\nTake a valid day from 1 to 30");
                        break;
                }

                //if (DateTime.Now.Second % 3 == 0)
                    //Console.ForegroundColor = (ConsoleColor)random.Next(0, 16);
            }
        }
    }
}
