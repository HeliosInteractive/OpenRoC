namespace oroc
{
    using liboroc;

    using System.IO;
    using System.Drawing.Imaging;

    using Nancy;
    using Nancy.TinyIoc;
    using Nancy.Bootstrapper;

    internal class WebInterfaceBootstrapper : DefaultNancyBootstrapper
    {
        private readonly ProcessManager manager;
        private readonly MainDialog mainDialog;
        private readonly byte[] favIcon;

        public WebInterfaceBootstrapper(ProcessManager pmanager, MainDialog dialog)
        {
            manager = pmanager;
            mainDialog = dialog;

            using (MemoryStream ms = new MemoryStream())
            {
                Properties.Resources.phoenix.ToBitmap().Save(ms, ImageFormat.Bmp);
                favIcon = ms.ToArray();
            }
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            container.Register(manager);
            container.Register(mainDialog);

            Log.d("TinyIoC dependencies registered.");
        }

        protected override byte[] FavIcon
        {
            get { return favIcon; }
        }
    }
}
