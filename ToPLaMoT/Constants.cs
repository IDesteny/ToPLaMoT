using System.Collections.Generic;

namespace ToPLaMoT
{
	class Constants
	{
		static public readonly List<string> Keywords = new() { "var", "integer", "begin", "end", "to", "for", "do", "read", "write", "endf" };
		static public readonly List<string> Operators = new() { ";", ",", "(", ")", "+", "-", "*", "=", ":", "." };
	}
}