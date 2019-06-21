using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Sonic.Downloader
{
    public static class SaveLoadFiles
    {
        public static string DownloadableConfigPath { get; set; } = "";
        public static void SaveFiles(List<Downloadable> files)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string ser = serializer.Serialize(files);
            StreamWriter writer = new StreamWriter(DownloadableConfigPath);
            writer.Write(ser);
            writer.Close();
        }
        public static List<Downloadable> LoadFiles()
        {
            if(!File.Exists(DownloadableConfigPath))
            {
                return new List<Downloadable>();
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            StreamReader reader = new StreamReader(DownloadableConfigPath);
            string ser = reader.ReadToEnd();
            reader.Close();
            return serializer.Deserialize<List<Downloadable>>(ser);
        }
    }
}
