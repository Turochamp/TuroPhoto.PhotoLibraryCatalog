﻿using System;
using TuroPhoto.PhotoLibraryCatalog.Data;
using TuroPhoto.PhotoLibraryCatalog.Model.Interface;

namespace TuroPhoto.PhotoLibraryCatalog.Model.Service
{
    // TODO: Optimize insert by making Context read only
    // TODO: Make crawler and repository execute directories in parallel. Feasible?
    public class CatalogLibraryService : ICatalogLibraryService, IDisposable
    {
        private readonly IPhotoLibraryCrawler _photoDirectoryCrawler;
        private readonly ITuroPhotoRepository _repository;

        public CatalogLibraryService(
            IPhotoLibraryCrawler photoDirectoryCrawler,
            ITuroPhotoRepository repository)
        {
            _photoDirectoryCrawler = photoDirectoryCrawler;
            _repository = repository;
        }

        public LibraryCatalog CreateLibraryCatalog(string computerName, string directoryPath, IOutputPort outputPort)
        {
            // Find all photos. Does not read metadata.
            var (photosRead, readErrors) = _photoDirectoryCrawler.FindPhotos(
                directoryPath, outputPort, true);
            outputPort.HandleMessage($"\n{photosRead.Count} photos in source directory ({readErrors.Count} errors)");

            // TBD: Handle read errors?

            // Create catalog index with metadata
            var catalog = new LibraryCatalog(computerName, directoryPath, photosRead);
            catalog.Init();

            return catalog;
        }

        public void SaveLibraryCatalog(LibraryCatalog catalog, IOutputPort outputPort)
        {
            // Persist to database
            _repository.Insert(catalog);
            outputPort.HandleMessage($"\nPrepared saving CatalogLibrary ({catalog.Directories.Count} directories, {catalog.Photos.Count} photos)");

            // Catch exception thrown by EF Core
            var exception = _repository.TrySave();

            var message = "Saved LibraryCatalog";
            if (exception != null)
            {
                outputPort.HandleException(exception, message);
            }
            else
            {
                outputPort.HandleMessage(message);
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _repository.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
