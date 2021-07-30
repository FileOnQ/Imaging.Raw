using System;
using System.IO;
using System.Reflection;
using FileOnQ.Imaging.Raw.Tests.Utilities;
using NUnit.Framework;

namespace FileOnQ.Imaging.Raw.Tests.Integration
{
	[TestFixture("Images\\@signatureeditsco(1).dng", "1C3315E8F93F01362330B18918EC11CED0A0D85E99710BFCDA731F8A58227765")]
	[TestFixture("Images\\@signatureeditsco.dng", "6B6D39A3775CE9112512D557B665CCB62751C11F996CAF59D5B2C45B81E043AF")]
	[TestFixture("Images\\canon_eos_r_01.cr3", "4BD7A32B3ECF50D050F0C4F22775B4931DD78E3505C71D05B243390842A40A3F")]
	[TestFixture("Images\\Christian - .unique.depth.dng", "9DC560B6A13ABBEB323F8B1665C9ED8FB110E22960539A0F1FB4110B9AA233FC")]
	[TestFixture("Images\\DSC_0118.nef", "7BD56CEBC790BE820CD08E7C438B2CDFD45FA107946A77D1091E39654544A441")]
	[TestFixture("Images\\DSC02783.ARW", "9CF73891BCBF15C1FFA58C5B6E71505192E827EE20761C757D0636682AD3F271")]
	[TestFixture("Images\\PANA2417.RW2", "B20D50F3D3CBA0B81A280609C3E0964141BB62141E929B83D1C3D3E82D542536")]
	[TestFixture("Images\\PANA8392.RW2", "8FD464FC211536CB93FEE00550BD4C489F6FD53164ABD0213C8AFF05FE97944A")]
	[TestFixture("Images\\photo by @Dupe.png--@Emily.rosegold.arw", "BB6517A9B23A65786DB249E8F53663CF71BFD72E83B0465A12F9110CBE9C3314")]
	[TestFixture("Images\\sample1.cr2", "12C0B83A67F83474C2AF55DE63F8B9834C6F3FB2CF8E7780B18B2C6A1222D795")]
	[TestFixture("Images\\signature edits APC_00171.dng", "912415C1909E79D434B31264BB3BFE4ECAAD47F0D7B7C926176C9DA5D9380104")]
	[TestFixture("Images\\signature edits free raws P1015526.dng", "EC87D117430BB22DC95565423BF5BFF50116E486A9DF9F4B41771E9C4FC65A9E")]
	[TestFixture("Images\\signature edits free raws_DSC7082.NEF", "49CDACDD6B40DEDB9D0EEA12DB22B5D1895F9594D0C96A33927B6EE7611FD7E3")]
	[TestFixture("Images\\signatureeditsfreerawphoto.NEF", "43D9C922A1C8BE0BAF8EC7351C1854351A6DEF3CA96C7F3B31A9988E0A083F6B")]
	[Category(Constants.Category.Integration)]
	public class Thumbnail_ImageBuffer_Tests
	{
		readonly string input;
		readonly string hash;

		public Thumbnail_ImageBuffer_Tests(string path, string hash)
		{
			this.hash = hash;

			var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
			input = Path.Combine(assemblyDirectory, path);
		}

		[Test]
		public void ThumbnailImageBuffer_Test()
		{
			using (var rawImage = new RawImage(input))
			using (var thumbnail = rawImage.UnpackThumbnail())
			{
				var image = thumbnail.AsProcessedImage();

				AssertUtilities.IsHashEqual(hash, image.Buffer.ToArray());
			}
		}
	}
}