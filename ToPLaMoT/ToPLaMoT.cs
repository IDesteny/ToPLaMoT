using ToPLaMoT;

var lexemes = Recognizer.RecognizeFromFile("../../../Source.isa");
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

Log4me.Success("Successfully");