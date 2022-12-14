using System.Collections.Generic;

namespace ToPLaMoT
{
	class Constants
	{
		static public readonly List<string> Keywords = new()
			{ "var", "integer", "begin", "end", "to", "for", "do", "read", "write", "endf" };

		static public readonly List<string> Operators = new()
			{ ";", ",", "(", ")", "+", "-", "*", "=", ":", "." };

		public enum States : byte { S0, S1 };
		public enum FuncTypes : byte { EMPTY, RECEIVE };

		static public readonly Dictionary<
			(States initialState, string token, string store),
			(States finalState, FuncTypes type, List<string> finalStore)
		> ForwardFunc = new()
		{
			{ (States.S0, "begin", "O1"), (States.S0, FuncTypes.RECEIVE, new() { ".", "end", "L2" }) },
			{ (States.S0, "var", "O2"), (States.S0, FuncTypes.RECEIVE, new() { ";", "integer", ":", "L1" }) },
			{ (States.S0, ",", "S"), (States.S0, FuncTypes.RECEIVE, new() { "L1" }) },
			{ (States.S0, "read", "A"), (States.S0, FuncTypes.RECEIVE, new() { ";", ")", "L1", "(" }) },
			{ (States.S0, "write", "A"), (States.S0, FuncTypes.RECEIVE, new() { ";", ")", "L1", "(" }) },
			{ (States.S0, "for", "A"), (States.S0, FuncTypes.RECEIVE, new() { "endf", "L2", "do", "V", "to", "V", "=", "E" }) },
			{ (States.S0, "-", "Y"), (States.S0, FuncTypes.RECEIVE, new() { }) },
			{ (States.S0, "+", "B"), (States.S0, FuncTypes.RECEIVE, new() { }) },
			{ (States.S0, "*", "B"), (States.S0, FuncTypes.RECEIVE, new() { }) },
			{ (States.S0, "-", "B"), (States.S0, FuncTypes.RECEIVE, new() { }) },
			{ (States.S0, "number", "C"), (States.S0, FuncTypes.RECEIVE, new() { "Z" }) },
			{ (States.S0, "variable", "E"), (States.S0, FuncTypes.RECEIVE, new() { "Q" }) },
			{ (States.S0, "(", "O"), (States.S0, FuncTypes.RECEIVE, new() { ")", "U" }) },
			{ (States.S0, "var", "P"), (States.S0, FuncTypes.EMPTY, new() { "O1", "O2" }) },
			{ (States.S0, "variable", "L1"), (States.S0, FuncTypes.EMPTY, new() { "S", "E" }) },
			{ (States.S0, "read", "L2"), (States.S0, FuncTypes.EMPTY, new() { "R", "A" }) },
			{ (States.S0, "write", "L2"), (States.S0, FuncTypes.EMPTY, new() { "R", "A" }) },
			{ (States.S0, "for", "L2"), (States.S0, FuncTypes.EMPTY, new() { "R", "A" }) },
			{ (States.S0, "variable", "L2"), (States.S0, FuncTypes.EMPTY, new() { "R", "A" }) },
			{ (States.S0, "read", "R"), (States.S0, FuncTypes.EMPTY, new() { "L2" }) },
			{ (States.S0, "write", "R"), (States.S0, FuncTypes.EMPTY, new() { "L2" }) },
			{ (States.S0, "for", "R"), (States.S0, FuncTypes.EMPTY, new() { "L2" }) },
			{ (States.S0, "variable", "R"), (States.S0, FuncTypes.EMPTY, new() { "L2" }) },
			{ (States.S0, "variable", "A"), (States.S0, FuncTypes.EMPTY, new() { ";", "V", "=", "E" }) },
			{ (States.S0, "-", "V"), (States.S0, FuncTypes.EMPTY, new() { "U", "Y" }) },
			{ (States.S0, "(", "V"), (States.S0, FuncTypes.EMPTY, new() { "U" }) },
			{ (States.S0, "variable", "V"), (States.S0, FuncTypes.EMPTY, new() { "U" }) },
			{ (States.S0, "number", "V"), (States.S0, FuncTypes.EMPTY, new() { "U" }) },
			{ (States.S0, "(", "U"), (States.S0, FuncTypes.EMPTY, new() { "F", "O" }) },
			{ (States.S0, "variable", "U"), (States.S0, FuncTypes.EMPTY, new() { "F", "O" }) },
			{ (States.S0, "number", "U"), (States.S0, FuncTypes.EMPTY, new() { "F", "O" }) },
			{ (States.S0, "variable", "O"), (States.S0, FuncTypes.EMPTY, new() { "X" }) },
			{ (States.S0, "number", "O"), (States.S0, FuncTypes.EMPTY, new() { "X" }) },
			{ (States.S0, "+", "F"), (States.S0, FuncTypes.EMPTY, new() { "O", "B" }) },
			{ (States.S0, "-", "F"), (States.S0, FuncTypes.EMPTY, new() { "O", "B" }) },
			{ (States.S0, "-", ";"), (States.S0, FuncTypes.EMPTY, new() { ";", "F" }) },
			{ (States.S0, "+", ";"), (States.S0, FuncTypes.EMPTY, new() { ";", "F" }) },
			{ (States.S0, "*", ";"), (States.S0, FuncTypes.EMPTY, new() { ";", "F" }) },
			{ (States.S0, "-", ")"), (States.S0, FuncTypes.EMPTY, new() { ")", "F" }) },
			{ (States.S0, "+", ")"), (States.S0, FuncTypes.EMPTY, new() { ")", "F" }) },
			{ (States.S0, "*", ")"), (States.S0, FuncTypes.EMPTY, new() { ")", "F" }) },
			{ (States.S0, "-", "U"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, "*", "F"), (States.S0, FuncTypes.EMPTY, new() { "O", "B" }) },
			{ (States.S0, "variable", "X"), (States.S0, FuncTypes.EMPTY, new() { "E" }) },
			{ (States.S0, "number", "X"), (States.S0, FuncTypes.EMPTY, new() { "C" }) },
			{ (States.S0, "number", "Z"), (States.S0, FuncTypes.EMPTY, new() { "C" }) },
			{ (States.S0, "variable", "Q"), (States.S0, FuncTypes.EMPTY, new() { "E" }) },
			{ (States.S0, ":", "S"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, ")", "S"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, "end", "R"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, "endf", "R"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, ";", "F"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, ")", "F"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, "to", "F"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, ",", "Q"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, "=", "Q"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, "+", "Q"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, "-", "Q"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, ":", "Q"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, "to", "Q"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, "do", "F"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, "+", "Z"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, "-", "Z"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, "*", "Z"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, ")", "Z"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, ";", "Z"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, "to", "Z"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, "do", "Z"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, "*", "Q"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, ")", "Q"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, ";", "Q"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, "do", "Q"), (States.S0, FuncTypes.EMPTY, new() { }) },
			{ (States.S0, "integer", "integer"), (States.S0, FuncTypes.RECEIVE, new() { }) },
			{ (States.S0, "end", "end"), (States.S0, FuncTypes.RECEIVE, new() { }) },
			{ (States.S0, ".", "."), (States.S0, FuncTypes.RECEIVE, new() { }) },
			{ (States.S0, "=", "="), (States.S0, FuncTypes.RECEIVE, new() { }) },
			{ (States.S0, "to",	"to"), (States.S0, FuncTypes.RECEIVE, new() { }) },
			{ (States.S0, "do",	"do"), (States.S0, FuncTypes.RECEIVE, new() { }) },
			{ (States.S0, ";", ";"), (States.S0, FuncTypes.RECEIVE, new() { }) },
			{ (States.S0, ")", ")"), (States.S0, FuncTypes.RECEIVE, new() { }) },
			{ (States.S0, "(", "("), (States.S0, FuncTypes.RECEIVE, new() { }) },
			{ (States.S0, ":", ":"), (States.S0, FuncTypes.RECEIVE, new() { }) },
			{ (States.S0, "endf", "endf"), (States.S0, FuncTypes.RECEIVE, new() { }) },
			{ (States.S0, "e", "h0"), (States.S1, FuncTypes.RECEIVE, new() { }) },
			{ (States.S0, "var", "h0"), (States.S0, FuncTypes.EMPTY, new() { "h0", "P" }) },
		};
	}
}