using System;
using System.IO;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Jobs;
using FileOnQ.Imaging.Raw.Processors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;

namespace FileOnQ.Imaging.Raw.Benchmarking
{
	[SimpleJob(RuntimeMoniker.Net50, launchCount: 10, invocationCount: 10)]
	//[NativeMemoryProfiler]
	//[MemoryDiagnoser]
	[JsonExporterAttribute.Full]
	public class ThumbnailResize
	{
		readonly string filePath;
		readonly string output;
		public ThumbnailResize()
		{
			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			filePath = Path.Combine(assemblyDirectory, Program.Input);
			if (!File.Exists(filePath))
				throw new FileNotFoundException(filePath);

			output = Path.Combine(assemblyDirectory, "output.jpeg");
		}

		//[Benchmark(Baseline = true)]
		//public void ExtractThumbnail()
		//{
		//	using (var image = new RawImage(filePath))
		//	using (var thumb = image.UnpackThumbnail())
		//	{
		//		thumb.Process(new ThumbnailProcessor());
		//		thumb.Write(output);
		//	}

		//	File.Delete(output);
		//}

		[Benchmark(Baseline = true)]
		public void ExtractThumbnailAndResize_Bicubic()
		{
			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new ImageSharpResizeProcessor(KnownResamplers.Bicubic));
				thumb.Write(output);
			}

			File.Delete(output);
		}

		[Benchmark]
		public void ExtractThumbnailAndResize_Box()
		{
			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new ImageSharpResizeProcessor(KnownResamplers.Box));
				thumb.Write(output);
			}

			File.Delete(output);
		}

		[Benchmark]
		public void ExtractThumbnailAndResize_CatmullRom()
		{
			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new ImageSharpResizeProcessor(KnownResamplers.CatmullRom));
				thumb.Write(output);
			}

			File.Delete(output);
		}

		[Benchmark]
		public void ExtractThumbnailAndResize_Hermite()
		{
			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new ImageSharpResizeProcessor(KnownResamplers.Hermite));
				thumb.Write(output);
			}

			File.Delete(output);
		}

		[Benchmark]
		public void ExtractThumbnailAndResize_Lanczos2()
		{
			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new ImageSharpResizeProcessor(KnownResamplers.Lanczos2));
				thumb.Write(output);
			}

			File.Delete(output);
		}

		[Benchmark]
		public void ExtractThumbnailAndResize_Lanczos3()
		{
			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new ImageSharpResizeProcessor(KnownResamplers.Lanczos3));
				thumb.Write(output);
			}

			File.Delete(output);
		}

		[Benchmark]
		public void ExtractThumbnailAndResize_Lanczos5()
		{
			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new ImageSharpResizeProcessor(KnownResamplers.Lanczos5));
				thumb.Write(output);
			}

			File.Delete(output);
		}

		[Benchmark]
		public void ExtractThumbnailAndResize_Lanczos8()
		{
			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new ImageSharpResizeProcessor(KnownResamplers.Lanczos8));
				thumb.Write(output);
			}

			File.Delete(output);
		}

		[Benchmark]
		public void ExtractThumbnailAndResize_MitchellNetravali()
		{
			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new ImageSharpResizeProcessor(KnownResamplers.MitchellNetravali));
				thumb.Write(output);
			}

			File.Delete(output);
		}

		[Benchmark]
		public void ExtractThumbnailAndResize_NearestNeighbor()
		{
			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new ImageSharpResizeProcessor(KnownResamplers.NearestNeighbor));
				thumb.Write(output);
			}

			File.Delete(output);
		}

		[Benchmark]
		public void ExtractThumbnailAndResize_Robidoux()
		{
			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new ImageSharpResizeProcessor(KnownResamplers.Robidoux));
				thumb.Write(output);
			}

			File.Delete(output);
		}

		[Benchmark]
		public void ExtractThumbnailAndResize_RobidouxSharp()
		{
			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new ImageSharpResizeProcessor(KnownResamplers.RobidouxSharp));
				thumb.Write(output);
			}

			File.Delete(output);
		}

		[Benchmark]
		public void ExtractThumbnailAndResize_Spline()
		{
			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new ImageSharpResizeProcessor(KnownResamplers.Spline));
				thumb.Write(output);
			}

			File.Delete(output);
		}

		[Benchmark]
		public void ExtractThumbnailAndResize_Triangle()
		{
			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new ImageSharpResizeProcessor(KnownResamplers.Triangle));
				thumb.Write(output);
			}

			File.Delete(output);
		}

		[Benchmark]
		public void ExtractThumbnailAndResize_Welch()
		{
			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new ImageSharpResizeProcessor(KnownResamplers.Welch));
				thumb.Write(output);
			}

			File.Delete(output);
		}

		class ImageSharpResizeProcessor : ThumbnailProcessor
		{
			Image image;
			IResampler resampler;
			public ImageSharpResizeProcessor(IResampler resampler)
			{
				this.resampler = resampler;
			}

			public override void Process(RawImageData data)
			{
				image = Image.Load(data.Buffer);
				if (image.Width > image.Height)
					image.Mutate(ctx => ctx.Resize(200, 0, resampler));
				else
					image.Mutate(ctx => ctx.Resize(0, 200));
			}

			public override void Write(RawImageData data, string file)
			{
				image.Save(file);
			}

			protected override void Dispose(bool disposing)
			{
				if (IsDisposed)
					return;

				if (disposing)
				{
					if (image != null)
					{
						image.Dispose();
						image = null;
					}
				}
			}
		}
	}
}
