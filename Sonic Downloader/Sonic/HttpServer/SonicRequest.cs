using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Sonic.HttpServer
{
    public class SonicRequest
    {

        private  TcpClient client;

       

        public SonicRequest( TcpClient client, NetworkStream stream)
        {
            this.client = client;
            InputStream = stream;
            ReadRequest();
        }

        private void ReadRequest()
        {
            if (InputStream.DataAvailable)
            {
                byte[] buffer = new byte[client.Available];
                 InputStream.Read(buffer, 0, buffer.Length);
                RawRequest = Encoding.UTF8.GetString(buffer);
                if (RawRequest.Length <= 0)
                    RawRequest = null;
            }
            RequestParser();
        }

        private void RequestParser()
        {
            string req = RawRequest;
            if (req == null)
                return;
            
            string[] lines=req.Split('\n');

            MethodResolver(lines[0]);
            int i = 0;
            foreach(string s in lines)
            {
                if (s == null || s == " " || s.Length == 0||i++==0||s=="\r")
                    continue;
                ReqResolverForAll(s);
                i++;
            }

        }

        private void MethodResolver(string s)
        {
            if (s.IndexOf("GET") >= 0 && s.IndexOf("GET") < 3)
                HttpMethod = HTTPMethod.GET;

            else if (s.IndexOf("PUT") >= 0 && s.IndexOf("PUT") < 3)
                HttpMethod = HTTPMethod.PUT;

            else if (s.IndexOf("POST") >= 0 && s.IndexOf("POST") < 3)
                HttpMethod = HTTPMethod.POST;

            else if (s.IndexOf("HEAD") >= 0 && s.IndexOf("HEAD") < 3)
                HttpMethod = HTTPMethod.HEAD;

            else if (s.IndexOf("CONNECT") >= 0 && s.IndexOf("CONNECT") < 3)
                HttpMethod = HTTPMethod.CONNECT;

            else if (s.IndexOf("DELETE") >= 0 && s.IndexOf("DELETE") < 3)
                HttpMethod = HTTPMethod.DELETE;

            else if (s.IndexOf("OPTIONS") >= 0 && s.IndexOf("OPTIONS") < 3)
                HttpMethod = HTTPMethod.OPTIONS;

            else if (s.IndexOf("PATCH") >= 0 && s.IndexOf("PATCH") < 3)
                HttpMethod = HTTPMethod.PATCH;

            else if (s.IndexOf("TRACE") >= 0 && s.IndexOf("TRACE") < 3)
                HttpMethod = HTTPMethod.TRACE;
            else
                HttpMethod = HTTPMethod.UNKNOWN;

            ///URL Processsing
            if(HttpMethod!=HTTPMethod.UNKNOWN)
            {
                int beg = s.IndexOf(HttpMethod.ToString()) + 1 + 3;
                int end = s.IndexOf("HTTP/") - 1;
                if (beg > 0 && end > 0 && beg < end)
                    URL= s.Substring(beg, end - beg);
            }

            //Protocol Version Processing
            if (HttpMethod!=HTTPMethod.UNKNOWN)
            {
                int beg = s.IndexOf("HTTP/") + 5;
                int endM = beg + 1;
                int mjor = int.Parse(s.Substring(beg, 1));
                int mnor = int.Parse(s.Substring(beg+2, 1));

                ProtocolVersion = new Version(mjor,mnor);
            }

        }

        private void ReqResolverForAll(string s)
        {

            int index;

            //Add In HeaderCollection
            int colon = s.IndexOf(':');
            if (colon > 0)
            {
                string name = s.Substring(0, colon - 1);
                string val = s.Substring(colon + 1, s.Length - colon - 1);
                Headers.Add(name, val);
            }


            //Host Processing
            if ((index = s.IndexOf("Host:")) >= 0)
            {
                index += 5;
                int end = s.IndexOf('\r') - 1;
                Host= s.Substring(index, end - index + 1);

                return;
            }

            //Range Processing
            if ((index = s.IndexOf("Range:")) >= 0)
            {
                index += 6;
                int end = s.IndexOf('\r') - 1;
                Range = s.Substring(index, end - index + 1);
                return;
            }

            //Content Length processing
            if ((index = s.IndexOf("Content-Length:")) >= 0)
            {
                index += 16;
                int end = s.IndexOf('\r') - 1;
                ContentLength = s.Substring(index, end - index + 1);
                return;
            }

            //Accept-Ranges Processing
            if ((index = s.IndexOf("Accept-Ranges:")) >= 0)
            {
                index += 15;
                int end = s.IndexOf('\r') - 1;
                AcceptRanges = s.Substring(index, end - index + 1);
                return;
            }

            //Connection
            if ((index = s.IndexOf("Connection:")) >= 0)
            {
                index += 11;
                int end = s.IndexOf('\r') - 1;
                string value = s.Substring(index, end - index + 1);
                if (value.IndexOf("close") >= 0)
                    KeepAlive = false;
                else KeepAlive = true;

                return;
            }

            //Accept
            if ((index = s.IndexOf("Accept:")) >= 0)
            {
                index += 7;
                int end = s.IndexOf('\r') - 1;
                Accept= s.Substring(index, end - index + 1).Split(',');
                return;
            }

            //UserAgent
            if ((index = s.IndexOf("User-Agent:")) >= 0)
            {
                index += 11;
                int end = s.IndexOf('\r') - 1;
                UserAgent = s.Substring(index, end - index + 1);
                return;
            }

            //Content Encoding
            if ((index = s.IndexOf("Content-Encoding:")) >= 0)
            {
                index += 17;
                int end = s.IndexOf('\r') - 1;
                ContentEncoding = s.Substring(index, end - index + 1);
                return;
            }

            //Content-Type
            if ((index = s.IndexOf("Content-Type:")) >= 0)
            {
                index += 13;
                int end = s.IndexOf('\r') - 1;
                ContentType = s.Substring(index, end - index + 1);
                return;
            }

            //Referer
            if ((index = s.IndexOf("Referer:")) >= 0)
            {
                index += 9;
                int end = s.IndexOf('\r') - 1;
                UrlReferer = s.Substring(index, end - index + 1);
                return;
            }

            //Cookie Parsing
            if ((index = s.IndexOf("Cookie:")) >= 0)
            {
                index += 7;
                int end = s.IndexOf('\r') - 1;
                CookieParsing(s.Substring(index, end - index + 1));
                return;
            }


        }

        private void CookieParsing(string data)
        {
            string[] allCookies = data.Split(';');
            foreach(string s in allCookies)
            {
                string[] nameValue = s.Split('=');
                Cookies.Add(nameValue[0], nameValue[1]);
            }
        }

        public string RawRequest { get; private set; }
        public HTTPMethod HttpMethod { get; private set; }
        public NetworkStream InputStream { get; }
        public NameValueCollection Cookies { get; private set; } = new NameValueCollection();
        public NameValueCollection Headers { get; private set; } = new NameValueCollection();
        public IPEndPoint LocalEndPoint
        {
            get
            {
                return  client.Client.LocalEndPoint as IPEndPoint;
            }
        }
        public IPEndPoint RemoteEndPoint
        {
            get
            {
                return client.Client.RemoteEndPoint as IPEndPoint;
            }
        }
        public string ContentEncoding { get; private set; }
        public string ContentType { get; private set; }
        public string Host { get; private set; }
        public string Range { get;private set; }
        public string AcceptRanges { get; private set; }
        public string[] Accept { get; private set; }
        public string ContentLength { get;private set; }
        public string URL { get; private set; } = null;
        public string UrlReferer { get; private set; } = null;
        public int Port
        {
            get
            {
                return ((IPEndPoint)client.Client.LocalEndPoint).Port;
            }
        }
        public bool KeepAlive { get; private set; }
        public Version ProtocolVersion { get; private set; }
        public bool IsLocal
        {
            get
            {
                if (Host.IndexOf("localhost") >= 0 || Host.IndexOf("127.0.0.1") >= 0)
                {
                    return true;
                }
                else
                    return false;
            }
        }
        public string UserAgent { get; private set; }

    }
    public enum HTTPMethod
    {
        GET,
        POST,
        HEAD,
        PUT,
        DELETE,
        OPTIONS,
        TRACE,
        CONNECT,
        PATCH,
        UNKNOWN
    }

}
