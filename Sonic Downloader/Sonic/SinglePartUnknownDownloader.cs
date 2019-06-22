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
    class SinglePartUnknownDownloader
    {
        public Downloadable File { get; private set; }
        public long BufferSize { get; set; } = 1024 * 4;
        public int Timeout { get; set; } = 30 * 1000;

        CancellationTokenSource token=new CancellationTokenSource();
        public SinglePartUnknownDownloader(Downloadable File)
        {
            this.File = File;
        }


        public void SingleDownload()
        {
            NetworkHelper.DeleteFile(File.FileName);

            //reset Download Amount and Get New Token for A new Task
            File.Downloaded = 0;
            token = new CancellationTokenSource();


            StartDownload();
        }

        public void StopSingle()
        {
            token.Cancel(true);
        }

        private async void StartDownload()
        {
            string tempFile = File.FullFilePath;

            //cretae file stream from temp file in write append mode
            FileStream writer = new FileStream(tempFile, FileMode.Append);

            HttpWebRequest req = WebRequest.Create(File.URL) as HttpWebRequest;

            req.UserAgent = "Mozilla/5.0 (Windows NT 6.2; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1667.0 Safari/537.36";
            req.Timeout = Timeout;
            req.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            req.Proxy = null;

            //Going to start download
            ReportStart();

            Stopwatch watch = new Stopwatch();
            watch.Start();

            long downloadAmountUntilExecuted = 0;

            using (var res = await req.GetResponseAsync())
            { 

                //get stream object
                using (var ress = res.GetResponseStream())
                {
                    //long live buffer 
                    byte[] data = new byte[BufferSize];

                    long read = 0; // amount read in one go (fs,response stream)
                    long totalRead = 0; // total amount read from file stream (fs,response stream)

                    //download whole stream
                    do
                    {
                        //go out of loop if cancellation Requested
                        if (token.IsCancellationRequested)
                            break;

                        //read web response stream
                        read = ress.Read(data, 0, data.Length);

                        //update total download 
                        totalRead += read;

                        //Update Download Amount (Transfer Rate)
                        downloadAmountUntilExecuted += read;

                        //output the read buffer data
                        writer.Write(data, 0, (int)read);

                        //update downloaded amount in range and File
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
                    while (read != 0);

                    //Report that download finished
                    ReportEnd();
                }

            }

            writer.Close();

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
            e.ResultType = FinishedEventArgs.Result.Success;

            e.File = File;

            OnDownloadFinished?.Invoke(this, e);
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
