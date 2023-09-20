using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{
    internal class FileWriter
    {
        internal static void CreateFile(List<string> contents, string fileName, string path)
        {
            File.WriteAllLines(path + fileName, contents);
        }
    }
}
