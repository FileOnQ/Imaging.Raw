namespace FileOnQ.Imaging.Raw.Tests
{
	public static partial class Constants
    {
		public static class Category
		{
			// Defines test that complete full End to End testing
			public const string Integration = "Integration";

			// Defines tests that certify LibRaw is working - used when pulling latest from upstream
			public const string LibRaw = "LibRaw";

			// Defines test that validates a specific function or unit of work.
			public const string Unit = "Unit";
		}
    }
}
