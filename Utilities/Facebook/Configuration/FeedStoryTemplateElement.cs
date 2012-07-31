using System;
using System.Xml.Linq;
using Facebook.Json;

namespace Facebook.Configuration
{
    /// <summary>Used to define the <see cref="FeedTemplateBundleElement.ShortStory" /> property.</summary>
    public class FeedStoryTemplateElement
    {
        /// <summary>Initializes a <see cref="FeedStoryTemplateElement" /> with no data.</summary>
        public FeedStoryTemplateElement() { }

        /// <summary>Initializes a <see cref="FeedStoryTemplateElement" /> based off the specified <see cref="XElement" />.</summary>
        /// <param name="element">A <see cref="XElement" /> object containing <c>template_title</c> and <c>template_body</c> child elements.</param>
        public FeedStoryTemplateElement(XElement element)
        {
            this.Title = element.Element("title").Value;
            this.Body = element.Element("body").Value;
        }

        /// <summary>Gets or sets the title of the story template.</summary>
        [JsonProperty("template_title")]
        public String Title { get; set; }

        /// <summary>Gets or sets the body of the story template.</summary>
        [JsonProperty("template_body")]
        public String Body { get; set; }
    }
}
