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
	public class Raw_Process_ImageBuffer_Tests
	{
		readonly string input;
		readonly string hash;

		public Raw_Process_ImageBuffer_Tests(string path)
		{
			this.hash = TestData.Integration.ProcessAsImageBuffer.HashCodes[path];

			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			input = Path.Combine(assemblyDirectory, path);
		}

		[Test]
		public void ProcessImageBuffer_Test()
		{
			using (var image = new RawImage(input))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());
				var processedImage = raw.AsProcessedImage();

				AssertUtilities.IsHashEqual(hash, processedImage.Buffer);
			}
		}
	}
}