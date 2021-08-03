using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw.Interop
{
	internal static class DllLoader
	{
		static bool isConfigured;

		internal static void Configure()
		{
			if (isConfigured)
				return;

#if NET5_0_OR_GREATER
			NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), DllImportResolver);
			isConfigured = true;
#endif
		}

#if NET5_0_OR_GREATER
		internal static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
		{
			var path = Path.GetDirectoryName(assembly.Location);
			switch (RuntimeInformation.ProcessArchitecture)
			{
				case Architecture.X86:
					path = Path.Combine(path, "runtimes\\win-x86\\native");
					break;
				case Architecture.X64:
					path = Path.Combine(path, "runtimes\\win-x64\\native");
					break;
			}

			var assemblyPath = Path.Combine(path, libraryName);
			return NativeLibrary.Load(assemblyPath);
		}
#endif
	}
}
