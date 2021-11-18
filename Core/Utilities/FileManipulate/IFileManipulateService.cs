using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.FileManipulate
{
    public interface IFileManipulateService
    {
        FileInfo Add(string toBeAddedFile);
        FileInfo Delete(string toBeDeletedFile);
        FileInfo Update(string toBeUpdatedFile, string newFile);
    }
}
