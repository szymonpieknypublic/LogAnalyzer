using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CommandLine;
using LogsAnalyzer.BLL.Detectors;
using LogsAnalyzer.BLL.Services;
using LogsAnalyzer.DAL.Stores;
using Serilog;
using System;
using System.Diagnostics;

namespace LogsAnalyzer.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.Console()
               .WriteTo.File("logs/logs.log", rollingInterval: RollingInterval.Day)
               .CreateLogger();

            var container = new WindsorContainer();
            container.Register(
                Component.For<IDetector, MaxEventDurationDetector>(),
                Component.For<ILogsFileParser, LogsFileParser>(),
                Component.For<IEventStore, EventStore>(),
                Component.For<ILogsAnalyzer, BLL.Services.LogsAnalyzer>()
            );

            var result = Parser.Default.ParseArguments<Options>(args);
            Log.Information($"Starting application...");
            result.WithParsed(r => Run(r, container));
            Log.Information($"Exiting...");
            Log.Information($"Press any key to exit...");
            Console.ReadKey();
        }

        private static void Run(Options options, WindsorContainer container)
        {
            var sw = new Stopwatch();
            var logsAnalyzer = container.Resolve<ILogsAnalyzer>();
            try
            {
                sw.Start();
                logsAnalyzer.Analyze(options.LogsFilePath);
                sw.Stop();
                Log.Debug($"File processing duration {sw.Elapsed}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error occured while analyze log file {options.LogsFilePath}");
            }
        }
    }
}