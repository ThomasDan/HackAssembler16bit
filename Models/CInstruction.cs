using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Models
{
    internal class CInstruction
    {
        private string raw;
        private string foundation;
        private string dest;
        private string destBin;
        private string jump;
        private string jumpBin;
        private string aBin;
        private string comp;
        private string compBin;
        private Dictionary<string, string> compBinDic = new Dictionary<string, string>()
        {
            {"0", "101010" },
            {"1", "111111" },
            {"-1", "111010" },
            {"D", "001100" },
            {"A", "110000" },   {"M", "110000" },
            {"!D", "001101" },
            {"!A", "110001" },  {"!M", "110001" },
            {"-D", "001111" },
            {"-A", "110011" },  {"-M", "110011" },
            {"D+1", "011111" },
            {"A+1", "110111" }, {"M+1", "110111" },
            {"D-1", "001110" },
            {"A-1", "110010" }, {"M-1", "110010" },
            {"D+A", "000010" }, { "A+D", "000010" }, {"D+M", "000010" }, {"M+D", "000010" },
            {"D-A", "010011" }, {"D-M", "010011" },
            {"A-D", "000111" }, {"M-D", "000111" },
            {"D&A", "000000" }, {"D&M", "000000" },
            {"D|A", "010101" }, {"D|M", "010101" }
        };

        private Dictionary<string, string> jumpBinDic = new Dictionary<string, string>()
        {
            {"JGT", "001" },
            {"JEQ", "010" },
            {"JGE", "011" },
            {"JLT", "100" },
            {"JNE", "101" },
            {"JLE", "110" },
            {"JMP", "111" }
        };

        public CInstruction(string _raw)
        {
            raw = _raw;
            foundation = "111";

            IsolateValues();
        }

        private void IsolateValues()
        {
            IsolateDestBin();
            IsolateJumpBin();
            IsolateCompBin();
        }

        private void IsolateDestBin()
        {
            if (raw.Contains('=')) // if it contains an =, then it has a Destination
            {
                string[] destSplit = raw.Split('=');
                dest = destSplit[0];
                raw = destSplit[1];
                destBin = (dest.Contains("A") ? "1" : "0") + (dest.Contains("D") ? "1" : "0") + (dest.Contains("M") ? "1" : "0");
            } 
            else
            {
                destBin = "000";
            }
        }

        private void IsolateJumpBin()
        {
            if (raw.Contains(';')) // If it contains a ;, then it has a Jump
            {
                string[] jumpSplit = raw.Split(';');
                jump = jumpSplit[1];
                raw = jumpSplit[0];
                jumpBin = jumpBinDic[jump];
            }
            else
            {
                jumpBin = "000";
            }
        }

        private void IsolateABin()
        {
            aBin = comp.Contains('M') ? "1" : "0";
        }

        private void IsolateCompBin()
        {
            comp = raw;
            compBin = compBinDic[comp];
            IsolateABin();
        }


        public string GetOutput() 
        { 
            return foundation + aBin + compBin + destBin + jumpBin; 
        }
    }
}
