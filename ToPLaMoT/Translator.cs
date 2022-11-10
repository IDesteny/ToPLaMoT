using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToPLaMoT
{
	class Translator
	{
		enum States { S, VAR, EXP, FOR, READ, WRITE, FIN }

		static public string Analyze(List<Lexeme> listOfLexemes)
		{
			var stackOfLexemes = new Stack<Lexeme>(listOfLexemes.AsEnumerable().Reverse());
			var sourceCSCode = new StringBuilder("using System;namespace ToPLaMoT{class Program{static void Main(){");

			var state = States.S;

			while (!state.Equals(States.FIN))
			{
				switch (state)
				{
					case States.S:
					{
						var tmp = stackOfLexemes.Peek();
						if (tmp.token.Equals("var"))
						{
							state = States.VAR;
							break;
						}

						if (tmp.lexemeType.Equals(Lexeme.LexemeTypes.IDENT))
						{
							state = States.EXP;
							break;
						}

						if (tmp.token.Equals("for"))
						{
							state = States.FOR;
							break;
						}

						if (tmp.token.Equals("endf"))
						{
							sourceCSCode.Append("}");
						}

						if (tmp.token.Equals("read"))
						{
							state = States.READ;
							break;
						}

						if (tmp.token.Equals("write"))
						{
							state = States.WRITE;
							break;
						}

						stackOfLexemes.Pop();

						if (!stackOfLexemes.Any())
						{
							state = States.FIN;
						}

						break;
					}

					case States.VAR:
					{
						var lexeme = stackOfLexemes.Pop();

						if (lexeme.lexemeType.Equals(Lexeme.LexemeTypes.IDENT))
						{
							sourceCSCode.Append($"int {lexeme.token};");
							break;
						}

						if (lexeme.token.Equals(":"))
						{
							state = States.S;
						}

						break;
					}

					case States.EXP:
					{
						var lexeme = stackOfLexemes.Pop();
						sourceCSCode.Append(lexeme.token);

						if (lexeme.token.Equals(";"))
						{
							state = States.S;
						}

						break;
					}

					case States.FOR:
					{
						stackOfLexemes.Pop();
						var it = stackOfLexemes.Pop();
						sourceCSCode.Append($"for({it.token}=");

						stackOfLexemes.Pop();
						var init = stackOfLexemes.Pop();
						sourceCSCode.Append($"{init.token};{it.token}<");

						stackOfLexemes.Pop();
						var fin = stackOfLexemes.Pop();
						sourceCSCode.Append($"{fin.token};++{it.token}){{");

						stackOfLexemes.Pop();

						state = States.S;
						break;
					}

					case States.READ:
					{
						var lexeme = stackOfLexemes.Pop();

						if (lexeme.lexemeType.Equals(Lexeme.LexemeTypes.IDENT))
						{
							sourceCSCode.Append($"{lexeme.token}=int.Parse(Console.ReadLine());");
							break;
						}

						if (lexeme.token.Equals(";"))
						{
							state = States.S;
						}

						break;
					}

					case States.WRITE:
					{
						var lexeme = stackOfLexemes.Pop();

						if (lexeme.lexemeType.Equals(Lexeme.LexemeTypes.IDENT))
						{
							sourceCSCode.Append($"Console.WriteLine({lexeme.token});");
							break;
						}

						if (lexeme.token.Equals(";"))
						{
							state = States.S;
						}

						break;
					}
				}
			}

			sourceCSCode.Append("}}}");

			return sourceCSCode.ToString();
		}

		static public Task<string> AnalyzeAsync(List<Lexeme> listOfLexemes) => Task.Run(() => Analyze(listOfLexemes));
	}
}
