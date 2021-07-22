using System;

namespace FileOnQ.Imaging.Raw
{
	public class RawImageException : Exception
	{
		const string ErrorMessage = "Raw image exception - {0}";
		public LibRaw.LibRawError Error { get; }

		public RawImageException(LibRaw.LibRawError error) : base(string.Format(ErrorMessage, error)) =>
			Error = error;	
	}
}