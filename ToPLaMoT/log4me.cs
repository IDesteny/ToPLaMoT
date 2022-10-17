using System;

namespace ToPLaMoT
{
	class Log4me
	{
		public enum DebugLevel { SUCCESS, INFO, WARNING, ERROR };

		static public DebugLevel DEBUG_LEVEL = DebugLevel.SUCCESS;

		static void ColorOutput<T>(T msg, ConsoleColor color)
		{
			var oldColor = Console.ForegroundColor;

			Console.ForegroundColor = color;
			Console.WriteLine(msg);
			Console.ForegroundColor = oldColor;
		}

		static void LevelOutput<T>(T msg, DebugLevel debugLevel, ConsoleColor color)
		{
			if (debugLevel >= DEBUG_LEVEL)
			{
				ColorOutput(msg, color);
			}
		}

		static public void Warning<T>(T msg) => LevelOutput(msg, DebugLevel.WARNING, ConsoleColor.Yellow);
		static public void Success<T>(T msg) => LevelOutput(msg, DebugLevel.SUCCESS, ConsoleColor.Green);
		static public void Error<T>(T msg) => LevelOutput(msg, DebugLevel.ERROR, ConsoleColor.Red);
		static public void Info<T>(T msg) => LevelOutput(msg, DebugLevel.INFO, ConsoleColor.White);
	}
}
