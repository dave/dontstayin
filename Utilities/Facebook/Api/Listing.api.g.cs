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
    
    
    [FacebookObjectAttribute("listing")]
    public class Listing : Facebook.Api.FacebookObjectBase {
        
        /// <summary>Intializes an instance of <see cref="Listing" /> using the specified xml document or snippet as the data source.</summary>
        /// <param name="content">An <see cref="XElement" /> object containing xml data for the object.</param>
        public Listing(System.Xml.Linq.XElement content) : 
                base(content) {
        }
        
        /// <summary>Intializes an instance of <see cref="Listing" />, prefilling the internal dictionary with the specified values.</summary>
        /// <param name="dict">An <see cref="IDictionary{String, Object}" /> object containing property names and values.</param>
        public Listing(System.Collections.Generic.IDictionary<string, object> dict) : 
                base(dict) {
        }
        
        /// <summary>Intializes an instance of <see cref="Listing" />.</summary>
        public Listing() {
        }
        
        public Int64 ListingId {
            get {
                return this.GetValueType<Int64>("listing_id");
            }
            set {
                this.InnerDictionary["listing_id"] = value;
            }
        }
        
        public String Url {
            get {
                return this.GetString("url");
            }
            set {
                this.InnerDictionary["url"] = value;
            }
        }
        
        public String Title {
            get {
                return this.GetString("title");
            }
            set {
                this.InnerDictionary["title"] = value;
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
        
        public Decimal Price {
            get {
                return this.GetValueType<Decimal>("price");
            }
            set {
                this.InnerDictionary["price"] = value;
            }
        }
        
        public Int64 Poster {
            get {
                return this.GetValueType<Int64>("poster");
            }
            set {
                this.InnerDictionary["poster"] = value;
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
        
        public String Category {
            get {
                return this.GetString("category");
            }
            set {
                this.InnerDictionary["category"] = value;
            }
        }
        
        public String Subcategory {
            get {
                return this.GetString("subcategory");
            }
            set {
                this.InnerDictionary["subcategory"] = value;
            }
        }
        
        public List<String> ImageUrls {
            get {
                return this.GetStringCollection("image_urls", "string");
            }
            set {
                this.InnerDictionary["image_urls"] = value;
            }
        }
        
        public Int64 Condition {
            get {
                return this.GetValueType<Int64>("condition");
            }
            set {
                this.InnerDictionary["condition"] = value;
            }
        }
        
        public String Isbn {
            get {
                return this.GetString("isbn");
            }
            set {
                this.InnerDictionary["isbn"] = value;
            }
        }
        
        public String NumBeds {
            get {
                return this.GetString("num_beds");
            }
            set {
                this.InnerDictionary["num_beds"] = value;
            }
        }
        
        public String NumMaths {
            get {
                return this.GetString("num_maths");
            }
            set {
                this.InnerDictionary["num_maths"] = value;
            }
        }
        
        public String Dogs {
            get {
                return this.GetString("dogs");
            }
            set {
                this.InnerDictionary["dogs"] = value;
            }
        }
        
        public String Cats {
            get {
                return this.GetString("cats");
            }
            set {
                this.InnerDictionary["cats"] = value;
            }
        }
        
        public String Smoking {
            get {
                return this.GetString("smoking");
            }
            set {
                this.InnerDictionary["smoking"] = value;
            }
        }
        
        public String SquareFootage {
            get {
                return this.GetString("square_footage");
            }
            set {
                this.InnerDictionary["square_footage"] = value;
            }
        }
        
        public String Street {
            get {
                return this.GetString("street");
            }
            set {
                this.InnerDictionary["street"] = value;
            }
        }
        
        public String Crossstreet {
            get {
                return this.GetString("crossstreet");
            }
            set {
                this.InnerDictionary["crossstreet"] = value;
            }
        }
        
        public String Postal {
            get {
                return this.GetString("postal");
            }
            set {
                this.InnerDictionary["postal"] = value;
            }
        }
        
        public String Rent {
            get {
                return this.GetString("rent");
            }
            set {
                this.InnerDictionary["rent"] = value;
            }
        }
        
        public String Pay {
            get {
                return this.GetString("pay");
            }
            set {
                this.InnerDictionary["pay"] = value;
            }
        }
        
        public String Full {
            get {
                return this.GetString("full");
            }
            set {
                this.InnerDictionary["full"] = value;
            }
        }
        
        public String Intern {
            get {
                return this.GetString("intern");
            }
            set {
                this.InnerDictionary["intern"] = value;
            }
        }
        
        public String Summer {
            get {
                return this.GetString("summer");
            }
            set {
                this.InnerDictionary["summer"] = value;
            }
        }
        
        public String Nonprofit {
            get {
                return this.GetString("nonprofit");
            }
            set {
                this.InnerDictionary["nonprofit"] = value;
            }
        }
        
        public String PayType {
            get {
                return this.GetString("pay_type");
            }
            set {
                this.InnerDictionary["pay_type"] = value;
            }
        }
    }
}
