//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Build");
var githubRef = Argument("ref", string.Empty); 
var version = Argument("package-version", "8.1.97");
var suffix = Argument("version-suffix", "alpha");
var configuration = Argument("configuration", "Release");
var platform = Argument("platform", "Any CPU");
var solution = Argument("solution", "FileOnQ.Imaging.Raw.sln");
var sample = Argument("sample", "./sample/FileOnQ.Imaging.Raw.Sample.sln");
var csproj = Argument("csproj", "./src/FileOnQ.Imaging.Raw/FileOnQ.Imaging.Raw.csproj");
var gpuProject = "./src/FileOnQ.Imaging.Raw.Gpu.Cuda/FileOnQ.Imaging.Raw.Gpu.Cuda.vcxproj";

//////////////////////////////////////////////////////////////////////
// MSBuild Settings
//////////////////////////////////////////////////////////////////////
var msbuildSettings = new MSBuildSettings
{
    ToolVersion = MSBuildToolVersion.VS2019,
    Configuration = configuration,
	PlatformTarget = GetPlatform(platform)
};

PlatformTarget GetPlatform(string platform)
{
	switch (platform)
	{
		case "Any CPU":
			return PlatformTarget.MSIL;
		case "x64":
			return PlatformTarget.x64;
		case "x86":
			return PlatformTarget.x86;
		case "Win32":
			return PlatformTarget.Win32;
		case "ARM64":
			return PlatformTarget.ARM64;
		case "ARM":
			return PlatformTarget.ARM;
	}

	throw new Exception("Invalid platform");
}

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
	// TODO - 7/27/2021 - @ahoefling - Use super clean technique instead of CleanDirectories
    CleanDirectories("./src/**/bin/");
    Information("Cleaning Directory ./src/**/bin/");
    
    CleanDirectories("./src/**/obj/");
    Information("Cleaning Directory ./src/**/obj/");
    
    CleanDirectories("./Sample/**/bin/");
    Information("Cleaning Directory ./Sample/**/bin/");
    
    CleanDirectories("./Sample/**/obj/");
    Information("Cleaning Directory ./Sample/**/obj/");
	
	CleanDirectories("./tests/**/bin/");
    Information("Cleaning Directory ./tests/**/bin/");
    
    CleanDirectories("./tests/**/obj/");
    Information("Cleaning Directory ./tests/**/obj/");
});

Task("Restore")
    .Does(() =>
{
    NuGetRestore(solution);
});
    

Task("Build")
    .Does(() =>
{
	MSBuild(csproj, msbuildSettings);
});

Task("Build-GPU")
	.Does(() =>
{
	msbuildSettings.WorkingDirectory = "./src/FileOnQ.Imaging.Raw.Gpu.Cuda";
    MSBuild("./src/FileOnQ.Imaging.Raw.Gpu.Cuda/FileOnQ.Imaging.Raw.Gpu.Cuda.vcxproj", msbuildSettings);
});

Task("Pack")
    .Does(() =>
{
    if (!string.IsNullOrEmpty(githubRef))
    {
        version = githubRef
            .Split("/")
            .LastOrDefault()
            .TrimStart('v');
          
        if (version.Contains("-dev."))
        {
            var segments = version.Split("-");
            version = segments[0];
            suffix = segments[1];
        }
        else
        {
            suffix = string.Empty;
        }
    }
    
    MSBuild(csproj, msbuildSettings
        .WithTarget("pack")
        .WithProperty("PackageVersion", string.IsNullOrEmpty(suffix) ? version : $"{version}-{suffix}")
        .WithProperty("Version", version)
        .WithProperty("AssemblyVersion", version) 
        .WithProperty("PackageOutputPath", "../../"));
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("CI-Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build");
    
Task("Package-Build")
    .IsDependentOn("CI-Build")
    .IsDependentOn("Pack");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);