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
    
    
    [FacebookObjectAttribute("page")]
    public class Page : Facebook.Api.FacebookObjectBase {
        
        /// <summary>Intializes an instance of <see cref="Page" /> using the specified xml document or snippet as the data source.</summary>
        /// <param name="content">An <see cref="XElement" /> object containing xml data for the object.</param>
        public Page(System.Xml.Linq.XElement content) : 
                base(content) {
        }
        
        /// <summary>Intializes an instance of <see cref="Page" />, prefilling the internal dictionary with the specified values.</summary>
        /// <param name="dict">An <see cref="IDictionary{String, Object}" /> object containing property names and values.</param>
        public Page(System.Collections.Generic.IDictionary<string, object> dict) : 
                base(dict) {
        }
        
        /// <summary>Intializes an instance of <see cref="Page" />.</summary>
        public Page() {
        }
        
        public Int64 PageId {
            get {
                return this.GetValueType<Int64>("page_id");
            }
            set {
                this.InnerDictionary["page_id"] = value;
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
        
        public String PicSmall {
            get {
                return this.GetString("pic_small");
            }
            set {
                this.InnerDictionary["pic_small"] = value;
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
        
        public String PicSquare {
            get {
                return this.GetString("pic_square");
            }
            set {
                this.InnerDictionary["pic_square"] = value;
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
        
        public String PicLarge {
            get {
                return this.GetString("pic_large");
            }
            set {
                this.InnerDictionary["pic_large"] = value;
            }
        }
        
        public String PageUrl {
            get {
                return this.GetString("page_url");
            }
            set {
                this.InnerDictionary["page_url"] = value;
            }
        }
        
        public UserStatus Status {
            get {
                return this.GetFacebookObject<UserStatus>("status");
            }
            set {
                this.InnerDictionary["status"] = value;
            }
        }
        
        public String Type {
            get {
                return this.GetString("type");
            }
            set {
                this.InnerDictionary["type"] = value;
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
        
        public Boolean HasAddedApp {
            get {
                return this.GetValueType<Boolean>("has_added_app");
            }
            set {
                this.InnerDictionary["has_added_app"] = value;
            }
        }
        
        public String Founded {
            get {
                return this.GetString("founded");
            }
            set {
                this.InnerDictionary["founded"] = value;
            }
        }
        
        public String CompanyOverview {
            get {
                return this.GetString("company_overview");
            }
            set {
                this.InnerDictionary["company_overview"] = value;
            }
        }
        
        public String Mission {
            get {
                return this.GetString("mission");
            }
            set {
                this.InnerDictionary["mission"] = value;
            }
        }
        
        public String Products {
            get {
                return this.GetString("products");
            }
            set {
                this.InnerDictionary["products"] = value;
            }
        }
        
        public Location Location {
            get {
                return this.GetFacebookObject<Location>("location");
            }
            set {
                this.InnerDictionary["location"] = value;
            }
        }
        
        public PageParking Parking {
            get {
                return this.GetFacebookObject<PageParking>("parking");
            }
            set {
                this.InnerDictionary["parking"] = value;
            }
        }
        
        public String PublicTransit {
            get {
                return this.GetString("public_transit");
            }
            set {
                this.InnerDictionary["public_transit"] = value;
            }
        }
        
        public PageHours Hours {
            get {
                return this.GetFacebookObject<PageHours>("hours");
            }
            set {
                this.InnerDictionary["hours"] = value;
            }
        }
        
        public String Attire {
            get {
                return this.GetString("attire");
            }
            set {
                this.InnerDictionary["attire"] = value;
            }
        }
        
        public String PaymentOptions {
            get {
                return this.GetString("payment_options");
            }
            set {
                this.InnerDictionary["payment_options"] = value;
            }
        }
        
        public String CulinaryTeam {
            get {
                return this.GetString("culinary_team");
            }
            set {
                this.InnerDictionary["culinary_team"] = value;
            }
        }
        
        public String GeneralManager {
            get {
                return this.GetString("general_manager");
            }
            set {
                this.InnerDictionary["general_manager"] = value;
            }
        }
        
        public String PriceRange {
            get {
                return this.GetString("price_range");
            }
            set {
                this.InnerDictionary["price_range"] = value;
            }
        }
        
        public PageRestaurantServices RestaurantServices {
            get {
                return this.GetFacebookObject<PageRestaurantServices>("restaurant_services");
            }
            set {
                this.InnerDictionary["restaurant_services"] = value;
            }
        }
        
        public PageRestaurantSpecialties RestaurantSpecialties {
            get {
                return this.GetFacebookObject<PageRestaurantSpecialties>("restaurant_specialties");
            }
            set {
                this.InnerDictionary["restaurant_specialties"] = value;
            }
        }
        
        public String ReleaseDate {
            get {
                return this.GetString("release_date");
            }
            set {
                this.InnerDictionary["release_date"] = value;
            }
        }
        
        public String Genre {
            get {
                return this.GetString("genre");
            }
            set {
                this.InnerDictionary["genre"] = value;
            }
        }
        
        public String Starring {
            get {
                return this.GetString("starring");
            }
            set {
                this.InnerDictionary["starring"] = value;
            }
        }
        
        public String ScreenplayBy {
            get {
                return this.GetString("screenplay_by");
            }
            set {
                this.InnerDictionary["screenplay_by"] = value;
            }
        }
        
        public String DirectedBy {
            get {
                return this.GetString("directed_by");
            }
            set {
                this.InnerDictionary["directed_by"] = value;
            }
        }
        
        public String ProducedBy {
            get {
                return this.GetString("produced_by");
            }
            set {
                this.InnerDictionary["produced_by"] = value;
            }
        }
        
        public String Studio {
            get {
                return this.GetString("studio");
            }
            set {
                this.InnerDictionary["studio"] = value;
            }
        }
        
        public String Awards {
            get {
                return this.GetString("awards");
            }
            set {
                this.InnerDictionary["awards"] = value;
            }
        }
        
        public String PlotOutline {
            get {
                return this.GetString("plot_outline");
            }
            set {
                this.InnerDictionary["plot_outline"] = value;
            }
        }
        
        public String Network {
            get {
                return this.GetString("network");
            }
            set {
                this.InnerDictionary["network"] = value;
            }
        }
        
        public String Season {
            get {
                return this.GetString("season");
            }
            set {
                this.InnerDictionary["season"] = value;
            }
        }
        
        public String Schedule {
            get {
                return this.GetString("schedule");
            }
            set {
                this.InnerDictionary["schedule"] = value;
            }
        }
        
        public String WrittenBy {
            get {
                return this.GetString("written_by");
            }
            set {
                this.InnerDictionary["written_by"] = value;
            }
        }
        
        public String BandMembers {
            get {
                return this.GetString("band_members");
            }
            set {
                this.InnerDictionary["band_members"] = value;
            }
        }
        
        public String Hometown {
            get {
                return this.GetString("hometown");
            }
            set {
                this.InnerDictionary["hometown"] = value;
            }
        }
        
        public String CurrentLocation {
            get {
                return this.GetString("current_location");
            }
            set {
                this.InnerDictionary["current_location"] = value;
            }
        }
        
        public String RecordLabel {
            get {
                return this.GetString("record_label");
            }
            set {
                this.InnerDictionary["record_label"] = value;
            }
        }
        
        public String BookingAgent {
            get {
                return this.GetString("booking_agent");
            }
            set {
                this.InnerDictionary["booking_agent"] = value;
            }
        }
        
        public String ArtistsWeLike {
            get {
                return this.GetString("artists_we_like");
            }
            set {
                this.InnerDictionary["artists_we_like"] = value;
            }
        }
        
        public String Influences {
            get {
                return this.GetString("influences");
            }
            set {
                this.InnerDictionary["influences"] = value;
            }
        }
        
        public String BandInterests {
            get {
                return this.GetString("band_interests");
            }
            set {
                this.InnerDictionary["band_interests"] = value;
            }
        }
        
        public String Bio {
            get {
                return this.GetString("bio");
            }
            set {
                this.InnerDictionary["bio"] = value;
            }
        }
        
        public String Affiliation {
            get {
                return this.GetString("affiliation");
            }
            set {
                this.InnerDictionary["affiliation"] = value;
            }
        }
        
        public String Birthday {
            get {
                return this.GetString("birthday");
            }
            set {
                this.InnerDictionary["birthday"] = value;
            }
        }
        
        public String PersonalInfo {
            get {
                return this.GetString("personal_info");
            }
            set {
                this.InnerDictionary["personal_info"] = value;
            }
        }
        
        public String PersonalInterests {
            get {
                return this.GetString("personal_interests");
            }
            set {
                this.InnerDictionary["personal_interests"] = value;
            }
        }
        
        public String Members {
            get {
                return this.GetString("members");
            }
            set {
                this.InnerDictionary["members"] = value;
            }
        }
        
        public String Built {
            get {
                return this.GetString("built");
            }
            set {
                this.InnerDictionary["built"] = value;
            }
        }
        
        public String Features {
            get {
                return this.GetString("features");
            }
            set {
                this.InnerDictionary["features"] = value;
            }
        }
        
        public String Mpg {
            get {
                return this.GetString("mpg");
            }
            set {
                this.InnerDictionary["mpg"] = value;
            }
        }
        
        public String GeneralInfo {
            get {
                return this.GetString("general_info");
            }
            set {
                this.InnerDictionary["general_info"] = value;
            }
        }
    }
}
