using SimpleSimd;

namespace Tests
{
    public static class TestArrs<T> where T : unmanaged
    {
        #pragma warning disable CA1819
        public static T[] Arr1N { get; private set; }
        public static T[] Arr1 { get; private set; }
        public static T[] Arr2 { get; private set; }
        public static T[] Arr3 { get; private set; }
        public static T[] Arr6 { get; private set; }
        public static T[] ArrAsc { get; private set; }

        public static int Length
        {
            get => Arr1.Length;
            set => Generate(value);
        }

        static TestArrs()
        {
            Generate(new System.Random().Next(20, 100));
        }

        private static void Generate(int length)
        {
            Arr1N = new T[length];
            Arr1 = new T[length];
            Arr2 = new T[length];
            Arr3 = new T[length];
            Arr6 = new T[length];
            ArrAsc = new T[length];

            T negOne = Converter<int, T>.Change(-1);
            T one = Converter<int, T>.Change(1);
            T two = Converter<int, T>.Change(2);
            T three = Converter<int, T>.Change(3);
            T six = Converter<int, T>.Change(6);

            Arr1N.Fill(negOne);
            Arr1.Fill(one);
            Arr2.Fill(two);
            Arr3.Fill(three);
            Arr6.Fill(six);

            for (int i = 0; i < length; i++)
            {
                ArrAsc[i] = Converter<int, T>.Change(i + 1);
            }
        }
    }
}
