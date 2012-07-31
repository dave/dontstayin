using System;
using System.Web.UI;
using System.Collections.Generic;

namespace Facebook.Web.UI
{
    public abstract class FacebookControl : Control
    {
        public FacebookControl()
        {
            this.Attributes = new Dictionary<String, Object>();
            this.AttributeDefaultValues = new Dictionary<String, Object>();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            var tagName = "fb:" + this.TagName;
            writer.Write("<");
            writer.Write(tagName);

            foreach (var key in this.Attributes.Keys)
            {
                Object value = this.Attributes[key];
                if (value != null)
                {
                    Object defaultValue;

                    // don't render attributes assigned their default values
                    if (this.AttributeDefaultValues.TryGetValue(key, out defaultValue)
                        && value.Equals(defaultValue)) continue;
                    else writer.WriteAttribute(key, value.ToString());
                }
            }
            
            if(this.Controls.Count == 0) writer.Write(" /");
            writer.Write(">");

            if (this.Controls.Count > 0)
            {
                this.RenderChildren(writer);
                writer.Write("</");
                writer.Write(tagName);
                writer.Write(">");
            }
        }

        public abstract String TagName { get; }

        public Dictionary<String, Object> Attributes { get; set; }
        public Dictionary<String, Object> AttributeDefaultValues { get; set; }

        public void AddAttribute(String name, Object defaultValue)
        {
            this.Attributes.Add(name, defaultValue);
            if (defaultValue != null) this.AttributeDefaultValues.Add(name, defaultValue);
        }
    }
}
