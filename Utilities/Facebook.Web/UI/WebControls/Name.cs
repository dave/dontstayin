using System;
using System.Web.UI;

namespace Facebook.Web.UI.WebControls
{
    public class Name : FacebookControl
    {
        public Name()
        {
            this.AddAttribute("uid", null);
            this.AddAttribute("firstnameonly", false);
            this.AddAttribute("linked", true);
            this.AddAttribute("lastnameonly", false);
            this.AddAttribute("possessive", false);
            this.AddAttribute("reflexive", false);
            this.AddAttribute("shownetwork", false);
            this.AddAttribute("useyou", true);
            this.AddAttribute("ifcantsee", "FacebookUser");
            this.AddAttribute("capitalize", false);
            this.AddAttribute("subjectid", null);

            this.Uid = "loggedinuser";
        }

        public override String TagName { get { return "name"; } }

        public String Uid
        {
            get { return (String)this.Attributes["uid"]; }
            set { this.Attributes["uid"] = value; }
        }

        public Boolean FirstNameOnly
        {
            get { return (Boolean)this.Attributes["firstnameonly"]; }
            set { this.Attributes["firstnameonly"] = value; }
        }

        public Boolean Linked
        {
            get { return (Boolean)this.Attributes["linked"]; }
            set { this.Attributes["linked"] = value; }
        }

        public Boolean LastNameOnly
        {
            get { return (Boolean)this.Attributes["lastnameonly"]; }
            set { this.Attributes["lastnameonly"] = value; }
        }

        public Boolean Possessive
        {
            get { return (Boolean)this.Attributes["possessive"]; }
            set { this.Attributes["possessive"] = value; }
        }

        public Boolean Reflexive
        {
            get { return (Boolean)this.Attributes["reflexive"]; }
            set { this.Attributes["reflexive"] = value; }
        }

        public Boolean ShowNetwork
        {
            get { return (Boolean)this.Attributes["shownetwork"]; }
            set { this.Attributes["shownetwork"] = value; }
        }

        public Boolean UseYou
        {
            get { return (Boolean)this.Attributes["useyou"]; }
            set { this.Attributes["useyou"] = value; }
        }

        public String IfCantSee
        {
            get { return (String)this.Attributes["ifcantsee"]; }
            set { this.Attributes["ifcantsee"] = value; }
        }

        public Boolean Capitalize
        {
            get { return (Boolean)this.Attributes["capitalize"]; }
            set { this.Attributes["capitalize"] = value; }
        }

        public String SubjectId
        {
            get { return (String)this.Attributes["subjectid"]; }
            set { this.Attributes["subjectid"] = value; }
        }
    }
}
