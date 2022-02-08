using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode
{
    internal class Day16
    {
        string input;
        BitArray bitArray;
        BitsPack mainPacket;

        public Day16()
        {
            input = File.ReadAllText("..\\..\\..\\Day16_Input.txt");    
            
            string binaryMap = "";
            for (int i = 0; i < input.Length; i++)
            {
                binaryMap += Convert.ToString(Convert.ToInt64(input[i].ToString(), 16), 2).PadLeft(4, '0');  
            }   

            bitArray = new BitArray(binaryMap.Length);   
            for (int b = 0; b < binaryMap.Length; b++)
            {
                bitArray.Set(b,binaryMap[b] == '1');
            }

            mainPacket = GetPacket(new BitSequence(bitArray));
        }

      
        internal void PartOne()
        {
            //906
            Console.WriteLine("PartOne :" + GetTotalVersion(mainPacket));
        }

        internal void PartTwo()
        {
            //819324480368
            Console.WriteLine("Part2 : " + Evaluate(mainPacket));
        }
        int GetTotalVersion(BitsPack packet)
        {
            return packet.version + packet.bitsPacks.Select(GetTotalVersion).Sum(); 
        }
        long Evaluate(BitsPack packet)
        {
            long[] parts = packet.bitsPacks.Select(Evaluate).ToArray();
            switch (packet.type)
            {
                case 0: 
                    return parts.Sum();
                case 1: 
                    return parts.Aggregate(1L, (acc, x) => acc * x);
                case 2: 
                    return parts.Min();
                case 3: 
                    return parts.Max();
                case 4: 
                    return packet.literalValue;
                case 5: 
                    return parts[0] > parts[1] ? 1 : 0;
                case 6: 
                    return parts[0] < parts[1] ? 1 : 0;
                case 7: 
                    return parts[0] == parts[1] ? 1 : 0;
                default: 
                    return 0;
            } 
        }
        BitsPack GetPacket(BitSequence bitsSequence)
        {
            int version = bitsSequence.ReadBits(3);
            int type = bitsSequence.ReadBits(3);
            List<BitsPack> packets = new List<BitsPack>();
            long literalValue = 0L;

            if (type == 4)
            {
                bool more = true;
                while (more)
                {
                    more = bitsSequence.ReadBits(1) != 0;
                    literalValue = literalValue * 16 + bitsSequence.ReadBits(4);      
                }
            }
            else 
            {
                if (bitsSequence.ReadBits(1) == 0)
                {                                                
                    BitSequence subPackages = bitsSequence.GetBitArray(bitsSequence.ReadBits(15));
                    while (subPackages.vectr < subPackages.bitArray.Length)
                    {
                        packets.Add(GetPacket(subPackages));
                    }
                }
                else
                {                                                 
                    int nPackets = bitsSequence.ReadBits(11);
                    for (int p = 0; p < nPackets; p++)
                    {
                        packets.Add(GetPacket(bitsSequence));
                    }
                }
            }

            return new BitsPack(version, type, literalValue, packets.ToArray());
        }

        class BitSequence
        {
            public BitArray bitArray { get; private set; }
            public int vectr { get; private set; }
                 
            public BitSequence(BitArray bitArray)
            {
                this.bitArray = bitArray;
                this.vectr = 0;
            }
            public BitSequence GetBitArray(int nBits)
            {
                BitArray newBitArray = new BitArray(nBits);
                for (int i = 0; i < nBits; i++)
                {
                    newBitArray.Set(i, bitArray[vectr++]);
                }
                return new BitSequence(newBitArray);
            }
            public int ReadBits(int nBits)
            {
                int result = 0;
                for (int i = 0; i < nBits; i++)
                {
                    result = result * 2 + (bitArray[vectr++] ? 1 : 0);
                }
                return result;
            }
        }
        class BitsPack
        {
            public int version { get; private set; }
            public int type { get; private set; }
            public long literalValue { get; private set; }
            public BitsPack[] bitsPacks { get; private set; }

            public BitsPack(int version, int type, long literalValue, BitsPack[] bitPacks)
            {
                this.version = version;
                this.type = type;
                this.literalValue = literalValue;
                this.bitsPacks = bitPacks;
            }
        }
    }
}