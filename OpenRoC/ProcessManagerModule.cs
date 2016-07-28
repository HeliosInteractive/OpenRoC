namespace oroc
{
    using Nancy;
    using liboroc;

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
            Get["/{uri*}"] = IndexPage;
        }

        private Response IndexPage(dynamic input)
        {
            return processManager.ProcessRunnerList.ToJsonResponse();
        }
    }
}
