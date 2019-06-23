using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sonic.Downloader
{
    class MultiPartDownloader
    {
        public Downloadable File { get; set; }
        public long BufferSize { get; set; } = 1024 * 4;
        public int Timeout { get; set; } = 30 * 1000;
        //private List<Range> RangeList { get; set; } = new List<Range>();

        private FileStream fileStream;

        private CancellationTokenSource token;

        private bool reportedOnce = false;
        public MultiPartDownloader(Downloadable File=null)
        {
            if(File!=null)
            this.File = File;
        }
        public void MultiDownload()
        {
            token = new CancellationTokenSource();
            CheckIfFirstDownload();
            fileStream = new FileStream(File.FullFilePath, FileMode.OpenOrCreate,
                FileAccess.ReadWrite, FileShare.ReadWrite);

            if (File.RangeList.Count <= 0)
                CalculateRanges();

            ParallelDownload();
        }

        private void CheckIfFirstDownload()
        {
            if(File.Downloaded<=0)
            {
                NetworkHelper.CreateNewEmptyFileOfSize(File.FullFilePath, File.Size);
            }
        }
        public void Pause()
        {
            token.Cancel(true);

        }
        private void ParallelDownload()
        {
            ParallelOptions options = new ParallelOptions() { MaxDegreeOfParallelism = (int)File.DegreeOfParallelism };


            ReportStart();

            reportedOnce = false;

            Parallel.ForEach(File.RangeList, options, range => RangeDownloader(range));

            fileStream.Close();
            ReportEnd();
        }
        private void RangeDownloader(Range range)
        {

            try
            {

                //long live buffer
                byte[] buffer = new byte[BufferSize];

                //long length=GetContentLengthProvided(File.URL, range);
                long length = File.Size;

                //Stopwatch for timely reporting of the progress
                Stopwatch watch = new Stopwatch();

                //Start watch as we are going to begin download
                watch.Start();

                long downloadedAmountTillExecuted = 0;

                //Download Whole Range data
                while (range.Downloaded < range.Diff)
                {
                    if (token.IsCancellationRequested)
                        break;

                    //Using FileDownloadResponse Get WebResponse 
                    using (var res = FileDownloadResponse(range))
                    {

                        //Length of the chunk supplied
                        length = res.ContentLength;

                        using (var ress = res.GetResponseStream())
                        {
                            long read = 0; //amount read in one go (fs,response streams)
                            long totalRead = 0; //total amount read  (fs,response streams)

                            //Buffer Data for chunking
                            byte[] data = new byte[BufferSize];


                            //iterate while total amount read is less then equal to content  length
                            while (totalRead < length)
                            {
                                if (token.IsCancellationRequested)
                                    break;

                                //Read Response Stream in chunked array and get read amount 
                                read = ress.Read(data, 0, data.Length);

                                //Write in Output Stream 
                                fileStream.Position = range.Start + range.Downloaded;
                                fileStream.Write(data, 0, (int)read);

                                //Update total read
                                totalRead += read;
                                downloadedAmountTillExecuted += read;


                                //Update Range Downloaded Amount
                                range.Downloaded += read;

                                //Update Total Amount downloaded in File
                                File.Downloaded += read;

                                //If time is 1 sec show progress
                                if (watch.ElapsedMilliseconds >= 1000)
                                {

                                    File.TransferRate = downloadedAmountTillExecuted;
                                    downloadedAmountTillExecuted = 0;

                                    // reports progress event 
                                    ReportProgress();

                                    //restart the stopwatch
                                    watch.Restart();
                                }

                            }
                        }

                    }

                }
            }
            catch(Exception e)
            {
                if (!reportedOnce)
                {
                    reportedOnce = true;
                    OnError?.Invoke(this, e);
                }
            }

        }

        private void ReportStart()
        {
            StartedEventArgs args = new StartedEventArgs();
            args.File = File;
            OnDownloadStarted?.Invoke(this, args);
        }
        private void ReportProgress()
        {
            ProgressEventArgs args = new ProgressEventArgs();
            args.RangeList = File.RangeList;
           
            args.File = File;

            OnProgress?.Invoke(this, args);
        }
        private void ReportEnd()
        {
            FinishedEventArgs e = new FinishedEventArgs();

            if (File.Downloaded == File.Size)
                e.ResultType = FinishedEventArgs.Result.Success;
            else
                e.ResultType = FinishedEventArgs.Result.Unsuccessful;

            e.File = File;

            OnDownloadFinished?.Invoke(this, e);
        }

        private void CalculateRanges()
        {
            File.RangeList.Clear();
            for (int i = 0; i < File.DegreeOfParallelism - 1; i++)
            {
                Range r = new Range();
                r.Start = (long)((i) * File.Size * 1.0f / File.DegreeOfParallelism);
                r.End = (long)((i + 1) * File.Size * 1.0f / File.DegreeOfParallelism) - 1;

                File.RangeList.Add(r);

            }

            Range end = new Range();
            if (File.RangeList.Any())
            {
                end.Start = File.RangeList.Last().End + 1;
            }
            else
                end.Start = 0;

            end.End = File.Size - 1;

            File.RangeList.Add(end);
        }
        private WebResponse FileDownloadResponse(Range range)
        {

            HttpWebRequest req = WebRequest.Create(File.URL) as HttpWebRequest;

            req.UserAgent = "Mozilla/5.0 (Windows NT 6.2; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1667.0 Safari/537.36";
            req.Timeout = Timeout;
            req.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            req.Proxy = null;

            //For Chunked downloads
            if (range.End > 0)
                req.AddRange(range.Start + range.Downloaded, range.End);

            //for Continous downloads
            else
                req.AddRange(range.Start + range.Downloaded);

            //Using GET Method for downloading
            req.Method = "GET";

            return req.GetResponse();

        }


        public delegate void RangeProgressHandler(object sender, ProgressEventArgs e);
        public event RangeProgressHandler OnProgress;

        public delegate void DownloadStartedHandler(object sender, StartedEventArgs e);
        public event DownloadStartedHandler OnDownloadStarted;
         
        public delegate void DownloadFinishedHandler(object sender, FinishedEventArgs e);
        public event DownloadFinishedHandler OnDownloadFinished;

        public delegate void OnErrorHandler(object sender, Exception e);
        public event OnErrorHandler OnError;
    }
}
