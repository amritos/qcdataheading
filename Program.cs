using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCDataHeading
{
    class Program
    {
        static void Main(string[] args)
        {
            string configPath = "config.xml";
            if(args.Length > 0)
            {
                configPath = args[0];
            }

            if(!File.Exists(configPath))
            {
                Console.WriteLine($"Unable to find the config.xml file here: {Path.GetFullPath(configPath)}.  Please check the location and try again.");
                Console.WriteLine($"Press any key to exit...");
                Console.ReadLine();
                return;
            }

            try
            {
                var parser = new ConfigParser(configPath);
                var config = parser.ParseConfig();

                foreach(var file in config.Files)
                {
                    Console.WriteLine($"Processing {file.FilePath}...");
                    file.ProcessFile(config.OutputPath);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Console.WriteLine($"Press ENTER to exit...");
                var _ = Console.ReadLine();
            }
            
        }
    }
}
