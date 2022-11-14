using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ToPLaMoT
{
	class Compiler
	{
		static public string OutputFilename = "out";

		static readonly string ExecutableName = string.Concat(OutputFilename, ".exe");
		static readonly string SourceFilename = string.Concat(OutputFilename, ".cs");

		static readonly string CompilerPath = @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\Roslyn\csc.exe";

		static public async Task<(bool executionStatus, string compilerReportMsg)> CompileAsync(string sourceCSCode)
		{
			await File.WriteAllTextAsync(SourceFilename, sourceCSCode);

			var startInfo = new ProcessStartInfo
			{
				FileName = CompilerPath,
				Arguments = SourceFilename,
				RedirectStandardOutput = true
			};

			using var process = Process.Start(startInfo);
			process.WaitForExit();

			File.Delete(SourceFilename);

			if (process.ExitCode != 0)
			{
				var errorMessage = await process.StandardOutput.ReadToEndAsync();
				var trimInfo = errorMessage.Remove(0, errorMessage.LastIndexOf(":") + 2);

				return (true, trimInfo);
			}

			return (false, string.Empty);
		}

		static public Task RunAsync()
		{
			return Task.Run(() =>
			{
				using var process = Process.Start(ExecutableName);

				process.WaitForExit();
			});
		}
	}
}
