using System;
using System.Collections;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //В среднем 33% от базового
            Test("50", CreateRandom(50));
            Test("100", CreateRandom(100));
            Test("500", CreateRandom(500));
            Test("1000", CreateRandom(1000));
            Test("1 sign 1000", CreateRandom(1000,1));
            Test("2 signs", CreateRandom(1000, 2));
            Test("3 signs", CreateRandom(1000, 3));
            Test("1 sign 900", CreateRandom(600, 1));
            Test("1 sign 300", CreateRandom(300, 1));
            Test("1 sign 100", CreateRandom(100, 1));
            Test("1 sign 10", CreateRandom(10, 1));
            Test("2 signs 10", CreateRandom(10, 2));
            Test("3 signs 10", CreateRandom(10, 3));
            //Худший тест, 100%, 8bit против 8bit - тут ничего не сделаешь.
            //в строку меньше байта не записать
            Test("1 signs 1", CreateRandom(1, 1));
            //66% от базового
            //99 - 110 0011
            //можно добавить 1 бит в начале указывающий на именно этот случай
            //тогда будет 50%
            //но так как есть другие частные случаи, 1 бита мало, что их все описать
            Test("1 signs 2", CreateRandom(2, 1));
            //100%
            Test("2 signs 1", CreateRandom(1, 2));
            //66%
            Test("3 signs 1", CreateRandom(1, 3));
            //Для последних идей нет, битов не хватает сжать меньше.
        }

        static void Test(string name,List<int> data)
        {
            Console.WriteLine("Start test "+name);
            BaseData baseData = new BaseData(data);
            PackedData packedData = new PackedData(data);
            var strBase = baseData.Serialize();
            var strPack = packedData.Serialize();
            Console.WriteLine("Base size:" + strBase.Length);
            Console.WriteLine("Pack size:" + strPack.Length);
            Console.WriteLine("Coof:" + (float)strPack.Length/(float)strBase.Length);
            var newPack = new PackedData(new());
            newPack.Deserialize(strPack);
            Console.WriteLine("Deserialize correct:" + packedData.Equal(newPack.Items));
            Console.WriteLine();
        }

        static List<int> CreateRandom(int count, int numberSize=-1)
        {
            int minNumber = 1;
            int maxNumber = 301;
            if (numberSize == 1)
                maxNumber = 10;
            if (numberSize == 2)
            {
                minNumber = 10;
                maxNumber = 100;
            }
            if (numberSize == 3)
            {
                minNumber = 100;
            }
            Random rnd = new Random();
            List<int> testData = new List<int>();
            for (int i = 0; i < count; i++)
            {
                testData.Add(rnd.Next(minNumber, maxNumber));
            }
            return testData;
        }
    }
}
