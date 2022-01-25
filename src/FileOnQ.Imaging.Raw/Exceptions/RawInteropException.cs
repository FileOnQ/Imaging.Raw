using System;

namespace FileOnQ.Imaging.Raw
{
	public class RawInteropException : Exception
	{
		const string ErrorMessage = "Error when interoping with native assemblies - {0}";

		public RawInteropException(string message)
			: base(string.Format(ErrorMessage, message))
		{
		}
	}
}
