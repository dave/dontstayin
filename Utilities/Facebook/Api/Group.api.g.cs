//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3082
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// ------------------------------------------------------------------------------
// <auto-generated>
//     This API code was generated by the DanDoes.NET Facebook API Generator.
//     Facebook.Api.Generator v1.0.3412.20062
//     
//     The following documents were used to generate this code:
//		Facebook API Object Schema:	http://api.facebook.com/1.0/facebook.xsd
//		Facebook API Wiki:			http://wiki.developers.facebook.com/index.php/API
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Facebook.Api {
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using Facebook;
    
    
    [FacebookObjectAttribute("group")]
    public class Group : Facebook.Api.FacebookObjectBase {
        
        /// <summary>Intializes an instance of <see cref="Group" /> using the specified xml document or snippet as the data source.</summary>
        /// <param name="content">An <see cref="XElement" /> object containing xml data for the object.</param>
        public Group(System.Xml.Linq.XElement content) : 
                base(content) {
        }
        
        /// <summary>Intializes an instance of <see cref="Group" />, prefilling the internal dictionary with the specified values.</summary>
        /// <param name="dict">An <see cref="IDictionary{String, Object}" /> object containing property names and values.</param>
        public Group(System.Collections.Generic.IDictionary<string, object> dict) : 
                base(dict) {
        }
        
        /// <summary>Intializes an instance of <see cref="Group" />.</summary>
        public Group() {
        }
        
        public Int64 Gid {
            get {
                return this.GetValueType<Int64>("gid");
            }
            set {
                this.InnerDictionary["gid"] = value;
            }
        }
        
        public String Name {
            get {
                return this.GetString("name");
            }
            set {
                this.InnerDictionary["name"] = value;
            }
        }
        
        public Int64 Nid {
            get {
                return this.GetValueType<Int64>("nid");
            }
            set {
                this.InnerDictionary["nid"] = value;
            }
        }
        
        public String Description {
            get {
                return this.GetString("description");
            }
            set {
                this.InnerDictionary["description"] = value;
            }
        }
        
        public String GroupType {
            get {
                return this.GetString("group_type");
            }
            set {
                this.InnerDictionary["group_type"] = value;
            }
        }
        
        public String GroupSubtype {
            get {
                return this.GetString("group_subtype");
            }
            set {
                this.InnerDictionary["group_subtype"] = value;
            }
        }
        
        public String RecentNews {
            get {
                return this.GetString("recent_news");
            }
            set {
                this.InnerDictionary["recent_news"] = value;
            }
        }
        
        public String Pic {
            get {
                return this.GetString("pic");
            }
            set {
                this.InnerDictionary["pic"] = value;
            }
        }
        
        public String PicBig {
            get {
                return this.GetString("pic_big");
            }
            set {
                this.InnerDictionary["pic_big"] = value;
            }
        }
        
        public String PicSmall {
            get {
                return this.GetString("pic_small");
            }
            set {
                this.InnerDictionary["pic_small"] = value;
            }
        }
        
        public Int64 Creator {
            get {
                return this.GetValueType<Int64>("creator");
            }
            set {
                this.InnerDictionary["creator"] = value;
            }
        }
        
        public DateTime UpdateTime {
            get {
                return this.GetValueType<DateTime>("update_time");
            }
            set {
                this.InnerDictionary["update_time"] = value;
            }
        }
        
        public String Office {
            get {
                return this.GetString("office");
            }
            set {
                this.InnerDictionary["office"] = value;
            }
        }
        
        public String Website {
            get {
                return this.GetString("website");
            }
            set {
                this.InnerDictionary["website"] = value;
            }
        }
        
        public Location Venue {
            get {
                return this.GetFacebookObject<Location>("venue");
            }
            set {
                this.InnerDictionary["venue"] = value;
            }
        }
        
        public String Privacy {
            get {
                return this.GetString("privacy");
            }
            set {
                this.InnerDictionary["privacy"] = value;
            }
        }
    }
}
