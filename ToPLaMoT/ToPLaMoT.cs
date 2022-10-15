using System;
using System.IO;

namespace ToPLaMoT
{
	class ToPLaMoT
	{
		static void Main()
		{
			var lexemes = Recognizer.Recognize(new StreamReader("../../../Source.isa"));
			var spotLexemes = LexicalAnalyzer.Analyze(lexemes);
			
			if (spotLexemes.Item1 is null)
			{
				Console.WriteLine(spotLexemes.Item2);
				return;
			}

			foreach (var sl in spotLexemes.Item1)
			{
				Console.WriteLine($"{sl.token}\t: {sl.lexemeType}");
			}

			var result = SyntacticalAnalyzer.Analyze(spotLexemes.Item1);

			if (result.Item1)
			{
				Console.WriteLine(result.Item2);
				return;
			}
		}
	}
}