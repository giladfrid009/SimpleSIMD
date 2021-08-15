namespace Generator
{
    static class Consts
    {
        /// <summary>
        /// The name of the generated attribute
        /// </summary>
        public const string AttrName = "ArrOverloadAttribute";

        /// <summary>
        /// The namespace the attribute will be generated into
        /// </summary>
        public const string AttrNamespace = "SimpleSimd";

        /// <summary>
        /// The class the generated methods are located in
        /// </summary>
        public const string ClassName = "SimdOps<T>";

        /// <summary>
        /// The name of the generated source file
        /// </summary>
        public const string FileName = "ArrayOverloads";
    }
}
