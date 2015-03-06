using System.Collections.Generic;
using System.IO;

namespace StructureMapExample.FileManagement
{
    public class FileWriter : IFileWriter
    {
        public void WriteFile(string path, IEnumerable<string> contents)
        {
            using (var fileWriter = new StreamWriter(path))
            {
                foreach (var line in contents)
                {
                    fileWriter.WriteLine(line);
                }
            }
        }
    }
}