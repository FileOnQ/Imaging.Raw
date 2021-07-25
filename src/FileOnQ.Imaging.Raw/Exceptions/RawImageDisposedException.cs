using System;

namespace FileOnQ.Imaging.Raw
{
	public class RawImageDisposedException : ObjectDisposedException
	{
		const string ErrorMessage = "The ProcessedImage must be used prior to disposing of the object.";
		public RawImageDisposedException(string name) : base(name, ErrorMessage) { }
	}
}
