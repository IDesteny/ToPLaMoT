using System;

namespace ToPLaMoT
{
	class Log4me
	{
		static void ColorOutput<T>(T msg, ConsoleColor color)
		{
			var oldColor = Console.ForegroundColor;

			Console.ForegroundColor = color;
			Console.WriteLine(msg);
			Console.ForegroundColor = oldColor;
		}

		static public void Success<T>(T msg) => ColorOutput(msg, ConsoleColor.Green);
		static public void Error<T>(T msg) => ColorOutput(msg, ConsoleColor.Red);
		static public void Info<T>(T msg) => ColorOutput(msg, ConsoleColor.White);
	}
}
