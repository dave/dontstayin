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
    
    
    [FacebookObjectAttribute("event")]
    public class Event : Facebook.Api.FacebookObjectBase {
        
        /// <summary>Intializes an instance of <see cref="Event" /> using the specified xml document or snippet as the data source.</summary>
        /// <param name="content">An <see cref="XElement" /> object containing xml data for the object.</param>
        public Event(System.Xml.Linq.XElement content) : 
                base(content) {
        }
        
        /// <summary>Intializes an instance of <see cref="Event" />, prefilling the internal dictionary with the specified values.</summary>
        /// <param name="dict">An <see cref="IDictionary{String, Object}" /> object containing property names and values.</param>
        public Event(System.Collections.Generic.IDictionary<string, object> dict) : 
                base(dict) {
        }
        
        /// <summary>Intializes an instance of <see cref="Event" />.</summary>
        public Event() {
        }
        
        public Int64 Eid {
            get {
                return this.GetValueType<Int64>("eid");
            }
            set {
                this.InnerDictionary["eid"] = value;
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
        
        public String Tagline {
            get {
                return this.GetString("tagline");
            }
            set {
                this.InnerDictionary["tagline"] = value;
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
        
        public String Host {
            get {
                return this.GetString("host");
            }
            set {
                this.InnerDictionary["host"] = value;
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
        
        public String EventType {
            get {
                return this.GetString("event_type");
            }
            set {
                this.InnerDictionary["event_type"] = value;
            }
        }
        
        public String EventSubtype {
            get {
                return this.GetString("event_subtype");
            }
            set {
                this.InnerDictionary["event_subtype"] = value;
            }
        }
        
        public DateTime StartTime {
            get {
                return this.GetValueType<DateTime>("start_time");
            }
            set {
                this.InnerDictionary["start_time"] = value;
            }
        }
        
        public DateTime EndTime {
            get {
                return this.GetValueType<DateTime>("end_time");
            }
            set {
                this.InnerDictionary["end_time"] = value;
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
        
        public String Location {
            get {
                return this.GetString("location");
            }
            set {
                this.InnerDictionary["location"] = value;
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
        
        public Boolean HideGuestList {
            get {
                return this.GetValueType<Boolean>("hide_guest_list");
            }
            set {
                this.InnerDictionary["hide_guest_list"] = value;
            }
        }
    }
}
