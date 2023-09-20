using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{
    internal class FileLoader
    {
        internal static List<string> LoadFile(string path)
        {
            List<string> list = File.ReadAllLines(path).ToList();
            return list;
        }
    }
}
