using System;
using System.Linq;
using System.Collections.Generic;

namespace ToPLaMoT
{
	class SyntacticalAnalyzer
	{
		static public (bool result, string syntMsg) Analyze(List<Lexeme> lexemes)
		{
			var inputLexemes = new Stack<string>(new[] { "e" });
			var currentStore = new Stack<string>(new[] { "h0" });
			var currentState = Constants.States.S0;

			ReverseList(lexemes).ForEach(lexeme => inputLexemes.Push(ConvertTockens(lexeme)));

			try
			{
				while (!currentState.Equals(Constants.States.S1))
				{
					var initialFunc = (currentState, inputLexemes.Peek(), currentStore.Peek());
					var (finalState, funcTypes, finalStore) = Constants.ForwardFunc[initialFunc];

					if (funcTypes.Equals(Constants.FuncTypes.RECEIVE))
					{
						inputLexemes.Pop();
					}

					currentStore.Pop();
					finalStore.ForEach(lexeme => currentStore.Push(lexeme));

					currentState = finalState;
				}
			}
			catch (KeyNotFoundException)
			{
				return (true, $"Suitable transition function for f({currentState}, {inputLexemes.Peek()}, {currentStore.Peek()}) not found");
			}
			catch (InvalidOperationException)
			{
				return (true, $"The input tape or magazine was unexpectedly empty.\n" +
					$"input tape: [{string.Join(", ", inputLexemes)}]\n" +
					$"magazine: [{string.Join(", ", currentStore)}]");
			}

			return (false, string.Empty);
		}

		static string ConvertTockens(Lexeme lexeme)
		{
			switch (lexeme.lexemeType)
			{
				case Lexeme.LexemeTypes.IDENT: return "w";
				case Lexeme.LexemeTypes.NUMBER: return "n";
				default: return lexeme.token;
			}
		}

		static List<T> ReverseList<T>(List<T> list) => Enumerable.Reverse(list).ToList();
	}
}
