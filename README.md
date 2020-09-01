# SimpleSIMD

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
* Equal
* Greater
* GreaterEqual
* Less
* LessEqual

#### Elementwise:
* Abs
* Add
* Div
* Mul
* Neg
* Select
* Sub

#### Reduction:
* Accumulate
* Avg
* Dot
* Max
* Min
* Sum

#### General Purpose:
* All
* Any
* Contains
* Copy
* Fill
* Foreach
* ToVector 

## Limitations
* Is not lazily evaluated as IEnumerable
* Benefitial only for hardware which supports SIMD instructions
* Only work for collections of type ```T[] where T : unmanaged```
* Could perform worse than simple for loop approach, for very small arrays
* Supports only **Primitive Numeric Types** as array elements. Supported types are:
  * ```byte, sbyte```
  * ```short, ushort```
  * ```int, uint```
  * ```long, ulong```
  * ```float```
  * ```double```

## License
This project is licensed under MIT license. For more info see the [License File](LICENSE)
