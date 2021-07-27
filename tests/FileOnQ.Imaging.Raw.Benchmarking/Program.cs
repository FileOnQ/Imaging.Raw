using System;
using System.Drawing;
using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace FileOnQ.Imaging.Raw.Benchmarking
{
	// TODO - 7/25/2021 - @ahoefling - Add x86 vs x64 platform support - https://github.com/dotnet/BenchmarkDotNet/issues/873
	[SimpleJob(RuntimeMoniker.Net48, launchCount: 15, invocationCount: 10)]
	[SimpleJob(RuntimeMoniker.Net50, launchCount: 15, invocationCount: 10)]
	[NativeMemoryProfiler]
	[MemoryDiagnoser]
	public class LibRaw
	{
		readonly string librawInput = @"images\sample1.cr2";
		readonly string librawInputBitmap = @"Images\\Christian - .unique.depth.dng";

		//[Benchmark()]
		//public Span<byte> Thumbnail_AsSpan()
		//{
		//	using (var image = new RawImage(librawInput))
		//	using (var thumbnail = image.UnpackThumbnail())
		//	{
		//		return thumbnail.AsProcessedImage().Buffer;
		//	}
		//}

		[Benchmark(Description = "Unpack Thumbnail", Baseline = true)]
		public object UnpackThumbnail()
		{
			using (var image = new RawImage(librawInputBitmap))
				return image.UnpackThumbnail();
		}

		[Benchmark]
		public Size Bitmap()
		{
			using (var image = new RawImage(librawInputBitmap))
			using (var thumbnail = image.UnpackThumbnail())
			using (var bitmap = thumbnail.AsBitmap())
			{
				return bitmap.Size;
			}
		}

		[Benchmark(Description = "Bitmap - GPU")]
		public Size Bitmap_Gpu()
		{
			using (var image = new RawImage(librawInputBitmap))
			using (var thumbnail = image.UnpackThumbnail())
			using (var bitmap = thumbnail.AsBitmap(true))
			{
				return bitmap.Size;
			}
		}
	}
	
	class Program
	{
		static void Main(string[] args) => BenchmarkRunner.Run<LibRaw>();
	}
}