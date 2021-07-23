using System.IO;
using NUnit.Framework;

namespace FileOnQ.Imaging.Raw.Tests
{
	[TestFixture("images/sample1.cr2")]
	public class ThumbnailWriteTests
	{
		readonly string input;
		readonly string output;
		
		public ThumbnailWriteTests(string path)
		{
			input = path;
			
			var filename = Path.GetFileNameWithoutExtension(input);
			output = $"{filename}.thumb.jpeg";
		}
		
		[Test]
		public void ThumbnailWriteTest()
		{
			using (var image = new RawImage(input))
			{
				var thumbnail = image.UnpackThumbnail();
				thumbnail.Write(output);
			}

			Assert.IsTrue(File.Exists(output));
		}
	}
}