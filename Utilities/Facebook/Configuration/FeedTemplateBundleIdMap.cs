using System;
using System.Xml.Serialization;

namespace Facebook.Configuration
{
    /// <summary>Defines a mapping between a locally configured Feed template bundle and the registered bundle on Facebook.</summary>
    [Serializable]
    [XmlRoot(ElementName = "feed_template_bundle_id_map")]
    public class FeedTemplateBundleIdMap
    {
        /// <summary>Gets or sets the friendly name of the template bundle.</summary>
        [XmlElement(ElementName = "name")]
        public String Name { get; set; }

        /// <summary>Gets or sets the Facebook assigned id of the registered template bundle.</summary>
        [XmlElement(ElementName = "id")]
        public Int64 Id { get; set; }
    }
}
