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
	[TestFixture(TestData.RawImage5, ImageType.Bitmap)]
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
	public class Thumbnail_WriteToFile_Tests
	{
		readonly string input;
		readonly string output;
		readonly string hash;

		public Thumbnail_WriteToFile_Tests(string path) : this(path, ImageType.Jpeg) { }
		public Thumbnail_WriteToFile_Tests(string path, ImageType imageFormat)
		{
			hash = TestData.Integration.ThumbnailWriteToFile.HashCodes[path];

			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			input = Path.Combine(assemblyDirectory, path);
			
			var filename = Path.GetFileNameWithoutExtension(input);
			var directory = Path.GetDirectoryName(input) ?? string.Empty;
			var format = imageFormat == ImageType.Jpeg ? "jpeg" : "ppm";

			output = Path.Combine(directory, $"{filename}.thumb.{format}");
		}

		[OneTimeSetUp]
		public void Execute()
		{
			using (var image = new RawImage(input))
			using (var thumbnail = image.UnpackThumbnail())
			{
				thumbnail.Process(new ThumbnailProcessor());
				thumbnail.Write(output);
			}
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			if (File.Exists(output))
				File.Delete(output);
		}
		
		[Test]
		public void ThumbnailWrite_FileExists_Test() =>
			Assert.IsTrue(File.Exists(output));

		[Test]
		public void ThumbnailWrite_MatchBytes_Test()
		{
			var actualBuffer = File.ReadAllBytes(output);

			Assert.IsTrue(actualBuffer.Length > 0);
			AssertUtilities.IsHashEqual(hash, actualBuffer);
		}
	}
}