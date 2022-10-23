using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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

				if (!buffer.Equals(string.Empty))
				{
					lexemes.Add(buffer);
					buffer = string.Empty;
				}

				if (!("\n\r\t ").Contains(symbol))
				{
					lexemes.Add(symbol.ToString());
				}
			}

			if (!buffer.Equals(string.Empty))
			{
				lexemes.Add(buffer);
			}

			return lexemes;
		}

		public static Task<List<string>> RecognizeFromFileAsync(string filepath)
		{
			return Task.Run(() =>
			{
				using var streamReader = new StreamReader(filepath);

				return Recognize(streamReader);
			});
		}

		public static async Task<(List<string> lexemes, string report)> Analyze(string filepath)
		{
			var lexemes = await RecognizeFromFileAsync(filepath);

			if (lexemes.Count.Equals(0))
			{
				return (null, "Missing source code.");
			}

			return (lexemes, string.Empty);
		}
	}
}
