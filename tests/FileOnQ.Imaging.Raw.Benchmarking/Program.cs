using System;
using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace FileOnQ.Imaging.Raw.Benchmarking
{
	// TODO - 7/25/2021 - @ahoefling - Add x86 vs x64 platform support - https://github.com/dotnet/BenchmarkDotNet/issues/873
	[SimpleJob(RuntimeMoniker.Net48, launchCount: 5, invocationCount: 10)]
	[SimpleJob(RuntimeMoniker.Net50, launchCount: 5, invocationCount: 10)]
	[NativeMemoryProfiler]
	[MemoryDiagnoser]
	public class LibRaw
	{
		readonly string librawInput = @"images\sample1.cr2";

		[Benchmark()]
		public Span<byte> Thumbnail_AsSpan()
		{
			using (var image = new RawImage(librawInput))
			using (var thumbnail = image.UnpackThumbnail())
			{
				return thumbnail.AsProcessedImage().Buffer;
			}
		}
	}
	
	class Program
	{
		static void Main(string[] args) => BenchmarkRunner.Run<LibRaw>();
	}
}