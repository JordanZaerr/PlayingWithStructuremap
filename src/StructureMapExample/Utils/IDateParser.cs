using System;

namespace StructureMapExample.Utils
{
    public interface IDateParser
    {
        DateTime GetDateTime(string value);
    }
}