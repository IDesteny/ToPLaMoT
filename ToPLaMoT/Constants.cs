using System.Collections.Generic;

namespace ToPLaMoT
{
	class Constants
	{
		static public readonly List<string> Keywords = new() { "var", "integer", "begin", "end", "to", "for", "do", "read", "write", "end_for" };
		static public readonly List<string> Operators = new() { ";", ",", "(", ")", "+", "-", "*", "=", ":", "." };

		public enum States { S0, S1 };
		public enum FuncTypes { EMPTY, RECEIVE };

		static public readonly Dictionary<
			(States initialState, Lexeme lexeme, string initialStore),
			(States finalState, FuncTypes funcTypes, List<string> finalStore)
		> ForwardFunc = new()
		{
			{ (States.S0, new Lexeme("begin", Lexeme.LexemeTypes.KEYWORD), "O1"), (States.S0, FuncTypes.RECEIVE, new() { ".", "end", "L2" }) },
			{ (States.S0, new Lexeme("var", Lexeme.LexemeTypes.KEYWORD), "O2"), (States.S0, FuncTypes.RECEIVE, new() { ";", "integer", ":", "L1" }) },
			{ (States.S0, new Lexeme(",", Lexeme.LexemeTypes.OPERATOR), "S"), (States.S0, FuncTypes.RECEIVE, new() { "L1" }) },
			{ (States.S0, new Lexeme("read", Lexeme.LexemeTypes.KEYWORD), "A"), (States.S0, FuncTypes.RECEIVE, new() { ";", ")", "L1", "(", ")" }) },
			{ (States.S0, new Lexeme("write", Lexeme.LexemeTypes.KEYWORD), "A"), (States.S0, FuncTypes.RECEIVE, new() { ";", ")", "L1", "(", ")" }) },
			{ (States.S0, new Lexeme("for", Lexeme.LexemeTypes.KEYWORD), "A"), (States.S0, FuncTypes.RECEIVE, new() { "endf", "L2", "do", "V", "to", "V", "=", "E" }) },
			{ (States.S0, new Lexeme("-", Lexeme.LexemeTypes.OPERATOR), "Y"), (States.S0, FuncTypes.RECEIVE, new() { /* e */ }) },
			{ (States.S0, new Lexeme("+", Lexeme.LexemeTypes.OPERATOR), "B"), (States.S0, FuncTypes.RECEIVE, new() { /* e */ }) },
			{ (States.S0, new Lexeme("*", Lexeme.LexemeTypes.OPERATOR), "B"), (States.S0, FuncTypes.RECEIVE, new() { /* e */ }) },
			{ (States.S0, new Lexeme("-", Lexeme.LexemeTypes.OPERATOR), "B"), (States.S0, FuncTypes.RECEIVE, new() { /* e */ }) },
			{ (States.S0, new Lexeme("n", Lexeme.LexemeTypes.NUMBER), "C"), (States.S0, FuncTypes.RECEIVE, new() { "Z" }) },
			{ (States.S0, new Lexeme("w", Lexeme.LexemeTypes.IDENT), "E"), (States.S0, FuncTypes.RECEIVE, new() { "Q" }) },
			{ (States.S0, new Lexeme("(", Lexeme.LexemeTypes.OPERATOR), "O"), (States.S0, FuncTypes.RECEIVE, new() { ")", "U" }) },
		};
	}
}