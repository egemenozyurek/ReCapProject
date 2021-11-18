using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.FileManipulate
{
    public class FileManipulator : IFileManipulateService
    {
        string _destinationFolder = Path.GetFullPath(@"..\Business\Images\CarImages\");

        string myExtension = ".jpg";
        public FileInfo Add(string toBeAddedFile)
        {
            string copiedFile = $"{_destinationFolder}{NewName()}{myExtension}";

            File.Copy(toBeAddedFile, copiedFile);
            return new FileInfo(copiedFile);
        }

        public FileInfo Delete(string toBeDeletedFile)
        {
            string deletedFile = toBeDeletedFile;

            File.Delete(toBeDeletedFile);
            return new FileInfo(deletedFile);
        }

        public FileInfo Update(string toBeUpdatedFile, string newFile)
        {
            File.Delete(toBeUpdatedFile);
            var updatedFile = Add(newFile);
            return updatedFile;
        }

        private string NewName()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
