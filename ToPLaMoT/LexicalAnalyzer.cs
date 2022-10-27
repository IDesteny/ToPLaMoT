using System.Collections.Generic;
using System.Linq;

namespace ToPLaMoT
{
	class LexicalAnalyzer
	{
		static public uint MAXIMUM_ALLOWED_LENGTH_OF_LEXEME = 8;

		static public (List<Lexeme> listOfLexemes, string lexicalAnalyzerReportMsg) Analyze(List<string> listOfTokens)
		{
			var listOfLexemes = new List<Lexeme>(listOfTokens.Count);

			foreach (var token in listOfTokens)
			{
				if (Constants.Keywords.Exists(kw => kw.Equals(token)))
				{
					listOfLexemes.Add(new Lexeme(token, Lexeme.LexemeTypes.KEYWORD));
					continue;
				}
				
				if (Constants.Operators.Exists(op => op.Equals(token)))
				{
					listOfLexemes.Add(new Lexeme(token, Lexeme.LexemeTypes.OPERATOR));
					continue;
				}

				if (int.TryParse(token, out _))
				{
					listOfLexemes.Add(new Lexeme(token, Lexeme.LexemeTypes.NUMBER));
					continue;
				}

				if (token.All(char.IsLetter))
				{
					if (token.Length > MAXIMUM_ALLOWED_LENGTH_OF_LEXEME)
					{
						return (null, $"The length of the token '{token}' equal to {token.Length} exceeds the maximum allowable value equal to {MAXIMUM_ALLOWED_LENGTH_OF_LEXEME}.");
					}

					listOfLexemes.Add(new Lexeme(token, Lexeme.LexemeTypes.IDENT));
					continue;
				}

				return (null, $"The token '{token}' contains invalid characters.");
			}

			return (listOfLexemes, string.Empty);
		}
	}
}