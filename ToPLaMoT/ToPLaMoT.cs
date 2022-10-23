using System.Linq;
using ToPLaMoT;

var lexemes = Recognizer.RecognizeFromFile("../../../Source.isa");

if (!lexemes.Any())
{
	Log4me.Warning("Missing source code.");
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