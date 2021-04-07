using CommandLine;

namespace LogsAnalyzer.Client
{
    internal class Options
    {
        [Option('f', "file", Required = true, HelpText = "Path to json file with logs")]
        public string LogsFilePath { get; set; }
    }
}