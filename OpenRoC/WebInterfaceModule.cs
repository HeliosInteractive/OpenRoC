namespace oroc
{
    using Nancy;
    using Nancy.ModelBinding;

    using liboroc;

    using System.IO;
    using System.Linq;

    public class WebInterfaceModule : NancyModule
    {
        private readonly ProcessManager processManager;
        private readonly MainDialog applicationDialog;

        private enum Command
        {
            Stop,
            Start,
            Disable,
            Restore,
            Restart,
        }

        public WebInterfaceModule(ProcessManager manager, MainDialog dialog)
        {
            applicationDialog = dialog;
            processManager = manager;
            SetupRoutes();
        }

        private void SetupRoutes()
        {
            Get["/"] = IndexPage;
            Get["/{process}"] = ProcessPage;
            Post["/{process}"] = CommandPage;
            Get["/{invalid*}"] = _ => string.Empty;
        }

        private Response IndexPage(dynamic input)
        {
            return Response.AsJson(
                processManager.ProcessRunnerList
                .Select(proc => new
                {
                    name = Path.GetFileName(proc.ProcessOptions.Path),
                    time = proc.Stopwatch.ElapsedMilliseconds,
                    stat = proc.State
                })
                .ToArray());
        }

        private Response ProcessPage(dynamic input)
        {
            string process = input["process"];

            return Response.AsJson(
                processManager.ProcessRunnerList
                .Where(proc => Path.GetFileName(proc.ProcessOptions.Path) == process)
                .Select(proc => new
                {
                    time = proc.Stopwatch.ElapsedMilliseconds,
                    stat = proc.State
                })
                .ToArray());
        }

        private Response CommandPage(dynamic input)
        {
            bool executed = false;
            Command cmd = this.Bind();
            string process = input["process"];

            processManager.ProcessRunnerList
                .Where(proc => Path.GetFileName(proc.ProcessOptions.Path) == process)
                .ToList()
                .ForEach(proc =>
                {
                    if (cmd == Command.Stop)
                    {
                        applicationDialog.ExecuteOnMainThread(() => { proc.Stop(); });
                        executed = true;
                    }
                    else if (cmd == Command.Start)
                    {
                        applicationDialog.ExecuteOnMainThread(() => { proc.Start(); });
                        executed = true;
                    }
                    else if (cmd == Command.Restore)
                    {
                        applicationDialog.ExecuteOnMainThread(() => { proc.RestoreState(); });
                        executed = true;
                    }
                    else if (cmd == Command.Restart)
                    {
                        applicationDialog.ExecuteOnMainThread(() => { proc.Stop(); proc.Start(); });
                        executed = true;
                    }
                    else if (cmd == Command.Disable)
                    {
                        applicationDialog.ExecuteOnMainThread(() => { proc.State = ProcessRunner.Status.Disabled; });
                        executed = true;
                    }
                });

            return Response.AsJson(executed);
        }
    }
}
