namespace Sonic.HttpServer
{
    public enum MIMEType
    {
        ASF,
        AVI,
        BIN,
        CSS,
        DLL,
        DMG,
        DEB,
        EXE,
        FLV,
        GIF,
        HTML,
        ICO,
        IMG,
        ISO,
        JPG,
        JS,
        MP3,
        MP4,
        MPG,
        MSI,
        PDF,
        PNG,
        RAR,
        SWF,
        TXT,
        WMV,
        XML,
        ZIP,
        NONE
    }

    public static class SonicMIMEType
    {

        public static string MIMEToString(MIMEType type)
        {
            switch (type)
            {
                case MIMEType.ASF:
                    return "video/x-ms-asf";
                case MIMEType.AVI:
                    return "video/x-msvideo";
                case MIMEType.BIN:
                    return "application/octet-stream";
                case MIMEType.CSS:
                    return "text/css";
                case MIMEType.DLL:
                    return "application/octet-stream";
                case MIMEType.DMG:
                    return "application/octet-stream";
                case MIMEType.EXE:
                    return "application/octet-stream";
                case MIMEType.FLV:
                    return "video/x-flv";
                case MIMEType.GIF:
                    return "image/gif";
                case MIMEType.HTML:
                    return "text/html";
                case MIMEType.ICO:
                    return "image/x-icon";
                case MIMEType.IMG:
                    return "application/octet-stream";
                case MIMEType.ISO:
                    return "application/octet-stream";
                case MIMEType.JPG:
                    return "image/jpeg";
                case MIMEType.JS:
                    return "application/x-javascript";
                case MIMEType.MP3:
                    return "audio/mpeg";
                case MIMEType.MP4:
                    return "video/mp4";
                case MIMEType.MPG:
                    return "video/mpeg";
                case MIMEType.MSI:
                    return "application/octet-stream";
                case MIMEType.PDF:
                    return "application/pdf";
                case MIMEType.PNG:
                    return "image/png";
                case MIMEType.RAR:
                    return "application/x-rar-compressed";
                case MIMEType.SWF:
                    return "application/x-shockwave-flash";
                case MIMEType.TXT:
                    return "text/plain";
                case MIMEType.WMV:
                    return "video/x-ms-wmv";
                case MIMEType.XML:
                    return "text/xml";
                case MIMEType.ZIP:
                    return "application/zip";
                default:
                    return null;
            }
        }
    }
}
