using System;
using System.Collections.Generic;

namespace ToPLaMoT
{
	class SyntacticalAnalyzer
	{
		static public (bool result, string syntMsg) Analyze(List<Lexeme> lexemes)
		{
			var inputLexemes = new Stack<Lexeme>(lexemes);
			var currentStore = new Stack<string>(new[] { "h0" });
			var currentState = Constants.States.S0;

			try
			{
				while (!currentState.Equals(Constants.States.S1))
				{
					var (finalState, funcTypes, finalStore) = Constants.ForwardFunc[(currentState, inputLexemes.Peek().ConvertTockens(), currentStore.Peek())];

					if (funcTypes.Equals(Constants.FuncTypes.RECEIVE))
					{
						inputLexemes.Pop();
					}

					currentStore.Pop();
					finalStore.ForEach(lexeme => currentStore.Push(lexeme));

					currentState = finalState;
				}
			}
			catch (KeyNotFoundException) { return (true, "A suitable transition function was not found.");}
			catch (InvalidOperationException) { return (true, "The input tape or magazine was unexpectedly empty.");}

			return (false, string.Empty);
		}
	}
}
