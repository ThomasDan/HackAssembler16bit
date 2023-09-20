using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{
    internal class InputCleaner
    {
        /// <summary>
        /// removes spacing and comments from inputs
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal List<string> CleanInput(List<string> input) 
        {
            List<string> output = new List<string>();
            foreach (string line in input)
            {
                line.Replace(" ", "");
                string[] lines = line.Split("//");
                if (lines[0].Length > 1)
                {
                    output.Add(lines[0]);
                }
            }

            return output;
        }
    }
}
