using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{
    internal class AssemblyHandler
    {
        private InputCleaner inputCleaner;
        private Parser parser;
        public AssemblyHandler() 
        {
            inputCleaner = new InputCleaner();
            parser = new Parser();
        }

        public List<string> Assemble(List<string> rawScript)
        {
            List<string> output;

            // CLeaning the input by removing all spacing and comments
            List<string> script = inputCleaner.CleanInput(rawScript);

            // Parsing the script
            output = parser.Parse(script);

            return output;
        }
    }
}
