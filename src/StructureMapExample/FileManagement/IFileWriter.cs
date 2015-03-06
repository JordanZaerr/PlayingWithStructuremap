using System.Collections.Generic;

namespace StructureMapExample.FileManagement
{
    public interface IFileWriter
    {
        void WriteFile(string path, IEnumerable<string> contents);
    }
}