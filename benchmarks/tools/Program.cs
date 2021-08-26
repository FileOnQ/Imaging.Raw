using System;
using System.IO;
using System.Reflection;
using BenchmarkDotNet.Running;

namespace FileOnQ.Imaging.Raw.Benchmarking
{
	class Program
	{
		public static string Input => "Images/sample1.cr2";
		static void Main(string[] args)
		{
			Console.WriteLine("FileOnQ Imaging Raw Benchmark tool");

			if (args.Length < 2 || args[0] != "-b")
			{
				Console.WriteLine("Available commands:");
				Console.WriteLine("\t-b benchmark to run (dcraw, thumbnail, etc.)");
				Console.WriteLine("\r\nExample: dotnet run -b thumbnail");
				return;
			}

			var benchmark = args[1].ToLower();
			switch (benchmark)
			{
				case "dcraw":
					Console.WriteLine("Starting DcrawProcess benchmarks . . .");
					DcrawProcess();
					break;
				case "dcraw-gpu":
					Console.WriteLine("Starting DcrawProcessGpu benchmarks . . .");
					DcrawProcessGpu();
					break;
				case "dcraw-whitebalance":
					Console.WriteLine("Starting DcrawProcessWhiteBalance benchmarks . . .");
					DcrawProcessWhiteBalance();
					break;
				case "dcraw-whitebalance-Gpu":
					Console.WriteLine("Starting DcrawProcessWhiteBalanceGpu benchmarks . . .");
					DcrawProcessWhiteBalanceGpu();
					break;
				case "thumbnail":
					Console.WriteLine("Starting thumbnail benchmarks . . .");
					Thumbnail();
					break;
				default:
					Console.WriteLine($"Benchmark {benchmark} is not available");
					break;
			}

			Console.WriteLine("Benchmark completed");
		}

		static void AsBitmapRunner()
		{
			Console.WriteLine("AsBitmap benchmark starting");
			Console.WriteLine("Generating raw ppm file to use in benchmark, this operation may take sometime . . .");

			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			var filePath = Path.Combine(assemblyDirectory, "generated-data-file");

			using (var image = new RawImage(Input))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());
				var processedImage = raw.AsProcessedImage();

				File.WriteAllBytes(filePath, processedImage.Buffer);
			}

			Console.WriteLine("Input data generated, starting benchmark!");
			BenchmarkRunner.Run<AsBitmap>();
		}

		static void DcrawProcess() => BenchmarkRunner.Run<DcrawProcess>();
		static void DcrawProcessGpu() => BenchmarkRunner.Run<DcrawProcessGpu>();
		static void DcrawProcessWhiteBalance() => BenchmarkRunner.Run<DcrawProcessWhiteBalance>();
		static void DcrawProcessWhiteBalanceGpu() => BenchmarkRunner.Run<DcrawProcessWhiteBalanceGpu>();
		static void Thumbnail() => BenchmarkRunner.Run<Thumbnail>();
	}
}