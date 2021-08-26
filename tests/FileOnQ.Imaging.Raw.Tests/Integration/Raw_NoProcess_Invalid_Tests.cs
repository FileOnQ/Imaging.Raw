using System;
using System.IO;
using System.Reflection;
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
	[TestFixture(TestData.RawImage15)]
	[Category(Constants.Category.Integration)]
	public class Raw_NoProcess_Invalid_Tests
	{
		readonly string input;

		public Raw_NoProcess_Invalid_Tests(string path)
		{
			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			input = Path.Combine(assemblyDirectory, path);
		}

		[Test]
		public void Raw_NoProcess_AsProcessedImage_Test()
		{
			var exception = Assert.Throws<NullReferenceException>(() =>
			{
				using (var image = new RawImage(input))
				using (var raw = image.UnpackRaw())
				{
					var processedImage = raw.AsProcessedImage();
					processedImage.Dispose();
				}
			});

			Assert.AreEqual("Image has not been processed, Call Process(IImageProcessor) first!", exception?.Message);
		}

		[Test]
		public void Raw_NoProcess_Write_Test()
		{
			var exception = Assert.Throws<NullReferenceException>(() =>
			{
				using (var image = new RawImage(input))
				using (var raw = image.UnpackRaw())
				{
					raw.Write("SomePath");
				}
			});

			Assert.AreEqual("Image has not been processed, Call Process(IImageProcessor) first!", exception?.Message);
		}

		[Test]
		public void Raw_NullProcess_Write_Test()
		{
			var exception = Assert.Throws<NullReferenceException>(() =>
			{
				using (var image = new RawImage(input))
				using (var raw = image.UnpackRaw())
				{
					raw.Process(null);
				}
			});

			Assert.AreEqual("Image has not been processed, Call Process(IImageProcessor) first!", exception?.Message);
		}
	}
}
