using ToPLaMoT;

Log4me.Info("recognition...");

var (listOfTokens, recognizerReportMsg) = await Recognizer.AnalyzeAsync(args[0]);

if (listOfTokens is null)
{
	Log4me.Warning(recognizerReportMsg);
	return;
}

Log4me.Info("lexical analysis...");

var (listOfLexemes, lexicalAnalyzerReportMsg) = await LexicalAnalyzer.AnalyzeAsync(listOfTokens);

if (listOfLexemes is null)
{
	Log4me.Error(lexicalAnalyzerReportMsg);
	return;
}

Log4me.Info("syntactic analysis...");

var (executionStatus, syntacticalAnalyzerReportMsg) = await SyntacticalAnalyzer.AnalyzeAsync(listOfLexemes);

if (executionStatus)
{
	Log4me.Error(syntacticalAnalyzerReportMsg);
	return;
}

Log4me.Info("interpretation...");

var interpretedCode = await Translator.AnalyzeAsync(listOfLexemes);

Log4me.Info("compilation...");

var (compilationResult, compilerReportMsg) = await Compiler.CompileAsync(interpretedCode);

if (compilationResult)
{
	Log4me.Error(compilerReportMsg);
	return;
}

Log4me.Info("launch...");

var exitCode = await Compiler.RunAsync();

Log4me.Info($"The program exited with code - {exitCode}");