namespace Sonic.Downloader
{
    public class Range
    {
        public long Start { get; set; }
        public long End { get; set; }
        public long Downloaded { get; set; }
        public long Diff => End - Start;

        
    }
}
