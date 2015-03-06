using System;
using System.Collections.Generic;
using System.IO;

namespace StructureMapExample.FileManagement
{
    public class DelimitedFileReader : IFileReader
    {
        private readonly char _delimiter;

        public DelimitedFileReader(char delimiter)
        {
            _delimiter = delimiter;
        }

        public IEnumerable<string[]> ParseFile(string filePath)
        {
            var parsedValues = new List<string[]>();

            using (var file = File.Open(filePath, FileMode.Open))
            using(var stream = new StreamReader(file))
            {
                var line = stream.ReadLine();
                while (!String.IsNullOrWhiteSpace(line))
                {
                    parsedValues.Add(Split(line));
                    line = stream.ReadLine();
                }
            }
            return parsedValues;
        }

        private string[] Split(string str)
        {
            return str.Split(new[] {_delimiter}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}