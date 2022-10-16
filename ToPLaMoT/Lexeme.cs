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

		public override bool Equals(object obj)
		{
			return obj is Lexeme lexeme && lexeme.token.Equals(token) && lexeme.lexemeType.Equals(lexemeType);
		}

		public override int GetHashCode()
		{
			return token.GetHashCode() + lexemeType.GetHashCode();
		}
	}
}