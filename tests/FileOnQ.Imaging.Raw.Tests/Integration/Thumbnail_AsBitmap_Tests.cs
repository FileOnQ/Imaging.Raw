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
	public class Thumbnail_AsBitmap_Tests
	{
		readonly string input;
		readonly string output;
		readonly string hash;

		public Thumbnail_AsBitmap_Tests(string path)
		{
			hash = TestData.Integration.ThumbnailAsBitmap.HashCodes[path];

			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			input = Path.Combine(assemblyDirectory, path);

			var filename = Path.GetFileNameWithoutExtension(input);
			var directory = Path.GetDirectoryName(input) ?? string.Empty;
			output = Path.Combine(directory, $"{filename}.thumb.bmp");
		}

		[TearDown]
		public void TearDown()
		{
			if (File.Exists(output))
				File.Delete(output);
		}

		[Test]
		public void ThumbnailAsBitmap_Cpu_Test()
		{
			using (var image = new RawImage(input))
			using (var thumbnail = image.UnpackThumbnail())
			using (var processedImage = thumbnail.AsProcessedImage())
			using (var bitmap = processedImage.AsBitmap())
			{
				bitmap.Save(output, System.Drawing.Imaging.ImageFormat.Bmp);
			}

			AssertUtilities.IsHashEqual(hash, File.ReadAllBytes(output));
		}

		//[Test]
		public void ThumbnailAsBitmap_Gpu_Test()
		{
			using (var image = new RawImage(input))
			using (var thumbnail = image.UnpackThumbnail())
			using (var processedImage = thumbnail.AsProcessedImage())
			using (var bitmap = processedImage.AsBitmap())
			{
				bitmap.Save(output, System.Drawing.Imaging.ImageFormat.Bmp);
			}

			AssertUtilities.IsHashEqual(hash, File.ReadAllBytes(output));
		}
	}
}
