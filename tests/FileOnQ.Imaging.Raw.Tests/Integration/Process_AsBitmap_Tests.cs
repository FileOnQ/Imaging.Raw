using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using FileOnQ.Imaging.Raw.Tests.Utilities;
using NUnit.Framework;

namespace FileOnQ.Imaging.Raw.Tests.Integration
{
	[TestFixture(TestData.RawImage1)]
	[TestFixture(TestData.RawImage2)]
	[TestFixture(TestData.RawImage3)]
	[TestFixture(TestData.RawImage4)]
	[TestFixture(TestData.RawImage5)]
	[TestFixture(TestData.RawImage6)]
	[TestFixture(TestData.RawImage7)]
	[TestFixture(TestData.RawImage8)]
	[TestFixture(TestData.RawImage9)]
	[TestFixture(TestData.RawImage10)]
	[TestFixture(TestData.RawImage11)]
	[TestFixture(TestData.RawImage12)]
	[TestFixture(TestData.RawImage13)]
	[TestFixture(TestData.RawImage14)]
	[Category(Constants.Category.Integration)]
	public class Process_AsBitmap_Tests
	{
		readonly string input;
		readonly string output;
		readonly string hash;

		public Process_AsBitmap_Tests(string path)
		{
			hash = TestData.Integration.ProccessAsBitmap.HashCodes[path];

			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			input = Path.Combine(assemblyDirectory, path);

			var filename = Path.GetFileNameWithoutExtension(input);
			var directory = Path.GetDirectoryName(input) ?? string.Empty;
			output = Path.Combine(assemblyDirectory, $"{filename}.process.bmp");
		}

		[TearDown]
		public void TearDown()
		{
			if (File.Exists(output))
				File.Delete(output);
		}

		[Test]
		public unsafe void ProcessAsBitmap_Cpu_Test()
		{
			Span<byte> actual = default;

			using (var image = new RawImage(input))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());
				using (var bitmap = raw.AsBitmap())
				{
					bitmap.Save(output);
					//var data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
					//actual = new Span<byte>((void*)data.Scan0, bitmap.Height * data.Stride);
					//bitmap.UnlockBits(data);
				}
			}

			AssertUtilities.IsHashEqual(hash, File.ReadAllBytes(output));
		}

		[Test]
		public unsafe void ProcessAsBitmap_Gpu_Test()
		{
			Span<byte> actual = default;

			using (var image = new RawImage(input))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());

				var processedImage = raw.AsProcessedImage();
				using (var bitmap = raw.AsBitmap(true))
				{
					bitmap.Save(output);
					//var data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
					//actual = new Span<byte>((void*)data.Scan0, bitmap.Height * data.Stride);
					//bitmap.UnlockBits(data);
				}
			}
			
			AssertUtilities.IsHashEqual(hash, File.ReadAllBytes(output));
		}
	}
}
