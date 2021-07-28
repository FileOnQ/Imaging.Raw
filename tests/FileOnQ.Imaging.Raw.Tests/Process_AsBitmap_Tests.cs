using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace FileOnQ.Imaging.Raw.Tests
{
	[TestFixture("Images\\sample1.cr2")]
	[TestFixture("Images\\@signatureeditsco(1).dng")]
	[TestFixture("Images\\@signatureeditsco.dng")]
	[TestFixture("Images\\canon_eos_r_01.cr3")] // fails on the GPU
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
	public class Process_AsBitmap_Tests
	{
		readonly string input;
		readonly string output;
		readonly string expectedOutput;

		public Process_AsBitmap_Tests(string path)
		{
			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			input = Path.Combine(assemblyDirectory, path);

			var filename = Path.GetFileNameWithoutExtension(input);
			var directory = Path.GetDirectoryName(input) ?? string.Empty;
			expectedOutput = Path.Combine(directory, $"{filename}.process.bmp");
			output = Path.Combine(assemblyDirectory, $"{filename}.process.bmp");
		}

		[TearDown]
		public void TearDown()
		{
			if (File.Exists(output))
				File.Delete(output);
		}

		[Test]
		public void ProcessAsBitmap_Test()
		{
			using (var image = new RawImage(input))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());

				var bitmap = raw.AsBitmap();
				bitmap.Save(output);
				bitmap.Dispose();
			}

			var expectedBuffer = new Span<byte>(File.ReadAllBytes(expectedOutput));
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

		// NOTE - This is failing for canon_eos cr3 file
		// We get an error about corrupt memory. I think pixel data isn't being built correctly
		// I think the cr3 image is invalid as we can't write blank pixels. This tells me that
		// something isn't adding up. The GDI+ save API must be trying to access something outside
		// the bounds of allocated memory (array). Maybe the width or height is wrong or the size
		[Test]
		public void ProcessAsBitmap_Gpu_Test()
		{
			using (var image = new RawImage(input))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());

				var processedImage = raw.AsProcessedImage();
				//File.WriteAllBytes("test.ppm", processedImage.Buffer.ToArray());
				var bitmap = raw.AsBitmap(1);
				bitmap.Save(output);
				bitmap.Dispose();
			}

			var expectedBuffer = new Span<byte>(File.ReadAllBytes(expectedOutput));
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

		[Test]
		public void ProcessAsBitmap_IntPtr_Test()
		{
			using (var image = new RawImage(input))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());

				var processedImage = raw.AsProcessedImage();
				var bitmap = raw.AsBitmap(2);
				bitmap.Save(output);
				bitmap.Dispose();
			}

			var expectedBuffer = new Span<byte>(File.ReadAllBytes(expectedOutput));
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
