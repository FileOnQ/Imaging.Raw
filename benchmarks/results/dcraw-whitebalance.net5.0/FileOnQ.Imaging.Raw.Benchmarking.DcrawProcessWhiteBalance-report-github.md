``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.17763.2458 (1809/October2018Update/Redstone5)
Intel Xeon Platinum 8272CL CPU 2.60GHz, 1 CPU, 2 logical and 2 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-YHUJNI : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT

Runtime=.NET 5.0  InvocationCount=1  LaunchCount=1  
UnrollFactor=1  

```
|             Method |     Mean |    Error |   StdDev |   Median | Allocated native memory | Native memory leak |     Gen 0 |     Gen 1 |     Gen 2 |  Allocated |
|------------------- |---------:|---------:|---------:|---------:|------------------------:|-------------------:|----------:|----------:|----------:|-----------:|
|   ProccessAndWrite | 10.344 s | 0.2065 s | 0.4949 s | 10.097 s |              810,799 KB |               0 KB |         - |         - |         - |       1 KB |
| ProccessIntoMemory |  9.664 s | 0.1219 s | 0.1141 s |  9.708 s |              810,769 KB |               0 KB |         - |         - |         - | 149,879 KB |
|  ProcessIntoBitmap | 10.283 s | 0.1431 s | 0.1339 s | 10.299 s |              810,771 KB |               0 KB | 1000.0000 | 1000.0000 | 1000.0000 | 299,756 KB |
