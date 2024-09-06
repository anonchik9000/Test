using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class PackedData : BaseData
    {
        private const int _halfByte = 15;
        private const int _base = 255;
        public PackedData(List<int> items) : base(items)
        {
        }

        public override string Serialize()
        {
            //15 - 1111 - 4бита - 0
            //299 - 1 0010 1011 - 9бит - 1 
            BitData bits = new BitData();
            foreach (int number in Items)
            {
                //время работы функции по задаче не важно, делаю как проще по коду.
                string bitArray = Convert.ToString(number, 2);
                bool half = number <= _halfByte;
                //0 - флаг, что запись 4 бита
                //1 - флаг, что запись 9 бит
                int recordSize = half ? 4 : 9;
                bits.AddNextBit(!half);
                if (bitArray.Length < recordSize)
                {
                    var needCount = recordSize - bitArray.Length;
                    for (int i = 0; i < needCount; i++)
                    {
                        bitArray = "0" + bitArray;
                    }
                }
                for (int i = 0; i < bitArray.Length; i++)
                {
                    bits.AddNextBit(bitArray[i] == '1');
                }
            }
            return bits.GetString();
        }

        public override void Deserialize(string str)
        {
            _items = new List<int>();
            BitData bitData = new BitData(str);
            while (!bitData.IsEnd())
            {
                int readCount = bitData.GetNextBit() ? 9 : 4;
                var numberStr = "";
                for (int i = 0; i < readCount; i++)
                {
                    if (bitData.IsEnd())
                        return;
                    numberStr += bitData.GetNextBit() ? "1" : "0";
                }
                var number = Convert.ToInt32(numberStr, 2);
                if(number>0)
                    _items.Add(number);
            }
        }
        
        

        
    }
}
