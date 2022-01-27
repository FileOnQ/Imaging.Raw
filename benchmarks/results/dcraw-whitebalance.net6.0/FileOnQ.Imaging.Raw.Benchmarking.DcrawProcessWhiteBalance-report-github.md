``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.17763.2452 (1809/October2018Update/Redstone5)
Intel Xeon Platinum 8272CL CPU 2.60GHz, 1 CPU, 2 logical and 2 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  Job-RPZEDY : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-TAGGTQ : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  Job-ZGQAKC : .NET Framework 4.8 (4.8.4420.0), X64 RyuJIT

InvocationCount=1  LaunchCount=1  UnrollFactor=1  

```
|             Method |        Job |            Runtime |    Mean |   Error |  StdDev |     Gen 0 |     Gen 1 |     Gen 2 |  Allocated | Allocated native memory | Native memory leak |
|------------------- |----------- |------------------- |--------:|--------:|--------:|----------:|----------:|----------:|-----------:|------------------------:|-------------------:|
|   ProccessAndWrite | Job-RPZEDY |           .NET 5.0 | 11.06 s | 0.009 s | 0.008 s |         - |         - |         - |       2 KB |              810,799 KB |                  - |
| ProccessIntoMemory | Job-RPZEDY |           .NET 5.0 | 10.85 s | 0.013 s | 0.013 s |         - |         - |         - | 149,879 KB |              810,769 KB |                  - |
|  ProcessIntoBitmap | Job-RPZEDY |           .NET 5.0 | 11.16 s | 0.010 s | 0.009 s | 1000.0000 | 1000.0000 | 1000.0000 | 299,756 KB |              810,770 KB |               0 KB |
|   ProccessAndWrite | Job-TAGGTQ |           .NET 6.0 | 11.12 s | 0.010 s | 0.009 s |         - |         - |         - |       3 KB |              810,799 KB |                  - |
| ProccessIntoMemory | Job-TAGGTQ |           .NET 6.0 | 10.84 s | 0.028 s | 0.024 s |         - |         - |         - | 149,887 KB |              810,772 KB |               3 KB |
|  ProcessIntoBitmap | Job-TAGGTQ |           .NET 6.0 | 11.18 s | 0.018 s | 0.017 s | 1000.0000 | 1000.0000 | 1000.0000 | 299,764 KB |              810,774 KB |               3 KB |
|   ProccessAndWrite | Job-ZGQAKC | .NET Framework 4.8 | 11.09 s | 0.007 s | 0.007 s |         - |         - |         - |       8 KB |              810,799 KB |                  - |
| ProccessIntoMemory | Job-ZGQAKC | .NET Framework 4.8 | 10.85 s | 0.008 s | 0.008 s |         - |         - |         - | 149,885 KB |              810,769 KB |                  - |
|  ProcessIntoBitmap | Job-ZGQAKC | .NET Framework 4.8 | 11.18 s | 0.018 s | 0.017 s | 1000.0000 | 1000.0000 | 1000.0000 | 299,763 KB |              810,770 KB |                  - |
