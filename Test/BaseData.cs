
using System.Text;

namespace Test
{
    public class BaseData
    {
        const char _separator = ';';
        
        protected List<int> _items;

        public List<int> Items => _items;

        public BaseData(List<int> items)
        { 
            _items = new List<int>(items); 
        }

        public bool Equal(List<int> other)
        {
            if(other.Count != _items.Count) return false;
            for(int i =0; i < _items.Count;i++)
                if(other[i] != _items[i]) return false;
            return true;
        }

        public void Write()
        {
            foreach (int item in _items)
            {
                Console.Write(item);
            }
            Console.WriteLine();
        }

        public virtual string Serialize()
        {
            StringBuilder result = new StringBuilder();
            for(int i =0;i < _items.Count;i++)
            {
                result.Append(_items[i]);
                if (i < _items.Count - 1)
                    result.Append(_separator);
            }
            return result.ToString();
        }

        public virtual void Deserialize(string str)
        {
            _items = new List<int>();
            var split = str.Split(new char[] { _separator }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string i in split)
            {
                //Предположим, что корректность данных проверять не надо, по условию задачи тестов на такое нет
                _items.Add(int.Parse(i));
            }
        }
    }
}
