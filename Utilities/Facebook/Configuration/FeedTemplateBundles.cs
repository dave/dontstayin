using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Facebook.Configuration
{
    internal class FeedTemplateBundles : List<FeedTemplateBundleElement>
    {
        public FeedTemplateBundles(XDocument doc)
        {
            var query = 
                from templateElement in doc.Root.Elements()
                select new FeedTemplateBundleElement
                {
                    Name = templateElement.Attribute("name").Value,
                    OneLineTemplate = this.HasElement(templateElement, "oneLineTemplate") ? templateElement.Element("oneLineTemplate").Value : null,
                    ShortStory = this.HasElement(templateElement, "shortStory") ? new FeedStoryTemplateElement(templateElement.Element("shortStory")) : null,
                    ActionLink = this.HasElement(templateElement, "actionLink") ? new FeedActionLinkElement(templateElement.Element("actionLink")) : null
                };
            this.AddRange(query);
        }

        private Boolean HasElement(XElement element, String name)
        {
            return element.Element(name) != null;
        }

        public static List<FeedTemplateBundleElement> Get()
        {
            var config = FacebookSection.Current;
            if (String.IsNullOrEmpty(config.FeedTemplateConfigSource) ||
                !File.Exists(FacebookUtility.MapPath(config.FeedTemplateConfigSource))) return new List<FeedTemplateBundleElement>();
            else
            {
                var result = new FeedTemplateBundles(XDocument.Load(FacebookUtility.MapPath(config.FeedTemplateConfigSource)));
                return (List<FeedTemplateBundleElement>)result;
            }
        }
    }
}
