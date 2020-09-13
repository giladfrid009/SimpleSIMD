# SimpleSIMD

[![NuGet version (SimpleSIMD)](https://img.shields.io/nuget/v/SimpleSIMD.svg?style=flat-square)](https://www.nuget.org/packages/SimpleSIMD/)

### What is SIMD?
Single Instruction, Multiple Data (SIMD) units refer to hardware components that perform the same operation on multiple data operands concurrently.
The concurrency is performed on a single thread, while utilizing the full size of the processor register to perform several operations at one.  
This approach could be combined with standard multithreading for massive performence boosts in numeric computations.

## Goals And Purpose
* Gain performence boost for mathematical computations using a simple API
* Simplifies SIMD usage, and to make it easy to integrate it into an already existing solutions
* Helps generalize several methemathical functions for supported types
* Performs less allocations compared to standard LINQ implementations

## Available Functions
#### Comparison:
* Equals
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
* Copy
* IndexOf
* Fill
* Foreach

## Limitations
* Methods are not lazily evaluated as IEnumerable
* Benefitial only for hardware which supports SIMD instructions
* Could perform worse than simple for loop approach, for very small arrays
* Only work for collections of type ```T[] where T : unmanaged```
* Supports only **Primitive Numeric Types** as array elements. Supported types are:
  * ```byte, sbyte```
  * ```short, ushort```
  * ```int, uint```
  * ```long, ulong```
  * ```float```
  * ```double```

## License
This project is licensed under MIT license. For more info see the [License File](LICENSE)
