using System;
using TuroPhoto.PhotoLibraryCatalog.Service;
using TuroPhoto.PhotoLibraryCatalog.View;

namespace TuroPhoto.PhotoLibraryCatalog.Controller
{
    // TODO: Make crawler and repository execute directories in parallel. Feasable?
    // x TODO: Add IDispose
    // TODO: Refactor View into more suitable model for console. What's more suitable?
    class LibraryCatalogController
    {
        public Configuration Configuration { get; set; }

        private readonly LibraryCatalogView _view;

        public LibraryCatalogController(
            LibraryCatalogView view)
        {
            _view = view;
        }

        public void InitConfiguration(string[] directoryPaths)
        {
            Configuration = new Configuration();
            Configuration.InitConfiguration();
            Configuration.DirectoryPaths = directoryPaths;
        }

        public void Run()
        {
            Starting();
            foreach (var directoryPath in Configuration.DirectoryPaths)
            {
                using (var service = Program.GetRequiredService<ICatalogLibraryService>())
                {
                    service.CreateLibraryCatalog(Configuration.ComputerName, directoryPath, _view);
                }
            }

            Closing();
        }

        private void Closing()
        {
            _view.HandleMessage("Closing TBD");
        }

        private void Starting()
        {
            var settingsMessage =
                $" Directory Path: {Configuration.Version}\n Computer: {Configuration.ComputerName}";
            var message = $"TuroPhoto starting up.\n{settingsMessage}.";

            _view.HandleMessage(message);
        }
    }
}