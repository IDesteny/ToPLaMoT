using System;
using System.Linq;
using System.Collections.Generic;

namespace ToPLaMoT
{
	class SyntacticalAnalyzer
	{
		public enum Status { SUCCESS, INPUT_TAPE_EMPTY, TRANSITION_FUNC_NOT_FOUND };

		static public (Status status, string report) Analyze(List<Lexeme> lexemes)
		{
			// TODO: Remove?
			if (lexemes.Count.Equals(0))
			{
				return (Status.INPUT_TAPE_EMPTY, "Input tape is empty.");
			}

			var inputLexemes = new Stack<string>(lexemes.Select(ConvertTokens).Append("e").Reverse());
			var currentStore = new Stack<string>(new[] { "h0" });
			var currentState = Constants.States.S0;

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
				return (Status.TRANSITION_FUNC_NOT_FOUND,
					$"Suitable transition function for f({currentState}, {inputLexemes.Peek()}, {currentStore.Peek()}) not found.");
			}

			return (Status.SUCCESS, string.Empty);
		}

		static string ConvertTokens(Lexeme lexeme)
		{
			if (lexeme.lexemeType.Equals(Lexeme.LexemeTypes.IDENT)) return "w";
			if (lexeme.lexemeType.Equals(Lexeme.LexemeTypes.NUMBER)) return "n";
			return lexeme.token;
		}
	}
}
