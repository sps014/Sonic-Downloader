using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonic.Downloader
{
    public class ProgressEventArgs : EventArgs
    {
        public List<Range> RangeList { get; set; }

        public Downloadable File { get; set; }
    }
    public class FinishedEventArgs : ProgressEventArgs
    {
        public enum Result
        {
            Success, Unsuccessful
        }
        public Result ResultType { get; set; }
    }
    public class StartedEventArgs : ProgressEventArgs
    {
    }


}
