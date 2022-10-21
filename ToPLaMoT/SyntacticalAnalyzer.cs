using System;
using System.Linq;
using System.Collections.Generic;

namespace ToPLaMoT
{
	class SyntacticalAnalyzer
	{
		static public (bool result, string report) Analyze(List<Lexeme> lexemes)
		{
			var inputLexemes = new Stack<string>(lexemes.Select(ConvertTokens).Append("e").Reverse());
			var currentStore = new Stack<string>(new[] { "h0" });
			var currentState = Constants.States.S0;

			try
			{
				while (!currentState.Equals(Constants.States.S1))
				{
					var currentFunc = (currentState, inputLexemes.Peek(), currentStore.Peek());
					var (finalState, funcTypes, finalStore) = Constants.ForwardFunc[currentFunc];

					if (funcTypes.Equals(Constants.FuncTypes.RECEIVE))
					{
						inputLexemes.Pop();
					}

					currentStore.Pop();
					finalStore.ForEach(lexeme => currentStore.Push(lexeme));

					currentState = finalState;
				}
			}
			catch
			{
				try
				{
					var currentFunc = (currentState, inputLexemes.Peek(), currentStore.Peek());
					var ErrorMessage = Constants.ErrorIdentification[currentFunc];

					return (true, $"Error: {ErrorMessage}");
				}
				catch
				{
					return (true,
						$"Suitable transition function for f({currentState}, {inputLexemes.Peek()}, {currentStore.Peek()}) not found.");
				}
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
