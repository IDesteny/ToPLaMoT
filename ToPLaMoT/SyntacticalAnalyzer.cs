using System;
using System.Linq;
using System.Collections.Generic;

namespace ToPLaMoT
{
	class SyntacticalAnalyzer
	{
		static public (bool executionStatus, string SyntacticalAnalyzerReportMsg) Analyze(List<Lexeme> listOfLexemes)
		{
			var inputLexemes = new Stack<string>(listOfLexemes.Select(ConvertTokens).Append("e").Reverse());
			var currentStore = new Stack<string>(new[] { "h0" });
			var initialState = Constants.States.S0;

			try
			{
				while (!initialState.Equals(Constants.States.S1))
				{
					var currentFunc = (initialState, inputLexemes.Peek(), currentStore.Peek());
					var (finalState, type, finalStore) = Constants.ForwardFunc[currentFunc];

					if (type.Equals(Constants.FuncTypes.RECEIVE))
					{
						inputLexemes.Pop();
					}

					currentStore.Pop();
					finalStore.ForEach(lexeme => currentStore.Push(lexeme));

					initialState = finalState;
				}
			}
			catch
			{
				return (true, $"Unexpected token '{inputLexemes.Peek()}' found.");
			}

			return (false, string.Empty);
		}

		static string ConvertTokens(Lexeme lexeme)
		{
			if (lexeme.lexemeType.Equals(Lexeme.LexemeTypes.IDENT)) return "w";
			if (lexeme.lexemeType.Equals(Lexeme.LexemeTypes.NUMBER)) return "n";
			return lexeme.token;
		}
	}
}
