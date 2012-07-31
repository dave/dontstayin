using System;
using System.Xml.Linq;
using Facebook.Json;

namespace Facebook.Configuration
{
    /// <summary>Used to define the <see cref="FeedTemplateBundleElement.ActionLink" /> property.</summary>
    public class FeedActionLinkElement
    {
        /// <summary>Initializes a <see cref="FeedActionLinkElement" /> with no data.</summary>
        public FeedActionLinkElement() { }

        /// <summary>Initializes a <see cref="FeedActionLinkElement" /> based off the specified <see cref="XElement" />.</summary>
        /// <param name="element">A <see cref="XElement" /> object containing <c>text</c> and <c>href</c> child elements.</param>
        public FeedActionLinkElement(XElement element)
        {
            this.Text = element.Element("text").Value;
            this.Href = element.Element("href").Value;
        }

        /// <summary>Gets or sets the text of the action link.</summary>
        [JsonProperty("text")]
        public String Text { get; set; }

        /// <summary>Gets or sets the href url of the action link.</summary>
        [JsonProperty("href")]
        public String Href { get; set; }
    }
}
