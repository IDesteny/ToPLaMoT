using System;
using System.IO;

namespace ToPLaMoT
{
	class ToPLaMoT
	{
		static void Main()
		{
			var lexemes = LexicalAnalyzer.Analyze(new StreamReader("../../../Source.isa"));
			if (lexemes.Item1 is null)
			{
				Console.WriteLine(lexemes.Item2);
				return;
			}

			foreach (var lexeme in lexemes.Item1)
			{
				Console.Write(lexeme + " ");
			}

			var result = SyntacticalAnalyzer.Analyze(lexemes.Item1);
			if (result is null)
			{
				Console.WriteLine(result);
				return;
			}

			Console.WriteLine("Successfully!");
		}
	}
}