using System;
using System.Collections.Generic;

namespace Facebook.Api
{
    public class UserActionStory : FacebookArgs
    {
        public UserActionStory(Int64 templateBundleId)
        {
            this.TemplateBundleId = templateBundleId;
            this.TemplateData = new Dictionary<String, Object>();
        }

        public Int64 TemplateBundleId
        {
            get { return (Int64)this["template_bundle_id"]; }
            set { this["template_bundle_id"] = value; }
        }

        public Dictionary<String, Object> TemplateData
        {
            get { return (Dictionary<String, Object>)this["template_data"]; }
            set { this["template_data"] = value; }
        }

        public List<Int64> TargetIds
        {
            get { return (List<Int64>)this["target_ids"]; }
            set { this["target_ids"] = value; }
        }

        public String BodyGeneral
        {
            get { return (String)this["body_general"]; }
            set { this["body_general"] = value; }
        }

        public FeedStorySize StorySize
        {
            get { return (FeedStorySize)(Byte)this["story_size"]; }
            set { this["story_size"] = (Byte)value; }
        }

        public String UserMessage
        {
            get { return (String)this["user_message"]; }
            set { this["user_message"] = value; }
        }

        public void SetCommentXid(String xid)
        {
            this.TemplateData["comment_xid"] = xid;
        }

        public void SetImage(String src, String href)
        {
            this.TemplateData["images"] = new Dictionary<String, String>
            {
                { "src", src },
                { "href", href }
            };
        }

        public void SetFlash(String swfSrc, String imgSrc)
        {
            this.TemplateData["flash"] = new Dictionary<String, String>
            {
                { "swfsrc", swfSrc },
                { "imgsrc", imgSrc }
            };
        }

        public void SetMp3(String src)
        {
            this.SetMp3(src, null, null, null);
        }

        public void SetMp3(String src, String title, String artist, String album)
        {
            var mp3 = new Dictionary<String, String>
            {
                { "src", src }
            };
            if (!String.IsNullOrEmpty(title)) mp3.Add("title", title);
            if (!String.IsNullOrEmpty(artist)) mp3.Add("artist", artist);

            this.TemplateData["mp3"] = mp3;            
        }

        public void SetVideo(String videoSrc, String previewImg)
        {
            this.SetVideo(videoSrc, previewImg, null, null, null);
        }

        public void SetVideo(String videoSrc, String previewImg, String videoTitle, String videoType, String videoLink)
        {
            var video = new Dictionary<String, String>
            {
                { "video_src", videoSrc },
                { "preview_img", previewImg }
            };
            if (!String.IsNullOrEmpty(videoTitle)) video.Add("video_title", videoTitle);
            if (!String.IsNullOrEmpty(videoType)) video.Add("video_type", videoType);
            if (!String.IsNullOrEmpty(videoLink)) video.Add("video_link", videoLink);

            this.TemplateData["video"] = video;
        }
    }
}
