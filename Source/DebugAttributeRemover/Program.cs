using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugAttributeRemover
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Usage: DebugAttributeRemover.exe \"<PATH TO EXECUTABLE>\"");

            if (args.Length == 0)
                return;

            string filePath = args[0];

            if (!File.Exists(filePath))
            {
                Console.WriteLine("ERROR - Specified assembly does not exist: " + filePath);
                return;
            }

            ModuleDefinition module = ModuleDefinition.ReadModule (filePath);

            if (!module.Assembly.HasCustomAttributes)
            {
                Console.WriteLine("ERROR - Specified assembly does not have any custom attributes.");
                return;
            }

            bool foundAttribute = false;

            var attributes = module.Assembly.CustomAttributes;
            for (int i = 0; i < attributes.Count; i++)
            {
                var attribute = attributes[i];
                if (attribute.AttributeType.Name.Contains("DebuggableAttribute"))
                {
                    Console.WriteLine("FOUND - Removing DebuggableAttribute from assembly.");
                    module.Assembly.CustomAttributes.RemoveAt(i);
                    foundAttribute = true;
                    break;
                }
            }

            if (!foundAttribute)
            {
                Console.WriteLine("ERROR - Specified assembly does not have a [Debuggable] attribute.");
                return;
            }

            string newFilePath = Path.ChangeExtension(filePath, ".Patched." + Path.GetExtension(filePath));

            module.Write(newFilePath);

            Console.WriteLine("DONE - Modified assembly saved as: " + newFilePath);
        }
    }
}
