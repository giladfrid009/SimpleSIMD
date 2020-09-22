# SimpleSIMD

[![NuGet version (SimpleSIMD)](https://img.shields.io/nuget/v/SimpleSIMD.svg?style=flat-square)](https://www.nuget.org/packages/SimpleSIMD/)

### What is SIMD?
Single Instruction, Multiple Data (SIMD) units refer to hardware components that perform the same operation on multiple data operands concurrently.
The concurrency is performed on a single thread, while utilizing the full size of the processor register to perform several operations at one.  
This approach could be combined with standard multithreading for massive performence boosts in numeric computations.

## Goals And Purpose
* Single API to unify SIMD for **All supported types**
* Gain performence boost for mathematical computations using a simple API
* Simplifies SIMD usage, and to make it easy to integrate it into an already existing solutions
* Helps generalize several methemathical functions for supported types
* Performs less allocations compared to standard LINQ implementations

## Available Functions
#### Comparison:
* Equal
* Greater
* GreaterOrEqual
* Less
* LessOrEqual

#### Elementwise:
* Negate
* Abs
* Add
* Divide
* Multiply
* Subtract
* And
* Or
* Xor
* Not
* Select
* Ternary (Conditional Select)
* Concat
* Sqrt

#### Reduction:
* Aggregate
* Sum
* Average
* Max
* Min
* Dot

#### General Purpose:
* All
* Any
* Contains
* IndexOf
* Fill
* Foreach

## Value Delegates
This library extensively uses the value delegate pattern. This pattern is used as a replacement for delegates.  
Calling functions using this patten may feel unusual since it requires creation of structs to pass as arguments instead of delegates, but it is very beneficial performance-wise. 
The performance difference makes using this pattern worthwhile in performance critical places.  
Since the focus of this library is **pure performance**, we use this pattern wherever possible.
#### Usage:

``` csharp
using System;
using System.Numerics;
using SimpleSimd;

namespace MyProgram
{
    class Program
    {
        static void Main()
        {
            int[] Data = new int[1000];

            Random rnd = new Random(1); 

            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = rnd.Next();
            }

            SimdOps<int>.Sum(Data, new VecSelector(), new Selector());
        }
    }             

    struct VecSelector : IFunc<Vector<int>, Vector<int>>
    {
        public Vector<int> Invoke(Vector<int> param) => param * 2;
    }

    struct Selector : IFunc<int, int>
    {
        public int Invoke(int param) => param * 2;
    }   
}
```

#### benchmark:

Both of the benchmarked methods have the exactly same code, both of them are accelerated using SIMD,  
the only difference is the argument types.

``` csharp
// Delegate entry, baseline
public static T Sum(Span<T> span, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector)

// ValueDelegate entry
public static T Sum<F1, F2>(in Span<T> span, F1 vSelector, F2 selector)
            where F1 : struct, IFunc<Vector<T>, Vector<T>>
            where F2 : struct, IFunc<T, T> 
```

|        Method |   Length |             Mean |          Error |         StdDev | Ratio |
|-------------- |--------- |-----------------:|---------------:|---------------:|------:|
|      Delegate |       10 |        10.697 ns |      0.0155 ns |      0.0145 ns |  1.00 |
| ValueDelegate |       10 |         5.069 ns |      0.0206 ns |      0.0182 ns |  0.47 |
|               |          |                  |                |                |       |
|      Delegate |      100 |        40.812 ns |      0.0977 ns |      0.0913 ns |  1.00 |
| ValueDelegate |      100 |        11.732 ns |      0.0149 ns |      0.0139 ns |  0.29 |
|               |          |                  |                |                |       |
|      Delegate |     1000 |       302.164 ns |      3.1291 ns |      2.6130 ns |  1.00 |
| ValueDelegate |     1000 |        66.808 ns |      0.2692 ns |      0.2518 ns |  0.22 |
|               |          |                  |                |                |       |
|      Delegate |    10000 |     2,884.803 ns |      8.9309 ns |      7.4577 ns |  1.00 |
| ValueDelegate |    10000 |       585.193 ns |      0.8926 ns |      0.6969 ns |  0.20 |
|               |          |                  |                |                |       |
|      Delegate |   100000 |    28,920.414 ns |    267.4154 ns |    250.1406 ns |  1.00 |
| ValueDelegate |   100000 |     8,519.340 ns |     41.2833 ns |     38.6164 ns |  0.29 |
|               |          |                  |                |                |       |
|      Delegate |  1000000 |   304,228.749 ns |  1,995.9951 ns |  1,769.3976 ns |  1.00 |
| ValueDelegate |  1000000 |    85,619.207 ns |    316.5366 ns |    280.6015 ns |  0.28 |
|               |          |                  |                |                |       |
|      Delegate | 10000000 | 3,181,029.471 ns |  5,987.0172 ns |  5,600.2596 ns |  1.00 |
| ValueDelegate | 10000000 | 2,615,592.902 ns | 13,642.9671 ns | 12,761.6399 ns |  0.82 |

```
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.14393.3930 (1607/AnniversaryUpdate/Redstone1)
Intel Core i7-4790 CPU 3.60GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
Frequency=3515621 Hz, Resolution=284.4448 ns, Timer=TSC
.NET Core SDK = 3.1.401
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
```

## Limitations
* Methods are not lazily evaluated as IEnumerable
* Benefitial only for hardware which supports SIMD instructions
* Could perform worse than simple for loop approach, for very small arrays
* Supported collection types:
  * ```T[] where T : unmanaged```
  * ```Span<T> where T : unmanaged```
* Supports only **Primitive Numeric Types** as array elements. Supported types are:
  * ```byte, sbyte```
  * ```short, ushort```
  * ```int, uint```
  * ```long, ulong```
  * ```float```
  * ```double```

## License
This project is licensed under MIT license. For more info see the [License File](LICENSE)
