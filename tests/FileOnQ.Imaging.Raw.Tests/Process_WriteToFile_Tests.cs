using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace FileOnQ.Imaging.Raw.Tests
{
	[TestFixture("Images\\sample1.cr2")]
	[TestFixture("Images\\@signatureeditsco(1).dng")]
	[TestFixture("Images\\@signatureeditsco.dng")]
	[TestFixture("Images\\canon_eos_r_01.cr3")]
	[TestFixture("Images\\Christian - .unique.depth.dng")]
	[TestFixture("Images\\DSC_0118.nef")]
	[TestFixture("Images\\DSC02783.ARW")]
	[TestFixture("Images\\PANA2417.RW2")]
	[TestFixture("Images\\PANA8392.RW2")]
	[TestFixture("Images\\photo by @Dupe.png--@Emily.rosegold.arw")]
	[TestFixture("Images\\signature edits APC_00171.dng")]
	[TestFixture("Images\\signature edits free raws P1015526.dng")]
	[TestFixture("Images\\signature edits free raws_DSC7082.NEF")]
	[TestFixture("Images\\signatureeditsfreerawphoto.NEF")]
	public class Process_WriteToFile_Tests
	{
		readonly string input;
		readonly string output;
		readonly string expectedThumbnail;

		public Process_WriteToFile_Tests(string path)
		{
			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			input = Path.Combine(assemblyDirectory, path);
			
			var filename = Path.GetFileNameWithoutExtension(input);
			var directory = Path.GetDirectoryName(input) ?? string.Empty;
			var format = "ppm";

			output = $"{filename}.processed.{format}";
			expectedThumbnail = Path.Combine(directory, $"{filename}.processed.{format}");
		}

		[OneTimeSetUp]
		public void Execute()
		{
			using (var image = new RawImage(input))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());
				raw.Write(output);
			}
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			if (File.Exists(output))
				File.Delete(output);
		}
		
		[Test]
		public void ProcessWrite_FileExists_Test() =>
			Assert.IsTrue(File.Exists(output));

		[Test]
		public void ProcessWrite_MatchPpm_Test()
		{
			var expectedBuffer = new Span<byte>(File.ReadAllBytes(expectedThumbnail));
			var actualBuffer = new Span<byte>(File.ReadAllBytes(output));

			Assert.IsTrue(actualBuffer.Length > 0);
			Assert.AreEqual(expectedBuffer.Length, actualBuffer.Length);

			bool isValid = true;
			for (int index = 0; index < expectedBuffer.Length; index++)
			{
				if (expectedBuffer[index] != actualBuffer[index])
				{
					isValid = false;
					break;
				}
			}

			Assert.IsTrue(isValid, "Expected buffer does not match actual buffer, files are different");
		}
	}
}