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
	[SimpleJob(RuntimeMoniker.Net48, launchCount: 2, invocationCount: 10)]
	[SimpleJob(RuntimeMoniker.Net50, launchCount: 2, invocationCount: 10)]
	[NativeMemoryProfiler]
	[MemoryDiagnoser]
	public class LibRaw
	{
		readonly string librawInputBitmap = @"images\sample1.cr2";
		//readonly string librawInputBitmap = @"Images\\Christian - .unique.depth.dng";
		//readonly string librawInputBitmap = "Images\\canon_eos_r_01.cr3";

		//[Benchmark()]
		//public Span<byte> Thumbnail_AsSpan()
		//{
		//	using (var image = new RawImage(librawInput))
		//	using (var thumbnail = image.UnpackThumbnail())
		//	{
		//		return thumbnail.AsProcessedImage().Buffer;
		//	}
		//}

		//[Benchmark(Description = "Unpack Thumbnail", Baseline = true)]
		//public object UnpackThumbnail()
		//{
		//	using (var image = new RawImage(librawInputBitmap))
		//		return image.UnpackThumbnail();
		//}

		[Benchmark(Baseline = true)]
		public Size Bitmap()
		{
			using (var image = new RawImage(librawInputBitmap))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());
				using (var bitmap = raw.AsBitmap())
					return bitmap.Size;
			}
		}

		[Benchmark(Description = "Bitmap - IntPtr")]
		public Size Bitmap_IntPtr()
		{
			using (var image = new RawImage(librawInputBitmap))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());
				using (var bitmap = raw.AsBitmap(2))
					return bitmap.Size;
			}
		}

		[Benchmark(Description = "Bitmap - GPU")]
		public Size Bitmap_Gpu()
		{
			using (var image = new RawImage(librawInputBitmap))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());
				using (var bitmap = raw.AsBitmap(1))
					return bitmap.Size;
			}
		}
	}
	
	class Program
	{
		static void Main(string[] args) => BenchmarkRunner.Run<LibRaw>();
	}
}