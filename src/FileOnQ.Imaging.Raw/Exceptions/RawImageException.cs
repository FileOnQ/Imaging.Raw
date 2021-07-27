using System;

namespace FileOnQ.Imaging.Raw
{
	public class RawImageException<T> : Exception
	{
		const string ErrorMessage = "Raw image exception - {0}";
		public T Error { get; }

		public RawImageException(T error) : base(string.Format(ErrorMessage, error)) =>
			Error = error;
	}
}