namespace oroc
{
    using Nancy;

    using liboroc;

    using System.IO;
    using System.Linq;

    public class ProcessManagerModule : NancyModule
    {
        private readonly ProcessManager processManager;

        public ProcessManagerModule(ProcessManager manager)
        {
            processManager = manager;
            SetupRoutes();
        }

        private void SetupRoutes()
        {
            Get["/"] = IndexPage;
            Get["/{process}"] = ProcessPage;
        }

        private Response IndexPage(dynamic input)
        {
            return processManager.ProcessRunnerList
                .Select(proc => new
                {
                    name = Path.GetFileName(proc.ProcessOptions.Path),
                    time = proc.Stopwatch.ElapsedMilliseconds,
                    stat = proc.State
                })
                .ToArray()
                .ToJsonResponse();
        }

        private Response ProcessPage(dynamic input)
        {
            string process = input["process"];

            return processManager.ProcessRunnerList
                .Where(proc => Path.GetFileName(proc.ProcessOptions.Path) == process)
                .Select(proc => new
                {
                    time = proc.Stopwatch.ElapsedMilliseconds,
                    stat = proc.State
                })
                .ToArray()
                .ToJsonResponse();
        }
    }
}
