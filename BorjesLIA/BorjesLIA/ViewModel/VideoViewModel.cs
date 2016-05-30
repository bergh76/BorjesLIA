using BorjesLIA.Models.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BorjesLIA.ViewModel
{
    public class VideoViewModel
    {
        public IEnumerable<VideoModel> ListOfVideoModels { get; set; }
    }
}