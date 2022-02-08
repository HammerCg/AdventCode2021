using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCode
{
    internal class Day17
    {
        string input;
        List<int> results = new List<int>();

        public Day17()
        {
            input = File.ReadAllText("..\\..\\..\\Day17_Input.txt");  
            int[] numbers = Regex.Split(input, @"\D+").Where(s => s != "").Select(n => int.Parse(n)).ToArray();

            int xMin = numbers[0];
            int xMax = numbers[1];
            int yMin = -numbers[2];
            int yMax = -numbers[3];    

            for (var initVelocityX = 0; initVelocityX <= xMax; initVelocityX++)
            {
                for (var initVelocityY = yMin; initVelocityY <= -yMin; initVelocityY++)
                {                                            
                    int xPos = 0;
                    int yPos = 0;
                    int maxY = 0;
                    int velocityX = initVelocityX;
                    int velocityY = initVelocityY;
                                                                                       
                    while (xPos <= xMax && yPos >= yMin)
                    {  
                        xPos += velocityX;
                        velocityX = Math.Max(0, velocityX - 1);

                        yPos += velocityY;
                        velocityY -= 1;

                        maxY = Math.Max(yPos, maxY);
                                                                   
                        if (xPos >= xMin && xPos <= xMax && yPos >= yMin && yPos <= yMax)
                        {
                            results.Add(maxY);
                            break;
                        }
                    }
                }
            }

        }
        internal void PartOne() => Console.WriteLine(results.Max());     
        internal void PartTwo() => Console.WriteLine(results.Count);   
       
    }
}