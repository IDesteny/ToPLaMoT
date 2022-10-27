using System;

namespace ToPLaMoT
{
	class Log4me
	{
		public enum DebugLevel : byte { SUCCESS, INFO, WARNING, ERROR };

		static public DebugLevel DEBUG_LEVEL = DebugLevel.SUCCESS;

		static void ColorMessageOutput<T>(T message, ConsoleColor consoleColor)
		{
			var previousColor = Console.ForegroundColor;

			Console.ForegroundColor = consoleColor;
			Console.WriteLine(message);
			Console.ForegroundColor = previousColor;
		}

		static void LogBasedOnLevel<T>(T message, DebugLevel debugLevel, ConsoleColor consoleColor)
		{
			if (debugLevel >= DEBUG_LEVEL)
			{
				ColorMessageOutput(message, consoleColor);
			}
		}

		static public void Warning<T>(T message) => LogBasedOnLevel(message, DebugLevel.WARNING, ConsoleColor.Yellow);
		static public void Success<T>(T message) => LogBasedOnLevel(message, DebugLevel.SUCCESS, ConsoleColor.Green);
		static public void Error<T>(T message) => LogBasedOnLevel(message, DebugLevel.ERROR, ConsoleColor.Red);
		static public void Info<T>(T message) => LogBasedOnLevel(message, DebugLevel.INFO, ConsoleColor.White);
	}
}
