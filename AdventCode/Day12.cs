using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode
{
    internal class Day12
    {
        string[] inputs;
        List<Connection> connections = new List<Connection>();
        public Day12()
        {
            inputs = File.ReadAllLines("..\\..\\..\\Day12_Input.txt");
            foreach (var line in inputs)
            {
                string[] caves = line.Split("-");
                connections.Add(new Connection(caves[0],caves[1]));
                //connections.Add(new Connection(caves[1], caves[0]));
            }

        }

        internal void PartOne()
        {
            int res = 0;
            foreach (var connection in connections)
            {
                if (connection.from == "start")
                    res+=CalculatePath(connection.to,connection,new List<string>() { "start",connection.to });
                if(connection.to == "start")
                    res+=CalculatePath(connection.from, connection, new List<string>() { "start",connection.from });
            }
            Console.WriteLine(" \nNmber of Paths " + res);
        }
        private int CalculatePath(string currentCave,Connection connection,List<string> alreadyVisitedCaves )
        {
            
            int res = 0;
            for (int i = 0; i < connections.Count; i++)
            {
                string nextCave = "";
                bool isBigCave = false;
                if(connections[i].from == currentCave)
                {
                    nextCave = connections[i].to;
                    isBigCave = nextCave == nextCave.ToUpper();
                }
                else if (connections[i].to == currentCave)
                {
                    nextCave = connections[i].from;
                    isBigCave = nextCave == nextCave.ToUpper();
                }

                if (nextCave != "" && (isBigCave || (!isBigCave && !alreadyVisitedCaves.Contains(nextCave))))
                {                                               
                    if (nextCave == "end")
                    {
                        res++;
                        //Console.WriteLine("");
                        for (int n = 0; n < alreadyVisitedCaves.Count; n++)
                        {
                         //   Console.Write(alreadyVisitedCaves[n] + " , ");
                        }
                       // Console.Write("end");
                    }
                    else
                    {
                        res +=CalculatePath(nextCave, connections[i], CloneList(alreadyVisitedCaves,nextCave));
                    }
                }
            }
            return res;
        }
        private List<string> CloneList(List<string> listToClone,string addItem = "")
        {
            List<string> newList = new List<string>();
            for (int i = 0; i < listToClone.Count; i++)
            {
                newList.Add(listToClone[i]);
            }
            if (addItem != "")
                newList.Add(addItem);
            return newList;
        }
        internal void PartTwo()
        {
            int res = 0;
            foreach (var connection in connections)
            {
                if (connection.from == "start")
                    res += CalculatePathPart2(connection.to, connection, new List<string>() { "start", connection.to });
                if (connection.to == "start")
                    res += CalculatePathPart2(connection.from, connection, new List<string>() { "start", connection.from });
            }
            Console.WriteLine(" \nNmber of Paths Part 2 " + res);
        }
        private int CalculatePathPart2(string currentCave, Connection connection, List<string> alreadyVisitedCaves)
        {

            int res = 0;
            for (int i = 0; i < connections.Count; i++)
            {
                string nextCave = "";
                bool isBigCave = false;
                if (connections[i].from == currentCave)
                {
                    nextCave = connections[i].to;
                    isBigCave = nextCave == nextCave.ToUpper();
                }
                else if (connections[i].to == currentCave)
                {
                    nextCave = connections[i].from;
                    isBigCave = nextCave == nextCave.ToUpper();
                }

                bool alreadyTwice = alreadyVisitedCaves.Where(x => x != x.ToUpper()).GroupBy(x => x).Where(g => g.Count() == 2).Count() > 0;

                if (nextCave != "" && nextCave != "start" && (isBigCave || (!isBigCave && (!alreadyVisitedCaves.Contains(nextCave) || !alreadyTwice) )))
                {
                    if (nextCave == "end")
                    {
                        res++;
                        //Console.WriteLine("");
                        for (int n = 0; n < alreadyVisitedCaves.Count; n++)
                        {
                            //Console.Write(alreadyVisitedCaves[n] + " , ");
                        }
                        //Console.Write("end");
                    }
                    else
                    {
                        res += CalculatePathPart2(nextCave, connections[i], CloneList(alreadyVisitedCaves, nextCave));
                    }
                }
            }
            return res;
        }
    }
    struct Connection
    {
        public string from;
        public string to;

        public Connection(string from,string to)
        {
            this.from = from;
            this.to = to;
        }
    }
}