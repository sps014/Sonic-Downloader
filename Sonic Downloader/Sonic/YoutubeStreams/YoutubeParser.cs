using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;

namespace Sonic.YoutubeStreams
{
    public class YoutubeParser
    {
        public async Task<VideoInfo> GetVideoInfo(string URL)
        {
            string id=null;
            bool res=YoutubeClient.TryParseVideoId(URL,out id);
            if(!res)
            {
                return null;
            }
            else
            {
                YoutubeClient client = new YoutubeClient();
                var streamSet= await client.GetVideoMediaStreamInfosAsync(id);
                var video = await client.GetVideoAsync(id);
                VideoInfo info = new VideoInfo() { Video = video, MediaStreamInfoSet = streamSet };

                return info;
            }
        }
        public class VideoInfo
        {
            public MediaStreamInfoSet MediaStreamInfoSet { get; set; }
            public Video Video { get; set; }
        }
    }
}
