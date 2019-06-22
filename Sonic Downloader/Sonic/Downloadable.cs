using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonic.Downloader
{
    public class Downloadable
    {
        public string URL { get; set; }
        public long Size { get; set; }

        public long Downloaded { get; set; }
        public long TransferRate { get; set; }
        public double Percent => Downloaded * 1.0f / Size;

        public string FileName { get; set; } = "";
        public string FilePath { get; set; } = @"C:\Users\shive\OneDrive\Documents\Visual Studio 2019\Projects\Sonic Downloader\Sonic Downloader\bin\Debug";

        public string FullFilePath => System.IO.Path.Combine(FilePath, FileName);

        public uint DegreeOfParallelism { get; set; } = 8;

        public string Referer { get; set; }
        public string Description { get; set; } = " ";

        public List<Range> RangeList { get; set; } = new List<Range>();

        public bool Completed => DownloadType != DownloadTypes.SinglePartUnknownSize ? (Downloaded >= Size ? true : false ): true;

        public DownloadTypes DownloadType { get; set; }

    }
    public enum DownloadTypes
    {
        MultiplePartResumable,
        SinglePartKnownSize,
        SinglePartUnknownSize
    }
}
