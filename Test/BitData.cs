
using System.Text;

namespace Test
{
    public class BitData
    {
        private List<byte> _data = new List<byte>();
        private int _bitCounts = 0;

        public BitData()
        {
        }

        public BitData(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                _data.Add((byte)str[i]);
            }
        }

        public string GetString()
        {
            StringBuilder sb = new StringBuilder();
            for(int i =0; i < _data.Count; i++)
            {
                sb.Append((char)_data[i]);
            }
            return sb.ToString();
        }

        public void AddNextBit(bool active)
        {
            if(_data.Count == 0)
            {
                _data.Add(0);
            }
            if (_bitCounts == 8)
            {
                _bitCounts = 0;
                _data.Add(0);
            }
            _data[_data.Count - 1] = SetBit(_data[_data.Count - 1], _bitCounts, active);
            _bitCounts++;
        }
        public bool IsEnd()
        {
            return _data.Count == 0;
        }
        public bool GetNextBit()
        {
            bool bit = GetBit(_data[0], _bitCounts);
            _bitCounts++;
            if (_bitCounts == 8)
            {
                _bitCounts = 0;
                _data.RemoveAt(0);
            }
            return bit;
        }

        private bool GetBit(byte value, int bitInByteIndex)
        {
            byte mask = (byte)(1 << bitInByteIndex);
            return (value & mask) != 0;
        }
        private byte SetBit(byte value, int bitInByteIndex, bool active)
        {
            byte mask = (byte)(1 << bitInByteIndex);
            if (active)
                return (byte)(value | mask);
            return (byte)(value & ~mask);
        }
    }
}
