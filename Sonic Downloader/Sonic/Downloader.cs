﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sonic.Downloader
{
    public class Downloader
    {
        public Downloadable File { get; set; }

        private SinglePartKnownDownloader singlePartKnownDownloader;
        private SinglePartUnknownDownloader singlePartUnknownDownloader;
        private MultiPartDownloader multiPartDownloader=new MultiPartDownloader();

        public Downloader(Downloadable File=null)
        {
            //optimize the network connction
            NetworkHelper.ConnectionOptimizer();

            if (File != null)
                this.File = File;
        }

        public void Download(bool restart=false)
        {
            if (File == null)
            {
                throw new Exception("Can't download an empty file ");
            }
            if (string.IsNullOrWhiteSpace(File.FileName))
            {
                throw new Exception("FileName empty Maybe you forgot to parse the header using HeaderParser");
            }


            Task.Run(()=>
            {
                if (File.DownloadType == DownloadTypes.MultiplePartResumable)
                {
                    MultiplePartDownloader(restart);
                }
                else if (File.DownloadType == DownloadTypes.SinglePartKnownSize)
                {
                    SinglePartKnownDownload();
                }
                else
                {
                    SinglePartUnkownDownload();
                }
            });
            
        }
        public void Pause()
        {
            if (File.DownloadType == DownloadTypes.MultiplePartResumable)
            {
                multiPartDownloader.Pause();
            }
            else if (File.DownloadType == DownloadTypes.SinglePartKnownSize)
            {
                singlePartKnownDownloader.StopSingle();
            }
            else
            {
                singlePartUnknownDownloader.StopSingle();
            }
        }
        private void SinglePartKnownDownload()
        {
            singlePartKnownDownloader = new SinglePartKnownDownloader(File);
            singlePartKnownDownloader.OnDownloadStarted += SinglePartKnownDownloader_OnDownloadStarted;
            singlePartKnownDownloader.OnProgress += SinglePartKnownDownloader_OnProgress;
            singlePartKnownDownloader.OnDownloadFinished += SinglePartKnownDownloader_OnDownloadFinished;
            singlePartKnownDownloader.SingleDownload();
        }
        private void SinglePartUnkownDownload()
        {
            singlePartUnknownDownloader = new SinglePartUnknownDownloader(File);
            singlePartUnknownDownloader.OnDownloadStarted += SinglePartUnknownDownloader_OnDownloadStarted;
            singlePartUnknownDownloader.OnProgress += SinglePartUnknownDownloader_OnProgress;
            singlePartUnknownDownloader.OnDownloadFinished += SinglePartUnknownDownloader_OnDownloadFinished;

            singlePartUnknownDownloader.SingleDownload();
        }
        private void MultiplePartDownloader(bool restart=false)
        {
            //multiPartDownloader = new MultiPartDownloader(File);
            multiPartDownloader.File = File;
            //MultiDownloader Set Association
            multiPartDownloader.OnDownloadStarted += MultiPartDownloader_OnDownloadStarted;
            multiPartDownloader.OnProgress += MultiPartDownloader_OnProgress;
            multiPartDownloader.OnDownloadFinished += MultiPartDownloader_OnDownloadFinished;

            if (restart)
                File.Downloaded = 0;

            multiPartDownloader.MultiDownload();
        }

        private void MultiPartDownloader_OnDownloadFinished(object sender, FinishedEventArgs e)
        {
            OnDownloadFinished?.Invoke(this, e);
        }

        private void MultiPartDownloader_OnProgress(object sender, ProgressEventArgs e)
        {
            OnProgress?.Invoke(this, e);
        }

        private void MultiPartDownloader_OnDownloadStarted(object sender, StartedEventArgs e)
        {
            OnDownloadStarted?.Invoke(this, e);
        }

        private void SinglePartKnownDownloader_OnDownloadFinished(object sender, FinishedEventArgs e)
        {
            OnDownloadFinished?.Invoke(this, e);
        }

        private void SinglePartKnownDownloader_OnProgress(object sender, ProgressEventArgs e)
        {
            OnProgress?.Invoke(this, e);
        }

        private void SinglePartKnownDownloader_OnDownloadStarted(object sender, StartedEventArgs e)
        {
            OnDownloadStarted?.Invoke(this, e);
        }



        private void SinglePartUnknownDownloader_OnDownloadFinished(object sender, FinishedEventArgs e)
        {
            OnDownloadFinished?.Invoke(this, e);
        }

        private void SinglePartUnknownDownloader_OnProgress(object sender, ProgressEventArgs e)
        {
            OnProgress?.Invoke(this, e);
        }

        private void SinglePartUnknownDownloader_OnDownloadStarted(object sender, StartedEventArgs e)
        {
            OnDownloadStarted?.Invoke(this, e);
        }


        public delegate void ProgressHandler(object sender, Sonic.Downloader.ProgressEventArgs e);
        public event ProgressHandler OnProgress;

        public delegate void DownloadStartedHandler(object sender,StartedEventArgs e);
        public event DownloadStartedHandler OnDownloadStarted;

        public delegate void DownloadFinishedHandler(object sender, FinishedEventArgs e);
        public event DownloadFinishedHandler OnDownloadFinished;

    }
}
