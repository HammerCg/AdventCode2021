using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode
{
    internal class Day3
    {
        string[] inputs;
        public Day3()
        {
            inputs = File.ReadAllLines("..\\..\\..\\Day3_Input.txt");
            Console.WriteLine(inputs.Length + " Inputs");
        }
        public void PartOne()
        {

            string gammaRate = "";
            string epsilonRate = "";
            for (int n = 0; n < inputs[0].Length; n++)
            {
                int nZero = 0;
                int nOne = 0;
                for (int i = 0; i < inputs.Length; i++)
                { 
                    if(inputs[i][n] == '0')
                    {
                        nZero++;
                    }   
                    else
                    {
                        nOne++;
                    }
                }
                if (nZero > nOne)
                {
                    gammaRate += "0";
                    epsilonRate += "1";
                }
                else
                {
                    gammaRate += "1";
                    epsilonRate += "0";
                }
            }
            int power = Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate, 2);
            Console.WriteLine(power + " power consumption");
        }

        public void PartTwo()
        {    
            int oxygen = OxygenRating();
            int scrubber = ScrubberRating();
            int supportRatting = oxygen * scrubber;
            Console.WriteLine(supportRatting + "  SupportRating   " + oxygen + "    " + scrubber);
        }
        private int OxygenRating()
        {
            List<string> listInputsOxygen = inputs.ToList<string>();        

            for (int n = 0; n < inputs[0].Length; n++)
            {
                int nZero = 0;
                int nOne = 0;
                for (int i = 0; i < listInputsOxygen.Count; i++)
                {                                                        
                    if (listInputsOxygen[i][n] == '0')
                    {
                        nZero++;
                    }
                    else
                    {
                        nOne++;
                    }
                }
                if (nZero > nOne)
                {
                    if (listInputsOxygen.Count > 1)
                        listInputsOxygen.RemoveAll(x => x.ToCharArray()[n] != '0');        
                }
                else if (nZero == nOne)
                {
                    if (listInputsOxygen.Count > 1)
                        listInputsOxygen.RemoveAll(x => x.ToCharArray()[n] != '1');        
                }
                else
                {
                    if (listInputsOxygen.Count > 1)
                        listInputsOxygen.RemoveAll(x => x.ToCharArray()[n] != '1');        
                }
                if (listInputsOxygen.Count == 1) break;
            }
            return Convert.ToInt32(listInputsOxygen.First(), 2);      
        }
        private int ScrubberRating()
        {                                                             
            List<string> listInputsCo2Scrubber = inputs.ToList<string>();

            for (int n = 0; n < inputs[0].Length; n++)
            {
                int nZero = 0;
                int nOne = 0;
                for (int i = 0; i < listInputsCo2Scrubber.Count; i++)
                {
                    if (listInputsCo2Scrubber[i][n] == '0')
                    {
                        nZero++;
                    }
                    else
                    {
                        nOne++;
                    }
                }
                if (nZero > nOne)
                {                                                                    
                    if (listInputsCo2Scrubber.Count > 1)
                        listInputsCo2Scrubber.RemoveAll(x => x.ToCharArray()[n] != '1');
                }
                else if (nZero == nOne)
                {                                                                    
                    if (listInputsCo2Scrubber.Count > 1)
                        listInputsCo2Scrubber.RemoveAll(x => x.ToCharArray()[n] != '0');
                }
                else
                {                                                                    
                    if (listInputsCo2Scrubber.Count > 1)
                        listInputsCo2Scrubber.RemoveAll(x => x.ToCharArray()[n] != '0');
                }
                if (listInputsCo2Scrubber.Count == 1) break;
            }                                                           
            return Convert.ToInt32(listInputsCo2Scrubber.First(), 2);                        
        }
    }
}