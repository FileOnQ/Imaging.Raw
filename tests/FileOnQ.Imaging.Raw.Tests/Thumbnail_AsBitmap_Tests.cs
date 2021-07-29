using System.IO;
using System.Reflection;
using FileOnQ.Imaging.Raw.Tests.Utilities;
using NUnit.Framework;

namespace FileOnQ.Imaging.Raw.Tests
{
	[TestFixture("Images\\@signatureeditsco(1).dng", "2EF59B98A5ED52162A70D7B6A85DA2DCF6E2E51CB946DE9011032A52D91FB2E4")]
	[TestFixture("Images\\@signatureeditsco.dng", "5E103695025C40ED0F78F47F29EBF5333D5B0529FDA955EE21DAECA6E6F3579F")]
	[TestFixture("Images\\canon_eos_r_01.cr3", "653786673184C67C128EFC89B67A6BB7FB19551583EB4176BC278A20489784B4")]
	[TestFixture("Images\\Christian - .unique.depth.dng", ImageFormat.Bitmap, "66EC0E0ABC132E005B6A163A898999D084AAE7E69DF670D40FA1E326C4406788")]
	[TestFixture("Images\\DSC_0118.nef", "3D41F9C3B240484BD380DA01428C0F293D0FC958C3D923B70DB1DF433F33DCD3")]
	[TestFixture("Images\\DSC02783.ARW", "D069B8AA36AC684C1B0F4EF6CF5C8C1EF562C1083C00D59E87D6B5CABDD45F26")]
	[TestFixture("Images\\PANA2417.RW2", "04796EB9BB9613D6F7701527D70D816F97E678615E90A75C91D18E9EB68250BB")]
	[TestFixture("Images\\PANA8392.RW2", "38792A6EDCE16E0CD9098AC8B3249B3E7BBD069AC78A65B080C87078829C0580")]
	[TestFixture("Images\\photo by @Dupe.png--@Emily.rosegold.arw", "D24EAF12AFE8D8DDD309428BEB43116F9A3C1353804BCE47EB0E130B3C6E2E65")]
	[TestFixture("Images\\sample1.cr2", "0674ECFC552DC9E4365D496FD10508333EAF71269F5451FED42AF5E2678B1A30")]
	[TestFixture("Images\\signature edits APC_00171.dng", "6FEE1556F783D971F4AB6A5145116E92585D68C60B6F019140DACA7538E83FC2")]
	[TestFixture("Images\\signature edits free raws P1015526.dng", "DF7C22D13CBABA9EB6DBC07A2723A139A9690EBC0894AFD5B272C2CD876C60D2")]
	[TestFixture("Images\\signature edits free raws_DSC7082.NEF", "FD8C804D24CF864895AC5C2A0B4A27D932FE067661CD8DD9E7D725FBE7D018F2")]
	[TestFixture("Images\\signatureeditsfreerawphoto.NEF", "A69D1A858C2C058BEA73C04099989F7F7AAAEF8BE355BA334F464E625DF7553A")]
	public class Thumbnail_AsBitmap_Tests
	{
		readonly string input;
		readonly string output;
		readonly string hash;

		public Thumbnail_AsBitmap_Tests(string path, string expectedHash) : this(path, ImageFormat.Jpeg, expectedHash) { }
		public Thumbnail_AsBitmap_Tests(string path, ImageFormat imageFormat, string expectedHash)
		{
			hash = expectedHash;

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
		public void ThumbnailAsBitmap_Test()
		{
			using (var image = new RawImage(input))
			using (var thumbnail = image.UnpackThumbnail())
			using (var bitmap = thumbnail.AsBitmap())
			{
				bitmap.Save(output);
			}

			AssertUtilities.IsHashEqual(hash, File.ReadAllBytes(output));
		}

		[Test]
		public void ThumbnailAsBitmap_Gpu_Test()
		{
			using (var image = new RawImage(input))
			using (var thumbnail = image.UnpackThumbnail())
			using (var bitmap = thumbnail.AsBitmap(1))
			{
				bitmap.Save(output);
			}

			AssertUtilities.IsHashEqual(hash, File.ReadAllBytes(output));
		}

		[Test]
		public void ThumbnailAsBitmap_IntPtr_Test()
		{
			using (var image = new RawImage(input))
			using (var thumbnail = image.UnpackThumbnail())
			using (var bitmap = thumbnail.AsBitmap(2))
			{
				bitmap.Save(output);
			}

			AssertUtilities.IsHashEqual(hash, File.ReadAllBytes(output));
		}
	}
}
