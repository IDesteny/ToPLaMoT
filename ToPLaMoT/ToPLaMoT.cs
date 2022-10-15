using System.IO;

namespace ToPLaMoT
{
	class ToPLaMoT
	{
		static void Main()
		{
			var lexemes = Recognizer.Recognize(new StreamReader("../../../Source.isa"));
			var (spotLexemes, lexMsg) = LexicalAnalyzer.Analyze(lexemes);
			
			if (spotLexemes is null)
			{
				Log4me.Error(lexMsg);
				return;
			}

			foreach (var spotLexeme in spotLexemes)
			{
				Log4me.Default($"{{{spotLexeme.token} : {spotLexeme.lexemeType}}}");
			}

			var (result, syntMsg) = SyntacticalAnalyzer.Analyze(spotLexemes);

			if (result)
			{
				Log4me.Error(syntMsg);
				return;
			}

			Log4me.Success("Successfully");
		}
	}
}