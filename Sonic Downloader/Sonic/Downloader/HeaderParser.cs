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
        private string MimeType=null;

        public HeaderParser(Downloadable File=null)
        {
            if (File != null)
                this.File = File;

            //optimize netwok connection
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
                }
                catch (Exception)
                {
                 //To Do   
                }
             

                try
                {

                    //Get File Name And File Type web server
                    GetFilePropertiesFromServer();
                }
                catch (Exception e)
                {
                    OnError?.Invoke(this, e);
                    return;
                }


                //Get explicit file Name
                ExplicitFileName();

                //Remove Illegal Character from file Name 
                RemoveIllegalCharFromName();

                //Inform User About Task Completition
                OnParseSuccess?.Invoke(this, this.File);


            });

        }

        private void RemoveIllegalCharFromName()
        {
            File.FileName = NetworkHelper.ReplaceInvalidPathCharacter(File.FileName);
        }

        private  void GetFilePropertiesFromServer()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(File.URL);
            request.Method = "GET";
            request.Proxy = null;
            request.AddRange(0,1);

            using (var response = request.GetResponse())
            {


                //Get File Name From Server from content disposition
                SetFileName(response);


                //Get File Type From Server
                SetFileType((HttpWebResponse)response);

                //Verify Status Code
                VerifyStatus(response as HttpWebResponse);

                //set content type
                GetContentType(response);
            }
        }

        private void GetContentType(WebResponse response)
        {
            if(response.ContentType!=null)
            {
                string parts=response.ContentType.Split(';')[0];
                MimeType = parts;
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
            var ext = new MimeToExtension();
            string mapValue;
            ext.mimeToExtMap.TryGetValue(MimeType, out mapValue);

            if (mapValue != null)
            {
                File.ContentType = mapValue;

                if (mapValue == ".html")
                    OnError?.Invoke(this, new Exception("Given URL seems to be a webpage ."));
            }

            if (!string.IsNullOrWhiteSpace(File.FileName))
                return;


            Uri uri = new Uri(File.URL);
            string FileNameEncoded = uri.Segments.Last();
            File.FileName = WebUtility.UrlDecode(FileNameEncoded);
            if(File.FileName.IndexOf('.')<=0)
            {
                if (File.ContentType != null)
                    File.FileName += File.ContentType;
            }
  
        }

        public delegate void OnParseSuccessHandler(object sender, Downloadable File);
        public event OnParseSuccessHandler OnParseSuccess;

        public delegate void OnErrorHandler(object sender, Exception e);
        public event OnErrorHandler OnError;
    }
}
