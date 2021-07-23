using System.IO;
using NUnit.Framework;

namespace FileOnQ.Imaging.Raw.Tests
{
	[TestFixture("images/sample1.cr2")]
	public class ThumbnailWriteTests
	{
		readonly string file;
		
		public ThumbnailWriteTests(string path)
		{
		}
		
		[Test]
		public void Happy()
		{
			Assert.Pass();
		}
	}
}