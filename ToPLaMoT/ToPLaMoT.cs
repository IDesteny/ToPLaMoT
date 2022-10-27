using ToPLaMoT;

var (listOfTokens, recognizerReportMsg) = await Recognizer.AnalyzeAsync(args[0]);

if (listOfTokens is null)
{
	Log4me.Warning(recognizerReportMsg);
	return;
}

var (listOfLexemes, lexicalAnalyzerReportMsg) = LexicalAnalyzer.Analyze(listOfTokens);

if (listOfLexemes is null)
{
	Log4me.Error(lexicalAnalyzerReportMsg);
	return;
}

var (executionStatus, syntacticalAnalyzerReportMsg) = SyntacticalAnalyzer.Analyze(listOfLexemes);

if (executionStatus)
{
	Log4me.Error(syntacticalAnalyzerReportMsg);
	return;
}

Log4me.Success("Successfully.");
