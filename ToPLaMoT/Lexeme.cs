namespace ToPLaMoT
{
	class Lexeme
	{
		public enum LexemeTypes { KEYWORD, OPERATOR, IDENT, NUMBER }

		public readonly LexemeTypes lexemeType;
		public readonly string token;

		public Lexeme(string token, LexemeTypes lexemeType)
		{
			this.token = token;
			this.lexemeType = lexemeType;
		}
	}
}