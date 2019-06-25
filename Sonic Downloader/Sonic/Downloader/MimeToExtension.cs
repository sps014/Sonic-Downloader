using System.Collections.Generic;
namespace Sonic.Downloader
{
	public class MimeToExtension
	{
		public Dictionary<string, string> mimeToExtMap = new Dictionary<string, string>();
		public MimeToExtension()
		{
            mimeToExtMap.Add(@"x-world/x-3dmf", ".3dm");
            mimeToExtMap.Add(@"application/octet-stream", ".exe");
            mimeToExtMap.Add(@"application/x-authorware-bin", ".aab");
            mimeToExtMap.Add(@"application/x-authorware-map", ".aam");
            mimeToExtMap.Add(@"application/x-authorware-seg", ".aas");
            mimeToExtMap.Add(@"text/vnd.abc", ".abc");
            mimeToExtMap.Add(@"text/html", ".html");
            mimeToExtMap.Add(@"video/animaflex", ".afl");
            mimeToExtMap.Add(@"application/postscript", ".ai");
            mimeToExtMap.Add(@"audio/aiff", ".aif");
            mimeToExtMap.Add(@"audio/x-aiff", ".aif");
            mimeToExtMap.Add(@"application/x-aim", ".aim");
            mimeToExtMap.Add(@"text/x-audiosoft-intra", ".aip");
            mimeToExtMap.Add(@"application/x-navi-animation", ".ani");
            mimeToExtMap.Add(@"application/x-nokia-9000-communicator-add-on-software", ".aos");
            mimeToExtMap.Add(@"application/mime", ".aps");
            mimeToExtMap.Add(@"application/arj", ".arj");
            mimeToExtMap.Add(@"image/x-jg", ".art");
            mimeToExtMap.Add(@"video/x-ms-asf", ".asf");
            mimeToExtMap.Add(@"text/x-asm", ".asm");
            mimeToExtMap.Add(@"text/asp", ".asp");
            mimeToExtMap.Add(@"application/x-mplayer2", ".asx");
            mimeToExtMap.Add(@"video/x-ms-asf-plugin", ".asx");
            mimeToExtMap.Add(@"audio/basic", ".au");
            mimeToExtMap.Add(@"audio/x-au", ".au");
            mimeToExtMap.Add(@"application/x-troff-msvideo", ".avi");
            mimeToExtMap.Add(@"video/avi", ".avi");
            mimeToExtMap.Add(@"video/msvideo", ".avi");
            mimeToExtMap.Add(@"video/x-msvideo", ".avi");
            mimeToExtMap.Add(@"video/avs-video", ".avs");
            mimeToExtMap.Add(@"application/x-bcpio", ".bcpio");
            mimeToExtMap.Add(@"application/mac-binary", ".bin");
            mimeToExtMap.Add(@"application/macbinary", ".bin");
            mimeToExtMap.Add(@"application/x-binary", ".bin");
            mimeToExtMap.Add(@"application/x-macbinary", ".bin");
            mimeToExtMap.Add(@"image/bmp", ".bmp");
            mimeToExtMap.Add(@"image/x-windows-bmp", ".bmp");
            mimeToExtMap.Add(@"application/book", ".boo");
            mimeToExtMap.Add(@"application/x-bzip2", ".boz");
            mimeToExtMap.Add(@"application/x-bsh", ".bsh");
            mimeToExtMap.Add(@"application/x-bzip", ".bz");
            mimeToExtMap.Add(@"text/plain", ".txt");
            mimeToExtMap.Add(@"text/x-c", ".c");
            mimeToExtMap.Add(@"application/vnd.ms-pki.seccat", ".cat");
            mimeToExtMap.Add(@"application/clariscad", ".ccad");
            mimeToExtMap.Add(@"application/x-cocoa", ".cco");
            mimeToExtMap.Add(@"application/cdf", ".cdf");
            mimeToExtMap.Add(@"application/x-cdf", ".cdf");
            mimeToExtMap.Add(@"application/x-netcdf", ".cdf");
            mimeToExtMap.Add(@"application/pkix-cert", ".cer");
            mimeToExtMap.Add(@"application/x-x509-ca-cert", ".cer");
            mimeToExtMap.Add(@"application/x-chat", ".chat");
            mimeToExtMap.Add(@"application/java", ".class");
            mimeToExtMap.Add(@"application/java-byte-code", ".class");
            mimeToExtMap.Add(@"application/x-java-class", ".class");
            mimeToExtMap.Add(@"application/x-cpio", ".cpio");
            mimeToExtMap.Add(@"application/mac-compactpro", ".cpt");
            mimeToExtMap.Add(@"application/x-compactpro", ".cpt");
            mimeToExtMap.Add(@"application/x-cpt", ".cpt");
            mimeToExtMap.Add(@"application/pkcs-crl", ".crl");
            mimeToExtMap.Add(@"application/pkix-crl", ".crl");
            mimeToExtMap.Add(@"application/x-x509-user-cert", ".crt");
            mimeToExtMap.Add(@"application/x-csh", ".csh");
            mimeToExtMap.Add(@"text/x-script.csh", ".csh");
            mimeToExtMap.Add(@"application/x-pointplus", ".css");
            mimeToExtMap.Add(@"text/css", ".css");
            mimeToExtMap.Add(@"application/x-director", ".dcr");
            mimeToExtMap.Add(@"application/x-deepv", ".deepv");
            mimeToExtMap.Add(@"video/x-dv", ".dif");
            mimeToExtMap.Add(@"video/dl", ".dl");
            mimeToExtMap.Add(@"video/x-dl", ".dl");
            mimeToExtMap.Add(@"application/msword", ".doc");
            mimeToExtMap.Add(@"application/commonground", ".dp");
            mimeToExtMap.Add(@"application/drafting", ".drw");
            mimeToExtMap.Add(@"application/x-dvi", ".dvi");
            mimeToExtMap.Add(@"drawing/x-dwf (old)", ".dwf");
            mimeToExtMap.Add(@"model/vnd.dwf", ".dwf");
            mimeToExtMap.Add(@"application/acad", ".dwg");
            mimeToExtMap.Add(@"image/vnd.dwg", ".dwg");
            mimeToExtMap.Add(@"image/x-dwg", ".dwg");
            mimeToExtMap.Add(@"application/dxf", ".dxf");
            mimeToExtMap.Add(@"text/x-script.elisp", ".el");
            mimeToExtMap.Add(@"application/x-bytecode.elisp (compiled elisp)", ".elc");
            mimeToExtMap.Add(@"application/x-elc", ".elc");
            mimeToExtMap.Add(@"application/x-envoy", ".env");
            mimeToExtMap.Add(@"application/x-esrehber", ".es");
            mimeToExtMap.Add(@"text/x-setext", ".etx");
            mimeToExtMap.Add(@"application/envoy", ".evy");
            mimeToExtMap.Add(@"text/x-fortran", ".f");
            mimeToExtMap.Add(@"application/vnd.fdf", ".fdf");
            mimeToExtMap.Add(@"application/fractals", ".fif");
            mimeToExtMap.Add(@"image/fif", ".fif");
            mimeToExtMap.Add(@"video/fli", ".fli");
            mimeToExtMap.Add(@"video/x-fli", ".fli");
            mimeToExtMap.Add(@"image/florian", ".flo");
            mimeToExtMap.Add(@"text/vnd.fmi.flexstor", ".flx");
            mimeToExtMap.Add(@"video/x-atomic3d-feature", ".fmf");
            mimeToExtMap.Add(@"image/vnd.fpx", ".fpx");
            mimeToExtMap.Add(@"image/vnd.net-fpx", ".fpx");
            mimeToExtMap.Add(@"application/freeloader", ".frl");
            mimeToExtMap.Add(@"audio/make", ".funk");
            mimeToExtMap.Add(@"image/g3fax", ".g3");
            mimeToExtMap.Add(@"image/gif", ".gif");
            mimeToExtMap.Add(@"video/gl", ".gl");
            mimeToExtMap.Add(@"video/x-gl", ".gl");
            mimeToExtMap.Add(@"audio/x-gsm", ".gsd");
            mimeToExtMap.Add(@"application/x-gsp", ".gsp");
            mimeToExtMap.Add(@"application/x-gss", ".gss");
            mimeToExtMap.Add(@"application/x-gtar", ".gtar");
            mimeToExtMap.Add(@"application/x-compressed", ".gz");
            mimeToExtMap.Add(@"application/x-gzip", ".gz");
            mimeToExtMap.Add(@"multipart/x-gzip", ".gzip");
            mimeToExtMap.Add(@"text/x-h", ".h");
            mimeToExtMap.Add(@"application/x-hdf", ".hdf");
            mimeToExtMap.Add(@"application/x-helpfile", ".help");
            mimeToExtMap.Add(@"application/vnd.hp-hpgl", ".hgl");
            mimeToExtMap.Add(@"text/x-script", ".hlb");
            mimeToExtMap.Add(@"application/hlp", ".hlp");
            mimeToExtMap.Add(@"application/x-winhelp", ".hlp");
            mimeToExtMap.Add(@"application/binhex", ".hqx");
            mimeToExtMap.Add(@"application/binhex4", ".hqx");
            mimeToExtMap.Add(@"application/mac-binhex", ".hqx");
            mimeToExtMap.Add(@"application/mac-binhex40", ".hqx");
            mimeToExtMap.Add(@"application/x-binhex40", ".hqx");
            mimeToExtMap.Add(@"application/x-mac-binhex40", ".hqx");
            mimeToExtMap.Add(@"application/hta", ".hta");
            mimeToExtMap.Add(@"text/x-component", ".htc");
            mimeToExtMap.Add(@"text/webviewhtml", ".htt");
            mimeToExtMap.Add(@"x-conference/x-cooltalk", ".ice");
            mimeToExtMap.Add(@"image/x-icon", ".ico");
            mimeToExtMap.Add(@"image/ief", ".ief");
            mimeToExtMap.Add(@"application/iges", ".iges");
            mimeToExtMap.Add(@"model/iges", ".iges");
            mimeToExtMap.Add(@"application/x-ima", ".ima");
            mimeToExtMap.Add(@"application/x-httpd-imap", ".imap");
            mimeToExtMap.Add(@"application/inf", ".inf");
            mimeToExtMap.Add(@"application/x-internett-signup", ".ins");
            mimeToExtMap.Add(@"application/x-ip2", ".ip");
            mimeToExtMap.Add(@"video/x-isvideo", ".isu");
            mimeToExtMap.Add(@"audio/it", ".it");
            mimeToExtMap.Add(@"application/x-inventor", ".iv");
            mimeToExtMap.Add(@"i-world/i-vrml", ".ivr");
            mimeToExtMap.Add(@"application/x-livescreen", ".ivy");
            mimeToExtMap.Add(@"audio/x-jam", ".jam");
            mimeToExtMap.Add(@"text/x-java-source", ".jav");
            mimeToExtMap.Add(@"application/x-java-commerce", ".jcm");
            mimeToExtMap.Add(@"image/jpeg", ".jfif");
            mimeToExtMap.Add(@"image/pjpeg", ".jfif");
            mimeToExtMap.Add(@"image/x-jps", ".jps");
            mimeToExtMap.Add(@"application/x-javascript", ".js");
            mimeToExtMap.Add(@"application/javascript", ".js");
            mimeToExtMap.Add(@"application/ecmascript", ".js");
            mimeToExtMap.Add(@"text/javascript", ".js");
            mimeToExtMap.Add(@"text/ecmascript", ".js");
            mimeToExtMap.Add(@"image/jutvision", ".jut");
            mimeToExtMap.Add(@"audio/midi", ".kar");
            mimeToExtMap.Add(@"music/x-karaoke", ".kar");
            mimeToExtMap.Add(@"application/x-ksh", ".ksh");
            mimeToExtMap.Add(@"text/x-script.ksh", ".ksh");
            mimeToExtMap.Add(@"audio/nspaudio", ".la");
            mimeToExtMap.Add(@"audio/x-nspaudio", ".la");
            mimeToExtMap.Add(@"audio/x-liveaudio", ".lam");
            mimeToExtMap.Add(@"application/x-latex", ".latex");
            mimeToExtMap.Add(@"application/lha", ".lha");
            mimeToExtMap.Add(@"application/x-lha", ".lha");
            mimeToExtMap.Add(@"application/x-lisp", ".lsp");
            mimeToExtMap.Add(@"text/x-script.lisp", ".lsp");
            mimeToExtMap.Add(@"text/x-la-asf", ".lsx");
            mimeToExtMap.Add(@"application/x-lzh", ".lzh");
            mimeToExtMap.Add(@"application/lzx", ".lzx");
            mimeToExtMap.Add(@"application/x-lzx", ".lzx");
            mimeToExtMap.Add(@"text/x-m", ".m");
            mimeToExtMap.Add(@"video/mpeg", ".mp4");
            mimeToExtMap.Add(@"audio/mpeg", ".mp3");
            mimeToExtMap.Add(@"audio/x-mpequrl", ".m3u");
            mimeToExtMap.Add(@"application/x-troff-man", ".man");
            mimeToExtMap.Add(@"application/x-navimap", ".map");
            mimeToExtMap.Add(@"application/mbedlet", ".mbd");
            mimeToExtMap.Add(@"application/x-magic-cap-package-1.0", ".mc$");
            mimeToExtMap.Add(@"application/mcad", ".mcd");
            mimeToExtMap.Add(@"application/x-mathcad", ".mcd");
            mimeToExtMap.Add(@"image/vasa", ".mcf");
            mimeToExtMap.Add(@"text/mcf", ".mcf");
            mimeToExtMap.Add(@"application/netmc", ".mcp");
            mimeToExtMap.Add(@"application/x-troff-me", ".me");
            mimeToExtMap.Add(@"message/rfc822", ".mht");
            mimeToExtMap.Add(@"application/x-midi", ".mid");
            mimeToExtMap.Add(@"audio/x-mid", ".mid");
            mimeToExtMap.Add(@"audio/x-midi", ".mid");
            mimeToExtMap.Add(@"music/crescendo", ".mid");
            mimeToExtMap.Add(@"x-music/x-midi", ".mid");
            mimeToExtMap.Add(@"application/x-frame", ".mif");
            mimeToExtMap.Add(@"application/x-mif", ".mif");
            mimeToExtMap.Add(@"www/mime", ".mime");
            mimeToExtMap.Add(@"audio/x-vnd.audioexplosion.mjuicemediafile", ".mjf");
            mimeToExtMap.Add(@"video/x-motion-jpeg", ".mjpg");
            mimeToExtMap.Add(@"application/base64", ".mm");
            mimeToExtMap.Add(@"application/x-meme", ".mm");
            mimeToExtMap.Add(@"audio/mod", ".mod");
            mimeToExtMap.Add(@"audio/x-mod", ".mod");
            mimeToExtMap.Add(@"video/quicktime", ".moov");
            mimeToExtMap.Add(@"video/x-sgi-movie", ".movie");
            mimeToExtMap.Add(@"audio/x-mpeg", ".mp3");
            mimeToExtMap.Add(@"video/x-mpeg", ".mp4");
            mimeToExtMap.Add(@"video/x-mpeq2a", ".mp2");
            mimeToExtMap.Add(@"audio/mpeg3", ".mp3");
            mimeToExtMap.Add(@"audio/x-mpeg-3", ".mp3");
            mimeToExtMap.Add(@"application/x-project", ".mpc");
            mimeToExtMap.Add(@"application/vnd.ms-project", ".mpp");
            mimeToExtMap.Add(@"application/marc", ".mrc");
            mimeToExtMap.Add(@"application/x-troff-ms", ".ms");
            mimeToExtMap.Add(@"application/x-vnd.audioexplosion.mzz", ".mzz");
            mimeToExtMap.Add(@"image/naplps", ".nap");
            mimeToExtMap.Add(@"application/vnd.nokia.configuration-message", ".ncm");
            mimeToExtMap.Add(@"image/x-niff", ".nif");
            mimeToExtMap.Add(@"application/x-mix-transfer", ".nix");
            mimeToExtMap.Add(@"application/x-conference", ".nsc");
            mimeToExtMap.Add(@"application/x-navidoc", ".nvd");
            mimeToExtMap.Add(@"application/oda", ".oda");
            mimeToExtMap.Add(@"application/x-omc", ".omc");
            mimeToExtMap.Add(@"application/x-omcdatamaker", ".omcd");
            mimeToExtMap.Add(@"application/x-omcregerator", ".omcr");
            mimeToExtMap.Add(@"text/x-pascal", ".p");
            mimeToExtMap.Add(@"application/pkcs10", ".p10");
            mimeToExtMap.Add(@"application/x-pkcs10", ".p10");
            mimeToExtMap.Add(@"application/pkcs-12", ".p12");
            mimeToExtMap.Add(@"application/x-pkcs12", ".p12");
            mimeToExtMap.Add(@"application/x-pkcs7-signature", ".p7a");
            mimeToExtMap.Add(@"application/pkcs7-mime", ".p7c");
            mimeToExtMap.Add(@"application/x-pkcs7-mime", ".p7c");
            mimeToExtMap.Add(@"application/x-pkcs7-certreqresp", ".p7r");
            mimeToExtMap.Add(@"application/pkcs7-signature", ".p7s");
            mimeToExtMap.Add(@"application/pro_eng", ".part");
            mimeToExtMap.Add(@"text/pascal", ".pas");
            mimeToExtMap.Add(@"image/x-portable-bitmap", ".pbm");
            mimeToExtMap.Add(@"application/vnd.hp-pcl", ".pcl");
            mimeToExtMap.Add(@"application/x-pcl", ".pcl");
            mimeToExtMap.Add(@"image/x-pict", ".pct");
            mimeToExtMap.Add(@"image/x-pcx", ".pcx");
            mimeToExtMap.Add(@"chemical/x-pdb", ".pdb");
            mimeToExtMap.Add(@"application/pdf", ".pdf");
            mimeToExtMap.Add(@"audio/make.my.funk", ".pfunk");
            mimeToExtMap.Add(@"image/x-portable-graymap", ".pgm");
            mimeToExtMap.Add(@"image/x-portable-greymap", ".pgm");
            mimeToExtMap.Add(@"image/pict", ".pic");
            mimeToExtMap.Add(@"application/x-newton-compatible-pkg", ".pkg");
            mimeToExtMap.Add(@"application/vnd.ms-pki.pko", ".pko");
            mimeToExtMap.Add(@"text/x-script.perl", ".pl");
            mimeToExtMap.Add(@"application/x-pixclscript", ".plx");
            mimeToExtMap.Add(@"image/x-xpixmap", ".pm");
            mimeToExtMap.Add(@"text/x-script.perl-module", ".pm");
            mimeToExtMap.Add(@"application/x-pagemaker", ".pm4");
            mimeToExtMap.Add(@"image/png", ".png");
            mimeToExtMap.Add(@"application/x-portable-anymap", ".pnm");
            mimeToExtMap.Add(@"image/x-portable-anymap", ".pnm");
            mimeToExtMap.Add(@"application/mspowerpoint", ".pot");
            mimeToExtMap.Add(@"application/vnd.ms-powerpoint", ".pot");
            mimeToExtMap.Add(@"model/x-pov", ".pov");
            mimeToExtMap.Add(@"image/x-portable-pixmap", ".ppm");
            mimeToExtMap.Add(@"application/powerpoint", ".ppt");
            mimeToExtMap.Add(@"application/x-mspowerpoint", ".ppt");
            mimeToExtMap.Add(@"application/x-freelance", ".pre");
            mimeToExtMap.Add(@"paleovu/x-pv", ".pvu");
            mimeToExtMap.Add(@"text/x-script.phyton", ".py");
            mimeToExtMap.Add(@"application/x-bytecode.python", ".pyc");
            mimeToExtMap.Add(@"audio/vnd.qcelp", ".qcp");
            mimeToExtMap.Add(@"image/x-quicktime", ".qif");
            mimeToExtMap.Add(@"video/x-qtc", ".qtc");
            mimeToExtMap.Add(@"audio/x-pn-realaudio", ".ra");
            mimeToExtMap.Add(@"audio/x-pn-realaudio-plugin", ".ra");
            mimeToExtMap.Add(@"audio/x-realaudio", ".ra");
            mimeToExtMap.Add(@"application/x-cmu-raster", ".ras");
            mimeToExtMap.Add(@"image/cmu-raster", ".ras");
            mimeToExtMap.Add(@"image/x-cmu-raster", ".ras");
            mimeToExtMap.Add(@"text/x-script.rexx", ".rexx");
            mimeToExtMap.Add(@"image/vnd.rn-realflash", ".rf");
            mimeToExtMap.Add(@"image/x-rgb", ".rgb");
            mimeToExtMap.Add(@"application/vnd.rn-realmedia", ".rm");
            mimeToExtMap.Add(@"audio/mid", ".rmi");
            mimeToExtMap.Add(@"application/ringing-tones", ".rng");
            mimeToExtMap.Add(@"application/vnd.nokia.ringing-tone", ".rng");
            mimeToExtMap.Add(@"application/vnd.rn-realplayer", ".rnx");
            mimeToExtMap.Add(@"application/x-troff", ".roff");
            mimeToExtMap.Add(@"image/vnd.rn-realpix", ".rp");
            mimeToExtMap.Add(@"text/richtext", ".rt");
            mimeToExtMap.Add(@"text/vnd.rn-realtext", ".rt");
            mimeToExtMap.Add(@"application/rtf", ".rtf");
            mimeToExtMap.Add(@"application/x-rtf", ".rtf");
            mimeToExtMap.Add(@"video/vnd.rn-realvideo", ".rv");
            mimeToExtMap.Add(@"audio/s3m", ".s3m");
            mimeToExtMap.Add(@"application/x-tbook", ".sbk");
            mimeToExtMap.Add(@"application/x-lotusscreencam", ".scm");
            mimeToExtMap.Add(@"text/x-script.guile", ".scm");
            mimeToExtMap.Add(@"text/x-script.scheme", ".scm");
            mimeToExtMap.Add(@"video/x-scm", ".scm");
            mimeToExtMap.Add(@"application/sdp", ".sdp");
            mimeToExtMap.Add(@"application/x-sdp", ".sdp");
            mimeToExtMap.Add(@"application/sounder", ".sdr");
            mimeToExtMap.Add(@"application/sea", ".sea");
            mimeToExtMap.Add(@"application/x-sea", ".sea");
            mimeToExtMap.Add(@"application/set", ".set");
            mimeToExtMap.Add(@"text/sgml", ".sgm");
            mimeToExtMap.Add(@"text/x-sgml", ".sgm");
            mimeToExtMap.Add(@"application/x-sh", ".sh");
            mimeToExtMap.Add(@"application/x-shar", ".sh");
            mimeToExtMap.Add(@"text/x-script.sh", ".sh");
            mimeToExtMap.Add(@"text/x-server-parsed-html", ".shtml");
            mimeToExtMap.Add(@"audio/x-psid", ".sid");
            mimeToExtMap.Add(@"application/x-sit", ".sit");
            mimeToExtMap.Add(@"application/x-stuffit", ".sit");
            mimeToExtMap.Add(@"application/x-koan", ".skd");
            mimeToExtMap.Add(@"application/x-seelogo", ".sl");
            mimeToExtMap.Add(@"application/smil", ".smi");
            mimeToExtMap.Add(@"audio/x-adpcm", ".snd");
            mimeToExtMap.Add(@"application/solids", ".sol");
            mimeToExtMap.Add(@"application/x-pkcs7-certificates", ".spc");
            mimeToExtMap.Add(@"text/x-speech", ".spc");
            mimeToExtMap.Add(@"application/futuresplash", ".spl");
            mimeToExtMap.Add(@"application/x-sprite", ".spr");
            mimeToExtMap.Add(@"application/x-wais-source", ".src");
            mimeToExtMap.Add(@"application/streamingmedia", ".ssm");
            mimeToExtMap.Add(@"application/vnd.ms-pki.certstore", ".sst");
            mimeToExtMap.Add(@"application/step", ".step");
            mimeToExtMap.Add(@"application/sla", ".stl");
            mimeToExtMap.Add(@"application/vnd.ms-pki.stl", ".stl");
            mimeToExtMap.Add(@"application/x-navistyle", ".stl");
            mimeToExtMap.Add(@"application/x-sv4cpio", ".sv4cpio");
            mimeToExtMap.Add(@"application/x-sv4crc", ".sv4crc");
            mimeToExtMap.Add(@"application/x-world", ".svr");
            mimeToExtMap.Add(@"x-world/x-svr", ".svr");
            mimeToExtMap.Add(@"application/x-shockwave-flash", ".swf");
            mimeToExtMap.Add(@"application/x-tar", ".tar");
            mimeToExtMap.Add(@"application/toolbook", ".tbk");
            mimeToExtMap.Add(@"application/x-tcl", ".tcl");
            mimeToExtMap.Add(@"text/x-script.tcl", ".tcl");
            mimeToExtMap.Add(@"text/x-script.tcsh", ".tcsh");
            mimeToExtMap.Add(@"application/x-tex", ".tex");
            mimeToExtMap.Add(@"application/x-texinfo", ".texi");
            mimeToExtMap.Add(@"application/plain", ".text");
            mimeToExtMap.Add(@"application/gnutar", ".tgz");
            mimeToExtMap.Add(@"image/tiff", ".tif");
            mimeToExtMap.Add(@"image/x-tiff", ".tif");
            mimeToExtMap.Add(@"audio/tsp-audio", ".tsi");
            mimeToExtMap.Add(@"application/dsptype", ".tsp");
            mimeToExtMap.Add(@"audio/tsplayer", ".tsp");
            mimeToExtMap.Add(@"text/tab-separated-values", ".tsv");
            mimeToExtMap.Add(@"text/x-uil", ".uil");
            mimeToExtMap.Add(@"text/uri-list", ".uni");
            mimeToExtMap.Add(@"application/i-deas", ".unv");
            mimeToExtMap.Add(@"application/x-ustar", ".ustar");
            mimeToExtMap.Add(@"multipart/x-ustar", ".ustar");
            mimeToExtMap.Add(@"text/x-uuencode", ".uu");
            mimeToExtMap.Add(@"application/x-cdlink", ".vcd");
            mimeToExtMap.Add(@"text/x-vcalendar", ".vcs");
            mimeToExtMap.Add(@"application/vda", ".vda");
            mimeToExtMap.Add(@"video/vdo", ".vdo");
            mimeToExtMap.Add(@"application/groupwise", ".vew");
            mimeToExtMap.Add(@"video/vivo", ".viv");
            mimeToExtMap.Add(@"video/vnd.vivo", ".viv");
            mimeToExtMap.Add(@"application/vocaltec-media-desc", ".vmd");
            mimeToExtMap.Add(@"application/vocaltec-media-file", ".vmf");
            mimeToExtMap.Add(@"audio/voc", ".voc");
            mimeToExtMap.Add(@"audio/x-voc", ".voc");
            mimeToExtMap.Add(@"video/vosaic", ".vos");
            mimeToExtMap.Add(@"audio/voxware", ".vox");
            mimeToExtMap.Add(@"audio/x-twinvq-plugin", ".vqe");
            mimeToExtMap.Add(@"audio/x-twinvq", ".vqf");
            mimeToExtMap.Add(@"application/x-vrml", ".vrml");
            mimeToExtMap.Add(@"model/vrml", ".vrml");
            mimeToExtMap.Add(@"x-world/x-vrml", ".vrml");
            mimeToExtMap.Add(@"x-world/x-vrt", ".vrt");
            mimeToExtMap.Add(@"application/x-visio", ".vsd");
            mimeToExtMap.Add(@"application/wordperfect6.0", ".w60");
            mimeToExtMap.Add(@"application/wordperfect6.1", ".w61");
            mimeToExtMap.Add(@"audio/wav", ".wav");
            mimeToExtMap.Add(@"audio/x-wav", ".wav");
            mimeToExtMap.Add(@"application/x-qpro", ".wb1");
            mimeToExtMap.Add(@"image/vnd.wap.wbmp", ".wbmp");
            mimeToExtMap.Add(@"application/vnd.xara", ".web");
            mimeToExtMap.Add(@"application/x-123", ".wk1");
            mimeToExtMap.Add(@"windows/metafile", ".wmf");
            mimeToExtMap.Add(@"text/vnd.wap.wml", ".wml");
            mimeToExtMap.Add(@"application/vnd.wap.wmlc", ".wmlc");
            mimeToExtMap.Add(@"text/vnd.wap.wmlscript", ".wmls");
            mimeToExtMap.Add(@"application/vnd.wap.wmlscriptc", ".wmlsc");
            mimeToExtMap.Add(@"application/wordperfect", ".wp");
            mimeToExtMap.Add(@"application/x-wpwin", ".wpd");
            mimeToExtMap.Add(@"application/x-lotus", ".wq1");
            mimeToExtMap.Add(@"application/mswrite", ".wri");
            mimeToExtMap.Add(@"application/x-wri", ".wri");
            mimeToExtMap.Add(@"text/scriplet", ".wsc");
            mimeToExtMap.Add(@"application/x-wintalk", ".wtk");
            mimeToExtMap.Add(@"image/x-xbitmap", ".xbm");
            mimeToExtMap.Add(@"image/x-xbm", ".xbm");
            mimeToExtMap.Add(@"image/xbm", ".xbm");
            mimeToExtMap.Add(@"video/x-amt-demorun", ".xdr");
            mimeToExtMap.Add(@"xgl/drawing", ".xgz");
            mimeToExtMap.Add(@"image/vnd.xiff", ".xif");
            mimeToExtMap.Add(@"application/excel", ".xl");
            mimeToExtMap.Add(@"application/x-excel", ".xla");
            mimeToExtMap.Add(@"application/x-msexcel", ".xla");
            mimeToExtMap.Add(@"application/vnd.ms-excel", ".xlb");
            mimeToExtMap.Add(@"audio/xm", ".xm");
            mimeToExtMap.Add(@"application/xml", ".xml");
            mimeToExtMap.Add(@"text/xml", ".xml");
            mimeToExtMap.Add(@"xgl/movie", ".xmz");
            mimeToExtMap.Add(@"application/x-vnd.ls-xpix", ".xpix");
            mimeToExtMap.Add(@"image/xpm", ".xpm");
            mimeToExtMap.Add(@"video/x-amt-showrun", ".xsr");
            mimeToExtMap.Add(@"image/x-xwd", ".xwd");
            mimeToExtMap.Add(@"image/x-xwindowdump", ".xwd");
            mimeToExtMap.Add(@"application/x-compress", ".z");
            mimeToExtMap.Add(@"application/x-zip-compressed", ".zip");
            mimeToExtMap.Add(@"application/zip", ".zip");
            mimeToExtMap.Add(@"multipart/x-zip", ".zip");
            mimeToExtMap.Add(@"text/x-script.zsh", ".zsh");
        }
	}
}