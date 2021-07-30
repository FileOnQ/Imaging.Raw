using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FileOnQ.Imaging.Raw.Tests
{
	public static partial class TestData
	{
		public const string RawImage1 = "Images\\@signatureeditsco(1).dng";
		public const string RawImage2 = "Images\\@signatureeditsco.dng";
		public const string RawImage3 = "Images\\canon_eos_r_01.cr3";
		public const string RawImage4 = "Images\\Christian - .unique.depth.dng";
		public const string RawImage5 = "Images\\DSC_0118.nef";
		public const string RawImage6 = "Images\\DSC02783.ARW";
		public const string RawImage7 = "Images\\PANA2417.RW2";
		public const string RawImage8 = "Images\\PANA8392.RW2";
		public const string RawImage9 = "Images\\photo by @Dupe.png--@Emily.rosegold.arw";
		public const string RawImage10 = "Images\\sample1.cr2";
		public const string RawImage11 = "Images\\signature edits APC_00171.dng";
		public const string RawImage12 = "Images\\signature edits free raws P1015526.dng";
		public const string RawImage13 = "Images\\signature edits free raws_DSC7082.NEF";
		public const string RawImage14 = "Images\\signatureeditsfreerawphoto.NEF";

		public static class Integration
		{
			public static class ProccessAsBitmap
			{
				public static IDictionary<string, string> HashCodes { get; set; } = BuildDictionary();

				/// <summary>
				/// Builds a hashcode mapping for proccess bitmap tests. The hash values are 
				/// slightly different between x64 and x86
				/// </summary>
				/// <returns></returns>
				static IDictionary<string, string> BuildDictionary()
				{
					if (RuntimeInformation.ProcessArchitecture == Architecture.X64)
					{
						return new Dictionary<string, string>
						{
							{ RawImage1, "BBA0AA3A611CBFA1B114037957061CC58D3484DA5005700D19F79860F0AA7DFC" },
							{ RawImage2, "937B694DE013859228F4209ACC93DCED0F9206B560A4949CA53EC695E80F52B5" },
							{ RawImage3, "B605E2A2F2C5CAE6796978039C702F3CB819B4334BD561A902D110DF35C4F936" },
							{ RawImage4, "585DA19A374F82BEA1AB202AEA03BA0B384BBC30D75CB0422D14576221153076" },
							{ RawImage5, "845BBAC7E73070725BD2F7E616784DA0736F37900A834CF6979142D41C043B6D" },
							{ RawImage6, "AAB21936231BE11A9AC48FA9DE685317F3AB4D0F350D7879E72CA62DA6935D67" },
							{ RawImage7, "BBB4582591D9689F4F586C3E361EA43FC73B598F693F67E38A79B35473A339C5" },
							{ RawImage8, "92DAAAAB9761A789F38303D397BF6E588C0CF6F9B02B5BD22EAF2395BC5711F5" },
							{ RawImage9, "4914D2574E020E819C516CD96126DFCF5B6027D245D8CB4442D014239AFE5D8B" },
							{ RawImage10, "02717173DBF92480B9FEC2AC099DCFFED546C1F920B2EF388B99051C95AE198F" },
							{ RawImage11, "F04BBB33B1F2D01115309DBCC6C951D25AF998EC749B6893D37B91352B20D71E" },
							{ RawImage12, "E3FC226754E815C0475C279559AE23C9EBDC5C57C63F0E484090ED1E5776B305" },
							{ RawImage13, "983E7E0FD41E2980DCFAC3A3C71C3F6BE88788DA6A6B8450EFA7578BED82F088" },
							{ RawImage14, "ABCC78EA917D681A2E36A240B64E40AA0E8727EE02CA71D8A5AD45ABBB8F0A96" }
						};
					}
					else if (RuntimeInformation.ProcessArchitecture == Architecture.X86)
					{
						return new Dictionary<string, string>
						{
							{ RawImage1, "" },
							{ RawImage2, "" },
							{ RawImage3, "" },
							{ RawImage4, "" },
							{ RawImage5, "" },
							{ RawImage6, "" },
							{ RawImage7, "" },
							{ RawImage8, "" },
							{ RawImage9, "" },
							{ RawImage10, "" },
							{ RawImage11, "" },
							{ RawImage12, "" },
							{ RawImage13, "" },
							{ RawImage14, "" }
						};
					}

					throw new NotSupportedException($"Current platform ({RuntimeInformation.ProcessArchitecture}) is not supported");
				}
			}
		}
	}
}
