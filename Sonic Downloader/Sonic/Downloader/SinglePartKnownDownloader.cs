using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sonic.Downloader
{
    class SinglePartKnownDownloader
    {
        public Downloadable File { get; private set; }
        public long BufferSize { get; set; } = 1024 * 4;
        public int Timeout { get; set; } = 30 * 1000;

        CancellationTokenSource token = new CancellationTokenSource();


        public SinglePartKnownDownloader(Downloadable File)
        {
            this.File = File;
        }


        public void SingleDownload()
        {

            //Since File is non resumable delete previous parts
            NetworkHelper.DeleteFile(File.FileName);

            //reset Download Amount and Get New Token for A new Task
            File.Downloaded = 0;
            token = new CancellationTokenSource();

            StartDownloading();

        }
        public void StartDownloading()
        {
            string tempFile = File.FullFilePath;

            //cretae file stream from temp file in write append mode
            FileStream writer = new FileStream(tempFile, FileMode.Append);


            Range range = new Range() { Start = 0, End = File.Size - 1 };

            //Report that going to download
            ReportStart();

            //Stopwatch for timely reporting of the progress
            Stopwatch watch = new Stopwatch();

            //Start watch as we are going to begin download
            watch.Start();

            long downloadAmountUntilExecuted = 0;

            //get web response object from server connection
            using (var res = FileDownloadResponse(range))
            {
                //get stream object
                using (var ress = res.GetResponseStream())
                {
                    //long live buffer 
                    byte[] data = new byte[BufferSize];

                    long read = 0; // amount read in one go (fs,response stream)
                    long totalRead = 0; // total amount read from file stream (fs,response stream)

                    //download whole stream
                    while (totalRead < range.Diff)
                    {
                        if (token.IsCancellationRequested)
                            break;

                        //read web response stream
                        read = ress.Read(data, 0, data.Length);

                        if (read == 0)
                            break;

                        //update total download 
                        totalRead += read;

                        //
                        downloadAmountUntilExecuted += read;

                        //output the read buffer data
                        writer.Write(data, 0, (int)read);

                        //update downloaded amount in range and File
                        range.Downloaded += read;
                        File.Downloaded += read;

                        //If time is 1 sec show progress
                        if (watch.ElapsedMilliseconds >= 1000)
                        {
                            //Set Transfer Rate
                            File.TransferRate = downloadAmountUntilExecuted;
                            downloadAmountUntilExecuted = 0;

                            //Report progress to user 
                            ReportProgress();

                            //Restart stopwatch from beginning
                            watch.Restart();
                        }
                    }
                }


            }
            writer.Close();
            //Report End
            ReportEnd();
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

        public void StopSingle()
        {
            token.Cancel(true);
        }

        public delegate void RangeProgressHandler(object sender, ProgressEventArgs e);
        public event RangeProgressHandler OnProgress;

        public delegate void DownloadStartedHandler(object sender, StartedEventArgs e);
        public event DownloadStartedHandler OnDownloadStarted;

        public delegate void DownloadFinishedHandler(object sender, FinishedEventArgs e);
        public event DownloadFinishedHandler OnDownloadFinished;
    }
}
