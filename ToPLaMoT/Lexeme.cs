namespace ToPLaMoT
{
	class Lexeme
	{
		public enum LexemeTypes { KEYWORD, OPERATOR, IDENT, NUMBER }

		public LexemeTypes lexemeType;
		public string token;

		public Lexeme(string token, LexemeTypes lexemeType)
		{
			this.token = token;
			this.lexemeType = lexemeType;
		}
	}
}
