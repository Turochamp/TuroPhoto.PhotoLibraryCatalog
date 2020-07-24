﻿using System.Collections.Generic;

namespace TuroPhoto.CatalogPhotoLibraryApp.Model
{
    internal class Directory
    {
        public Directory(string path) : this()
        {
            Path = path;
            Photos = new List<Photo>();
        }

        public Directory() { }


        public int Id { get; }
        public string Path { get; }
        public List<Photo> Photos { get; private set; }
        public string RelativePath { get; internal set; }

        internal void Add(Photo photo)
        {
            Photos.Add(photo);
        }

        public override string ToString()
        {
            return Path;
        }
    }
}