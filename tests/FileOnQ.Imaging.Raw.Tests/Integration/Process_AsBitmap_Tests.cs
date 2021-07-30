using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using FileOnQ.Imaging.Raw.Tests.Utilities;
using NUnit.Framework;

namespace FileOnQ.Imaging.Raw.Tests.Integration
{
	[TestFixture("Images\\@signatureeditsco(1).dng", "77DACCE4DB91618AC1AE87D35ED6BBC39BF33B083FCAADAD9CA3ADD2CB6151C3")]
	[TestFixture("Images\\@signatureeditsco.dng", "FCDB75BED709DE181502EDC757A3D32C10CFB638127BFF9E4697DE2E13BB5F86")]
	[TestFixture("Images\\canon_eos_r_01.cr3", "9148CCB3B49DEF1D421ADF3AC4447305C2F1784A7553B15C1EA19C5F9FD163D1")]
	[TestFixture("Images\\Christian - .unique.depth.dng", "150BAC2A9A75F217535A271176789413E13FA5FA51541AA567A39A829974EAA1")]
	[TestFixture("Images\\DSC_0118.nef", "51D48929E0BE2F4A9BC74418E4BF975C8B18A9AD13F4D545FE4783AD3CB5E6AF")]
	[TestFixture("Images\\DSC02783.ARW", "BD406DC1024395127DF2D43646F6687807AA001E8E8B0FD06AC8018CB0EDC048")]
	[TestFixture("Images\\PANA2417.RW2", "742383A48760F4D6AD4103C1A8F275DAF4CF0F7E0466BD2598F676BF1E369501")]
	[TestFixture("Images\\PANA8392.RW2", "9BA5FF3DF150C88F346680DE1ACC7E9C137519D69E9B4828DD310F409296FC11")]
	[TestFixture("Images\\photo by @Dupe.png--@Emily.rosegold.arw", "E09260F40565C8E81F8090C7257F925B30836D6A987F50472033844A22D0CD39")]
	[TestFixture("Images\\sample1.cr2", "F72505C4440A805A0D557559DB6B5D4A5A1A77C228D037BA67881E0BF4D03C94")]
	[TestFixture("Images\\signature edits APC_00171.dng", "4645977F6C06513C382AE6F55C4FB7F499129E7D01B6D1E5FFC8D397EE34E44D")]
	[TestFixture("Images\\signature edits free raws P1015526.dng", "5348356CBC3206FF9C0395F3BF657637B5AEE90403CA528E00A3556153595D5C")]
	[TestFixture("Images\\signature edits free raws_DSC7082.NEF", "84624D0A0F6F7CDBFF2E31399243244223A90AA905981248C0215CBFAE2EA80B")]
	[TestFixture("Images\\signatureeditsfreerawphoto.NEF", "450170A5A1DB4AF2CCA2AAB973433439B9F0A02FBA744EE6C34D0A52C33C3DED")]
	[Category(Constants.Category.Integration)]
	public class Process_AsBitmap_Tests
	{
		readonly string input;
		readonly string output;
		readonly string hash;

		public Process_AsBitmap_Tests(string path, string expectedHash)
		{
			hash = expectedHash;

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
					var data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
					actual = new Span<byte>((void*)data.Scan0, bitmap.Height * data.Stride);
					bitmap.UnlockBits(data);
				}
			}

			AssertUtilities.IsHashEqual(hash, actual.ToArray());
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
				using (var bitmap = raw.AsBitmap())
				{
					var data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
					actual = new Span<byte>((void*)data.Scan0, bitmap.Height * data.Stride);
					bitmap.UnlockBits(data);
				}
			}
			
			AssertUtilities.IsHashEqual(hash, actual.ToArray());
		}
	}
}
