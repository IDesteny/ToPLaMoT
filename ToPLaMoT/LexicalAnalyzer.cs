using System.Collections.Generic;
using System.Linq;

namespace ToPLaMoT
{
	class LexicalAnalyzer
	{
		static public int MAX_LEX_LEN = 8;

		static public (List<Lexeme>, string) Analyze(List<string> lexemes)
		{
			var spotLexemes = new List<Lexeme>(lexemes.Count);

			foreach (var lexeme in lexemes)
			{
				if (Constants.Keywords.Exists(kw => kw.Equals(lexeme)))
				{
					spotLexemes.Add(new Lexeme(lexeme, Lexeme.LexemeTypes.KEYWORD));
				}
				else if (Constants.Operators.Exists(op => op.Equals(lexeme)))
				{
					spotLexemes.Add(new Lexeme(lexeme, Lexeme.LexemeTypes.OPERATOR));
				}
				else if (int.TryParse(lexeme, out _))
				{
					spotLexemes.Add(new Lexeme(lexeme, Lexeme.LexemeTypes.NUMBER));
				}
				else if (lexeme.All(char.IsLetter))
				{
					if (lexeme.Length > MAX_LEX_LEN)
					{
						return (null, $"The length of the token '{lexeme}' equal to {lexeme.Length} exceeds the maximum allowable value equal to {MAX_LEX_LEN}.");
					}

					spotLexemes.Add(new Lexeme(lexeme, Lexeme.LexemeTypes.IDENT));
				}
				else
				{
					return (null, $"The token {lexeme} contains invalid characters.");
				}
			}

			return (spotLexemes, string.Empty);
		}
	}
}