using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QCDataHeading
{
    public class ConfigParser
    {
        private Config _config;
        private string _configPath;

        public ConfigParser(string configPath)
        {
            _configPath = configPath ?? throw new ArgumentNullException(nameof(configPath));
        }

        public Config ParseConfig()
        {
            var doc = XDocument.Load(_configPath);
            _config = new Config();

            var root = doc.Root;
            _config.OutputPath = root.Descendants("outputPath").Select(path => path.Value).FirstOrDefault();
            _config.Files = new List<QCFile>();

            if(root.Element("files").Elements("file").Count().Equals(1))
            {
                var xfile = root.Element("files").Elements("file");
                ExtractFile(xfile.First());
            }

            foreach (var xfile in root.Element("files").Elements("file"))
            {
                ExtractFile(xfile);
            }

            return _config;
        }

        private void ExtractFile(XElement xfile)
        {
            var qc_file = new QCFile
            {
                FilePath = xfile.Descendants("filePath").First().Value,
                Site = xfile.Descendants("Site").First().Value,
                Address = xfile.Descendants("Address").First().Value,
                OptiScannerSN = xfile.Descendants("OptiScannerSN").First().Value,
                OSCartidgeLot = xfile.Descendants("OSCartridgeLot").First().Value,
                QCLots = new List<string>()
            };

            foreach (var xlot in xfile.Descendants("QCLots").Elements())
            {
                qc_file.QCLots.Add(xlot.Value);
            }

            _config.Files.Add(qc_file);
        }
    }
}
