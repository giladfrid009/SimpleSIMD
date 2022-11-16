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

### Auto-Generated Functions
For any of the ``Elementwise`` functions, an auto-generated overload is generated, which doesn't accept ```Span<T> result```, 
and instead creates ```T[]``` internally and returns the result within this array.  
  
For any of the functions with the Value Delagate pattern, an auto-generated overload is generated, which accepts regular delegates.
Note that using this overload results in performence losses. Check `Value Delegates - Benchmark` section for more info.  

## Performance Benefits

A simple benchmark to demonstrate performance gains of using SIMD.  
Benchmarked method was a ``Sum`` over an ``int[]``.

| Method | Length |          Mean |       Error |      StdDev |        Median | Ratio |
|------- |------- |--------------:|------------:|------------:|--------------:|------:|
|   SIMD |     10 |      3.556 ns |   0.0655 ns |   0.0581 ns |      3.537 ns |  0.66 |
|  Naive |     10 |      5.357 ns |   0.0568 ns |   0.0531 ns |      5.348 ns |  1.00 |
|   SIMD |    100 |      9.079 ns |   0.1948 ns |   0.1822 ns |      9.032 ns |  0.20 |
|  Naive |    100 |     46.178 ns |   0.5255 ns |   0.4658 ns |     46.203 ns |  1.00 |
|   SIMD |   1000 |     66.018 ns |   0.6931 ns |   0.6483 ns |     65.802 ns |  0.17 |
|  Naive |   1000 |    388.244 ns |   3.0852 ns |   2.8859 ns |    389.093 ns |  1.00 |
|   SIMD |   3000 |    185.507 ns |   1.3070 ns |   1.1587 ns |    185.375 ns |  0.16 |
|  Naive |   3000 |  1,139.552 ns |  11.9608 ns |  11.1881 ns |  1,139.374 ns |  1.00 |
|   SIMD |   6000 |    365.993 ns |   3.2114 ns |   3.0039 ns |    365.075 ns |  0.16 |
|  Naive |   6000 |  2,274.374 ns |  14.2898 ns |  12.6675 ns |  2,271.185 ns |  1.00 |
|   SIMD |  10000 |    585.275 ns |   5.2631 ns |   4.1091 ns |    586.638 ns |  0.15 |
|  Naive |  10000 |  3,938.198 ns |  46.8599 ns |  43.8328 ns |  3,926.622 ns |  1.00 |
|   SIMD |  30000 |  1,791.966 ns |  30.4379 ns |  48.2777 ns |  1,778.255 ns |  0.15 |
|  Naive |  30000 | 11,848.767 ns | 184.5488 ns | 163.5977 ns | 11,773.515 ns |  1.00 |
|   SIMD |  60000 |  3,612.872 ns |  71.7281 ns | 113.7683 ns |  3,580.606 ns |  0.15 |
|  Naive |  60000 | 23,606.125 ns | 249.0765 ns | 232.9863 ns | 23,542.178 ns |  1.00 |
|   SIMD | 100000 |  7,325.734 ns | 156.6350 ns | 451.9279 ns |  7,138.866 ns |  0.19 |
|  Naive | 100000 | 40,283.073 ns | 464.1261 ns | 434.1439 ns | 40,328.790 ns |  1.00 |

## Value Delegates
This library uses the value delegate pattern. This pattern is used as a replacement for regular delegates.  
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
            // Creating the data
            // Can be int[], Span<int>, ReadOnlySpan<int>
            int[] Data = GetData();
            
            // We need to create 2 structs which will serve as a replacement for delegates
            SimdOps.Sum(Data, new VecSelector(), new Selector());
        }
    }             
    
    // A struct which is used as Vector<int> selector
    // Inheritence from IFunc<Vector<T>, Vector<T>> is according to Sum() signature
    struct VecSelector : IFunc<Vector<int>, Vector<int>>
    {
        public Vector<int> Invoke(Vector<int> param) => param * 2;
    }

    // A struct which is used as int selector
    // Inheritence from IFunc<T, T> is according to Sum() signature
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
// Delegate, baseline
public static T Sum<T>(ReadOnlySpan<T> span, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector) 
            where T : struct;

// ValueDelegate
public static T Sum<T, F1, F2>(ReadOnlySpan<T> span, F1 vSelector, F2 selector)
            where T  : struct
            where F1 : struct, IFunc<Vector<T>, Vector<T>>
            where F2 : struct, IFunc<T, T>;
```

|        Method |   Length |             Mean |          Error |         StdDev | Ratio |
|-------------- |--------- |-----------------:|---------------:|---------------:|------:|
|      Delegate |       10 |        10.697 ns |      0.0155 ns |      0.0145 ns |  1.00 |
| ValueDelegate |       10 |         5.069 ns |      0.0206 ns |      0.0182 ns |  0.47 |
|      Delegate |      100 |        40.812 ns |      0.0977 ns |      0.0913 ns |  1.00 |
| ValueDelegate |      100 |        11.732 ns |      0.0149 ns |      0.0139 ns |  0.29 |
|      Delegate |     1000 |       302.164 ns |      3.1291 ns |      2.6130 ns |  1.00 |
| ValueDelegate |     1000 |        66.808 ns |      0.2692 ns |      0.2518 ns |  0.22 |
|      Delegate |    10000 |     2,884.803 ns |      8.9309 ns |      7.4577 ns |  1.00 |
| ValueDelegate |    10000 |       585.193 ns |      0.8926 ns |      0.6969 ns |  0.20 |
|      Delegate |   100000 |    28,920.414 ns |    267.4154 ns |    250.1406 ns |  1.00 |
| ValueDelegate |   100000 |     8,519.340 ns |     41.2833 ns |     38.6164 ns |  0.29 |
|      Delegate |  1000000 |   304,228.749 ns |  1,995.9951 ns |  1,769.3976 ns |  1.00 |
| ValueDelegate |  1000000 |    85,619.207 ns |    316.5366 ns |    280.6015 ns |  0.28 |

## Limitations
* Methods are not lazily evaluated as IEnumerable
* Old hardware might not support SIMD
* Supported collection types:
  * ```T[]```
  * ```Span<T>```
  * ```ReadOnlySpan<T>```
* Supports only **Primitive Numeric Types** as array elements. Supported types are:
  * ```byte, sbyte```
  * ```short, ushort```
  * ```int, uint```
  * ```long, ulong```
  * ```nint, nuint```
  * ```float```
  * ```double```

## Contributing
**All ideas and suggestions are welcome.**
Feel free to open an issue if you have an idea or a suggestion that might improve this project.
If you encounter a bug or have a feature request, please open a relevent issue.

## License
This project is licensed under MIT license. For more info see the [License File](LICENSE)
