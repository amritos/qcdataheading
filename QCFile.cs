using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCDataHeading
{
    public class QCFile
    {
        public string FilePath { get; set; }

        public string Site { get; set; }

        public string Address { get; set; }

        public string OptiScannerSN { get; set; }

        public string OSCartidgeLot { get; set; }

        public List<string> QCLots { get; set; }

        public void ProcessFile(string outputPath)
        {
            if(QCLots.Count <= 0)
            {
                throw new InvalidOperationException($"Misconfigured config.xml file...missing QC Lots");
            }

            FileStream fs_in = null;

            var csv_filename = Path.GetFileNameWithoutExtension(FilePath);
            var out_csv_filename = $"{outputPath}{csv_filename}_header.csv";

            FileStream fs_out = null;
            StreamWriter sw_out = null;
            StreamReader sr_in = null;

            try
            {
                fs_in = File.OpenRead(FilePath);
                sr_in = new StreamReader(fs_in);
                fs_out = File.OpenWrite(out_csv_filename);
                sw_out = new StreamWriter(fs_out);

                var site = $"Site: {Site}";
                var address = $"Address: {Address}";

                var sn = $"OptiScanner SN: {OptiScannerSN}";
                var oslot = $"OS Cartidge Lot: {OSCartidgeLot}";

                sw_out.WriteLine(site);
                sw_out.WriteLine(address);
                sw_out.WriteLine();
                sw_out.WriteLine(sn);
                sw_out.WriteLine(oslot);

                foreach (var qc_lot in QCLots)
                {
                    sw_out.WriteLine($"QC lot:{qc_lot}");
                }

                sw_out.WriteLine();

                while(!sr_in.EndOfStream)
                {
                    sw_out.Write(sr_in.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fs_in?.Close();
                fs_in?.Dispose();

                fs_out?.Close();
                fs_out?.Dispose();
            }
        }
    }
}
