using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCDataHeading
{
    public class Config
    {
        private string _outputPath;
        public string OutputPath
        {
            get
            {
                return _outputPath;
            }

            set
            {
                if(!value.EndsWith(@"\"))
                {
                    _outputPath = $@"{value}\";
                }
            }
        }

        public List<QCFile> Files { get; set; }
    }
}
