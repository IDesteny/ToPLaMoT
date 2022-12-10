using System;
using System.IO;

namespace ToPLaMoT
{
	class Log4me
	{
		public enum DebugLevel : byte { INFO, WARNING, ERROR };

		static public DebugLevel DEBUG_LEVEL = DebugLevel.INFO;

		static async void MessageOutput<T>(TextWriter textWriter, T message, ConsoleColor consoleColor)
		{
			var previousColor = Console.ForegroundColor;

			Console.ForegroundColor = consoleColor;
			await textWriter.WriteLineAsync($"[{DateTime.Now}] {message}");
			Console.ForegroundColor = previousColor;
		}

		static void LogBasedOnLevel<T>(TextWriter textWriter, T message, DebugLevel debugLevel, ConsoleColor consoleColor)
		{
			if (debugLevel >= DEBUG_LEVEL)
			{
				MessageOutput(textWriter, message, consoleColor);
			}
		}

		static public void Warning<T>(T message) => LogBasedOnLevel(Console.Out, $"WARN: {message}", DebugLevel.WARNING, ConsoleColor.Yellow);
		static public void Error<T>(T message) => LogBasedOnLevel(Console.Error, $"ERROR: {message}", DebugLevel.ERROR, ConsoleColor.Red);
		static public void Info<T>(T message) => LogBasedOnLevel(Console.Out, $"INFO: {message}", DebugLevel.INFO, ConsoleColor.White);
	}
}
