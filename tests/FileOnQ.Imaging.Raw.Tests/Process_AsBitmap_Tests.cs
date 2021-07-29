using System.IO;
using System.Reflection;
using FileOnQ.Imaging.Raw.Tests.Utilities;
using NUnit.Framework;

namespace FileOnQ.Imaging.Raw.Tests
{
	[TestFixture("Images\\@signatureeditsco(1).dng", "BBA0AA3A611CBFA1B114037957061CC58D3484DA5005700D19F79860F0AA7DFC")]
	[TestFixture("Images\\@signatureeditsco.dng", "937B694DE013859228F4209ACC93DCED0F9206B560A4949CA53EC695E80F52B5")]
	[TestFixture("Images\\canon_eos_r_01.cr3", "B605E2A2F2C5CAE6796978039C702F3CB819B4334BD561A902D110DF35C4F936")]
	[TestFixture("Images\\Christian - .unique.depth.dng", "585DA19A374F82BEA1AB202AEA03BA0B384BBC30D75CB0422D14576221153076")]
	[TestFixture("Images\\DSC_0118.nef", "845BBAC7E73070725BD2F7E616784DA0736F37900A834CF6979142D41C043B6D")]
	[TestFixture("Images\\DSC02783.ARW", "AAB21936231BE11A9AC48FA9DE685317F3AB4D0F350D7879E72CA62DA6935D67")]
	[TestFixture("Images\\PANA2417.RW2", "BBB4582591D9689F4F586C3E361EA43FC73B598F693F67E38A79B35473A339C5")]
	[TestFixture("Images\\PANA8392.RW2", "92DAAAAB9761A789F38303D397BF6E588C0CF6F9B02B5BD22EAF2395BC5711F5")]
	[TestFixture("Images\\photo by @Dupe.png--@Emily.rosegold.arw", "4914D2574E020E819C516CD96126DFCF5B6027D245D8CB4442D014239AFE5D8B")]
	[TestFixture("Images\\sample1.cr2", "02717173DBF92480B9FEC2AC099DCFFED546C1F920B2EF388B99051C95AE198F")]
	[TestFixture("Images\\signature edits APC_00171.dng", "F04BBB33B1F2D01115309DBCC6C951D25AF998EC749B6893D37B91352B20D71E")]
	[TestFixture("Images\\signature edits free raws P1015526.dng", "E3FC226754E815C0475C279559AE23C9EBDC5C57C63F0E484090ED1E5776B305")]
	[TestFixture("Images\\signature edits free raws_DSC7082.NEF", "983E7E0FD41E2980DCFAC3A3C71C3F6BE88788DA6A6B8450EFA7578BED82F088")]
	[TestFixture("Images\\signatureeditsfreerawphoto.NEF", "ABCC78EA917D681A2E36A240B64E40AA0E8727EE02CA71D8A5AD45ABBB8F0A96")]
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
		public void ProcessAsBitmap_Cpu_Test()
		{
			using (var image = new RawImage(input))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());
				using (var bitmap = raw.AsBitmap())
					bitmap.Save(output);
			}

			AssertUtilities.IsHashEqual(hash, File.ReadAllBytes(output));
		}

		[Test]
		public void ProcessAsBitmap_Gpu_Test()
		{
			using (var image = new RawImage(input))
			using (var raw = image.UnpackRaw())
			{
				raw.Process(new DcrawProcessor());

				var processedImage = raw.AsProcessedImage();
				using (var bitmap = raw.AsBitmap(true))
					bitmap.Save(output);
			}
			
			AssertUtilities.IsHashEqual(hash, File.ReadAllBytes(output));
		}
	}
}
