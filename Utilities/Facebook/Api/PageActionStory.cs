using System;
using System.Collections.Generic;

namespace Facebook.Api
{
    public class PageActionStory : FacebookArgs
    {
        public PageActionStory(String titleTemplate)
        {
            this.TitleTemplate = titleTemplate;
        }

        public String TitleTemplate
        {
            get { return (String)this["title_template"]; }
            set { this["title_template"] = value; }
        }

        public Dictionary<String, String> TitleData
        {
            get { return (Dictionary<String, String>)this["title_data"]; }
            set { this["title_data"] = value; }
        }

        public String BodyTemplate
        {
            get { return (String)this["body_template"]; }
            set { this["body_template"] = value; }
        }

        public Dictionary<String, String> BodyData
        {
            get { return (Dictionary<String, String>)this["body_data"]; }
            set { this["body_data"] = value; }
        }

        public String BodyGeneral
        {
            get { return (String)this["body_general"]; }
            set { this["body_general"] = value; }
        }

        public Int64? PageActorId
        {
            get { return this.ContainsKey("page_actor_id") ? (Int64)this["page_actor_id"] : (Int64?)null; }
            set { this["page_actor_id"] = value.HasValue ? value.Value : (Int64?)null; }
        }

        public String Image1
        {
            get { return (String)this["image_1"]; }
            set { this["image_1"] = value; }
        }

        public String Image1Link
        {
            get { return (String)this["image_1_link"]; }
            set { this["image_1_link"] = value; }
        }

        public String Image2
        {
            get { return (String)this["image_2"]; }
            set { this["image_2"] = value; }
        }

        public String Image2Link
        {
            get { return (String)this["image_2_link"]; }
            set { this["image_2_link"] = value; }
        }

        public String Image3
        {
            get { return (String)this["image_3"]; }
            set { this["image_3"] = value; }
        }

        public String Image3Link
        {
            get { return (String)this["image_3_link"]; }
            set { this["image_3_link"] = value; }
        }

        public String Image4
        {
            get { return (String)this["image_4"]; }
            set { this["image_4"] = value; }
        }

        public String Image4Link
        {
            get { return (String)this["image_4_link"]; }
            set { this["image_4_link"] = value; }
        }

        public List<Int64> TargetIds
        {
            get { return (List<Int64>)this["target_ids"]; }
            set { this["target_ids"] = value; }
        }
    }
}
