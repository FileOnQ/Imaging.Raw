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
				var benchmarkKeys = new System.Collections.Generic.List<string>(BenchmarkOptions.Keys);
				Console.WriteLine("Available commands:");
				Console.WriteLine($"\t-b benchmark to run ({string.Join(", ", benchmarkKeys)})");
				Console.WriteLine("\r\nExample: dotnet run -c release -b thumbnail");
				return;
			}

			BenchmarkOptions.TryGetValue(args[1].ToLower(), out var benchmark);

			switch (benchmark)
			{
				case BenchmarkOption.all:
					Console.WriteLine("Starting DcrawProcess benchmarks . . .");
					DcrawProcess();
					Console.WriteLine();
					Console.WriteLine("Starting thumbnail benchmarks . . .");
					Thumbnail();
					break;
				case BenchmarkOption.dcraw:
					Console.WriteLine("Starting DcrawProcess benchmarks . . .");
					DcrawProcess();
					break;
				case BenchmarkOption.dcraw_gpu:
					Console.WriteLine("Starting DcrawProcessGpu benchmarks . . .");
					DcrawProcessGpu();
					break;
				case BenchmarkOption.dcraw_whitebalance:
					Console.WriteLine("Starting DcrawProcessWhiteBalance benchmarks . . .");
					DcrawProcessWhiteBalance();
					break;
				case BenchmarkOption.dcraw_whitebalance_gpu:
					Console.WriteLine("Starting DcrawProcessWhiteBalanceGpu benchmarks . . .");
					DcrawProcessWhiteBalanceGpu();
					break;
				case BenchmarkOption.thumbnail:
					Console.WriteLine("Starting thumbnail benchmarks . . .");
					Thumbnail();
					break;
				default:
					Console.WriteLine($"Benchmark {args[1].ToLower()} is not available");
					break;
			}

			Console.WriteLine("Benchmark completed");
		}

		private static System.Collections.Generic.Dictionary<string, BenchmarkOption> BenchmarkOptions = new System.Collections.Generic.Dictionary<string, BenchmarkOption>
		{
			{ "all", BenchmarkOption.dcraw },
			{ "dcraw", BenchmarkOption.dcraw },
			{ "dcraw-gpu", BenchmarkOption.dcraw_gpu },
			{ "dcraw-whitebalance", BenchmarkOption.dcraw_whitebalance },
			{ "dcraw-whitebalance-gpu", BenchmarkOption.dcraw_whitebalance_gpu },
			{ "thumbnail", BenchmarkOption.thumbnail }
		};

		internal enum BenchmarkOption
		{
			none_selected = 0,
			all = 1,
			dcraw = 2,
			dcraw_gpu = 3,
			dcraw_whitebalance = 4,
			dcraw_whitebalance_gpu = 5,
			thumbnail = 6
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

		static void DcrawProcess()
		{
			BenchmarkRunner.Run<DcrawProcess>();
		}
		static void DcrawProcessGpu()
		{
			BenchmarkRunner.Run<DcrawProcessGpu>();
		}
		static void DcrawProcessWhiteBalance()
		{
			BenchmarkRunner.Run<DcrawProcessWhiteBalance>();
		}
		static void DcrawProcessWhiteBalanceGpu()
		{
			BenchmarkRunner.Run<DcrawProcessWhiteBalanceGpu>();
		}
		static void Thumbnail()
		{
			BenchmarkRunner.Run<Thumbnail>();
		}
	}
}