using System;

namespace ToPLaMoT
{
	class Log4me
	{
		static void ColorOutput(string msg, ConsoleColor color)
		{
			var oldColor = Console.ForegroundColor;
			Console.ForegroundColor = color;
			Console.WriteLine(msg);
			Console.ForegroundColor = oldColor;
		}

		static public void Success(string msg) => ColorOutput(msg, ConsoleColor.Green);
		static public void Error(string msg) => ColorOutput(msg, ConsoleColor.Red);
		static public void Default(string msg) => ColorOutput(msg, ConsoleColor.White);
	}
}
