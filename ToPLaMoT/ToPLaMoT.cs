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

			var result = SyntacticalAnalyzer.Analyze(lexemes.Item1);
			if (result.Item1)
			{
				Console.WriteLine(result.Item2);
				return;
			}

			Console.WriteLine("Successfully!");
		}
	}
}