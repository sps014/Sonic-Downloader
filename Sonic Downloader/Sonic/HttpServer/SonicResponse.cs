using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Sonic.HttpServer
{
    public class SonicResponse
    {
        private NetworkStream outPutStream;
        public NetworkStream OutputStream
        {
            get
            {
                if(!IsHeaderDeliverd&&!IsRedirectionRequest)
                {
                    IsHeaderDeliverd = true;
                    SendHeader();
                }
                return outPutStream;
            }
            private set
            {
                outPutStream = value;
            }
        }

        private  TcpClient client;

        private List<string> HeaderList = new List<string>();

        private const string EOL = "\r\n";

        public StatusCode StatusCode { get; set; } = StatusCode.OK;
        public string StatusDescription { get; set; } = null;
        public long ContentLength64 { get; set; } = 0;
        public string ContentEncoding { get; set; }
        public MIMEType ContentType { get; set; } = MIMEType.NONE;
        public string ContentRange { get; set; }
        public string AcceptRanges { get; set; }
        public bool KeepAlive { get; set; } = true;
        public Version ProtocolVersion { get; set; } =new Version(2,0);
        public bool SendChunked { get; set; } = false;
        public string RedirectLocation { get; set; }
        public List<string> Cookies { get; set; } = new List<string>();

        private bool IsHeaderDeliverd = false;
        private bool IsRedirectionRequest = false;

        public SonicResponse( TcpClient client, NetworkStream stream)
        {
            this.client = client;
            OutputStream = stream;
        }

        public void AddHeader(string name,string value)
        {
            HeaderList.Add(name + ":" + value);
        }

        public void AppendHeader(string name, string value)
        {
            HeaderList.Add(name + ":" + value);
        }

        /// <summary>
        /// Add Cookie to Collection And send it automatically to client
        /// </summary>
        /// <param name="name">Name of Cookie</param>
        /// <param name="value">Value of The Cookie</param>
        /// <param name="HttpParam">extra params like "secure" or "secure;HttpOnly;Path=''"</param>
        public void AddCookie(string name,string value,string HttpParam=null)
        {
            if (HttpParam == null)
                Cookies.Add("Set-Cookie:"+name + "=" + value);
            else
                Cookies.Add("Set-Cookie:"+name + "=" + value+";" + HttpParam);
        }

        /// <summary>
        /// Appends Cookie to Collection And send it automatically to client
        /// </summary>
        /// <param name="name">Name of Cookie</param>
        /// <param name="value">Value of The Cookie</param>
        /// <param name="HttpParam">extra params like "Secure" or multiple "Secure;HttpOnly;Path=''"</param>
        public void AppendCookie(string name, string value, string HttpParam = null)
        {
            AddCookie(name,value,HttpParam);
        }
    
        private string HeaderBuilder()
        {
            StringBuilder builder = new StringBuilder("HTTP/" + VersionString());
            builder.Append(" " + ((int)StatusCode) + " ");
            if (StatusDescription == null)
                builder.Append(SonicHTTPStatus.StatusToString(StatusCode) + EOL);
            else
                builder.Append(StatusDescription + EOL);


            if (ContentLength64 != 0)
                builder.Append("Content-Length:" + ContentLength64.ToString() + EOL);

            if (ContentType != MIMEType.NONE)
                builder.Append("Content-Type:" + SonicMIMEType.MIMEToString(ContentType) + EOL);

            if (KeepAlive)
                builder.Append("Connection:keep-alive" + EOL);
            else
                builder.Append("Connection:close" + EOL);

            if (ContentEncoding != null)
                builder.Append("Content-Encoding:" + ContentEncoding + EOL);

            if (AcceptRanges != null)
            {
                builder.Append("Accept-Ranges:" + AcceptRanges + EOL);

            }

            if (ContentRange != null)
                builder.Append("Content-Range:" + ContentRange + EOL);

            if (SendChunked)
                builder.Append("Transfer-Encoding:chunked" + EOL);

            foreach (string value in HeaderList)
            {
                builder.Append(value + EOL);
            }
            foreach(string c in Cookies)
            {
                builder.Append(c+EOL);
            }
            builder.Append(EOL);
            return builder.ToString();
        }

        private void SendHeader()
        {
            byte[] data = ToBytes(HeaderBuilder());
            OutputStream.Write(data, 0, data.Length);
        }
        
        public void Redirect()
        {
            string mainStr = "HTTP/" + VersionString()+ " " + (int)StatusCode.SEE_OTHER + " "+SonicHTTPStatus.StatusToString(StatusCode.SEE_OTHER)+"\r\n";
            mainStr += "Location:" + RedirectLocation + "\r\n\r\n";
            byte[] buffer = ToBytes(mainStr);
            IsRedirectionRequest = true;
            OutputStream.Write(buffer, 0, buffer.Length);
            OutputStream.Flush();
            OutputStream.Close();
            IsRedirectionRequest = false;
        }

        public void Abort()
        {
            client.Close();
        }

        public void Close()
        {
            OutputStream.Close();
        }

        public void Flush()
        {
            OutputStream.Flush();
        }

        /// <summary>
        /// Write to the HTTP stream
        /// </summary>
        /// <param name="msg">Message</param>
        public void Write(string msg)
        {
            try
            {
                byte[] data = ToBytes(msg);
                OutputStream.Write(data, 0, data.Length);
            }
            catch { }
        }
        /// <summary>
        /// Write to the Http Stream
        /// </summary>
        /// <param name="data">buffer data </param>
        public void Write(byte[] data)
        {
            try
            {
                OutputStream.Write(data, 0, data.Length);
            }
            catch { }
        }

        public void End(string msg)
        {
            Write(msg);
            OutputStream.Flush();
            OutputStream.Close();
        }

        public void End(byte[] data)
        {
            Write(data);
            OutputStream.Flush();
            OutputStream.Close();
        }


        private byte[] ToBytes(string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }

        private string VersionString()
        {
            return ProtocolVersion.Major + "." + ProtocolVersion.Minor;
        }

    }

}
