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
    
    
    [FacebookObjectAttribute("page_restaurant_services")]
    public class PageRestaurantServices : Facebook.Api.FacebookObjectBase {
        
        /// <summary>Intializes an instance of <see cref="PageRestaurantServices" /> using the specified xml document or snippet as the data source.</summary>
        /// <param name="content">An <see cref="XElement" /> object containing xml data for the object.</param>
        public PageRestaurantServices(System.Xml.Linq.XElement content) : 
                base(content) {
        }
        
        /// <summary>Intializes an instance of <see cref="PageRestaurantServices" />, prefilling the internal dictionary with the specified values.</summary>
        /// <param name="dict">An <see cref="IDictionary{String, Object}" /> object containing property names and values.</param>
        public PageRestaurantServices(System.Collections.Generic.IDictionary<string, object> dict) : 
                base(dict) {
        }
        
        /// <summary>Intializes an instance of <see cref="PageRestaurantServices" />.</summary>
        public PageRestaurantServices() {
        }
        
        public Boolean Reserve {
            get {
                return this.GetValueType<Boolean>("reserve");
            }
            set {
                this.InnerDictionary["reserve"] = value;
            }
        }
        
        public Boolean Walkins {
            get {
                return this.GetValueType<Boolean>("walkins");
            }
            set {
                this.InnerDictionary["walkins"] = value;
            }
        }
        
        public Boolean Groups {
            get {
                return this.GetValueType<Boolean>("groups");
            }
            set {
                this.InnerDictionary["groups"] = value;
            }
        }
        
        public Boolean Kids {
            get {
                return this.GetValueType<Boolean>("kids");
            }
            set {
                this.InnerDictionary["kids"] = value;
            }
        }
        
        public Boolean Takeout {
            get {
                return this.GetValueType<Boolean>("takeout");
            }
            set {
                this.InnerDictionary["takeout"] = value;
            }
        }
        
        public Boolean Delivery {
            get {
                return this.GetValueType<Boolean>("delivery");
            }
            set {
                this.InnerDictionary["delivery"] = value;
            }
        }
        
        public Boolean Catering {
            get {
                return this.GetValueType<Boolean>("catering");
            }
            set {
                this.InnerDictionary["catering"] = value;
            }
        }
        
        public Boolean Waiter {
            get {
                return this.GetValueType<Boolean>("waiter");
            }
            set {
                this.InnerDictionary["waiter"] = value;
            }
        }
        
        public Boolean Outdoor {
            get {
                return this.GetValueType<Boolean>("outdoor");
            }
            set {
                this.InnerDictionary["outdoor"] = value;
            }
        }
    }
}
