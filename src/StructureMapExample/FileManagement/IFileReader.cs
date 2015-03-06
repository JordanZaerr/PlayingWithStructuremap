using System.Collections.Generic;

namespace StructureMapExample.FileManagement
{
    public interface IFileReader
    {
        IEnumerable<string[]> ParseFile(string filePath);
    }
}