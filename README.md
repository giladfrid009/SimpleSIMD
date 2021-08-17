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

| Method | Length |           Mean |       Error |      StdDev | Ratio |
|------- |------- |---------------:|------------:|------------:|------:|
|   LINQ |     10 |      58.428 ns |   1.1658 ns |   1.4743 ns |  9.65 |
|  Naive |     10 |       6.138 ns |   0.1226 ns |   0.1087 ns |  1.00 |
|   SIMD |     10 |       5.739 ns |   0.1397 ns |   0.1372 ns |  0.93 |
|   LINQ |    100 |     475.290 ns |   9.3530 ns |  17.7951 ns |  7.36 |
|  Naive |    100 |      65.447 ns |   0.8545 ns |   0.7575 ns |  1.00 |
|   SIMD |    100 |      12.879 ns |   0.2039 ns |   0.1592 ns |  0.20 |
|   LINQ |   1000 |   4,620.020 ns |  80.4166 ns |  71.2872 ns |  7.47 |
|  Naive |   1000 |     617.992 ns |   7.6832 ns |   7.1869 ns |  1.00 |
|   SIMD |   1000 |      78.865 ns |   0.7991 ns |   0.6673 ns |  0.13 |
|   LINQ |  10000 |  43,103.800 ns | 700.6532 ns | 655.3915 ns |  6.99 |
|  Naive |  10000 |   6,164.725 ns |  51.9217 ns |  48.5676 ns |  1.00 |
|   SIMD |  10000 |     738.459 ns |  14.7266 ns |  32.3252 ns |  0.13 |
|   LINQ | 100000 | 393,739.178 ns | 755.6571 ns | 631.0079 ns |  6.73 |
|  Naive | 100000 |  58,510.310 ns |  58.0928 ns |  54.3400 ns |  1.00 |
|   SIMD | 100000 |   8,897.370 ns | 102.2559 ns |  95.6502 ns |  0.15 |

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
            int[] Data = GetData()
            
            // We need to create 2 structs which will serve as a replacement for delegates
            SimdOps<int>.Sum(Data, new VecSelector(), new Selector());
        }
    }             
    
    // A struct which is used as Vector<int> selector
    // Inheritence from IFunc is according to Sum() signature
    struct VecSelector : IFunc<Vector<int>, Vector<int>>
    {
        public Vector<int> Invoke(Vector<int> param) => param * 2;
    }

    // A struct which is used as int selector
    // Inheritence from IFunc is according to Sum() signature
    struct Selector : IFunc<int, int>
    {
        public int Invoke(int param) => param * 2;
    }   
}
```

#### Benchmark:

Both of the benchmarked methods have the exactly same code, both of them are accelerated using SIMD,  
the only difference is the argument types.

``` csharp
// Delegate, baseline
public static T Sum(Span<T> span, Func<Vector<T>, Vector<T>> vSelector, Func<T, T> selector)

// ValueDelegate
public static T Sum<F1, F2>(in Span<T> span, F1 vSelector, F2 selector)
            where F1 : struct, IFunc<Vector<T>, Vector<T>>
            where F2 : struct, IFunc<T, T> 
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
* Supports All the types supported by ```System.Numerics.vector<T>```. Supported types are:
  * ```byte, sbyte```
  * ```short, ushort```
  * ```int, uint```
  * ```nint, nuint```
  * ```long, ulong```
  * ```float```
  * ```double```

## Contributing
**All ideas and suggestions are welcome.**
Feel free to open an issue if you have an idea or a suggestion that might improve this project.
If you encounter a bug or have a feature request, please open a relevent issue.

## License
This project is licensed under MIT license. For more info see the [License File](LICENSE)
