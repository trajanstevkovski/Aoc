using System.Collections.Generic;
using System.IO;

namespace Aoc.Shared
{
    public class FileManager
    {
        protected string[] GetFileContent(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            return File.ReadAllLines(path);
        }

        protected string GetStringContent(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            return File.ReadAllText(path);
        }
    }
}
