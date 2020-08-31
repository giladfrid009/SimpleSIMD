using SimpleSimd;
using System;

namespace Benchmarks
{
    public static class RndArrs<T> where T : unmanaged
    {
        #pragma warning disable CA1819
        public static T[] Arr1 { get; private set; }
        public static T[] Arr2 { get; private set; }
        public static T[] Arr3 { get; private set; }
        public static int Scale { get; } = 10;

        private static Random Rnd = new Random();

        public static int Length
        {
            get => Arr1.Length;
            set => Generate(value);
        }

        public static int Seed
        {
            set => Rnd = new Random(value);
        }

        static RndArrs()
        {
            Generate(Rnd.Next(20, 100));
        }

        private static void Generate(int length)
        {
            Arr1 = new T[length];
            Arr2 = new T[length];
            Arr3 = new T[length];

            for (int i = 0; i < length; i++)
            {
                Arr1[i] = NConverter<double, T>.Convert(Rnd.NextDouble() * Scale);
                Arr2[i] = NConverter<double, T>.Convert(Rnd.NextDouble() * Scale);
                Arr3[i] = NConverter<double, T>.Convert(Rnd.NextDouble() * Scale);
            }
        }
    }
}
