using ToPLaMoT;

var (listOfTokens, recognizerReportMsg) = await Recognizer.AnalyzeAsync(args[0]);

if (listOfTokens is null)
{
	Log4me.Warning(recognizerReportMsg);
	return;
}

var (listOfLexemes, lexicalAnalyzerReportMsg) = await LexicalAnalyzer.AnalyzeAsync(listOfTokens);

if (listOfLexemes is null)
{
	Log4me.Error(lexicalAnalyzerReportMsg);
	return;
}

var (executionStatus, syntacticalAnalyzerReportMsg) = await SyntacticalAnalyzer.AnalyzeAsync(listOfLexemes);

if (executionStatus)
{
	Log4me.Error(syntacticalAnalyzerReportMsg);
	return;
}

var sourceCSCode = await Translator.AnalyzeAsync(listOfLexemes);

var (exitCode, compilerReportMsg) = await Compiler.CompileAsync(sourceCSCode);

if (exitCode != 0)
{
	Log4me.Error(compilerReportMsg);
	return;
}

await Compiler.Run();