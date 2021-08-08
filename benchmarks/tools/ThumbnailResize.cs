using System;
using System.IO;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Jobs;
using FileOnQ.Imaging.Raw.Processors;
using ImageMagick;
using PhotoSauce.MagicScaler;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;

namespace FileOnQ.Imaging.Raw.Benchmarking
{
	[SimpleJob(RuntimeMoniker.Net50, launchCount: 5, invocationCount: 10)]
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

		[Benchmark(Baseline = true)]
		public void ExtractThumbnail()
		{
			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new ThumbnailProcessor());
				thumb.Write(output);
			}

			File.Delete(output);
		}

		[Benchmark(Baseline = true)]
		public void ImageSharp_ThumbnailAndResize_Bicubic()
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
		public void ImageSharp_ThumbnailAndResize_Box()
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
		public void ImageSharp_ThumbnailAndResize_CatmullRom()
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
		public void ImageSharp_ThumbnailAndResize_Hermite()
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
		public void ImageSharp_ThumbnailAndResize_Lanczos2()
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
		public void ImageSharp_ThumbnailAndResize_Lanczos3()
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
		public void ImageSharp_ThumbnailAndResize_Lanczos5()
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
		public void ImageSharp_ThumbnailAndResize_Lanczos8()
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
		public void ImageSharp_ThumbnailAndResize_MitchellNetravali()
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
		public void ImageSharp_ThumbnailAndResize_NearestNeighbor()
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
		public void ImageSharp_ThumbnailAndResize_Robidoux()
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
		public void ImageSharp_ThumbnailAndResize_RobidouxSharp()
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
		public void ImageSharp_ThumbnailAndResize_Spline()
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
		public void ImageSharp_ThumbnailAndResize_Triangle()
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
		public void ImageSharp_ThumbnailAndResize_Welch()
		{
			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new ImageSharpResizeProcessor(KnownResamplers.Welch));
				thumb.Write(output);
			}

			File.Delete(output);
		}

		[Benchmark]
		public void MagicScaler_ThumbnailAndResize_Wic()
		{
			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new MagicScalerProcessor(200, 200));
				thumb.Write(output);
			}

			File.Delete(output);
		}

		[Benchmark]
		public void ImageMagick_ThumbnailAndResize()
		{

			using (var image = new RawImage(filePath))
			using (var thumb = image.UnpackThumbnail())
			{
				thumb.Process(new ImageMagickResizerProcessor());
				thumb.Write(output);
			}

			File.Delete(output);
		}

		class ImageMagickResizerProcessor : ThumbnailProcessor
		{
			byte[] buffer;
			public override void Process(RawImageData data)
			{
				var image = new MagickImage(data.Buffer);
				image.Resize(200, 200);

				var stream = new MemoryStream();
				image.Write(stream);
				buffer = stream.ToArray();
			}

			public override void Write(RawImageData data, string file)
			{
				File.WriteAllBytes(file, buffer);
			}
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

		class MagicScalerProcessor : ThumbnailProcessor
		{
			byte[] buffer;
			readonly int width, height;
			public MagicScalerProcessor(int width, int height)
			{
				this.width = width;
				this.height = height;
			}

			public override void Process(RawImageData data)
			{
				using (var stream = new MemoryStream())
				{
					var settings = new ProcessImageSettings
					{
						Width = this.width
					};

					MagicImageProcessor.ProcessImage(data.Buffer, stream, settings);
					buffer = stream.ToArray();
				}
			}

			public override void Write(RawImageData data, string file)
			{
				File.WriteAllBytes(file, buffer);
			}
		}

		// I was thinking of testing out a raw resize algorithm
		//class ResizeProcessor : ThumbnailProcessor
		//{
		//	byte[] buffer;
		//	readonly int width, height;
		//	public ResizeProcessor(int width, int height)
		//	{
		//		this.width = width;
		//		this.height = height;
		//	}

		//	public override void Process(RawImageData data)
		//	{
		//		var info = new ImageMagick.MagickImageInfo(data.Buffer);

		//		var start = DateTime.Now;
		//		var i = Image.Load<Rgb24>(data.Buffer);

		//		var end = DateTime.Now;
		//		var elapsed = end.Subtract(start).TotalMilliseconds;

		//		int dataSize = (width * height);// * data.Bits * data.Colors;
		//		buffer = new byte[dataSize];
		//		(double X, double Y) ratio = (0, 0);
		//		ratio.X = info.Width / (double)width;
		//		ratio.Y = info.Height / (double)height;

		//		for (int y = 0; y < height; y++)
		//		{
		//			for (int x = 0; x < width; x++)
		//			{
		//				var pixelX = Math.Floor((double)x * ratio.X);
		//				var pixelY = Math.Floor((double)y * ratio.Y);

		//				int position = (y * width) + x;
		//				int sourcePosition = (int)((pixelY * info.Width) + pixelX);
		//				buffer[position] = data.Buffer[sourcePosition];
		//			}
		//		}
		//	}
		//}
	}
}
