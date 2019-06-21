using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sonic.Downloader
{
    public static class NetworkHelper
    {
        public static void ConnectionOptimizer()
        {
            ServicePointManager.SetTcpKeepAlive(false, 0, 0);
            ServicePointManager.UseNagleAlgorithm = false;
            ServicePointManager.DefaultConnectionLimit = 1000000;
            ServicePointManager.Expect100Continue = false;
        }
        public static string DataFormatter(long data)
        {
            if (data < 1024)
                return data.ToString() + " Bytes";
            else if (data < 1024 * 1024 && data >= 1024)
                return (data / 1024.0f).ToString() + " KB";
            else if (data < 1024 * 1024 * 1024 && data >= 1024 * 1024)
                return (data / (1024 * 1024.0f)).ToString() + " MB";
            else
                return (data / (1024 * 1024 * 1024.0f)).ToString() + " GB";
        }
        public static void CreateNewEmptyFileOfSize(string fullLocalPath,long Size)
        {
            if (File.Exists(fullLocalPath))
                File.Delete(fullLocalPath);

            using(var fs=new FileStream(fullLocalPath,FileMode.Create,FileAccess.Write,FileShare.None))
            {
                fs.SetLength(Size);
            }
        }
        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
