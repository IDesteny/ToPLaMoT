using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ToPLaMoT
{
	class Compiler
	{
		static public async Task<(int exitCode, string compilerReportMsg)> CompileAsync(string sourceCSCode)
		{
			await File.WriteAllTextAsync("out.cs", sourceCSCode);

			var startInfo = new ProcessStartInfo
			{
				FileName = "C:/Windows/Microsoft.NET/Framework64/v4.0.30319/csc.exe",
				Arguments = "out.cs",
				RedirectStandardOutput = true
			};

			using var process = Process.Start(startInfo);
			process.WaitForExit();

			if (process.ExitCode != 0)
			{
				var errorMessage = await process.StandardOutput.ReadToEndAsync();
				var trimInfo = errorMessage.Remove(0, errorMessage.LastIndexOf(":") + 2);

				return (process.ExitCode, trimInfo);
			}

			return (0, string.Empty);
		}

		static public Task Run()
		{
			return Task.Run(() =>
			{
				using var process = Process.Start("out.exe");

				process.WaitForExit();
			});
		}
	}
}
