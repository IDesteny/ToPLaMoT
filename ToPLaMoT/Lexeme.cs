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

		public Lexeme ConvertTockens()
		{
			switch (lexemeType)
			{
				case LexemeTypes.IDENT: return new Lexeme("w", LexemeTypes.IDENT);
				case LexemeTypes.NUMBER: return new Lexeme("n", LexemeTypes.NUMBER);
				default: return this;
			}
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

/*
if (obj is not Lexeme lexeme)
{
	return false;
}

if (lexeme.lexemeType.Equals(LexemeTypes.IDENT) ||
	lexeme.lexemeType.Equals(LexemeTypes.NUMBER))
{
	return lexeme.lexemeType.Equals(lexemeType);
}
else
{
	return lexeme.token.Equals(token) && lexeme.lexemeType.Equals(lexemeType);
}
*/