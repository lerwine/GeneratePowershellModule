using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GeneratePowershellModule
{
    class Program
    {
        static void Main(string[] args)
        {   
            if (args.Length != 2)
            {
                Console.WriteLine(String.Format("Syntax: {0} [input xml file] [output subdirectory]",
                    Environment.GetCommandLineArgs()[0]));
                return;
            }

            ModuleGenerationConfig config;

            //try
            //{
                config = new ModuleGenerationConfig(args[0]);
            //}
            //catch (Exception exc)
            //{
            //    Console.WriteLine("Error reading input xml config file: " + exc.Message);
            //    config = null;
            //}

            if (config == null)
                return;

            //try
            //{
                config.GeneratePSModule(args[1]);
            //}
            //catch (Exception exc)
            //{
            //    Console.WriteLine("Error generating powershell module file: " + exc.Message);
            //    config = null;
            //}
        }
    }
}
