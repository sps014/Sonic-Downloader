using System;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;


namespace Sonic.Downloader
{
    public class HeaderParser
    {
        Downloadable File { get; set; }

        public HeaderParser(Downloadable File=null)
        {
            if (File != null)
                this.File = File;

            //optimize netwok connction
            NetworkHelper.ConnectionOptimizer();

        }

        public  void ParseHeader(Downloadable File=null)
        {
            if(File!=null)
            this.File = File;

            if(this.File==null)
            {
                throw new Exception("File was Null Supply a Downloadable to Header Parser");
            }

            //Set File size negative 
            this.File.Size = -1;

            Task.Run(() =>
            {
                try
                {
                    //Get File Size from server
                    SetFileSize();

                    //Get File Name And File Type web server
                    GetFilePropertiesFromServer();


                    //Get explicit file Name
                    ExplicitFileName();

                    //Remove Illegal Character from file Name 
                    RemoveIllegalCharFromName();

                    //Inform User About Task Completition
                    OnParseSuccess?.Invoke(this, this.File);
                }
                catch(Exception e)
                {
                    OnError?.Invoke(this, e);
                }
            });

        }

        private void RemoveIllegalCharFromName()
        {
            File.FileName = NetworkHelper.ReplaceInvalidPathCharacter(File.FileName);
        }

        private  void GetFilePropertiesFromServer()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(File.URL);
            request.Method = "HEAD";
            request.Proxy = null;
            request.AddRange(0);

            using (var response= request.GetResponse())
            {
                //Get File Name From Server from content disposition
                SetFileName(response);

                //Get File Type From Server
                SetFileType((HttpWebResponse)response);

                //Verify Status Code
                VerifyStatus(response as HttpWebResponse);
            }
        }

        private void VerifyStatus(HttpWebResponse response)
        {
            if((int)(response.StatusCode)/100!=2)
            {
                OnError?.Invoke(this, new Exception($"Web site : {File.URL} returned {response.StatusCode}"));
            }
        }

        private  void SetFileSize()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(File.URL);
            request.Method = "HEAD";
            request.Proxy = null;
            using (var response =  request.GetResponse())
            {
                File.Size = response.ContentLength;
            }
        }
        private void SetFileName(WebResponse response)
        {
            string contentDisposition = response.Headers["Content-Disposition"];
            if (contentDisposition != null)
            {
                ContentDisposition disposition = new ContentDisposition(contentDisposition);
                File.FileName = disposition.FileName;
            }
        }
        private  void SetFileType(HttpWebResponse response)
        {
            string supportRanges = response.Headers["Accept-Ranges"];
  
            if( File.Size>0)
            {
                if(response.StatusCode==HttpStatusCode.PartialContent)
                {
                    File.DownloadType = DownloadTypes.MultiplePartResumable;
                }
                else
                {
                    File.DegreeOfParallelism = 1;
                    File.DownloadType = DownloadTypes.SinglePartKnownSize;
                }
            }
            else if(supportRanges != null)
            {
                if(supportRanges.IndexOf("bytes") >= 0)
                {
                    File.DownloadType = DownloadTypes.MultiplePartResumable;
                }
                else
                {
                    File.DegreeOfParallelism = 1;
                    if (File.Size > 0)
                        File.DownloadType = DownloadTypes.SinglePartKnownSize;
                    else
                        File.DownloadType = DownloadTypes.SinglePartUnknownSize;
                }
            }
            else
            {
                File.DegreeOfParallelism = 1;

                if (File.Size <= 0)
                    File.DownloadType = DownloadTypes.SinglePartUnknownSize;
                else
                    File.DownloadType = DownloadTypes.SinglePartKnownSize;

            }
        }
        private void ExplicitFileName()
        {

            if (!string.IsNullOrWhiteSpace(File.FileName))
                return;

            Uri uri = new Uri(File.URL);
            string FileNameEncoded = uri.Segments.Last();
            File.FileName = WebUtility.UrlDecode(FileNameEncoded);

  
        }

        public delegate void OnParseSuccessHandler(object sender, Downloadable File);
        public event OnParseSuccessHandler OnParseSuccess;

        public delegate void OnErrorHandler(object sender, Exception e);
        public event OnErrorHandler OnError;
    }
}
