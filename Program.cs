// See https://aka.ms/new-console-template for more information
using Assembler;

Console.WriteLine("Please enter the name of the .ASM file found in the /ASMFiles/ folder:");

string fileName = Console.ReadLine();
List<string> input = FileLoader.LoadFile("../../../ASMFiles/" + fileName + ".asm");

AssemblyHandler assemblyHandler = new AssemblyHandler();
List<string> result = assemblyHandler.Assemble(input);

FileWriter.CreateFile(result, fileName + ".txt", "../../../");

Console.WriteLine("Saved to " + fileName + ".txt in project folder");