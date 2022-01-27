using System.Drawing;
using System.IO;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Jobs;

namespace FileOnQ.Imaging.Raw.Benchmarking
{
#if NET48
	[SimpleJob(RuntimeMoniker.Net48, launchCount: 1, invocationCount: 1)]
#elif NET5_0
	[SimpleJob(RuntimeMoniker.Net50, launchCount: 1, invocationCount: 1)]
#elif NET6_0
	[SimpleJob(RuntimeMoniker.Net60, launchCount: 1, invocationCount: 1)]
#endif
	[NativeMemoryProfiler]
	[MemoryDiagnoser]
	[JsonExporterAttribute.Full]
	public class DcrawProcessWhiteBalance
	{
		readonly string filePath;
		readonly string output;
		public DcrawProcessWhiteBalance()
		{
			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			filePath = Path.Combine(assemblyDirectory, Program.Input);
			if (!File.Exists(filePath))
				throw new FileNotFoundException(filePath);

			output = Path.Combine(assemblyDirectory, "output.ppm");
		}

		[Benchmark]
		public void ProccessAndWrite()
		{
			using (var image = new RawImage(filePath))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new WhiteBalanceProcessor());
				raw.Write(output);
			}

			File.Delete(output);
		}

		[Benchmark]
		public byte[] ProccessIntoMemory()
		{
			using (var image = new RawImage(filePath))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new WhiteBalanceProcessor());
				using (var processedImage = raw.AsProcessedImage())
					return processedImage.Buffer;
			}
		}

		[Benchmark]
		public Size ProcessIntoBitmap()
		{
			using (var image = new RawImage(filePath))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new WhiteBalanceProcessor());
				using (var processedImage = raw.AsProcessedImage())
				using (var bitmap = processedImage.AsBitmap())
				{
					return bitmap.Size;
				}
			}
		}

		class WhiteBalanceProcessor : DcrawProcessor
		{
			protected override void SetOutputParameters(DcrawOutputParameters parameters)
			{
				parameters.UseCameraWhiteBalance = 1;
			}
		}
	}
}
