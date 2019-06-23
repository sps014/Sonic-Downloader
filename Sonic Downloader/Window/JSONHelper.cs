using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Sonic_Downloader.Window
{
    public static class JSONHelper
    {
        public static string DownloadableConfigPath { get; set; } = "";
        public static void SaveFiles(GeneralSettings files)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string ser = serializer.Serialize(files);
            StreamWriter writer = new StreamWriter(DownloadableConfigPath);
            writer.Write(ser);
            writer.Close();
        }
        public static GeneralSettings LoadFiles()
        {
            if (!File.Exists(DownloadableConfigPath))
            {
                return new GeneralSettings();
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            StreamReader reader = new StreamReader(DownloadableConfigPath);
            string ser = reader.ReadToEnd();
            reader.Close();
            return serializer.Deserialize<GeneralSettings>(ser);
        }
    }

}

