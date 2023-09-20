using Assembler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{
    internal class Parser
    {
        public Parser() {  }

        Dictionary<string, string> UniqueSymbolDic = new Dictionary<string, string>()
        {
            {"SP", "0"},
            {"LCL", "1" },
            {"ARG", "2" },
            {"THIS", "3" },
            {"THAT", "4" },
            {"SCREEN", "16384" },
            {"KBD", "24576" }
        };

        internal List<string> Parse(List<string> script)
        {
            List<string> output = new List<string>();

            // FIrst we register all the labels, so we already know Where they are
            Dictionary<string, string> labels = new Dictionary<string, string>();
            for (int i = 0; i < script.Count; i++)
            {
                if (script[i][0] == '(')
                {
                    string labelName = script[i].Substring(1, script[i].Length - 2);
                    labels.Add(labelName, Convert.ToString(i-labels.Count));
                }
            }

            Dictionary<string, string> variables = new Dictionary<string, string>();

            for (int i = 0; i < script.Count; i++)
            {
                char c = script[i][0];
                switch (c)
                {
                    case '@': // A-Instruction or Symbol
                        string removedAT = script[i].Substring(1);
                        if (char.IsDigit(removedAT[0]))
                        {
                            // A-Instruction (pure value)
                            output.Add(ParseAInstruction(removedAT));
                        } 
                        else if (removedAT[0] == 'R' && char.IsDigit(removedAT[1]))
                        {
                            // @R-references
                            output.Add(ParseAInstruction(script[i].Substring(2)));
                        }
                        // We must check for unique symbols before going into variables
                        else if (UniqueSymbolDic.ContainsKey(removedAT))
                        {
                            // Unique symbol references
                            string value = UniqueSymbolDic[removedAT];
                            output.Add(ParseAInstruction(value));
                        }
                        // It is important we check for label-references before going into variables
                        else if (labels.ContainsKey(removedAT))
                        {
                            // Label references
                            string value = labels[removedAT];
                            output.Add(ParseAInstruction(value));
                        }
                        else
                        {
                            // Variable Name

                            // Exists in existing variables?
                            if (!variables.ContainsKey(removedAT))
                            {
                                // Add variable
                                variables.Add(removedAT, Convert.ToString(16 + variables.Count));
                            }
                            string value = variables[removedAT];
                            output.Add(ParseAInstruction(value));
                        }

                        break;
                    case '(':
                        // Labels, simply skip
                        break;
                    default: // C-Instruction
                        CInstruction cIns = new CInstruction(script[i]);
                        output.Add(cIns.GetOutput());
                        break;
                }
            }

            // Convert Variables

            return output;
        }


        private string ParseAInstruction(string value)
        {
            string output = "0";
            output += IntToBinary(Convert.ToInt32(value));
            return output;
        }

        private string IntToBinary (int num)
        {
            return Convert.ToString(num, 2).PadLeft(15, '0');
        }
    }

    /*
        0	@END	8
        1	AB1	    AB1
        2	AB2	    AB2
        3	(GOOP)	AB4
        4	AB4	    AB6
        5	(COOL)	3
        6	AB6	    4
        7	@GOOP	AB9
        8	@COOL	AB11
        9	AB9	    AB12
        10	(END)	3
        11	AB11	8
        12	AB12	4
        13	@GOOP
        14	@END
        15	@COOL


        GOOP stays the same, because it is the First label
        COOL gets -1 because it is the second label
        END gets -2 because it is the third label


        Label has: Name, Index, (indexes of references..?)
    */
}
