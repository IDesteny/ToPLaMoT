using ToPLaMoT;

var lexemes = Recognizer.RecognizeFromFile("../../../Source.isa");

var (spotLexemes, lexMsg) = LexicalAnalyzer.Analyze(lexemes);

if (spotLexemes is null)
{
	Log4me.Error(lexMsg);
	return;
}

var (result, syntMsg) = SyntacticalAnalyzer.Analyze(spotLexemes);

if (result != SyntacticalAnalyzer.Status.SUCCESS)
{
	if (result != SyntacticalAnalyzer.Status.INPUT_TAPE_EMPTY)
	{
		Log4me.Error(syntMsg);
		return;
	}

	Log4me.Warning(syntMsg);
}

Log4me.Success("Successfully.");