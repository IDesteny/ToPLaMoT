using System.Collections.Generic;
using System.IO;

namespace ToPLaMoT
{
	class LexicalAnalyzer
	{
		static public int MAX_LEX_LEN = 8;

		static public (List<string>, string) Analyze(StreamReader streamReader)
		{
			var lexemes = new List<string>();
			var lexemesBuffer = string.Empty;

			while (streamReader.EndOfStream == false)
			{
				var symbol = (char)streamReader.Read();

				if (char.IsLetterOrDigit(symbol))
				{
					lexemesBuffer += symbol;

					if (lexemesBuffer.Length > MAX_LEX_LEN)
					{
						return (null, $"The length of the '{lexemesBuffer}' identifier exceeds the allowed value of {MAX_LEX_LEN}.");
					}
				}
				else
				{
					if (lexemesBuffer != string.Empty)
					{
						lexemes.Add(lexemesBuffer);
						lexemesBuffer = string.Empty;
					}

					if (Constants.Operators.Exists(op => op.Equals(symbol.ToString())))
					{
						lexemes.Add(symbol.ToString());
					}
					else if (symbol.ToString().Contains(" \n\r\t\0"))
					{
						return (null, $"'{symbol}' is an invalid character.");
					}
				}
			}
			return (lexemes, string.Empty);
		}
	}
}
