using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ToPLaMoT
{
	class Recognizer
	{
		public static List<string> Recognize(StreamReader streamReader)
		{
			var listOfTokens = new List<string>();
			var holdingBuffer = new StringBuilder();

			while (!streamReader.EndOfStream)
			{
				var readingObject = (char)streamReader.Read();

				if (char.IsLetterOrDigit(readingObject))
				{
					holdingBuffer.Append(readingObject);
					continue;
				}

				if (!holdingBuffer.Equals(string.Empty))
				{
					listOfTokens.Add(holdingBuffer.ToString());
					holdingBuffer.Clear();
				}

				if (!"\n\r\t ".Contains(readingObject))
				{
					listOfTokens.Add(readingObject.ToString());
				}
			}

			if (!holdingBuffer.Equals(string.Empty))
			{
				listOfTokens.Add(holdingBuffer.ToString());
			}

			return listOfTokens;
		}

		public static Task<List<string>> RecognizeFromFileAsync(string filepath)
		{
			return Task.Run(() =>
			{
				using var streamReader = new StreamReader(filepath);

				return Recognize(streamReader);
			});
		}

		public static async Task<(List<string> lexemes, string report)> AnalyzeAsync(string filepath)
		{
			var listOfTokens = await RecognizeFromFileAsync(filepath);

			if (listOfTokens.Count.Equals(0))
			{
				return (null, "Missing source code.");
			}

			return (listOfTokens, string.Empty);
		}
	}
}
