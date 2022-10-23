using ToPLaMoT;

var (lexemes, recMsg) = await Recognizer.Analyze("../../../Source.isa");

if (lexemes is null)
{
	Log4me.Warning(recMsg);
	return;
}

var (spotLexemes, lexMsg) = LexicalAnalyzer.Analyze(lexemes);

if (spotLexemes is null)
{
	Log4me.Error(lexMsg);
	return;
}

var (result, syntMsg) = SyntacticalAnalyzer.Analyze(spotLexemes);

if (result)
{
	Log4me.Error(syntMsg);
	return;
}

Log4me.Success("Successfully.");
