using System;
using System.Linq;
using Facebook.Api;

namespace Facebook.Configuration
{
    /// <summary>Defines a Feed template that will can be automatically registered when the application initializes.</summary>
    public class FeedTemplateBundleElement : IEquatable<TemplateBundle>
    {
        /// <summary>Gets or sets a friendly name for the <see cref="FeedTemplateBundleElement" />.</summary>
        public String Name { get; set; }

        /// <summary>Gets or sets the one line template for the template bundle.</summary>
        public String OneLineTemplate { get; set; }

        /// <summary>Gets or sets the optional short story template for the template bundle.</summary>
        public FeedStoryTemplateElement ShortStory { get; set; }

        /// <summary>Gets or sets the optional action link for the template bundle.</summary>
        public FeedActionLinkElement ActionLink { get; set; }

        #region [ IEquatable<TemplateBundle> Members ]

        /// <summary>Gets a <see cref="Boolean" /> value representign whether the <see cref="FeedTemplateBundleElement" /> instance contains the same
        /// data as the specified <see cref="TemplateBundle" /> object.</summary>
        /// <param name="other">A <see cref="TemplateBundle" /> object, usually returned by the Facebook API.</param>
        /// <returns><c></c></returns>
        /// <remarks><para>This method <see cref="FeedTemplateBundleElement" /> instances to be compared with <see cref="TemplateBundle" /> objects which are returned
        /// by the API so that the application can determine which template bundles need to be registered.</para></remarks>
        public Boolean Equals(TemplateBundle other)
        {            
            String otherOneLine = other.OneLineStoryTemplates.FirstOrDefault();
            Boolean otherHasShortStory = other.ShortStoryTemplates != null && other.ShortStoryTemplates.Count > 0;
            ShortStoryTemplate shortStory = other.ShortStoryTemplates.FirstOrDefault();
            String otherShortStoryTitle = null;
            String otherShortStoryBody = null;
            if(shortStory != null)
            {
                otherShortStoryTitle = shortStory.TemplateTitle;
                otherShortStoryBody = shortStory.TemplateBody;
            }
            Boolean otherHasActionLink = other.ActionLinks != null && other.ActionLinks.Count > 0;
            ActionLink otherActionLink = other.ActionLinks.FirstOrDefault();
            String otherActionLinkText = null;
            String otherActionLinkHref = null;
            if(otherActionLink != null)
            {
                otherActionLinkText = otherActionLink.Text;
                otherActionLinkHref = otherActionLink.Href;
            }
            Boolean oneline = this.OneLineTemplate == other.OneLineStoryTemplates[0];
            Boolean shortstory = this.ShortStory == null ? otherHasShortStory : (otherShortStoryTitle == this.ShortStory.Title && otherShortStoryBody == this.ShortStory.Body);
            Boolean actionlink = this.ActionLink == null ? otherHasActionLink : (otherActionLinkText == this.ActionLink.Text && otherActionLinkHref == this.ActionLink.Href);
            return oneline
                && shortstory;
                //&& actionlink;
        }

        #endregion
    }
}
