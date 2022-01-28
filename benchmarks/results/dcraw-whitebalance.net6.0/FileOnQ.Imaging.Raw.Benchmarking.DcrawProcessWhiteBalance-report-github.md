``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.17763.2458 (1809/October2018Update/Redstone5)
Intel Xeon Platinum 8272CL CPU 2.60GHz, 1 CPU, 2 logical and 2 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  Job-KKMFSC : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Runtime=.NET 6.0  InvocationCount=1  LaunchCount=1  
UnrollFactor=1  

```
|             Method |    Mean |   Error |  StdDev | Allocated native memory | Native memory leak |     Gen 0 |     Gen 1 |     Gen 2 |  Allocated |
|------------------- |--------:|--------:|--------:|------------------------:|-------------------:|----------:|----------:|----------:|-----------:|
|   ProccessAndWrite | 11.10 s | 0.053 s | 0.044 s |              810,799 KB |                  - |         - |         - |         - |       3 KB |
| ProccessIntoMemory | 10.89 s | 0.124 s | 0.110 s |              810,769 KB |                  - |         - |         - |         - | 149,887 KB |
|  ProcessIntoBitmap | 11.19 s | 0.207 s | 0.194 s |              810,774 KB |               3 KB | 1000.0000 | 1000.0000 | 1000.0000 | 299,764 KB |
