using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ToPLaMoT
{
	class Recognizer
	{
		public static List<string> Recognize(StreamReader streamReader)
		{
			var lexemes = new List<string>();
			var buffer = string.Empty;

			while (!streamReader.EndOfStream)
			{
				var symbol = (char)streamReader.Read();

				if (char.IsLetterOrDigit(symbol))
				{
					buffer += symbol;
					continue;
				}

				if (buffer != string.Empty)
				{
					lexemes.Add(buffer);
					buffer = string.Empty;
				}

				if (!("\n\r\t ").Contains(symbol))
				{
					lexemes.Add(symbol.ToString());
				}
			}
			return buffer != string.Empty ? lexemes.Append(buffer).ToList() : lexemes;
		}

		public static List<string> RecognizeFromFile(string filepath)
		{
			using var streamReader = new StreamReader(filepath);

			return Recognize(streamReader);
		}
	}
}
