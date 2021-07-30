using System.IO;
using System.Reflection;
using FileOnQ.Imaging.Raw.Tests.Utilities;
using NUnit.Framework;

namespace FileOnQ.Imaging.Raw.Tests.Integration
{
	[TestFixture("Images\\@signatureeditsco(1).dng", "D67DF1FA67F77F4D19890E034F90CBC6B7094F6D48984B6F84C21EAC91A165B8")]
	[TestFixture("Images\\@signatureeditsco.dng", "D7048D84D81F933E056324C6F20F5106AB9E5795942A0258E3438E7642593D18")]
	[TestFixture("Images\\canon_eos_r_01.cr3", "814F56A3F06B90A020C862FEB4BF1920B1CC2FD0EBD8D60C9B5D9F22BC72139D")]
	[TestFixture("Images\\Christian - .unique.depth.dng", "43EAB31E37B1CA727DB6E9401297D3922E26D651859A531CEE2ECF88130AC5A5")]
	[TestFixture("Images\\DSC_0118.nef", "2B21A3B2838A28AF778B1A162A1F2DAFD9CFDDF3841A9B4C7AA8275124B2AA29")]
	[TestFixture("Images\\DSC02783.ARW", "C03AF487348444C35837EA09579B5A70B9EBCE32DEEBF27BC71F09E6646DC7D9")]
	[TestFixture("Images\\PANA2417.RW2", "044C21783EAE7AFE352A0BE9B5BEC175B30EC464FA7681E829A47E9357DE9780")]
	[TestFixture("Images\\PANA8392.RW2", "C638E6EB8883BA2A5C18C49CC1ECB6787593E825629AA53FE5B02F974060EC23")]
	[TestFixture("Images\\photo by @Dupe.png--@Emily.rosegold.arw", "4C277522C7007C1D47D9CBF62EFDCD459516C9AC6B81215FC6EF4FE20CEE18D5")]
	[TestFixture("Images\\sample1.cr2", "9FDA185233232345DB2D29639D451FC0C696114ED60AD36D4A9DF527BF99B92C")]
	[TestFixture("Images\\signature edits APC_00171.dng", "12ACC818F1531970BF8657E0938A6271D61600C166F3D0DE24D6DD49552D3AB7")]
	[TestFixture("Images\\signature edits free raws P1015526.dng", "48628FEEE5C48DC277C257CD360029DBA496CFE99EC7EC519D8E04EAF04F380E")]
	[TestFixture("Images\\signature edits free raws_DSC7082.NEF", "86E90F71A7537056DCC4FA17CEDE63373927E2ED53F01255B6686D876B270190")]
	[TestFixture("Images\\signatureeditsfreerawphoto.NEF", "7EDCD3646582F60ACB9620129EC9E1D18985126C714D0BE52E44739C1F7F0FFB")]
	[Category(Constants.Category.Integration)]
	public class Process_ImageBuffer_Tests
	{
		readonly string input;
		readonly string hash;

		public Process_ImageBuffer_Tests(string path, string hash)
		{
			this.hash = hash;

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

				AssertUtilities.IsHashEqual(hash, processedImage.Buffer.ToArray());
			}
		}
	}
}